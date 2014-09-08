using System.Windows.Forms;

namespace Gui {
	class IOBox : RichTextBox {
		public IOBox(int x, int y, int w, int h) {
			new FormAdapter(this).Adapt(x,y,w,h);
			Multiline = true;
			//ScrollBars = ScrollBars.Vertical;
		}
	}
}
