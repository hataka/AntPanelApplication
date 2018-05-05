//css_ref .\PicturePanel.dll;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace XMLTreeMenu.Controls
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			PicturePanel form;
			if (args.Length == 0) form = new PicturePanel();
			else form = new PicturePanel(args);

			form.Dock = DockStyle.Fill;
			Form app = new Form();
			app.Controls.Add(form);
			app.ClientSize = new Size(800, 600);
			app.StartPosition = FormStartPosition.CenterScreen;
			app.Text = "PicturePanel";
			Application.Run(app);
		}
	}
}
