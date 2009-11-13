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
	using System.Web;
	using Castle.Windsor;
	using Castle.MonoRail.Framework;

	public static class WindsorContainerAccessorUtil
	{
		public static IWindsorContainer ObtainContainer()
		{
            // Mono loads things in a different order to ASP.NET, which makes the way 1.0.3 works incompatible with mono
			HttpApplication app = Castle.MonoRail.Framework.EngineContextModule.Application;

			if (app == null)
				throw new RailsException("Application instance is null");

			IContainerAccessor containerAccessor =
				app as IContainerAccessor;

			if (containerAccessor == null)
			{
				throw new RailsException("You must extend the HttpApplication in your web project " +
					"and implement the IContainerAccessor to properly expose your container instance");
			}

			IWindsorContainer container = containerAccessor.Container;

			if (container == null)
			{
				throw new RailsException("The container seems to be unavailable in " +
					"your HttpApplication subclass");
			}

			return container;
		}
	}
}
