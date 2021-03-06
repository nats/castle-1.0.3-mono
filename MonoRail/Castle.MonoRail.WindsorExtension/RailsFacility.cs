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

namespace Castle.MonoRail.WindsorExtension
{
	using Castle.Core;
	using Castle.MicroKernel;
	using Castle.MicroKernel.Facilities;
	using Castle.MonoRail.Framework;
	using Castle.MonoRail.Framework.Internal;
	using Castle.MonoRail.Framework.Controllers;
	using Castle.MonoRail.Framework.Services;
	using Castle.MonoRail.Framework.Services.Utils;

	/// <summary>
	/// Facility responsible for registering the controllers in
	/// the controllerTree.
	/// </summary>
	public class RailsFacility : AbstractFacility
	{
		private IControllerTree controllerTree;
		private IViewComponentRegistry componentRegistry;

		public RailsFacility()
		{
		}

		protected override void Init()
		{
			RegisterWindsorLocationWithinMonoRail();

			Kernel.AddComponent("rails.controllertree", typeof(IControllerTree), typeof(DefaultControllerTree));
			Kernel.AddComponent("rails.wizardpagefactory", typeof(IWizardPageFactory), typeof(DefaultWizardPageFactory));
			Kernel.AddComponent("rails.viewcomponentregistry", typeof(IViewComponentRegistry), typeof(DefaultViewComponentRegistry));

			controllerTree = (IControllerTree) Kernel["rails.controllertree"];
			componentRegistry = (IViewComponentRegistry) Kernel["rails.viewcomponentregistry"];

			Kernel.ComponentModelCreated += OnComponentModelCreated;

			AddBuiltInControllers();
		}

		private void RegisterWindsorLocationWithinMonoRail()
		{
			ServiceProviderLocator.Instance.AddLocatorStrategy(new WindsorAccessorStrategy());
		}

		protected virtual void AddBuiltInControllers()
		{
			Kernel.AddComponent("files", typeof(FilesController), typeof(FilesController));
		}

		private void OnComponentModelCreated(ComponentModel model)
		{
			bool isController = typeof(Controller).IsAssignableFrom(model.Implementation);
			bool isViewComponent = typeof(ViewComponent).IsAssignableFrom(model.Implementation);

			if (!isController && !isViewComponent)
			{
				return;
			}

			// Ensure it's transient
			model.LifestyleType = LifestyleType.Transient;
			model.InspectionBehavior = PropertiesInspectionBehavior.DeclaredOnly;

			if (isController)
			{
				ControllerDescriptor descriptor = ControllerInspectionUtil.Inspect(model.Implementation);

				controllerTree.AddController(descriptor.Area, descriptor.Name, model.Implementation);
			}

			if (isViewComponent)
			{
				componentRegistry.AddViewComponent(model.Name, model.Implementation);
			}
		}

		public class WindsorAccessorStrategy : ServiceProviderLocator.IAccessorStrategy
		{
			public IServiceProviderEx LocateProvider()
			{
				return WindsorContainerAccessorUtil.ObtainContainer();
			}
		}
	}
}
