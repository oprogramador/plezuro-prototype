/*
 * Program.cs
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
 

using System.Windows.Forms;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using Gui;

namespace Program {
	class Program {
		private static void test() {
			Debug.Assert(Engine.Parser.Parse("2+3").Equals("5"));
		}

		private static void just4fun() {
			//System.Console.WriteLine(null+4);
			//Engine.Engine.GetInstance();
			//test();
			/*var f = (Func<System.Collections.Generic.List<int>,int>) (x => 3);
			Type t = f.GetType();
			Console.WriteLine("t="+t+" gen:"+t.IsGenericType+" func:"+(t.GetGenericTypeDefinition()==typeof(Func<,>)));
			Type[] pars = t.GetGenericArguments();
			Console.WriteLine(pars[0].GetGenericArguments()[0]);
			foreach(var p in pars) Console.WriteLine("p="+p+" int:"+(p==typeof(int)));
			var st = new Stopwatch();
			st.Start();
			Console.WriteLine(Engine.Parser.Parse(System.IO.File.ReadAllText("primes.ml")));
			st.Stop();
			Console.WriteLine("ticks="+st.Elapsed.TotalMilliseconds);
			Console.WriteLine(MyCollections.General.Converges("ala ma kota","maw",4));
			*/
			//int xx = new int[]{1,2,3}[3];
			//Console.WriteLine(string.Concat(Enumerable.Repeat("asd",4)));
			/*var x = new System.Collections.Generic.List<int>(new int[]{1,90,21,234,51,0,29,56,783,34,56,123,94});	
			var y = x.GroupBy( a => a%10 ).Select( a => a.ToList() ).ToList();
			foreach(var i in y){
				foreach(var j in (System.Collections.Generic.List<int>)i) Console.WriteLine(j);
				Console.WriteLine(",");
			}*/
			//object[] ar = new object[]{"dwe","as"};
			//ar[0] = 45;
		}

		private static void startGui() {
			Application.Run(new MainWindow(Engine.Engine.GetInstance().IOMap, 600, 400));
		}

		private static void startConsole(string[] args) {
			var l = new MyTypes.MyClasses.TupleT(args);
			l.RemoveAt(0);
			Console.WriteLine( Engine.Parser.Parse(System.IO.File.ReadAllText(args[0]).TrimEnd('\n'), l) );
		}

		public static void Main(string[] args) {
			try {
				System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
				Engine.Engine.GetInstance();
				just4fun();
				if(args.Length>0) startConsole(args);
				else startGui();
			} catch(Exception e) {
				System.Console.WriteLine(""+e);
			}
		}
	}
}
