using System;
using System.Collections.Generic;
namespace MyCollections {
	interface IParseable {
		object Parse(string str, ITextable t);
	}
}
