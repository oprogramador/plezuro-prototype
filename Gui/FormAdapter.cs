using System;
using System.Collections.Generic;using System.Windows.Forms;
using System.Drawing;

namespace Gui {
	public class FormAdapter {
		public Control Control { get; private set; }

		public FormAdapter(Control c) {
			Control = c;
		}

		public FormAdapter Adapt(int x,int y,int w,int h) {
			Control.Width = w;
			Control.Height = h;
			Control.Left = x;
			Control.Top = y;
			return this;
		}

		public FormAdapter Adapt(string text, int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			Control.Text = text;
			return this;
		}
		
		public FormAdapter Adapt(string text, Color fc, Color bc, int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			SetColors(fc,bc);
			Control.Text = text;
			return this;
		}
		
		public FormAdapter Adapt(string text, Color bc, int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			Control.BackColor = bc;
			Control.Text = text;
			return this;
		}

		public FormAdapter AdaptConst(int x, int y, int w, int h) {
			Adapt(x,y,w,h);
			Control.MinimumSize = new Size(w,h);
			Control.MaximumSize = new Size(w,h);
			return this;
		}	

		public FormAdapter AdaptClick(IClickable cli, string name, Color backCol, int x, int y, int w, int h) {
			Adapt(name,backCol,x,y,w,h);
			Control.Click += new EventHandler(cli.ClickFun);
			return this;
		}
		
		public FormAdapter AdaptClick(EventHandler eh, string name, Color backCol, int x, int y, int w, int h) {
			Adapt(name,backCol,x,y,w,h);
			Control.Click += eh;
			return this;
		}

		public FormAdapter AdaptLike(Control c) {
			Control.Width = c.Width;
			Control.Height = c.Height;
			Control.Left = c.Left;
			Control.Top = c.Top;
			Control.MinimumSize = c.MinimumSize;
			Control.MaximumSize = c.MaximumSize;
			return this;
		}

		public FormAdapter AdaptFill(Control c) {
			Control.Width = c.Width;
			Control.Height = c.Height;
			return this;
		}
		
		public FormAdapter AdaptFill(Control c, int offset) {
			Control.Width = c.Width-offset;
			Control.Height = c.Height-offset;
			return this;
		}

		public FormAdapter Adapt(int x,int y) {
			Control.Left = x;
			Control.Top = y;
			return this;
		}

		public FormAdapter SetColors(Color fc, Color bc) {
			Control.ForeColor = fc;
			Control.BackColor = bc;
			return this;
		}

	}

}
