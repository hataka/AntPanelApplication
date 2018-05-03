// -*- mode: cs -*-  Time-stamp: <2017-03-24 9:51:28 kahata>
/*================================================================
 * title: 
 * file: MdiHosting.cs
 * path: C:\Users\˜a•F\Desktop\MdiHosting.cs
 * url:  C:/Users/˜a•F/Desktop/MdiHosting.cs
 * created: Time-stamp: <2017-03-24 9:51:28 kahata>
 * revision: $Id$
 * Programmed By: kahata
 * To compile:
 * To run: 
 * link: http://d.hatena.ne.jp/machi_pon/20070516/1179358410
 * description: 
 *================================================================*/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MDIForm
{
	public static class MdiUtil
	{
		[DllImport( "user32.dll", SetLastError = true )]
		private static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		public static void LoadProcessInControl(string filename, Control ctrl)
		{
			Process p = Process.Start( filename );
			p.WaitForInputIdle();
			SetParent( p.MainWindowHandle, ctrl.Handle );
		}

		public static MdiClient GetMdiClient(Form form)
		{
			foreach( Control c in form.Controls )
			{
				if( c is MdiClient )
				{
					return (MdiClient)c;
				}
			}
			return null;
		}
	}
}

// Žg‚¤‘¤
// MdiUtil.LoadProcessInControl( "calc.exe", MdiUtil.GetMdiClient( this ) );
