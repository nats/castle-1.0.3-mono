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
	[ActiveRecord("Hands")]
	public class Hand : Test2ARBase
	{
		private int _id;
		private string _side;

		public Hand()
		{
		}

		[PrimaryKey(PrimaryKeyType.Identity)]
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		[Property]
		public string Side
		{
			get { return _side; }
			set { _side = value; }
		}

		public static Hand[] FindAll()
		{
			return (Hand[]) ActiveRecordBase.FindAll(typeof (Hand));
		}
	}
}
