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
	using System.IO;
	using System.Web.UI;

	/// <summary>
	/// Pendent
	/// </summary>
	public class MasterPageBase : Page
	{
		private const String ViewStateKey = "__MASTERVIEWSTATE";

		/// <summary>
		/// Loads any saved view-state information to the <see cref="T:System.Web.UI.Page"></see> object.
		/// </summary>
		/// <returns>The saved view state.</returns>
		protected override object LoadPageStateFromPersistenceMedium()
		{
			String viewState =  Request.Params[ViewStateKey];

			if (viewState == null)
			{
				return null;
			}
			else
			{
				LosFormatter formatter = new LosFormatter();
				return formatter.Deserialize(viewState);
			}
		}

		/// <summary>
		/// Saves the page state to persistence medium.
		/// </summary>
		/// <param name="viewState">State of the view.</param>
		protected override void SavePageStateToPersistenceMedium(object viewState)
		{
			using(StringWriter writer = new StringWriter())
			{
				new LosFormatter().Serialize(writer, viewState);
				ClientScript.RegisterHiddenField(ViewStateKey, writer.GetStringBuilder().ToString());
			}
		}
	}
}
