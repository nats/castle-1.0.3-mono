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

namespace Castle.ActiveRecord.Framework.Internal.Tests
{
	using NUnit.Framework;

	using Castle.ActiveRecord.Framework.Internal.Tests.Model;


	[TestFixture]
	public class SemanticCheckTestCase : AbstractActiveRecordTest
	{
		[Test,Ignore("meta-type is optional")]
		[ExpectedException(typeof(ActiveRecordException), "MetaType is a required attribute of AnyAttribute on Castle.ActiveRecord.Framework.Internal.Tests.Model.BadClassWithAnyAttribute.PaymentMethod.")]
		public void UsingAnyWithoutSpecifyingTheMetaType()
		{
			ActiveRecordModelBuilder builder = new ActiveRecordModelBuilder();
			ActiveRecordModel model = builder.Create(typeof(BadClassWithAnyAttribute));
			Assert.IsNotNull(model);

			Assert.IsNotNull(model);

			SemanticVerifierVisitor semanticVisitor = new SemanticVerifierVisitor(builder.Models);
			semanticVisitor.VisitNode(model);
		}

		[Test]
		[ExpectedException( typeof(ActiveRecordException), "Unfortunatelly you can't have a discriminator class and a joined subclass at the same time - check type Castle.ActiveRecord.Framework.Internal.Tests.Model.Company" )]
		public void JoinedAndDiscriminatorClass()
		{
			ActiveRecordStarter.Initialize( GetConfigSource(), typeof(Company) );
		}

		[Test]
		[ExpectedException( typeof(ActiveRecordException) )]
		public void VersionedTimestampedClass()
		{
			ActiveRecordStarter.Initialize( GetConfigSource(), typeof(VersionedTimestampedClass) );
		}

		[Test]
		[ExpectedException( typeof(ActiveRecordException), "A type must declare a primary key. Check type Castle.ActiveRecord.Framework.Internal.Tests.Model.ClassWithoutPrimaryKey" )]
		public void ClassWithoutPrimaryKey()
		{
			ActiveRecordStarter.Initialize( GetConfigSource(), typeof(ClassWithoutPrimaryKey) );
		}

        [Test]
        [ExpectedException(typeof(ActiveRecordException), "Property Clazz must be virtual because class LazyClassWithoutVirtualPropertyOnBelongsTo support lazy loading [ActiveRecord(Lazy=true)]")]
        public void LazyClassWithoutVirtualPropertyOnBelongsTo()
        {
            ActiveRecordStarter.Initialize(GetConfigSource(), typeof(LazyClassWithoutVirtualPropertyOnBelongsTo));
        }

        [Test]
        [ExpectedException(typeof(ActiveRecordException), "You can't use [Property] on ClassWithBadMapping.ClassA because Castle.ActiveRecord.Framework.Internal.Tests.Model.ClassA is an active record class, did you mean to use BelongTo?")]
        public void ClassThatMapAnotherActiveRecordClassAsAPropertyInsteadOfBelongsTo()
        {
            ActiveRecordStarter.Initialize(GetConfigSource(), typeof(ClassWithBadMapping), typeof(ClassA));
        }

		[Test]
		[ExpectedException(typeof(ActiveRecordException), "To use type 'BadCompositeKey' as a composite id, you must implement Equals and GetHashCode.")]
		public void ClassWithBadCompositeKey()
		{
			ActiveRecordStarter.Initialize(GetConfigSource(), typeof(ClassWithBadCompositeKey));
		}

		[Test]
		[ExpectedException(typeof(ActiveRecordException), "You can't specify a PrimaryKeyAttribute in a joined subclass. Check type Castle.ActiveRecord.Framework.Internal.Tests.Model.JoinedSubClassWithPrimaryKey")]
		public void JoinedClassWithPrimaryKey()
		{
			ActiveRecordStarter.Initialize(GetConfigSource(), typeof(BaseJoinedClass), typeof(JoinedSubClassWithPrimaryKey));
		}
	
	}
}
