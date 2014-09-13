/*
 * TypeT.cs
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

namespace MyTypes.MyClasses {
	class TypeT : Pointer<Type>, IVariable {
		public static readonly Dictionary<Type,int> CompareTree;

		public static readonly Dictionary<Type,string> TypeNames = new Dictionary<Type,string>();

		private static readonly object[] types =   {
			typeof(NullType), "nullType",
			typeof(EmptyT), "empty",
			typeof(TypeT), "type",
			typeof(ErrorT), "error",
			typeof(BooleanT), "boolean",
			typeof(Number), "number",
			typeof(StringT), "string",
			typeof(PointerT), "pointer",
			typeof(ReferenceT), "reference",
			typeof(PairT), "pair",
			typeof(ListT), "list",
			typeof(TupleT), "tuple",
			typeof(SetT), "set",
			typeof(DictionaryT), "dictionary",
			typeof(ProcedureT), "procedure",
			typeof(BuiltinFunc), "builtinFunc",
			typeof(CallFunc), "callfunc",
		};

		static TypeT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Type", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(TypeT) ); 
			CompareTree = new Dictionary<Type,int>();
			TypeNames = new Dictionary<Type,string>();
			for(int i=0; i<types.Length; i+=2) {
				CompareTree.Add((Type)types[i], i);
				TypeNames.Add((Type)types[i], (string)types[i+1]);
			}
		}

		public TypeT(Type t) : base(t) {
		}

		private static int compValue(Type t) {
			try {
				return CompareTree[t];
			} catch {
				return -1;
			}
		}

		private static string nameValue(Type t) {
			try {
				return TypeNames[t];
			} catch {
				return "otherType";
			}
		}

		public static int PreCompare(object a, object b) {
			return compValue(a.GetType()) . CompareTo( compValue(b.GetType()) );
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is TypeT) return CompareTree[Value].CompareTo( CompareTree[((TypeT)ob).Value] );
			return 0;
		}

		public object Clone() {
			return new TypeT(Value);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};

		public ClassT GetClass() {
			return MyClass;
		}

		public override string ToString() {
			return nameValue(Value);
		}
	}
}
