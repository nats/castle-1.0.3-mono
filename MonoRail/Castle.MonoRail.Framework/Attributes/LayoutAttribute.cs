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

namespace Castle.MonoRail.Framework
{
	using System;

	using Castle.MonoRail.Framework.Internal;

	/// <summary>
	/// Associates a layout name with a controller.
	/// The layout can later be changed using the LayoutName
	/// property of the <see cref="Controller"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple=false), Serializable]
	public class LayoutAttribute : Attribute, ILayoutDescriptorBuilder
	{
		private readonly String layoutName;

		/// <summary>
		/// Constructs a LayoutAttribute with the 
		/// layout name.
		/// </summary>
		public LayoutAttribute(String layoutName)
		{
			if (layoutName == null || layoutName.Length == 0)
			{
				throw new ArgumentNullException("layoutName", "Invalid layout name");
			}

			this.layoutName = layoutName;
		}

		/// <summary>
		/// Gets the layout name
		/// </summary>
		public String LayoutName
		{
			get { return layoutName; }
		}

		/// <summary>
		/// <see cref="ILayoutDescriptorBuilder"/> implementation.
		/// Gets the descriptor that describes the layout.
		/// </summary>
		public LayoutDescriptor BuildLayoutDescriptor()
		{
			return new LayoutDescriptor(layoutName);
		}
	}
}
