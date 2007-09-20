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

namespace Castle.Components.Validator.Tests
{
	using Castle.Components.Validator.Tests.Models;
	using NUnit.Framework;

	[TestFixture]
	public class ValidatorRunnerTestCase
	{
		private ValidatorRunner runner;

		[SetUp]
		public void Init()
		{
			runner = new ValidatorRunner(new CachedValidationRegistry());
		}

		[Test]
		public void IsValidForEverything()
		{
			Person person = new Person();
			Assert.IsFalse(runner.IsValid(person));

			person = new Person(1, 27, "hammett", "100, street");
			Assert.IsTrue(runner.IsValid(person));
		}

		[Test]
		public void IsValidForInsertUpdate()
		{
			InsertUpdateClass obj = new InsertUpdateClass();

			Assert.IsFalse(runner.IsValid(obj, RunWhen.Insert));

			obj.Prop1 = "value";
			obj.Prop2 = "value";

			Assert.IsTrue(runner.IsValid(obj, RunWhen.Insert));

			Assert.IsFalse(runner.IsValid(obj, RunWhen.Update));

			obj.Prop3 = "value";
			obj.Prop4 = "value";

			Assert.IsTrue(runner.IsValid(obj, RunWhen.Update));
		}

		[Test]
		public void InheritanceTest()
		{
			Client client = new Client();
			Assert.IsFalse(runner.IsValid(client));
			client = new Client(1, 27, "hammett", "100, street", "hammett@gmail.com", "123", "123");
			Assert.IsTrue(runner.IsValid(client));
		}

		[Test]
		public void GroupValidation()
		{
			Client client = new Client();
			client.Email = "foo@bar.com";
			Assert.IsFalse(runner.IsValid(client));

			Assert.AreEqual(0, runner.GetErrorSummary(client).GetErrorsForProperty("Email").Length);
			Assert.AreEqual(0, runner.GetErrorSummary(client).GetErrorsForProperty("Password").Length);
		}
	}
}