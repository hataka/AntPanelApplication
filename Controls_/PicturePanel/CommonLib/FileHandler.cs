using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
// 追加 Time-stamp: <2011-01-24 9:29:57 kahata>
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
//using PluginCore.Helpers;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
//using IWshRuntimeLibrary;
namespace CommonLibrary
{
	public class FileHandler
	{
		//////////////////////////////////////////////////////////////////////////
		// Path Url 操作関数
		public static String Path2Url(String path)
		{
			return path.Replace("F:\\", "http://localhost/").Replace("\\", "/");
		}

		public static String Url2Path(String url)
		{
			return url.Replace("http://localhost", "F:").Replace("/", "\\");
		}

		public static Boolean IsWebSite(String path)
		{
			//$link = ereg_replace("(https?|ftp|news)(://[[:alnum:]\+\$\;\?\.%,!#~*/:@&=_-]+)",
			//"＜a href=”\\1\\2” target=”_blank”＞\\1\\2＜/a＞",$link);
			//return path.IndexOf("http://") != -1 ? true : false;

			//URLっぽいか調べる
			//http://dobon.net/vb/dotnet/string/regexismatch.html
			if (System.Text.RegularExpressions.Regex.IsMatch(
				path, @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"))
			{
				return true;
			}
			else return false;
		}

		/// <summary>
		/// fileの内容をList (String)に読み込む
		/// http://dobon.net/vb/dotnet/file/readfile.html
		/// </summary>
		/// <param name="path"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static List<String> ReadFileToList(String path, String encoding)
		{
			List<String> output = new List<String>();
			if(encoding == String.Empty) encoding = "shift_jis";
			//"C:\test.txt"をShift-JISコードとして開く
			System.IO.StreamReader sr 
				= new System.IO.StreamReader(path,System.Text.Encoding.GetEncoding(encoding));
			//内容を一行ずつ読み込む
			while (sr.Peek() > -1)
			{
				//Console.WriteLine(sr.ReadLine());
				output.Add(sr.ReadLine());
			}
			//閉じる
			sr.Close();
			return output;
		}
		/// <summary>
		/// fileの内容をString[]に読み込む
		/// http://dobon.net/vb/dotnet/file/readfile.html
		/// </summary>
		/// <param name="path"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static String[] ReadFileToArray(String path, String encoding)
		{
			return ReadFileToList(path, encoding).ToArray();
		}
	
	
	}
}
