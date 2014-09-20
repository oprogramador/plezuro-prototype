/*
 * PointerT.cs
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
	class PointerT : Pointer<ReferenceT>, IVariable {
		public PointerT(ReferenceT op) :  base(op) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is PointerT) return Value.Value.ID.CompareTo(((PointerT)ob).Value.Value.ID);
			return Value.Value.CompareTo(((ReferenceT)ob).Value);
		}

		public object Clone() {
			return new PointerT(Value);
		}
		
		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {
			"**",	(Func<PointerT,ReferenceT>) ((x) => x.Value),
		};
		
		static PointerT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Pointer", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(PointerT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public override string ToString() {
			return "pointer: "+Value.ID;
		}
	}
}
