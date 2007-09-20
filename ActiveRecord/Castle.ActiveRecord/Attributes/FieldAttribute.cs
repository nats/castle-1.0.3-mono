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

namespace Castle.ActiveRecord
{
	using System;

	/// <summary>
	/// Maps a standard column of the table.
	/// </summary>
	/// <example>
	/// In the following example, the column is also
	/// called 'name', so you don't have to specify.
	/// <code>
	/// public class Blog : ActiveRecordBase
	/// {
	///		[Field]
	///		string name;
	///		
	///		
	///	</code>
	/// </example>
	[AttributeUsage(AttributeTargets.Field), Serializable]
	public class FieldAttribute : WithAccessAttribute
	{
		private String column;
		private String formula;
		private String type;
		private String uniqueKey;
		private String index;
		private String sqlType;
		private String check;
		private int length;
		private bool notNull;
		private bool unique;
		private bool update = true;
		private bool insert = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldAttribute"/> class.
		/// </summary>
		public FieldAttribute() 
		{
			Access = PropertyAccess.Field;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldAttribute"/> class.
		/// </summary>
		/// <param name="column">The column name.</param>
		public FieldAttribute(String column) : this()
		{
			this.column = column;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldAttribute"/> class.
		/// </summary>
		/// <param name="column">The column name</param>
		/// <param name="type">The column type.</param>
		public FieldAttribute(String column, String type) : this(column)
		{
			this.type = type;
		}

		/// <summary>
		/// Gets or sets a value indicating whether the column allows null values
		/// </summary>
		/// <value><c>true</c> if [not null]; otherwise, <c>false</c>.</value>
		public bool NotNull
		{
			get { return notNull; }
			set { notNull = value; }
		}

		/// <summary>
		/// Gets or sets the length of this column. char(10), etc
		/// </summary>
		/// <value>The length.</value>
		public int Length
		{
			get { return length; }
			set { length = value; }
		}

		/// <summary>
		/// Gets or sets the column name
		/// </summary>
		/// <value>The column.</value>
		public String Column
		{
			get { return column; }
			set { column = value; }
		}

		/// <summary>
		/// From NHibernate documentation:
		/// A unique-key attribute can be used to group columns 
		/// in a single unit key constraint. 
		/// </summary>
		/// <value>unique key name</value>
		/// <remarks>
		/// Currently, the 
		/// specified value of the unique-key attribute is not 
		/// used to name the constraint, only to group the columns 
		/// in the mapping file.
		/// </remarks>
		public string UniqueKey
		{
			get { return uniqueKey; }
			set { uniqueKey = value; }
		}

		/// <summary>
		/// From NHibernate documentation:
		/// specifies the name of a (multi-column) index
		/// </summary>
		/// <value>index name</value>
		public string Index
		{
			get { return index; }
			set { index = value; }
		}

		/// <summary>
		/// From NHibernate documentation:
		/// overrides the default column type
		/// </summary>
		/// <value>column_type</value>
		public string SqlType
		{
			get { return sqlType; }
			set { sqlType = value; }
		}

		/// <summary>
		/// From NHibernate documentation:
		/// create an SQL check constraint on either column or table
		/// </summary>
		/// <value>Sql Expression</value>
		public string Check
		{
			get { return check; }
			set { check = value; }
		}

		/// <summary>
		/// Set to <c>false</c> to ignore this 
		/// field when updating entities of this ActiveRecord class.
		/// </summary>
		public bool Update
		{
			get { return update; }
			set { update = value; }
		}

		/// <summary>
		/// Set to <c>false</c> to ignore this 
		/// field when inserting entities of this ActiveRecord class.
		/// </summary>
		public bool Insert
		{
			get { return insert; }
			set { insert = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FieldAttribute"/> is unique.
		/// </summary>
		/// <value><c>true</c> if unique; otherwise, <c>false</c>.</value>
		public bool Unique
		{
			get { return unique; }
			set { unique = value; }
		}

		/// <summary>
		/// Gets or sets the formula used to calculate this field
		/// </summary>
		/// <value>The formula.</value>
		public String Formula
		{
			get { return formula; }
			set { formula = value; }
		}

		/// <summary>
		/// Gets or sets the type of the column.
		/// </summary>
		/// <value>The type of the column.</value>
		public String ColumnType
		{
			get { return type; }
			set { type = value; }
		}
	}
}
