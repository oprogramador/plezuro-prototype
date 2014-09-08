using System.Drawing;
using System;
using System.Collections.Generic;using MyCollections;

namespace Gui {
	class VisualSyntax {
		public static Dictionary<TokenTypes,Color> SyntaxColors = new Dictionary<TokenTypes,Color>() {
			{TokenTypes.Symbol, Color.Green},
			{TokenTypes.Number, Color.Brown},
			{TokenTypes.Operator, Color.Black},
			{TokenTypes.BracketOpen, Color.Black},
			{TokenTypes.BracketClose, Color.Black},
			{TokenTypes.SquareOpen, Color.Black},
			{TokenTypes.SquareClose, Color.Black},
			{TokenTypes.CurlyOpen, Color.Blue},
			{TokenTypes.CurlyClose, Color.Blue},
			{TokenTypes.OneString, Color.Orange},
			{TokenTypes.BiString, Color.Orange},
			{TokenTypes.TriString, Color.Yellow},
			{TokenTypes.SoftLink, Color.Olive},
			{TokenTypes.OneLineComment, Color.Purple},
			{TokenTypes.MultiLineComment, Color.Purple},
			{TokenTypes.WhiteSpace, Color.Brown},
			{TokenTypes.EndLine, Color.Brown},
		};
	}
}
