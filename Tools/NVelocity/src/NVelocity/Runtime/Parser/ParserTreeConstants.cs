namespace NVelocity.Runtime.Parser
{
	using System;

	/* Generated By:JJTree: Do not edit this line. ParserTreeConstants.java */

	public class ParserTreeConstants
	{
		/// <summary>
		/// private constructor as class is meant to hold constants only.
		/// Class was orginally an interface in Java, but as C# does not support Fields in an interface and
		/// the jjtNodeName field, I converted it to a class with no constructor.
		/// </summary>
		private ParserTreeConstants()
		{
		}

		public const int PROCESS = 0;
		public const int VOID = 1;
		public const int ESCAPED_DIRECTIVE = 2;
		public const int ESCAPE = 3;
		public const int COMMENT = 4;
		public const int NUMBER_LITERAL = 5;
		public const int STRING_LITERAL = 6;
		public const int IDENTIFIER = 7;
		public const int WORD = 8;
		public const int DIRECTIVE = 9;
		public const int BLOCK = 10;
		public const int OBJECT_ARRAY = 11;
		public const int INTEGER_RANGE = 12;
		public const int METHOD = 13;
		public const int REFERENCE = 14;
		public const int TRUE = 15;
		public const int FALSE = 16;
		public const int TEXT = 17;
		public const int IF_STATEMENT = 18;
		public const int ELSE_STATEMENT = 19;
		public const int ELSE_IF_STATEMENT = 20;
		public const int SET_DIRECTIVE = 21;
		public const int EXPRESSION = 22;
		public const int ASSIGNMENT = 23;
		public const int OR_NODE = 24;
		public const int AND_NODE = 25;
		public const int EQ_NODE = 26;
		public const int NE_NODE = 27;
		public const int LT_NODE = 28;
		public const int GT_NODE = 29;
		public const int LE_NODE = 30;
		public const int GE_NODE = 31;
		public const int ADD_NODE = 32;
		public const int SUBTRACT_NODE = 33;
		public const int MUL_NODE = 34;
		public const int DIV_NODE = 35;
		public const int MOD_NODE = 36;
		public const int NOT_NODE = 37;

		public static readonly String[] NodeName =
			new String[]
				{
					"process", "void", "EscapedDirective", "Escape", "Comment", "NumberLiteral", "StringLiteral", "Identifier", "Word",
					"Directive", "Block", "ObjectArray", "IntegerRange", "Method", "Reference", "True", "False", "Text", "IfStatement",
					"ElseStatement", "ElseIfStatement", "SetDirective", "Expression", "Assignment", "OrNode", "AndNode", "EQNode",
					"NENode", "LTNode", "GTNode", "LENode", "GENode", "AddNode", "SubtractNode", "MulNode", "DivNode", "ModNode",
					"NotNode"
				};
	}
}