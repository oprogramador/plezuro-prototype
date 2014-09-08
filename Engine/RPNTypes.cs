using System;
using System.Collections.Generic;
namespace Engine {
	enum RPNTypes {
		Function,
		Operator,
		Symbol,
		Constant,
		BracketOpen,
		BracketClose,
		CurlyOpen,
		CurlyClose,
	}
}
