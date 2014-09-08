using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;using Engine;
using MyCollections;

namespace Gui {
	class InputBox : IOBox, ITextable {
		private IOMap map;
		private List<Token> myTokens;
		public bool AutoRefresh {
			get;
			set;
		}
		
		public InputBox(IOMap map, int x, int y, int w, int h) : base(x,y,w,h) {
			this.map = map;
			//BackColor = Color.Green;
			KeyUp += new KeyEventHandler(KeyDownFun);
			//VisibleChanged += new EventHandler(ColorEv);
			//KeyDown += new KeyEventHandler(ColorEv);
			KeyPress += new KeyPressEventHandler(ColorEv);
		}
	
		void KeyDownFun(object sender, KeyEventArgs e) {
			if(AutoRefresh) map.Process(this);
		}

		private void ColorEv(object sender, EventArgs e) {
			if(myTokens!=null) ColorIn(myTokens);
		}

		public void ColorIn(List<Token> tokens) {
			myTokens = tokens;
			int pos = 0;
			int startCursorPosition = SelectionStart;
			int selectionLength = SelectionLength;
			foreach(var t in tokens) {
				if(pos+t.Text.Length >= Text.Length) break;
				Select(pos, pos+t.Text.Length);
				try{
					SelectionColor = VisualSyntax.SyntaxColors[t.Type];
				} catch(Exception e) {
					Console.WriteLine("color e: "+e);
				}
				pos += t.Text.Length;
			}
			SelectionStart = startCursorPosition;
			SelectionLength = selectionLength;
		}

	}
}
