using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonInterface.CommonLibrary
{
	public class FileHandler
	{
		public static string Path2Url(string path)
		{
			return path.Replace("F:\\", "http://localhost/").Replace("\\", "/");
		}

		public static string Url2Path(string url)
		{
			return url.Replace("http://localhost", "F:").Replace("/", "\\");
		}

		public static bool IsWebSite(string path)
		{
			return Regex.IsMatch(path, "^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$");
		}

		public static List<string> ReadFileToList(string path, string encoding)
		{
			List<string> list = new List<string>();
			if (encoding == string.Empty)
			{
				encoding = "shift_jis";
			}
			StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding(encoding));
			while (streamReader.Peek() > -1)
			{
				list.Add(streamReader.ReadLine());
			}
			streamReader.Close();
			return list;
		}

		public static string[] ReadFileToArray(string path, string encoding)
		{
			return FileHandler.ReadFileToList(path, encoding).ToArray();
		}
	}
}
