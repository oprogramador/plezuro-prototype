/*
 * MyObject.cs
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

namespace Mondo.MyTypes.MyClasses {
	class MyObject : IVariable {
		public  ClassT Class { get; private set; }
		public DictionaryT Pools { get; private set; }
		public int ID { get; private set; }

		public MyObject(ClassT c, DictionaryT p) {
			Class = c;
			Pools = p;
		}

		public MyObject(ClassT c) {
			Class = c;
			Pools = new DictionaryT();
		}

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ReferenceT.PreCompare(this,ob);
			if(pre!=0) return pre;
			//if(ob is BooleanT) return Value.CompareTo(((BooleanT)ob).Value);
			return 0;
		}

		public object Clone() {
			return new MyObject(Class, Pools);
		}

		public override string ToString() {
			return "object of "+Class;
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public ClassT GetClass() {
			return Class;
		}

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}
