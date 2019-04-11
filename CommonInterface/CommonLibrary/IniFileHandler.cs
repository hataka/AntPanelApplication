using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonInterface.CommonLibrary
{
	public class IniFileHandler
	{
		[DllImport("KERNEL32.DLL")]
		public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringA")]
		public static extern uint GetPrivateProfileStringByByteArray(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, uint nSize, string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

		private static string GetValueFromIniFile(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			IniFileHandler.GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, stringBuilder, (uint)stringBuilder.Capacity, lpFileName);
			return stringBuilder.ToString();
		}

		private static void Sample()
		{
			IniFileHandler.WritePrivateProfileString("アプリ1", "キー1", "ハロー", "c:\\sample.ini");
			IniFileHandler.WritePrivateProfileString("アプリ1", "キー2", "1234", "c:\\sample.ini");
			IniFileHandler.WritePrivateProfileString("アプリ2", "キー1", "good morning", "c:\\sample.ini");
			StringBuilder stringBuilder = new StringBuilder(1024);
			IniFileHandler.GetPrivateProfileString("アプリ1", "キー1", "default", stringBuilder, (uint)stringBuilder.Capacity, "c:\\sample.ini");
			Console.WriteLine("アプリ1セクションに含まれるキー1の値: {0}", stringBuilder.ToString());
			uint privateProfileInt = IniFileHandler.GetPrivateProfileInt("アプリ1", "キー2", 0, "c:\\sample.ini");
			Console.WriteLine("アプリ1セクションに含まれるキー2の値: {0}", privateProfileInt);
			byte[] array = new byte[1024];
			uint privateProfileStringByByteArray = IniFileHandler.GetPrivateProfileStringByByteArray("アプリ1", null, "default", array, (uint)array.Length, "c:\\sample.ini");
			string @string = Encoding.Default.GetString(array, 0, (int)(privateProfileStringByByteArray - 1u));
			string arg_F2_0 = @string;
			char[] separator = new char[1];
			string[] array2 = arg_F2_0.Split(separator);
			string[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				string arg = array3[i];
				Console.WriteLine("アプリ1セクションに含まれるキー名: {0}", arg);
			}
			byte[] array4 = new byte[1024];
			uint privateProfileStringByByteArray2 = IniFileHandler.GetPrivateProfileStringByByteArray(null, null, "default", array4, (uint)array4.Length, "c:\\sample.ini");
			string string2 = Encoding.Default.GetString(array4, 0, (int)(privateProfileStringByByteArray2 - 1u));
			string arg_167_0 = string2;
			char[] separator2 = new char[1];
			string[] array5 = arg_167_0.Split(separator2);
			string[] array6 = array5;
			for (int j = 0; j < array6.Length; j++)
			{
				string arg2 = array6[j];
				Console.WriteLine("このファイルに含まれるセクション名: {0}", arg2);
			}
			IniFileHandler.WritePrivateProfileString("アプリ2", "キー1", null, "c:\\sample.ini");
			IniFileHandler.WritePrivateProfileString("アプリ1", null, null, "c:\\sample.ini");
		}
	}
}
