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

namespace Castle.VSNetIntegration.CastleWizards
{
	using System;

	[System.Runtime.InteropServices.ComVisible(false)]
	public class MRConfigConstants
	{
		public static readonly String Web = "web.config";

		public static readonly String Properties = "properties.config";
		public static readonly String PropertiesConfig = @"Config\properties.config";

		public static readonly String Facilities = "facilities.config";
		public static readonly String FacilitiesConfig = @"Config\facilities.config";
	
		public static readonly String Controllers = "controllers.config";
		public static readonly String ControllersConfig = @"Config\controllers.config";

		public static readonly String Components = "components.config";
		public static readonly String ComponentsConfig = @"Config\components.config";
	}
}
