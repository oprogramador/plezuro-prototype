/*
 * PairT.cs
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

namespace Mondo.MyTypes.MyClasses {
	class PairT : IVariable {
		public readonly IVariable Key;
		public IVariable Value { get; set; }

		public PairT(IVariable k,IVariable v) {
			ID = ObjectContainer.Instance.Add(this);
			Key = k;
			Value = v;
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is PairT) {
				pre = Key.CompareTo(((PairT)ob).Key);
				if(pre!=0) return pre;
				pre = Value.CompareTo(((PairT)ob).Value);
				return pre;
			}
			return 0;
		}

		public object Clone() {
			return new PairT(Key,Value);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"key",		(Func<PairT,IVariable>) ((x) => x.Key),
			"value",	(Func<PairT,IVariable>) ((x) => x.Value),
		};
		
		static PairT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Pair", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(PairT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return ""+Key+":"+Value;
		}
	}
}
