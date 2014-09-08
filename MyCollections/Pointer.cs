using System;
using System.Collections.Generic;
namespace MyCollections {
	public class Pointer<T> {
		public T Value {
			get;
			set;
		}

		public Pointer(T val) {
			Value = val;
		}

		public object ObValue {
			get { return Value; }
			set { Value = (T)value; }
		}

		public override string ToString() {
			return "Pointer at "+Value;
		}
	}
}
