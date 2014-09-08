using System;
using System.Collections.Generic;
namespace MyCollections {
	public class WList<T> : List<T> {
		public override string ToString() {
			return General.EnumToString(this);
		}
		
		public T Peek() {
			return this[Count-1];
		}

		public void Shift(T ic) {
			Insert(0,ic);
		}

		public T Pop() {
			T v = this[Count-1];
			RemoveAt(Count-1);
			return v;
		}
		
		public WList<T> Concat(WList<T> st, Type t) {
			var ret = (WList<T>)Activator.CreateInstance(t,this);
			ret.AddRange(st);
			return ret;
		}

	}
}
