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

namespace Castle.ActiveRecord.Tests.Model
{
	using System;
	using System.Collections;
	using Castle.ActiveRecord.Framework;
	using NHibernate;
	using NHibernate.Expression;

	[ActiveRecord("BlogTable")]
	public class Blog : ActiveRecordBase
	{
		private int _id;
		private String _name;
		private String _author;
		private IList _posts;
		private IList _publishedposts;
		private IList _unpublishedposts;
		private IList _recentposts;
		private bool onSaveCalled, onUpdateCalled, onDeleteCalled, onLoadCalled;

		public Blog()
		{
		}

		public Blog(int _id)
		{
			this._id = _id;
		}

		[PrimaryKey(PrimaryKeyType.Native)]
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		[Property]
		public String Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[Property]
		public String Author
		{
			get { return _author; }
			set { _author = value; }
		}

		[HasMany(typeof(Post), Table="Posts", ColumnKey="blogid")]
		public IList Posts
		{
			get { return _posts; }
			set { _posts = value; }
		}

		[HasMany(typeof(Post), Table="Posts", ColumnKey="blogid", Where="published = 1")]
		public IList PublishedPosts
		{
			get { return _publishedposts; }
			set { _publishedposts = value; }
		}

		[HasMany(typeof(Post), Table="Posts", ColumnKey="blogid", Where="published = 0")]
		public IList UnPublishedPosts
		{
			get { return _unpublishedposts; }
			set { _unpublishedposts = value; }
		}

		[HasMany(typeof(Post), Table="Posts", ColumnKey="blogid", OrderBy="created desc")]
		public IList RecentPosts
		{
			get { return _recentposts; }
			set { _recentposts = value; }
		}

		public static void DeleteAll()
		{
			ActiveRecordMediator.DeleteAll(typeof(Blog));
		}

		public static void DeleteAll(string where)
		{
			ActiveRecordMediator.DeleteAll(typeof(Blog), where);
		}

		public static Blog[] FindAll()
		{
			return (Blog[]) ActiveRecordMediator.FindAll(typeof(Blog));
		}

		public static Blog Find(int id)
		{
			return (Blog) ActiveRecordMediator.FindByPrimaryKey(typeof(Blog), id);
		}

		public static int FetchCount()
		{
			return Count(typeof(Blog));
		}

		public static int FetchCount(string filter, params object[] args)
		{
			return Count(typeof(Blog), filter, args);
		}

		public static int FetchCount(params ICriterion[] criterias)
		{
			return Count(typeof(Blog), criterias);
		}

		public static bool Exists()
		{
			return Exists(typeof(Blog));
		}

		public static bool Exists(string filter, params object[] args)
		{
			return Exists(typeof(Blog), filter, args);
		}

		public static bool Exists(int id)
		{
			return Exists(typeof(Blog), id);
		}

		public static bool Exists(params ICriterion[] criteria)
		{
			return Exists(typeof(Blog), criteria);
		}

		public static Blog[] FindByProperty(String property, object value)
		{
			return (Blog[]) FindAllByProperty(typeof(Blog), property, value);
		}

		public static Blog[] FindByProperty(String property, String orderByColumn, object value)
		{
			return (Blog[]) FindAllByProperty(typeof(Blog), orderByColumn, property, value);
		}

		/// <summary>
		/// Lifecycle method invoked during Save of the entity
		/// </summary>
		protected override void OnSave()
		{
			onSaveCalled = true;
		}

		/// <summary>
		/// Lifecycle method invoked during Update of the entity
		/// </summary>
		protected override void OnUpdate()
		{
			onUpdateCalled = true;
		}

		/// <summary>
		/// Lifecycle method invoked during Delete of the entity
		/// </summary>
		protected override void OnDelete()
		{
			onDeleteCalled = true;
		}

		/// <summary>
		/// Lifecycle method invoked during Load of the entity
		/// </summary>
		protected override void OnLoad(object id)
		{
			onLoadCalled = true;
		}

		public bool OnSaveCalled
		{
			get { return onSaveCalled; }
		}

		public bool OnUpdateCalled
		{
			get { return onUpdateCalled; }
		}

		public bool OnDeleteCalled
		{
			get { return onDeleteCalled; }
		}

		public bool OnLoadCalled
		{
			get { return onLoadCalled; }
		}

		public ISession CurrentSession
		{
			get
			{
				return (ISession)
				       ActiveRecordMediator.Execute(typeof(Blog), new NHibernateDelegate(GrabSession), this);
			}
		}

		private static object GrabSession(ISession session, object instance)
		{
			return session;
		}

		public void CustomAction()
		{
			ActiveRecordMediator.Execute(typeof(Blog), new NHibernateDelegate(MyCustomMethod), this);
		}

		private static object MyCustomMethod(ISession session, object blogInstance)
		{
			session.Delete(blogInstance);
			session.Flush();

			return null;
		}

		internal static ISessionFactoryHolder Holder
		{
			get { return ActiveRecordMediator.GetSessionFactoryHolder(); }
		}
	}
}
