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
using System.Collections.Generic;
using Mondo.MyCollections;
using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;
using System.Threading;
using Mondo.Controller;

namespace Mondo.Engine {
	class Parser {
		private static List<Thread> threads;
		private IRefreshable ir;

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

		public static object Parse(string str, ITuplable args) {
			return new Parser(str,null,args).Result;
		}

		public static object Parse(string str, IPrintable p, ITuplable args) {
			return new Parser(str, p, args).Result;
		}

		static Parser() {
			threads = new List<Thread>();
		}

		public Parser(string str, IPrintable p, ITuplable args) {
			var t = new Thread(() => init(str,p,args));
			threads.Add(t);
			t.Start();
		}

		public Parser(string str, ITextable input, ITextable output, ITuplable args) {
			var t = new Thread(() => init(str,input,output,args));
			threads.Add(t);
			t.Start();
		}

		public Parser(IRefreshable ir, string str, ITextable input, ITextable output, ITuplable args) {
			this.ir = ir;
			var t = new Thread(() => init(str,input,output,args));
			threads.Add(t);
			t.Start();
		}

		private void init(string str, IPrintable p, ITuplable args) {
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
			if(ir!=null) ir.Refresh();
			Console.WriteLine("parser init iscorrect="+IsCorrectSyntax+" res="+Result);
		}

		private void init(string str, ITextable input, ITextable output, ITuplable args) {
			try {
				IsCorrectSyntax = true;
				var tokens = new Tokenizer(str).Output;
				//if(input!=null) input.ColorIn(tokens);
				var convTokens = new TokenConverter(new List<Token>(tokens)).Output;
				var ws = new RPN(convTokens).Output;
				Result = args==null ? Evaluator.Eval(ws, output) : Evaluator.Eval(ws, output, args);
			} catch(Exception e) {
				IsCorrectSyntax = false;
				Result = new ErrorText(e);
			}
			if(ir!=null) ir.Refresh();
			Console.WriteLine("parser init iscorrect="+IsCorrectSyntax+" res="+Result);
		}

		public static void Stop() {
			foreach(var t in threads) t.Abort();
		}
	}
}
