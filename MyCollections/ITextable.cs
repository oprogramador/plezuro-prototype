using System;
using System.Collections.Generic;
namespace MyCollections {
	public interface ITextable {
		string Text {
			get;
			set;
		}

		void AppendText( string str );
		void ColorIn( List<Token> tokens );
	}
}
