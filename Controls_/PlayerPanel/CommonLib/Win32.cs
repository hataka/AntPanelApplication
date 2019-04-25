using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MDIForm.CommonLibrary
{
	public class Win32
	{
		public enum GWL
		{
			WINDPROC = -4,
			HINSTANCE = -6,
			HWNDPARENT = -8,
			STYLE = -16,
			EXSTYLE = -20,
			USERDATA = -21,
			ID = -12
		}

		public enum SWP
		{
			NOSIZE = 1,
			NOMOVE,
			NOZORDER = 4,
			NOREDRAW = 8,
			NOACTIVATE = 16,
			FRAMECHANGED = 32,
			SHOWWINDOW = 64,
			HIDEWINDOW = 128,
			NOCOPYBITS = 256,
			NOOWNERZORDER = 512,
			NOSENDCHANGING = 1024
		}

		public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

		public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

		public static class MdiUtil
		{
			public const int SW_RESTORE = 9;

			public const uint SWP_SHOWWINDOW = 64u;

			private const int SW_HIDE = 0;

			private const int SW_SHOWNORMAL = 1;

			private const int SW_SHOWMINIMIZED = 2;

			private const int SW_SHOWMAXIMIZED = 3;

			private const int SW_SHOWNOACTIVATE = 4;

			private const int SW_SHOW = 5;

			private const int SW_MINIMIZE = 6;

			private const int SW_SHOWMINNOACTIVE = 7;

			private const int SW_SHOWNA = 8;

			private const int SW_SHOWDEFAULT = 10;

			[DllImport("user32.dll", SetLastError = true)]
			private static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

			public static Process LoadProcessInControl(Process p, Control ctrl)
			{
				p.WaitForInputIdle();
				int num = 0;
				while (p.MainWindowHandle == IntPtr.Zero && num < 1000)
				{
					Thread.Sleep(100);
					num++;
					p.Refresh();
				}
				if (p.MainWindowHandle != IntPtr.Zero)
				{
					Win32.MdiUtil.SetParent(p.MainWindowHandle, ctrl.Handle);
				}
				return p;
			}

			public static Process LoadProcessInControl(string filename, Control ctrl)
			{
				Process process = new Process();
				process.StartInfo.FileName = filename;
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(filename);
				process.Start();
				process.WaitForInputIdle();
				int num = 0;
				while (process.MainWindowHandle == IntPtr.Zero && num < 1000)
				{
					Thread.Sleep(100);
					num++;
					process.Refresh();
				}
				if (process.MainWindowHandle != IntPtr.Zero)
				{
					Win32.MdiUtil.SetParent(process.MainWindowHandle, ctrl.Handle);
				}
				return process;
			}

			public static Process LoadProcessInControl(string filename, string args, Control ctrl)
			{
				Process process = new Process();
				process.StartInfo.FileName = filename;
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(filename);
				process.StartInfo.Arguments = args;
				process.Start();
				process.WaitForInputIdle();
				int num = 0;
				while (process.MainWindowHandle == IntPtr.Zero && num < 1000)
				{
					Thread.Sleep(100);
					num++;
					process.Refresh();
				}
				if (process.MainWindowHandle != IntPtr.Zero)
				{
					Win32.MdiUtil.SetParent(process.MainWindowHandle, ctrl.Handle);
				}
				return process;
			}

			public static Process LoadProcessInControl(ProcessStartInfo hPsInfo, Control ctrl)
			{
				Process process = Process.Start(hPsInfo);
				process.WaitForInputIdle();
				int num = 0;
				while (process.MainWindowHandle == IntPtr.Zero && num < 1000)
				{
					Thread.Sleep(100);
					num++;
					process.Refresh();
				}
				if (process.MainWindowHandle != IntPtr.Zero)
				{
					Win32.MdiUtil.SetParent(process.MainWindowHandle, ctrl.Handle);
				}
				return process;
			}

			public static MdiClient GetMdiClient(Form form)
			{
				foreach (Control control in form.Controls)
				{
					if (control is MdiClient)
					{
						return (MdiClient)control;
					}
				}
				return null;
			}

			[DllImport("user32.dll")]
			public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			public static void ShowMaximized(IntPtr hWnd)
			{
				Win32.MdiUtil.ShowWindow(hWnd, 3);
			}
		}

		public const int SW_RESTORE = 9;

		public const uint SWP_SHOWWINDOW = 64u;

		private const int SW_HIDE = 0;

		private const int SW_SHOWNORMAL = 1;

		private const int SW_SHOWMINIMIZED = 2;

		private const int SW_SHOWMAXIMIZED = 3;

		private const int SW_SHOWNOACTIVATE = 4;

		private const int SW_SHOW = 5;

		private const int SW_MINIMIZE = 6;

		private const int SW_SHOWMINNOACTIVE = 7;

		private const int SW_SHOWNA = 8;

		private const int SW_SHOWDEFAULT = 10;

		private const int GW_HWNDFIRST = 0;

		private const int GW_HWNDLAST = 1;

		private const int GW_HWNDNEXT = 2;

		private const int GW_HWNDPREV = 3;

		private const int GW_OWNER = 4;

		private const int GW_CHILD = 5;

		private const uint WS_EX_NOACTIVATE = 134217728u;

		[DllImport("user32.dll")]
		public static extern bool IsIconic(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern uint SetForegroundWindow(IntPtr hwnd);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

		[DllImport("user32.dll")]
		public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(Point pt);

		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpStr, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpStr, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("user32.dll")]
		public static extern uint GetWindowLong(IntPtr hWnd, Win32.GWL index);

		[DllImport("user32.dll")]
		public static extern uint SetWindowLong(IntPtr hWnd, Win32.GWL index, uint unValue);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll")]
		public static extern IntPtr GetParent(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumChildWindows(IntPtr hwndParent, Win32.EnumWindowProc lpEnumFunc, IntPtr lParam);

		[DllImport("user32.Dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EnumChildWindows(IntPtr parentHandle, Win32.Win32Callback callback, IntPtr lParam);

		public static void SetWinFullScreen(IntPtr hwnd)
		{
			Screen screen = Screen.FromHandle(hwnd);
			int top = screen.WorkingArea.Top;
			int left = screen.WorkingArea.Left;
			int width = screen.WorkingArea.Width;
			int height = screen.WorkingArea.Height;
			Win32.SetWindowPos(hwnd, IntPtr.Zero, left, top, width, height, 64u);
		}

		public static void ActivateWindow(IntPtr handle)
		{
			Win32.SystemParametersInfo(8193u, 0u, 0u, 3u);
			if (Win32.IsIconic(handle))
			{
				Win32.ShowWindow(handle, 9);
			}
			else
			{
				Win32.SetForegroundWindow(handle);
			}
			Win32.SystemParametersInfo(8193u, 200000u, 200000u, 3u);
		}

		public static void ShowMaximized(IntPtr hWnd)
		{
			Win32.ShowWindow(hWnd, 3);
		}

		public static void SetWindowStyleNoCaption(IntPtr hWnd)
		{
			uint num = 12582912u;
			uint num2 = Win32.GetWindowLong(hWnd, Win32.GWL.STYLE);
			num2 &= ~num;
			Win32.SetWindowLong(hWnd, Win32.GWL.STYLE, num2);
		}

		public static void ActivateWindowByName(string name)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			IntPtr intPtr = Win32.GetForegroundWindow();
			while (intPtr != IntPtr.Zero)
			{
				if (Win32.IsWindowVisible(intPtr))
				{
					Win32.GetWindowText(intPtr, stringBuilder, stringBuilder.Capacity);
					if (stringBuilder.ToString().IndexOf(name) != -1)
					{
						Win32.SetForegroundWindow(intPtr);
						return;
					}
				}
				intPtr = Win32.GetWindow(intPtr, 2u);
			}
		}

		public static List<IntPtr> GetWindowsInControl(IntPtr ptr)
		{
			List<IntPtr> list = new List<IntPtr>();
			IntPtr window = Win32.GetWindow(ptr, 5u);
			while (window != IntPtr.Zero)
			{
				list.Add(window);
				window = Win32.GetWindow(window, 2u);
			}
			return list;
		}

		private void this_activate(Form form)
		{
			int windowThreadProcessId = Win32.GetWindowThreadProcessId(Win32.GetForegroundWindow(), IntPtr.Zero);
			int currentThreadId = AppDomain.GetCurrentThreadId();
			Win32.AttachThreadInput(currentThreadId, windowThreadProcessId, true);
			form.Activate();
			Win32.AttachThreadInput(currentThreadId, windowThreadProcessId, false);
		}

		public static void ActivateWindowByClassName(string name)
		{
			IntPtr intPtr = Win32.FindWindow(name, null);
			if (intPtr != IntPtr.Zero && Win32.IsWindowVisible(intPtr))
			{
				Win32.SetForegroundWindow(intPtr);
			}
		}

		public static List<IntPtr> GetChildWindows(IntPtr parent)
		{
			List<IntPtr> list = new List<IntPtr>();
			GCHandle value = GCHandle.Alloc(list);
			try
			{
				Win32.EnumWindowProc lpEnumFunc = new Win32.EnumWindowProc(Win32.EnumWindow);
				Win32.EnumChildWindows(parent, lpEnumFunc, GCHandle.ToIntPtr(value));
			}
			finally
			{
				if (value.IsAllocated)
				{
					value.Free();
				}
			}
			return list;
		}

		private static bool EnumWindow(IntPtr handle, IntPtr pointer)
		{
			List<IntPtr> list = GCHandle.FromIntPtr(pointer).Target as List<IntPtr>;
			if (list == null)
			{
				throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
			}
			list.Add(handle);
			return true;
		}
	}
}
