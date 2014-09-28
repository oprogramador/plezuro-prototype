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

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
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

		private static ClassT myClass;
		
		public const string ClassName = "Pointer";

		private static object[] lambdas = {
			"**",	(Func<PointerT,ReferenceT>) ((x) => x.Value),
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(PairT) );
			return myClass;
		}	

		public override string ToString() {
			return "pointer: "+Value.ID;
		}
	}
}
