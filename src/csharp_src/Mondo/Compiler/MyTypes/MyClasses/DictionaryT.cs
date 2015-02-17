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
using Mondo.MyCollections;
using Mondo.Engine;

namespace Mondo.MyTypes.MyClasses {
	public class DictionaryT : SortedDictionary<IVariable,IVariable>, IVariable {
		public DictionaryT() : base() {
		}

		public DictionaryT(DictionaryT d) :  base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(var i in d) Add(i.Key,i.Value);
		}

		public DictionaryT(Dictionary<object,object> d) : base() {
			foreach(var i in d) {
				Add(new StringT((string)i.Key), new StringT((string)i.Value));
			}
		}

		public DictionaryT(Dictionary<string,Method> d) : base() {
			foreach(var i in d) {
				Add(new ReferenceT(new StringT(i.Key)), new ReferenceT(i.Value));
			}
		}

		public DictionaryT(TupleT l) : base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(var i in l) {
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
			ID = ObjectContainer.Instance.Add(this);
			addFromIE(ie);
		}

		private void addFromIE(IEnumerable ie) {
			object key = null;
			foreach(var i in ie) {
				if(key==null) key = i;
				else {
					Add((IVariable)key,(IVariable)i);
					key = null;
				}
			}
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ReferenceT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return General.Compare(this, (IEnumerable)ob); 
		}

		public object Clone() {
			return new DictionaryT(this);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		private static ClassT myClass;
		
		public const string ClassName = "Dictionary";

		private static object[] lambdas = {
			SymbolMap.RefSymbol,	(Func<DictionaryT,IVariable,ReferenceT>) ((a,i) => (ReferenceT)a[new ReferenceT(i)] ),
			"len",		(Func<DictionaryT,double>) ((a) => a.Count),
			"contains",	(Func<DictionaryT,IVariable,bool>) ((a,x) => a.ContainsKey(new ReferenceT(x))),
			"keys",		(Func<DictionaryT,SetT>) ((x) => new SetT(x.Keys)),
			"remove",	(Func<DictionaryT,IVariable,DictionaryT>) ((x,i) => {var y=(DictionaryT)x.Clone(); y.Remove(new ReferenceT(i)); return y;}),
                        "window",	(Func<IPrintable,DictionaryT,WindowT>) ((p,x) => new WindowT(p, (DictionaryT)x.Clone(), null)),
			"<<",		(Func<DictionaryT,PairT,DictionaryT>) ((a,v) => {a.Add((PairT)v.Clone()); return a;} ),
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(DictionaryT) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			string ret = "";
			foreach(var i in Keys) ret += (ret=="" ? "" : ", ")+TypeTrans.dereference(i)+":"+TypeTrans.dereference(this[i]);
			ret = Tokenizer.DicSymbol + ret + "]";
			return ret;
		}
	}
}
