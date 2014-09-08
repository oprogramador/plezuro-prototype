/*
 * Parser.cs
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
using System.Collections.Generic;using MyCollections;
using MyTypes;
using MyTypes.MyClasses;

namespace Engine {
	class Parser {

		public string Str {
			get {
				return ""+Result;
			}
			private set{}
		}

		public object Result { get; private set; }

		public bool IsCorrectSyntax { get; private set; }

		public static object Parse(string str, ITextable output) {
			return new Parser(str,null,output,null).Result;
		}
		
		public static object Parse(string str) {
			return new Parser(str,null,null).Result;
		}

		public static object Parse(string str, TupleT args) {
			return new Parser(str,null,args).Result;
		}

		public static object Parse(string str, IPrintable p, TupleT args) {
			return new Parser(str, p, args).Result;
		}

		public Parser(string str, IPrintable p, TupleT args) {
			try {
				IsCorrectSyntax = true;
				var tokens = new Tokenizer(str).Output;
				var convTokens = new TokenConverter(new List<Token>(tokens)).Output;
				var ws = new RPN(convTokens).Output;
				Result = args==null ? Evaluator.Eval(ws, p) : Evaluator.Eval(ws, p, args);
			} catch(Exception e) {
				IsCorrectSyntax = false;
				Result = new ErrorText(e);
			}
		}

		public Parser(string str, ITextable input, ITextable output, TupleT args) {
			try {
				IsCorrectSyntax = true;
				var tokens = new Tokenizer(str).Output;
				if(input!=null) input.ColorIn(tokens);
				var convTokens = new TokenConverter(new List<Token>(tokens)).Output;
				var ws = new RPN(convTokens).Output;
				Result = args==null ? Evaluator.Eval(ws, output) : Evaluator.Eval(ws, output, args);
			} catch(Exception e) {
				IsCorrectSyntax = false;
				Result = new ErrorText(e);
			}
		}
	}
}
