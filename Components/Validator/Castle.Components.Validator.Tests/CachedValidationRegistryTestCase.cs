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

using System.Globalization;
using System.Resources;
using System.Threading;

namespace Castle.Components.Validator.Tests
{
	using Castle.Components.Validator.Tests.Models;
	using NUnit.Framework;

	[TestFixture]
	public class CachedValidationRegistryTestCase
	{
		private CachedValidationRegistry registry;

		[SetUp]
		public void Init()
		{
			registry = new CachedValidationRegistry();
		}

		[Test]
		public void RunWhenEverytimeTest()
		{
			IValidator[] validators = registry.GetValidators(new ValidatorRunner(registry), typeof(Client), RunWhen.Everytime);

			Assert.IsNotNull(validators);
			Assert.AreEqual(8, validators.Length);

			foreach (IValidator val in validators)
			{
				Assert.IsTrue((val.RunWhen & RunWhen.Everytime) != 0);
			}
		}

		[Test]
		public void RunWhenCustomTest()
		{
			IValidator[] validators = registry.GetValidators(new ValidatorRunner(registry), typeof(Client), RunWhen.Custom);

			Assert.IsNotNull(validators);
			Assert.AreEqual(9, validators.Length); // RunWhen.Everytime is returned too

			foreach (IValidator val in validators)
			{
				Assert.IsTrue((val.RunWhen & RunWhen.Custom) != 0 || (val.RunWhen & RunWhen.Everytime) != 0);
			}
		}

		[Test]
		public void WithCustomResource()
		{
			CultureInfo prev = Thread.CurrentThread.CurrentCulture;
			CultureInfo prevUI = Thread.CurrentThread.CurrentUICulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

				ResourceManager resourceManager = new ResourceManager("Castle.Components.Validator.Tests.Messages", typeof(CachedValidationRegistryTestCase).Assembly);
				registry = new CachedValidationRegistry(resourceManager);
				string fromResource = registry.GetStringFromResource("time_invalid");
				Assert.AreEqual("This is a test value", fromResource);
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = prev;
				Thread.CurrentThread.CurrentUICulture = prevUI;
			}

		}

		[Test]
		public void WillFallbackToDefaultResourceIfNotFoundOnCustomOne()
		{
			CultureInfo prev = Thread.CurrentThread.CurrentCulture;
			CultureInfo prevUI = Thread.CurrentThread.CurrentUICulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

				ResourceManager resourceManager = new ResourceManager("Castle.Components.Validator.Tests.Messages", typeof(CachedValidationRegistryTestCase).Assembly);
				registry = new CachedValidationRegistry(resourceManager);
				string fromResource = registry.GetStringFromResource("collection_not_empty");
				Assert.AreEqual("Collection must not be empty", fromResource);
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = prev;
				Thread.CurrentThread.CurrentUICulture = prevUI;
			}
		}
	}
}