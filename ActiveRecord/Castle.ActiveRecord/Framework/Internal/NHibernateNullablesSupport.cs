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

namespace Castle.ActiveRecord.Framework.Internal
{
	using System;

	internal class NHibernateNullablesSupport
	{
		private const String NullableAsm = "Nullables";

		private const String NullableIType = "Nullables.NHibernate.{0}Type, Nullables.NHibernate";

		private static readonly Type tINullableType;

		static NHibernateNullablesSupport()
		{
			try
			{
				tINullableType = Type.GetType("Nullables.INullableType, " + NullableAsm, false);
			}
			catch
			{
				//This can happen when someone is registerring to the AssemblyResolve event and doesn't handle
				// missing assemblies correctly.
				//For instance, MbUnit.AddIn.MbUnitTestRunner.AssemblyResolveHandler has this behavior.
				tINullableType = null;
			}
		}

		public static bool IsNHibernateNullableType(Type type)
		{
			return tINullableType != null && tINullableType.IsAssignableFrom(type);
		}

		public static String GetITypeTypeNameForNHibernateNullable(Type type)
		{
			if (type == null)
			{
				return null;
			}

			bool isSupported = type.Assembly.GetName().Name == NullableAsm;

			if (!isSupported)
			{
				throw new ActiveRecordException(String.Format("ActiveRecord does not support Nullable for {0} natively.", type));
			}

			return String.Format(NullableIType, type.Name);
		}
	}
}
