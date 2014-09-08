using System;
using System.Collections.Generic;
namespace MyCollections {
	public class Token {
		public string Text { get; private set; }

		public TokenTypes Type { get; private set; }

		public object Value { get; private set; }

		public Token(string txt, TokenTypes t, object v) {
			Text = txt;
			Type = t;
			Value = v;
		}

		public override string ToString() {
			return "token( text: "+Text+" type: "+Type+" value: "+Value+")";
		}
		
	}
}
