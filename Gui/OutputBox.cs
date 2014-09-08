using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System;
using Engine;
using MyCollections;

namespace Gui {
	class OutputBox : IOBox, IOutputable {
		private Color normalCol = Color.Khaki;
		private Color errorCol = Color.Red;

		public OutputBox(int x, int y, int w, int h) : base(x,y,w,h) {
			BackColor = Color.Khaki;
			ReadOnly = true;
		}
		
		public void ShowError(bool b) {
			BackColor = b ? errorCol : normalCol;
		}

		public void ColorIn(List<Token> tokens) {
			
		}
	}
}

