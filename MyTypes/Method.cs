using System;
using System.Collections.Generic;

namespace MyTypes {
	public class Method {
		public ICallable Proc { get; private set; }
		public AccessModifier Access { get; private set; }

		public Method(ICallable proc, AccessModifier access) {
			Proc = proc;
			Access = access;
		}
	}
}
