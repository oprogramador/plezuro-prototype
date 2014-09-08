using System;
using System.Collections.Generic;

namespace MyTypes {
	public enum AccessEnum {
		Public,
		Protected,
		Private,
		Other		
	}

	public class AccessModifier {
		public AccessEnum Mod { get; private set; }
		
		public AccessModifier(AccessEnum mod) {
			Mod = mod;
		}
	}
}
