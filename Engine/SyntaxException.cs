using System;
using System.Collections.Generic;using MyCollections;

namespace Engine {
	class SyntaxException : Exception {
		public SyntaxException() : base("Error, invalid syntax.") {
		}

		public SyntaxException(string msg) : base(msg) {
		}
	
		public SyntaxException(TokenTypes t): this("Error, invalid syntax.") {
		}
	}
}
