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
	using System.Collections;

	/// <summary>
	/// Dictates the contract for resources that are publishable
	/// through the PropertyBag context.
	/// </summary>
	public interface IResource : IDisposable, IEnumerable
	{
		/// <summary>
		/// Returns the object linked to the specific key.
		/// </summary>
		object this[String key] { get; }

		/// <summary>
		/// Returns the object linked to the specific key as a string.
		/// </summary>
		String GetString(String key);

		/// <summary>
		/// Returns the object linked to the specific key.
		/// </summary>
		object GetObject(String key);
	}
}