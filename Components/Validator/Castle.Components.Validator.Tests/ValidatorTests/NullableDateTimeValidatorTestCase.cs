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

namespace Castle.Components.Validator.Tests.ValidatorTests
{
	using System;
	using System.Globalization;
	using System.Threading;
	using NUnit.Framework;

	[TestFixture]
	public class NullableDateTimeValidatorTestCase
	{
		private NullableDateTimeValidator validator;
		private TestTarget target;

		[SetUp]
		public void Init()
		{
			Thread.CurrentThread.CurrentCulture =
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

			validator = new NullableDateTimeValidator();
			validator.Initialize(new CachedValidationRegistry(), typeof(TestTarget).GetProperty("DtField"));
			target = new TestTarget();
		}

		[Test]
		public void InvalidDate()
		{
			Assert.IsFalse(validator.IsValid(target, "some"));
			Assert.IsFalse(validator.IsValid(target, "122"));
			Assert.IsFalse(validator.IsValid(target, "99/99/99"));
			Assert.IsFalse(validator.IsValid(target, "99-99-99"));
		}

		[Test]
		public void ValidDate()
		{
			Assert.IsTrue(validator.IsValid(target, "01/12/2004"));
			Assert.IsTrue(validator.IsValid(target, "07/16/79"));
			Assert.IsTrue(validator.IsValid(target, "2007-01-14T12:05:25"));
			Assert.IsTrue(validator.IsValid(target, null));
			Assert.IsTrue(validator.IsValid(target, ""));
		}

		public class TestTarget
		{
			private DateTime dtField;

			public DateTime DtField
			{
				get { return dtField; }
				set { dtField = value; }
			}
		}
	}
}
