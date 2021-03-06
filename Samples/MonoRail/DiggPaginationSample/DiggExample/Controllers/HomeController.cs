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

namespace DiggExample.Controllers
{
	using System.Collections.Generic;

	using Castle.MonoRail.Framework;
	using Castle.MonoRail.Framework.Helpers;

	using DiggExample.Model;
	using NHibernate.Expression;

	[Layout("default"), Rescue("generalerror")]
	public class HomeController : SmartDispatcherController
	{
		public void Index()
		{
		}

		public void Index1()
		{
			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				MyEntity.FindAll(new Order("Index", true)),
				10
			);
		}
		public void Index2()
		{
			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				MyEntity.FindAll(new Order("Index", true)),
				10
			);
		}
		public void Index3()
		{
			List<MyEntity> el = new List<MyEntity>(MyEntity.FindAll(new Order("Index", true)));

			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				el.GetRange(0,5),
				10
			);
		}
		public void Index4()
		{
			List<MyEntity> el = new List<MyEntity>(MyEntity.FindAll(new Order("Index", true)));

			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				el.GetRange(0, 5),
				10
			);
		}
		public void Index5()
		{
			List<MyEntity> el = new List<MyEntity>(MyEntity.FindAll(new Order("Index", true)));

			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				el.GetRange(0, 5),
				10
			);
		}
		public void Index6()
		{
			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				MyEntity.FindAll(new Order("Index", true)),
				10
			);
		}
		public void Index7()
		{
			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				MyEntity.FindAll(new Order("Index", true)),
				10
			);
		}
		public void Index8(bool desc)
		{
			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				MyEntity.FindAll(new Order("Index", (!desc))),
				10
			);
			PropertyBag["desc"] = desc;
		}

		public void Index9()
		{
			PropertyBag["items"] = PaginationHelper.CreatePagination<MyEntity>(
				this,
				MyEntity.FindAll(new Order("Index", true)),
				10
			);
		}
	}
}
