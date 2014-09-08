using System;
using System.Collections.Generic;using System.Windows.Forms;

namespace Gui {
	public class MyMenu : MainMenu {
		public MyMenu(string[] str, MyItem[][] items) {
			if(str==null) return;
			if(str.Length<1) return;
			MenuItem[] mi = new MenuItem[str.Length];
			for(int i=0; i<str.Length; i++) MenuItems.Add(mi[i] = new MenuItem(str[i]));
			for(int i=0; i<items.Length; i++)
				for(int k=0; k<items[i].Length; k++)
					mi[i].MenuItems.Add(items[i][k].str, items[i][k].eh);
		}
	}
}
