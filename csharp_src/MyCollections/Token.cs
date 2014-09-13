/*
 * Token.cs
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
 

using System;
using System.Collections.Generic;
namespace MyCollections {
	public class Token {
		public string Text { get; private set; }

		public TokenTypes Type { get; private set; }

		public object Value { get; private set; }

		public Token(string txt, TokenTypes t, object v) {
			Text = txt;
			Type = t;
			Value = v;
		}

		public override string ToString() {
			return "token( text: "+Text+" type: "+Type+" value: "+Value+")";
		}
		
	}
}
