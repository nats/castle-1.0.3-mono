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

namespace Castle.ActiveRecord.Queries
{
	using System;
	using System.Collections;

	using Castle.ActiveRecord.Framework;

	using NHibernate;

	/// <summary>
	/// Simple query.
	/// </summary>
	public class SimpleQuery : HqlBasedQuery
	{
		private readonly Type returnType;

		#region Constructors
		
		/// <summary>
		/// Creates a new <c>SimpleQuery</c>.
		/// </summary>
		public SimpleQuery(Type targetType, Type returnType, string query, params object[] parameters)
			: base(targetType, query, parameters)
		{
			this.returnType = returnType;
		}

		/// <summary>
		/// Creates a new <c>SimpleQuery</c>.
		/// </summary>
		public SimpleQuery(Type returnType, string query, params object[] positionalParameters)
			: this(returnType, returnType, query, positionalParameters)
		{
		}
		
		/// <summary>
		/// Creates a new <c>SimpleQuery</c>.
		/// </summary>
		public SimpleQuery(Type targetType, Type returnType, QueryLanguage queryLanguage, string query, params object[] parameters)
			: base(targetType, queryLanguage, query, parameters)
		{
			this.returnType = returnType;
		}

		/// <summary>
		/// Creates a new <c>SimpleQuery</c>.
		/// </summary>
		public SimpleQuery(Type returnType, QueryLanguage queryLanguage, string query, params object[] positionalParameters)
			: this(returnType, returnType, queryLanguage, query, positionalParameters)
		{
		}
		
		#endregion

		/// <summary>
		/// Executes the query and converts the results into a strongly-typed
		/// array of <see cref="returnType"/>.
		/// </summary>
		/// <param name="session">The <c>NHibernate</c>'s <see cref="ISession"/></param>
		protected override object InternalExecute(ISession session)
		{
			IList results = (IList) base.InternalExecute(session);
			return SupportingUtils.BuildArray(returnType, results, false);
		}
	}
}
