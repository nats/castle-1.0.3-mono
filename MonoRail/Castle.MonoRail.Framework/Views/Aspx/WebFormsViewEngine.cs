// Copyright 2004-2007 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.MonoRail.Framework.Views.Aspx
{
	using System;
	using System.Web;
	using System.Web.UI;
	using System.IO;
	using System.Collections;
	using System.Reflection;

	/// <summary>
	/// Default implementation of a <see cref="IViewEngine"/>.
	/// Uses ASP.Net WebForms as views.
	/// </summary>
	public class WebFormsViewEngine : ViewEngineBase
	{
		private static readonly String ProcessedBeforeKey = "processed_before";

		private static readonly BindingFlags PropertyBindingFlags = BindingFlags.Public |
			BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebFormsViewEngine"/> class.
		/// </summary>
		public WebFormsViewEngine()
		{
		}

		#region ViewEngineBase overrides

		/// <summary>
		/// Gets a value indicating whether the view engine
		/// support the generation of JS.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if JS generation is supported; otherwise, <c>false</c>.
		/// </value>
		public override bool SupportsJSGeneration
		{
			get { return false; }
		}

		/// <summary>
		/// Gets the view file extension.
		/// </summary>
		/// <value>The view file extension.</value>
		public override string ViewFileExtension
		{
			get { return ".aspx"; }
		}

		/// <summary>
		/// Gets the JS generator file extension.
		/// </summary>
		/// <value>The JS generator file extension.</value>
		public override string JSGeneratorFileExtension
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Evaluates whether the specified template exists.
		/// </summary>
		/// <param name="templateName"></param>
		/// <returns><c>true</c> if it exists</returns>
		public override bool HasTemplate(String templateName)
		{
			return ViewSourceLoader.HasTemplate(templateName + ".aspx");
		}

		/// <summary>
		/// Obtains the aspx Page from the view name dispatch
		/// its execution using the standard ASP.Net API.
		/// </summary>
		public override void Process(IRailsEngineContext context, Controller controller, String viewName)
		{
			AdjustContentType(context);

			bool processedBefore = false;

			//TODO: document this hack for the sake of our users
			HttpContext httpContext = context.UnderlyingContext;
			
			if (httpContext != null)
			{
				if (!(processedBefore = httpContext.Items.Contains(ProcessedBeforeKey)))
				{
					httpContext.Items[ProcessedBeforeKey] = true;
				}
			}
			
			if (processedBefore)
			{
#if !MONO
				ProcessExecuteView(context, controller, viewName);
				return;
#else
				if (IsTheSameView(httpContext, viewName)) return;
#endif				
			}

			ProcessInlineView(controller, viewName, httpContext);	
		}

		/// <summary>
		/// Processes the view - using the templateName
		/// to obtain the correct template
		/// and writes the results to the System.IO.TextWriter.
		/// </summary>
		/// <param name="output"></param>
		/// <param name="context"></param>
		/// <param name="controller"></param>
		/// <param name="templateName"></param>
		public override void Process(TextWriter output, IRailsEngineContext context, Controller controller,
		                             string templateName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes the partial.
		/// </summary>
		/// <param name="output">The output.</param>
		/// <param name="context">The context.</param>
		/// <param name="controller">The controller.</param>
		/// <param name="partialName">The partial name.</param>
		public override void ProcessPartial(TextWriter output, IRailsEngineContext context, Controller controller,
		                                    string partialName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates the JS generator.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public override object CreateJSGenerator(IRailsEngineContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Generates the JS.
		/// </summary>
		/// <param name="output">The output.</param>
		/// <param name="context">The context.</param>
		/// <param name="controller">The controller.</param>
		/// <param name="templateName">Name of the template.</param>
		public override void GenerateJS(TextWriter output, IRailsEngineContext context, Controller controller,
		                                string templateName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wraps the specified content in the layout using the
		/// context to output the result.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="controller"></param>
		/// <param name="contents"></param>
		public override void ProcessContents(IRailsEngineContext context, Controller controller, String contents)
		{
			AdjustContentType(context);

			HttpContext httpContext = context.UnderlyingContext;

			Page masterHandler = ObtainMasterPage(httpContext, controller);

			httpContext.Items.Add("rails.contents", contents);

			ProcessPage(controller, masterHandler, httpContext);
		}

		#endregion

		private void ProcessInlineView(Controller controller, String viewName, HttpContext httpContext)
		{
			PrepareLayout(controller, httpContext);

			IHttpHandler childPage = GetCompiledPageInstance(viewName, httpContext);

			ProcessPropertyBag(controller.PropertyBag, childPage);

#if !MONO		
			Page page = childPage as Page;

			if (page != null)
			{
				page.Init += new EventHandler(PrepareMasterPage);
			}
#endif			
			
			ProcessPage(controller, childPage, httpContext);

			ProcessLayoutIfNeeded(controller, httpContext, childPage);			
		}

#if !MONO
		private void ProcessExecuteView(IRailsEngineContext context, Controller controller, String viewName)
		{				
			HttpContext httpContext = context.UnderlyingContext;

			PrepareLayout(controller, httpContext);

			ExecutePageProvider pageProvider = new ExecutePageProvider(this, viewName);

			Page childPage = pageProvider.ExecutePage(context);

			ProcessLayoutIfNeeded(controller, httpContext, childPage);

			// Prevent the parent Page from continuing to process.
			httpContext.Response.End();
		}

		private void PrepareView(object sender, EventArgs e)
		{
			Page view = (Page) sender;

			Controller controller = GetCurrentController();

			ProcessPropertyBag(controller.PropertyBag, view);

			PreSendView(controller, view);

			PrepareMasterPage(sender, e);
						
			view.Unload += new EventHandler(FinalizeView);
		}

		private void PrepareMasterPage(object sender, EventArgs e)
		{
			Page view = (Page) sender;
			MasterPage masterPage = view.Master;
			
			if (masterPage != null)
			{
				Controller controller = GetCurrentController();

				if (masterPage is IControllerAware)
				{
					(masterPage as IControllerAware).SetController(controller);
				}				
			}
		}
		
		private void FinalizeView(object sender, EventArgs e)
		{
			Controller controller = GetCurrentController();
			
			PostSendView(controller,  sender);
		}
#endif

		private String MapViewToPhysicalPath(String viewName)
		{
			viewName += ".aspx";

			//TODO: There needs to be a more efficient way to do this than two replace operations
			return Path.Combine(ViewSourceLoader.ViewRootDir, viewName).Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
		}
		
		private String MapViewToVirtualPath(String viewName, ref string physicalPath, HttpContext httpContext)
		{
			if (physicalPath == null)
			{
				physicalPath = MapViewToPhysicalPath(viewName);
			}

			String physicalAppPath = httpContext.Request.PhysicalApplicationPath;	
			if (physicalPath.StartsWith(physicalAppPath))
			{
				viewName = "~/" + physicalPath.Substring(physicalAppPath.Length);
			}

			return viewName;
		}
		
		private IHttpHandler GetCompiledPageInstance(String viewName, HttpContext httpContext)
		{
			String physicalPath = null;

			// This is a hack until I can understand the different behavior exhibited by
			// PageParser.GetCompiledPageInstance(..) when running ASP.NET 2.0.  It appears
			// that the virtualPath (first argument) to this method must represent a valid
			// virtual directory with respect to the web application.   As a result, the
			// viewRootDir must be relative to the web application project.  If it is not,
			// this will certainly fail.  If this is indeed the case, it may make sense to
			// be able to obtain an absolute virtual path to the views directory from the
			// ViewSourceLoader.

			viewName = MapViewToVirtualPath(viewName, ref physicalPath, httpContext);

			return PageParser.GetCompiledPageInstance(viewName, physicalPath, httpContext);
		}

		private bool IsTheSameView(HttpContext httpContext, String viewName)
		{
			String original = ((String) httpContext.Items[Constants.OriginalViewKey]);
			String actual = viewName;

			return String.Compare(original, actual, true) == 0;
		}

		private void ProcessPage(Controller controller, IHttpHandler page, HttpContext httpContext)
		{
			PreSendView(controller, page);
	
			page.ProcessRequest(httpContext);
	
			PostSendView(controller, page);
		}

		private void PrepareLayout(Controller controller, HttpContext httpContext)
		{
			if (HasLayout(controller))
			{
				bool layoutPending = httpContext.Items.Contains("wfv.masterPage");
				
				Page masterHandler = ObtainMasterPage(httpContext, controller);

				if (!layoutPending && masterHandler != null)
				{
					StartFiltering(httpContext.Response);
				}
			}
		}
		
		private bool ProcessLayoutIfNeeded(Controller controller, HttpContext httpContext, IHttpHandler childPage)
		{
			Page masterHandler = (Page) httpContext.Items["wfv.masterPage"];
			
			if (masterHandler != null && HasLayout(controller))
			{
//				if (httpContext.Response.StatusCode == 200)
//				{
					byte[] contents = RestoreFilter(httpContext.Response);

					// Checks if its only returning from a inner process invocation
					if (!Convert.ToBoolean(httpContext.Items["rails.layout.processed"]))
					{
						httpContext.Items.Add("rails.contents", contents);
						httpContext.Items.Add("rails.child", childPage);

						httpContext.Items["rails.layout.processed"] = true;
					}

					ProcessPage(controller, masterHandler, httpContext);

					return true;
//				}
//				else
//				{
//					WriteBuffered(httpContext.Response);
//				}
			}

			return false;
		}

		private bool HasLayout(Controller controller)
		{
			return controller.LayoutName != null;
		}

//		private void WriteBuffered(HttpResponse response)
//		{
//			// response.Flush();
//
//			// Restores the original stream
//			DelegateMemoryStream filter = (DelegateMemoryStream) response.Filter;
//			response.Filter = filter.OriginalStream;
//			
//			// Writes the buffered contents
//			byte[] buffer = filter.GetBuffer();
//			response.OutputStream.Write(buffer, 0, buffer.Length);
//		}

		private byte[] RestoreFilter(HttpResponse response)
		{
			response.Flush();
			response.Filter.Flush();

			// Restores the original stream
			DelegateMemoryStream filter = (DelegateMemoryStream) response.Filter;
			response.Filter = filter.OriginalStream;

			return filter.GetBuffer();
		}

		private Page ObtainMasterPage(HttpContext httpContext, Controller controller)
		{
			Page masterHandler;
			String layout = "layouts/" + controller.LayoutName;

#if !MONO
			masterHandler = (Page)httpContext.Items["wfv.masterPage"];

			if (masterHandler != null)
			{
				String currentLayout = (String) masterHandler.Items["wfv.masterLayout"];
				if (layout == currentLayout) return masterHandler;
			}
#endif
			masterHandler = (Page) GetCompiledPageInstance(layout, httpContext);
#if !MONO
			masterHandler.Items["wfv.masterLayout"] = layout;
#endif

			httpContext.Items["wfv.masterPage"] = masterHandler;
			return masterHandler;
		}
	
		private void StartFiltering(HttpResponse response)
		{
			Stream filter = response.Filter;
			response.Filter = new DelegateMemoryStream(filter);
			response.BufferOutput = true;
		}

		private void ProcessPropertyBag(IDictionary bag, IHttpHandler handler)
		{
			foreach(DictionaryEntry entry in bag)
			{
				SetPropertyValue( handler, entry.Key, entry.Value );
			}
		}

		private void SetPropertyValue(IHttpHandler handler, object key, object value)
		{
			if (value == null) return;

			String name = key.ToString();
			Type type = handler.GetType();
			Type valueType = value.GetType();
		
			FieldInfo fieldInfo = type.GetField(name, PropertyBindingFlags);

			if (fieldInfo != null)
			{
				if (fieldInfo.FieldType.IsAssignableFrom(valueType))
				{
					fieldInfo.SetValue(handler, value);
				}
			}
			else
			{
				PropertyInfo propInfo = type.GetProperty(name, PropertyBindingFlags);

				if (propInfo != null && (propInfo.CanWrite &&
					(propInfo.PropertyType.IsAssignableFrom(valueType))))
				{
					propInfo.GetSetMethod().Invoke(handler, new object[] { value });
				}
			}
		}

#if !MONO

		private Controller GetCurrentController()
		{
			IControllerLifecycleExecutor executor = (IControllerLifecycleExecutor)
													MonoRailHttpHandler.CurrentContext.Items[ControllerLifecycleExecutor.ExecutorEntry];

			return executor.Controller;
		}

		internal class ExecutePageProvider : IMonoRailHttpHandlerProvider
		{
			private Page page;
			private string viewName;
			private WebFormsViewEngine engine;

			/// <summary>
			/// Initializes a new instance of the <see cref="ExecutePageProvider"/> class.
			/// </summary>
			/// <param name="engine">The engine.</param>
			/// <param name="viewName">Name of the view.</param>
			public ExecutePageProvider(WebFormsViewEngine engine, string viewName)
			{
				this.page = null;
				this.engine = engine;
				this.viewName = viewName;
			}

			/// <summary>
			/// Executes the page.
			/// </summary>
			/// <param name="context">The context.</param>
			/// <returns></returns>
			public Page ExecutePage(IRailsEngineContext context)
			{
				HttpContext httpContext = context.UnderlyingContext;
				context.RemoveService(typeof(IMonoRailHttpHandlerProvider));
				context.AddService(typeof(IMonoRailHttpHandlerProvider), this);
				httpContext.Server.Execute(httpContext.Request.RawUrl, false);
				return page;
			}

			/// <summary>
			/// Implementors should perform their logic to
			/// return a instance of <see cref="IHttpHandler"/>.
			/// If the <see cref="IHttpHandler"/> can not be created,
			/// it should return <c>null</c>.
			/// </summary>
			/// <param name="context"></param>
			/// <returns></returns>
			public IHttpHandler ObtainMonoRailHttpHandler(IRailsEngineContext context)
			{
				HttpContext httpContext = context.UnderlyingContext;
	
				string physicalPath = null;
				string virtualPath = engine.MapViewToVirtualPath(
					viewName, ref physicalPath, httpContext);

				if (virtualPath != null && physicalPath != null)
				{
					page = (Page) PageParser.GetCompiledPageInstance(
						virtualPath, physicalPath, httpContext);

					if (page != null)
					{
						page.Init += new EventHandler(engine.PrepareView);
					}
				}

				return page;
			}

			/// <summary>
			/// Implementors should perform their logic
			/// to release the <see cref="IHttpHandler"/> instance
			/// and its resources.
			/// </summary>
			/// <param name="handler"></param>
			public void ReleaseHandler(IHttpHandler handler)
			{

			}
		}
#endif
	}

	
	internal class DelegateMemoryStream : MemoryStream
	{
		public Stream _original;

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateMemoryStream"/> class.
		/// </summary>
		/// <param name="original">The original.</param>
		public DelegateMemoryStream(Stream original)
		{
			_original = original;
		}

		/// <summary>
		/// Gets the original stream.
		/// </summary>
		/// <value>The original stream.</value>
		public Stream OriginalStream
		{
			get { return _original; }
		}
	}
}
