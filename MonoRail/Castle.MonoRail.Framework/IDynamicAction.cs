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
	/// <summary>
	/// An action that is not a method
	/// on the controller.
	/// </summary>
	/// 
	/// <seealso cref="IDynamicActionProvider"/>
	/// 
	/// <remarks>
	/// To attach a dynamic action to a controller use 
	/// the <see cref="Controller.DynamicActions"/> dictionary.
	/// </remarks>
	public interface IDynamicAction
	{
		/// <summary>
		/// Implementors should perform the action 
		/// upon this invocation
		/// </summary>
		void Execute(Controller controller);
	}
}
