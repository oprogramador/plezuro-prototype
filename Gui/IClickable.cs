using System.Windows.Forms;
using System;
using System.Collections.Generic;
namespace Gui {
	public interface IClickable {
		void ClickFun(object sender, EventArgs e);
	}
}
