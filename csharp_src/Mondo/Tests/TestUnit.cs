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

namespace Mondo.Tests {
	class TestUnit {
		private System.Threading.Thread t1,t2;

		public TestUnit() {
			//testGenerics();
			//testHtml();
			//threads();
			//testGener();
		}

		private void threads() {
			t1 = new System.Threading.Thread(new System.Threading.ThreadStart(run1));
			t2 = new System.Threading.Thread(new System.Threading.ThreadStart(run2));
			t1.Start();
			t2.Start();
		}

		private void run1() {
			for(int i=0; i<20; i++) {
				Console.WriteLine("t1: "+i);
				System.Threading.Thread.Sleep(1);
				if(i>10) t2.Abort(); 
			}
		}

		private void run2() {
			for(int i=0; i<20; i++) {
				Console.WriteLine("t2: "+i);
				System.Threading.Thread.Sleep(1);
				if(i>10) t1.Abort(); 
			}
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
