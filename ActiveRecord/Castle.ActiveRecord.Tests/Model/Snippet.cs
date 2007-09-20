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

	[ActiveRecord]
	public class Snippet : ActiveRecordBase
	{
		private int id;
		private int refid;
		private String langcode;
		private String text;

		public Snippet()
		{
		}

		[PrimaryKey]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		[Property]
		public int Refid
		{
			get { return refid; }
			set { refid = value; }
		}

		[Property]
		public string LangCode
		{
			get { return langcode; }
			set { langcode = value; }
		}

		[Property]
		public string Text
		{
			get { return text; }
			set { text = value; }
		}
	}
}
