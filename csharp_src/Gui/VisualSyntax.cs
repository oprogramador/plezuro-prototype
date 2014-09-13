/*
 * VisualSyntax.cs
 * Copyright 2014 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301, USA.
 * 
 * 
 */
 

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
