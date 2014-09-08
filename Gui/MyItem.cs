using System;
using System.Collections.Generic;using System.Windows.Forms;

namespace Gui {
	public class MyItem {
		public string str { get; private set; }
		public EventHandler eh { get; private set; }

		public MyItem(string str, EventHandler eh) {
			this.str = str;
			this.eh = eh;
		}
	}
}
