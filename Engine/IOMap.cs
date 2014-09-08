using System;
using System.Collections.Generic;using MyCollections;

namespace Engine {
	class IOMap : Dictionary<ITextable, IOutputable> {
		public IOMap() {

		}

		public void Process(ITextable t) {
			this[t].Text = "";
			var par = new Parser(t.Text, t, this[t], null);
			this[t].Text += par.Str;
			this[t].ShowError(!par.IsCorrectSyntax);
		}

	}
}
