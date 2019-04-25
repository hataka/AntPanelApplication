using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntPanelApplication.Controls
{
	public struct PageFont
	{
		public Font font;

		public Label label;

		public GetLabelDelegate method;

		public void Initialize(Font f, Label l)
		{
			this.font = f;
			this.label = l;
		}

		public void SetLabel()
		{
			this.label.Text = this.method(this.font);
		}
	}
}
