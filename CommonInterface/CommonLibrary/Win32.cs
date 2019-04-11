using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace CommonInterface.CommonLibrary
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
        try
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
            Win32.ShowMaximized(process.MainWindowHandle);
          }
          return process;
        }
        catch (Exception exc)
        {
          MessageBox.Show(exc.Message.ToString(), "LoadProcessInControl");
          return null;
        }
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





























    // INIT
    private static Boolean shouldUseWin32 = Type.GetType("Mono.Runtime") == null;

		/// <summary>
		/// Checks if Win32 functionality should be used
		/// </summary>
		public static Boolean ShouldUseWin32()
		{
			return shouldUseWin32;
		}

		/// <summary>
		/// Checks if we are running on Windows
		/// </summary>
		public static Boolean IsRunningOnWindows()
		{
			return shouldUseWin32;
		}

		/// <summary>
		///  Checks if we are running on Wine
		/// </summary>
		public static Boolean isRunningOnWine()
		{
			return Registry.LocalMachine.OpenSubKey(@"Software\Wine\") != null;
		}

		/// <summary>
		/// Checks if we are running on Mono
		/// </summary>
		public static Boolean IsRunningOnMono()
		{
			return !shouldUseWin32;
		}

		#region Enums

		public enum SHGFI
		{
			SmallIcon = 0x00000001,
			LargeIcon = 0x00000000,
			Icon = 0x00000100,
			DisplayName = 0x00000200,
			Typename = 0x00000400,
			SysIconIndex = 0x00004000,
			UseFileAttributes = 0x00000010,
			ShellIconSize = 0x4,
			AddOverlays = 0x000000020
		}

		public enum HitTest
		{
			HTCLIENT = 1,
			HTCAPTION = 2,
			HTSYSMENU = 3,
			HTGROWBOX = 4,
			HTMENU = 5,
			HTHSCROLL = 6,
			HTVSCROLL = 7,
			HTMINBUTTON = 8,
			HTMAXBUTTON = 9,
			HTLEFT = 10,
			HTRIGHT = 11,
			HTTOP = 12,
			HTTOPLEFT = 13,
			HTTOPRIGHT = 14,
			HTBOTTOM = 15,
			HTBOTTOMLEFT = 16,
			HTBOTTOMRIGHT = 17,
			HTBORDER = 18,
			HTOBJECT = 19,
			HTCLOSE = 20,
			HTHELP = 21
		}

		#endregion

		#region Structs

		public struct COPYDATASTRUCT
		{
			public Int32 dwData;
			public Int32 cbData;
			public IntPtr lpData;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO
		{
			public SHFILEINFO(Boolean b)
			{
				hIcon = IntPtr.Zero;
				iIcon = 0;
				dwAttributes = 0;
				szDisplayName = "";
				szTypeName = "";
			}
			public IntPtr hIcon;
			public Int32 iIcon;
			public UInt32 dwAttributes;
			[MarshalAs(UnmanagedType.LPStr, SizeConst = 260)]
			public String szDisplayName;
			[MarshalAs(UnmanagedType.LPStr, SizeConst = 80)]
			public String szTypeName;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class SCROLLINFO
		{
			public int cbSize = Marshal.SizeOf(typeof(Win32.SCROLLINFO));
			public int fMask;
			public int nMin;
			public int nMax;
			public int nPage;
			public int nPos;
			public int nTrackPos;
		}

		#endregion

		#region Constants

		public const Int32 SB_HORZ = 0;
		public const Int32 SB_VERT = 1;
		public const Int32 SB_BOTH = 3;
		public const Int32 SB_THUMBPOSITION = 4;
		public const Int32 SB_THUMBTRACK = 5;
		public const Int32 SB_LEFT = 6;
		public const Int32 SB_RIGHT = 7;
		public const Int32 WM_HSCROLL = 0x0114;
		public const Int32 WM_VSCROLL = 0x0115;
		public const UInt32 LVM_SCROLL = 0x1014;
		//public const UInt32 SWP_SHOWWINDOW = 64;
		//public const Int32 SW_RESTORE = 9;
		public const Int32 WM_SETREDRAW = 0xB;
		public const Int32 WM_PRINTCLIENT = 0x0318;
		public const Int32 PRF_CLIENT = 0x00000004;
		public const Int32 TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
		public const Int32 TVS_EX_DOUBLEBUFFER = 0x0004;
		public const Int32 TV_FIRST = 0x1100;
		public const Int32 WM_NCLBUTTONDOWN = 0x00A1;
		public const Int32 WM_LBUTTONDOWN = 0x0201;
		public const Int32 VK_ESCAPE = 0x1B;
		public const Int32 WM_COPYDATA = 74;
		public const Int32 WM_MOUSEWHEEL = 0x20A;
		public const Int32 WM_KEYDOWN = 0x100;
		public const Int32 WM_KEYUP = 0x101;
		public const Int32 SIF_RANGE = 0x0001;
		public const Int32 SIF_PAGE = 0x0002;
		public const Int32 SIF_POS = 0x0004;
		public const Int32 SIF_DISABLENOSCROLL = 0x0008;
		public const Int32 SIF_TRACKPOS = 0x0010;
		public const Int32 SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);

		#endregion

		#region DllImports

		//[DllImport("user32.dll")]
		//public static extern Boolean IsIconic(IntPtr hWnd);

		//[DllImport("user32.dll")]
		//public static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

		//[DllImport("user32.dll")]
		//public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, Int32 x, Int32 y, Int32 width, Int32 height, UInt32 flags);

		//[DllImport("user32.dll")]
		//public static extern IntPtr SendMessage(IntPtr hWnd, Int32 msg, IntPtr wp, IntPtr lp);

		[DllImport("user32.dll")]
		public static extern Int32 SendMessage(IntPtr handle, Int32 messg, Int32 wparam, Int32 lparam);

		//[DllImport("user32.dll")]
		//public static extern IntPtr WindowFromPoint(Point pt);

		[DllImport("user32.dll")]
		public static extern Boolean ShowScrollBar(IntPtr hWnd, Int32 wBar, Boolean bShow);

		[DllImport("user32.dll")]
		public static extern Int32 GetScrollPos(IntPtr hWnd, Int32 nBar);

		[DllImport("user32.dll")]
		public static extern Int32 SetScrollPos(IntPtr hWnd, Int32 nBar, Int32 nPos, Boolean bRedraw);

		[DllImport("user32.dll")]
		public static extern Boolean GetScrollInfo(IntPtr hWnd, Int32 fnBar, SCROLLINFO scrollInfo);

		[DllImport("user32.dll")]
		public static extern Boolean ReleaseCapture();

		//[DllImport("user32.dll")]
		//public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

		//[DllImport("user32.dll")]
		//public static extern UInt32 SetForegroundWindow(IntPtr hwnd);

		[DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
		public static extern Boolean PathCompactPathEx([MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszOut, [MarshalAs(UnmanagedType.LPTStr)] String pszSource, [MarshalAs(UnmanagedType.U4)] Int32 cchMax, [MarshalAs(UnmanagedType.U4)] Int32 dwReserved);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 GetLongPathName([MarshalAs(UnmanagedType.LPTStr)] String path, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder longPath, Int32 longPathLength);
		
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 GetShortPathName(String lpszLongPath, StringBuilder lpszShortPath, Int32 cchBuffer);

		[DllImport("shell32.dll", EntryPoint = "#28")]
		public static extern UInt32 SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] String pszPath, out IntPtr ppidl, ref Int32 rgflnOut);

		[DllImport("shell32.dll", EntryPoint = "SHGetPathFromIDListW")]
		public static extern Boolean SHGetPathFromIDList(IntPtr pidl, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszPath);

		[DllImport("user32.dll")]
		public static extern Int32 DestroyIcon(IntPtr hIcon);

		[DllImport("kernel32.dll")]
		public static extern Int32 GetModuleHandle(String lpModuleName);

		[DllImport("shell32.dll")]
		public static extern IntPtr ExtractIcon(Int32 hInst, String FileName, Int32 nIconIndex);

		[DllImport("shell32.dll")]
		public static extern Int32 SHGetFileInfo(String pszPath, UInt32 dwFileAttributes, out SHFILEINFO psfi, UInt32 cbfileInfo, SHGFI uFlags);

    #endregion

    #region Window

    /// <summary>
    /// Sets the window specified by handle to fullscreen
    /// </summary>
    /*
    public static void SetWinFullScreen(IntPtr hwnd)
		{
			Screen screen = Screen.FromHandle(hwnd);
			Int32 screenTop = screen.WorkingArea.Top;
			Int32 screenLeft = screen.WorkingArea.Left;
			Int32 screenWidth = screen.WorkingArea.Width;
			Int32 screenHeight = screen.WorkingArea.Height;
			SetWindowPos(hwnd, IntPtr.Zero, screenLeft, screenTop, screenWidth, screenHeight, SWP_SHOWWINDOW);
		}
    */
		/// <summary>
		/// Restores the window with Win32
		/// </summary>
		public static void RestoreWindow(IntPtr handle)
		{
			if (IsIconic(handle)) ShowWindow(handle, SW_RESTORE);
		}

		#endregion

		#region Scrolling

		/// <summary>
		/// 
		/// </summary>
		public static SCROLLINFO GetFullScrollInfo(Control lv, Boolean horizontalBar)
		{
			Int32 fnBar = (horizontalBar ? SB_HORZ : SB_VERT);
			SCROLLINFO scrollInfo = new SCROLLINFO();
			scrollInfo.fMask = SIF_ALL;
			if (GetScrollInfo(lv.Handle, fnBar, scrollInfo)) return scrollInfo;
			else return null;
		}

		/// <summary>
		/// 
		/// </summary>
		public static Point GetScrollPos(Control ctrl)
		{
			return new Point(GetScrollPos(ctrl.Handle, SB_HORZ), GetScrollPos(ctrl.Handle, SB_VERT));
		}

		/// <summary>
		/// 
		/// </summary>
		public static void SetScrollPos(Control ctrl, Point scrollPosition)
		{
			SetScrollPos(ctrl.Handle, SB_HORZ, scrollPosition.X, true);
			SetScrollPos(ctrl.Handle, SB_VERT, scrollPosition.Y, true);
		}

		/// <summary>
		/// 
		/// </summary>
		public static void ScrollToLeft(Control ctrl)
		{
			SendMessage(ctrl.Handle, WM_HSCROLL, SB_LEFT, 0);
		}

		#endregion

	}

}

