/*
 * DictionaryT.cs
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
using System.Collections;
using MyCollections;
using Engine;

namespace MyTypes.MyClasses {
	class DictionaryT : SortedDictionary<IVariable,IVariable>, IVariable {
		public DictionaryT(DictionaryT d) :  base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(var i in d) Add(i.Key,i.Value);
		}

		public DictionaryT(Dictionary<object,object> d) : base() {
			Console.WriteLine("dictionaryT ctor");
			foreach(var i in d) {
				Console.WriteLine("dic ctor key="+i);
				Add(new StringT((string)i.Key), new StringT((string)i.Value));
			}
		}

		public DictionaryT(TupleT l) : base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(var i in l) {
				//Console.WriteLine("i="+i+" type="+i.GetType());
				if(!(((ReferenceT)i).Value is PairT)) {
					addFromIE(l);
					break;
				}
				Add(new ReferenceT(((PairT)((ReferenceT)i).Value).Key), 
					new ReferenceT(((PairT)((ReferenceT)i).Value).Value));
			}
		}

		public void Add(PairT x) {
			Add(new ReferenceT(x.Key), new ReferenceT(x.Value));
		}

		public DictionaryT(IEnumerable ie) :  base() {
			Console.WriteLine("dictionaryT iector");
			ID = ObjectContainer.Instance.Add(this);
			addFromIE(ie);
		}

		private void addFromIE(IEnumerable ie) {
			object key = null;
			Console.WriteLine("dictionaryT init");
			foreach(var i in ie) {
				if(key==null) key = i;
				else {
					Console.WriteLine("key="+key+" value="+i);
					Add((IVariable)key,(IVariable)i);
					key = null;
				}
			}
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			return 0;
		}

		public object Clone() {
			return new DictionaryT(this);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			SymbolMap.RefSymbol,	(Func<DictionaryT,IVariable,ReferenceT>) ((a,i) => (ReferenceT)a[new ReferenceT(i)] ),
			"len",		(Func<DictionaryT,double>) ((a) => a.Count),
			"contains",	(Func<DictionaryT,IVariable,bool>) ((a,x) => a.ContainsKey(new ReferenceT(x))),
			"keys",		(Func<DictionaryT,SetT>) ((x) => new SetT(x.Keys)),
			"remove",	(Func<DictionaryT,IVariable,DictionaryT>) ((x,i) => {var y=(DictionaryT)x.Clone(); y.Remove(new ReferenceT(i)); return y;}),
			"<<",		(Func<DictionaryT,PairT,DictionaryT>) ((a,v) => {a.Add((PairT)v.Clone()); return a;} ),
		};
		
		static DictionaryT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Dictionary", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(DictionaryT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			string ret = "{";
			foreach(var i in Keys) ret += (ret=="{" ? "" : ", ")+i+":"+this[i];
			ret += "}";
			return ret;
		}
	}
}
