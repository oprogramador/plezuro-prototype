using System;
using System.Collections.Generic;
namespace MyCollections {
	public class OStack : WStack<object> {
		public OStack() : base() {
		}

		public OStack(OStack os) :  base(os) {
		}

	}
}
