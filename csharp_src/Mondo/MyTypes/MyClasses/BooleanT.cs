/*
 * BooleanT.cs
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
 

using Mondo.MyCollections;
using System;
using System.Collections.Generic;

namespace Mondo.MyTypes.MyClasses {
	class BooleanT : Pointer<bool>, IVariable {
		public BooleanT(bool x) : base(x) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			if(ob is bool) return Value==(bool)ob ? 0 : (Value ? 1 : -1);
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is BooleanT) return Value.CompareTo(((BooleanT)ob).Value);
			return 0;
		}

		public object Clone() {
			return new BooleanT(Value);
		}

		public override string ToString() {
			return Value ? "true" : "false";
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;

		public static object[] Constants = {
			"true",		true,
			"false",	false,
		};

		private static object[] lambdas = {
			"if",	(Func<IPrintable,bool,ProcedureT,ProcedureT,object>) ((p, con, t, f) => p.EvalDyn(con ? t : f, p)),
			"?",	(Func<bool,PairT,IVariable>) ((c,p) => c ? p.Key : p.Value),
			"|",	(Func<bool,bool,bool>) ((x,y) => x||y),
			"&",	(Func<bool,bool,bool>) ((x,y) => x&&y),
			"!",	(Func<bool,bool>) ((x) => !x),
		};
		
		static BooleanT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Boolean", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(BooleanT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}
	}
}
