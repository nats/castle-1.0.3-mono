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
namespace Castle.Components.Validator
{
	using System;

	/// <summary>
	/// Validate that this date is a valid one.
	/// </summary>
	/// <remarks>
	/// This checks the format of the date
	/// </remarks>
	[Serializable, CLSCompliant(false)]
	public class ValidateSingleAttribute : AbstractValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidateSingleAttribute"/> class.
		/// </summary>
		public ValidateSingleAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidateSingleAttribute"/> class.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		public ValidateSingleAttribute(string errorMessage)
			: base(errorMessage)
		{
		}

		/// <summary>
		/// Constructs and configures an <see cref="IValidator"/>
		/// instance based on the properties set on the attribute instance.
		/// </summary>
		/// <returns></returns>
		public override IValidator Build()
		{
			IValidator validator = new SingleValidator();

			ConfigureValidatorMessage(validator);

			return validator;
		}
	}
}
