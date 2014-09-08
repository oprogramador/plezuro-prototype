using System;
using System.Collections.Generic;
namespace Engine {
	class SymbolException : Exception {
		public SymbolException(string msg) : base("Error, symbol '"+msg+"' not found.") {
		}
	}
}
