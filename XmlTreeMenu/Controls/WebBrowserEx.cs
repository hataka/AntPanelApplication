using PluginCore;
using System;
using System.Windows.Forms;

namespace XMLTreeMenu.Controls
{
	internal class WebBrowserEx : WebBrowser
	{
		public override bool PreProcessMessage(ref Message msg)
		{
			return ((Form)PluginBase.MainForm).PreProcessMessage(ref msg);
		}
	}
}
