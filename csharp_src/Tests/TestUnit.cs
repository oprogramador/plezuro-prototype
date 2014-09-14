/*
 * TestUnit.cs
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

namespace Tests {
	class TestUnit {

		private void just4fun() {
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
			//Console.WriteLine(new lib.HtmlArrayTable(new object[]{new object[]{1,9,2.3}, new object[]{3,"asd",9}, 4444}).SetBorder(2).Generate());
			MyTypes.VariableFactory.GetInstance();
			var db = DataFixtures.DataFixtures.GetInstance();
		}

		public TestUnit() {
			//just4fun();
			//testGenerics();
			//testHtml();
			testGener();
		}

		private void testHtml() {
			Console.WriteLine(lib.HtmlTableFactory.Create(new object[]{
						new Dictionary<object,object>(){{"one",1}, {"two", 2}},
						new Dictionary<object,object>(){{"three",3}, {"two", 2}},
						new Dictionary<object,object>(){{"one",10}, {"two", 2}},
						22211
						}).SetBorder(2).Generate());
		}

		private void testGener() {
			int cmp = MyCollections.General.Compare(new int[]{23,4,122}, new int[]{23,4,122});
			Console.WriteLine("gener cmp="+cmp);
		}

		private void testGenerics() {
			var l = (System.Collections.Generic.List<int>) createList(typeof(int), new object[]{1,9,34,2.0,"wfwefw",13});
			Console.WriteLine("list: ");
			foreach(var i in l) Console.WriteLine(i);
			swapFirstTwoElements(l);
			Console.WriteLine("list: ");
			foreach(var i in l) Console.WriteLine(i);
		}

		private static void swapFirstTwoElements<T>(System.Collections.Generic.List<T> l) {
			if(l.Count<2) return;
			T c = l[0];
			l[0] = l[1];
			l[1] = c;
		}

		private static System.Collections.IList createList(Type t, object[] data) {
			Type listType = typeof(List<>).MakeGenericType(new [] { t } );
			var l =  (System.Collections.IList)Activator.CreateInstance(listType);
			foreach(var i in data) if(i.GetType().IsAssignableFrom(t)) l.Add(i);
			return l;
		}
	}
}
