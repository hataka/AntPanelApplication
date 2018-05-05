using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AntPlugin.CommonLibrary
{
	internal class OsVersion
	{
		private struct OSVERSIONINFOEX
		{
			public int dwOSVersionInfoSize;

			public int dwMajorVersion;

			public int dwMinorVersion;

			public int dwBuildNumber;

			public int dwPlatformId;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string szCSDVersion;

			public short wServicePackMajor;

			public short wServicePackMinor;

			public short wSuiteMask;

			public byte wProductType;

			public byte wReserved;
		}

		private const int VER_NT_WORKSTATION = 1;

		private const int VER_NT_DOMAIN_CONTROLLER = 2;

		private const int VER_NT_SERVER = 3;

		private const int VER_SUITE_SMALLBUSINESS = 1;

		private const int VER_SUITE_ENTERPRISE = 2;

		private const int VER_SUITE_TERMINAL = 16;

		private const int VER_SUITE_DATACENTER = 128;

		private const int VER_SUITE_SINGLEUSERTS = 256;

		private const int VER_SUITE_PERSONAL = 512;

		private const int VER_SUITE_BLADE = 1024;

		private const int VER_SUITE_STORAGE_SERVER = 8192;

		private const int VER_SUITE_WH_SERVER = 32768;

		private const int SM_SERVERR2 = 89;

		private const string ERR = "取得失敗";

		private StringBuilder log = new StringBuilder();

		[DllImport("kernel32.dll")]
		private static extern bool GetVersionEx(ref OsVersion.OSVERSIONINFOEX osVersionInfo);

		[DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int nIndex);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process([In] IntPtr hProcess, out bool lpSystemInfo);

		public string GetOsDisplayString()
		{
			string text = "取得失敗";
			OsVersion.OSVERSIONINFOEX oSVERSIONINFOEX = default(OsVersion.OSVERSIONINFOEX);
			OperatingSystem oSVersion = Environment.OSVersion;
			oSVERSIONINFOEX.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OsVersion.OSVERSIONINFOEX));
			if (!OsVersion.GetVersionEx(ref oSVERSIONINFOEX))
			{
				return "取得失敗";
			}
			bool flag = this.Is64();
			this.log.AppendLine("osInfo.Platform=" + oSVersion.Platform);
			this.log.AppendLine("osInfo.Version.Major=" + oSVersion.Version.Major);
			this.log.AppendLine("osInfo.Version.Minor=" + oSVersion.Version.Minor);
			this.log.AppendLine("osVersionInfo.wProductType=" + oSVERSIONINFOEX.wProductType);
			this.log.AppendLine("osVersionInfo.wSuiteMask=" + oSVERSIONINFOEX.wSuiteMask);
			this.log.AppendLine("IntPtr.Size=" + IntPtr.Size);
			this.log.AppendLine("64 bit OS=" + Convert.ToString(flag));
			if (oSVersion.Platform == PlatformID.Win32NT)
			{
				if (oSVersion.Version.Major > 4)
				{
					text = "Microsoft ";
				}
				if (oSVersion.Version.Major == 6)
				{
					if (oSVersion.Version.Minor == 0)
					{
						if (oSVERSIONINFOEX.wProductType == 1)
						{
							text += "Windows Vista";
						}
						else
						{
							text += "Windows Server 2008";
						}
					}
					else if (oSVersion.Version.Minor == 1)
					{
						if (oSVERSIONINFOEX.wProductType == 1)
						{
							text += "Windows 7";
						}
						else
						{
							text += "Windows Server 2008 R2";
						}
					}
					else if (oSVersion.Version.Minor == 2)
					{
						if (oSVERSIONINFOEX.wProductType == 1)
						{
							text += "Windows 8";
						}
						else
						{
							text += "Windows Server 2012";
						}
					}
				}
				else if (oSVersion.Version.Major == 5)
				{
					if (oSVersion.Version.Minor == 2)
					{
						if (OsVersion.GetSystemMetrics(89) != 0)
						{
							text += "Windows Server 2003 R2";
						}
						else if ((oSVERSIONINFOEX.wSuiteMask & 8192) > 0)
						{
							text += "Windows Storage Server 2003";
						}
						else if (((int)oSVERSIONINFOEX.wSuiteMask & 32768) > 0)
						{
							text += "Windows Home Server";
						}
						else if (oSVERSIONINFOEX.wProductType == 1 && flag)
						{
							text += "Windows XP Professional x64 Edition";
						}
						else
						{
							text += "Windows Server 2003";
						}
					}
					else if (oSVersion.Version.Minor == 1)
					{
						text += "Windows XP";
					}
					else if (oSVersion.Version.Minor == 0)
					{
						text += "Windows 2000";
					}
				}
				if (oSVERSIONINFOEX.szCSDVersion.Length > 0)
				{
					text = text + " " + oSVERSIONINFOEX.szCSDVersion;
				}
				if (!flag)
				{
					text += ", 32-bit";
				}
				else
				{
					text += ", 64-bit";
				}
			}
			else if (oSVersion.Platform == PlatformID.Win32Windows)
			{
				text = "This sample does not support this version of Windows.";
			}
			return text;
		}

		private bool Is64()
		{
			bool result;
			if (IntPtr.Size == 4)
			{
				bool flag = false;
				IntPtr procAddress = OsVersion.GetProcAddress(OsVersion.GetModuleHandle("Kernel32.dll"), "IsWow64Process");
				if (procAddress != IntPtr.Zero && !OsVersion.IsWow64Process(Process.GetCurrentProcess().Handle, out flag))
				{
					flag = false;
				}
				result = flag;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public string GetLog()
		{
			return this.log.ToString();
		}
	}
}
