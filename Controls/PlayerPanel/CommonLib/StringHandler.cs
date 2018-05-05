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
namespace MDIForm.CommonLibrary
{
	public class StringHandler
	{
		
		/// <summary>
		/// バイト型配列を文字列に変換する
		/// バイト型配列"bytesData"に文字列データが入っているものとする
		/// string str;
		/// Shift JISとして文字列に変換
		/// str = System.Text.Encoding.GetEncoding(932).GetString(bytesData);
		/// JISとして変換
		/// str = System.Text.Encoding.GetEncoding(50220).GetString(bytesData);
		/// EUCとして変換
		/// str = System.Text.Encoding.GetEncoding(51932).GetString(bytesData);
		/// UTF-8として変換
		/// str = System.Text.Encoding.UTF8.GetString(bytesData);
		/// 結果を表示
		/// Console.WriteLine(str);
		/// 文字列をバイト型配列に変換する
		/// string str = "テストです。";
		/// byte [] bytesData;
		/// Shift JISとして文字列に変換
		/// bytesData = System.Text.Encoding.GetEncoding(932).GetBytes(str);
		/// JISとして変換
		/// bytesData = System.Text.Encoding.GetEncoding(50220).GetBytes(str);
		/// EUCとして変換
		/// bytesData = System.Text.Encoding.GetEncoding(51932).GetBytes(str);
		/// UTF-8として変換
		/// bytesData = System.Text.Encoding.UTF8.GetBytes(str);
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static byte[] StringToBytes(string str)
		{
			//string str = "シフトJISへ変換";
			System.Text.Encoding sjisEnc = System.Text.Encoding.GetEncoding("Shift_JIS");
			byte[] bytes = sjisEnc.GetBytes(str);
			return bytes;
			//Console.WriteLine(BitConverter.ToString(bytes));
			// 出力：83-56-83-74-83-67-4A-49-53-82-D6-95-CF-8A-B7
		}
	
		/// <summary>
		/// http://ameblo.jp/only-human/entry-10104676221.html
		/// 文字コードの変換方法を書きます。
		/// 文字コードの変換って何かというと、下記のような感じ
		/// Shift-JIS →　UTF-8
		/// Windowsの標準が[Shift-JIS]で世の中がUnicodeに流れて行ってる中、必要な技術かと。
		/// Shift-JISのファイルを読み取って、UTF-8に変換するとか。
		/// ちなみにやらないと、意味不明な文字がテキストに表示されたりとか。。。
		/// まぁ文字化けですね。
		/// そんな問題を解決する方法を記載します。
		/// System.Text.Encoding src = System.Text.Encoding.ASCII;
		/// System.Text.Encoding dest = System.Text.Encoding.GetEncoding("Shift_JIS");
		/// string str = "テスト文章";
		/// byte [] temp = src.GetBytes(str);
		/// byte[] sjis_temp = System.Text.Encoding.Convert(src, dest, temp);
		/// string sjis_str = dest.GetString(sjis_temp);
		/// Console.WriteLine(str);
		/// Console.WriteLine(sjis_str);	
		/// これでUTF-8→Shift_JISに変換してます。
		/// 2つの目のコンソール出力で文字化けしているかと思います。
		///（デフォルト、.netがUTF-8標準のため）
		/// 下記のように独自ユーティリティークラスのメソッドにしておくと便利かと
		/// </summary>
		/// <param name="src"></param>
		/// <param name="destEnc"></param>
		/// <returns></returns>
		public static string ConvertEncoding(string src, System.Text.Encoding destEnc)
		{
			byte[] src_temp = System.Text.Encoding.ASCII.GetBytes(src);
			byte[] dest_temp = System.Text.Encoding.Convert(System.Text.Encoding.ASCII, destEnc, src_temp);
			string ret = destEnc.GetString(dest_temp);
			return ret;
		}


		/// <summary>
		/// 文字コードを判別する
		/// </summary>
		/// <remarks>
		/// Jcode.pmのgetcodeメソッドを移植したものです。
		/// Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
		/// Jcode.pmのCopyright: Copyright 1999-2005 Dan Kogai
		/// </remarks>
		/// <param name="bytes">文字コードを調べるデータ</param>
		/// <returns>適当と思われるEncodingオブジェクト。
		/// 判断できなかった時はnull。</returns>
		//  http://dobon.net/vb/dotnet/string/detectcode.html
		public static System.Text.Encoding GetCode(byte[] bytes)
		{
			const byte bEscape = 0x1B;
			const byte bAt = 0x40;
			const byte bDollar = 0x24;
			const byte bAnd = 0x26;
			const byte bOpen = 0x28;	//'('
			const byte bB = 0x42;
			const byte bD = 0x44;
			const byte bJ = 0x4A;
			const byte bI = 0x49;

			int len = bytes.Length;
			byte b1, b2, b3, b4;

			//Encode::is_utf8 は無視

			bool isBinary = false;
			for (int i = 0; i < len; i++)
			{
				b1 = bytes[i];
				if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
				{
					//'binary'
					isBinary = true;
					if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
					{
						//smells like raw unicode
						return System.Text.Encoding.Unicode;
					}
				}
			}
			if (isBinary)
			{
				return null;
			}

			//not Japanese
			bool notJapanese = true;
			for (int i = 0; i < len; i++)
			{
				b1 = bytes[i];
				if (b1 == bEscape || 0x80 <= b1)
				{
					notJapanese = false;
					break;
				}
			}
			if (notJapanese)
			{
				return System.Text.Encoding.ASCII;
			}

			for (int i = 0; i < len - 2; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				b3 = bytes[i + 2];

				if (b1 == bEscape)
				{
					if (b2 == bDollar && b3 == bAt)
					{
						//JIS_0208 1978
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					else if (b2 == bDollar && b3 == bB)
					{
						//JIS_0208 1983
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					else if (b2 == bOpen && (b3 == bB || b3 == bJ))
					{
						//JIS_ASC
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					else if (b2 == bOpen && b3 == bI)
					{
						//JIS_KANA
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					if (i < len - 3)
					{
						b4 = bytes[i + 3];
						if (b2 == bDollar && b3 == bOpen && b4 == bD)
						{
							//JIS_0212
							//JIS
							return System.Text.Encoding.GetEncoding(50220);
						}
						if (i < len - 5 &&
							b2 == bAnd && b3 == bAt && b4 == bEscape &&
							bytes[i + 4] == bDollar && bytes[i + 5] == bB)
						{
							//JIS_0208 1990
							//JIS
							return System.Text.Encoding.GetEncoding(50220);
						}
					}
				}
			}

			//should be euc|sjis|utf8
			//use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
			int sjis = 0;
			int euc = 0;
			int utf8 = 0;
			for (int i = 0; i < len - 1; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
					((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
				{
					//SJIS_C
					sjis += 2;
					i++;
				}
			}
			for (int i = 0; i < len - 1; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
					(b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
				{
					//EUC_C
					//EUC_KANA
					euc += 2;
					i++;
				}
				else if (i < len - 2)
				{
					b3 = bytes[i + 2];
					if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
						(0xA1 <= b3 && b3 <= 0xFE))
					{
						//EUC_0212
						euc += 3;
						i += 2;
					}
				}
			}
			for (int i = 0; i < len - 1; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
				{
					//UTF8
					utf8 += 2;
					i++;
				}
				else if (i < len - 2)
				{
					b3 = bytes[i + 2];
					if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
						(0x80 <= b3 && b3 <= 0xBF))
					{
						//UTF8
						utf8 += 3;
						i += 2;
					}
				}
			}
			//M. Takahashi's suggestion
			//utf8 += utf8 / 2;
			System.Diagnostics.Debug.WriteLine(
				string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
			if (euc > sjis && euc > utf8)
			{
				//EUC
				return System.Text.Encoding.GetEncoding(51932);
			}
			else if (sjis > euc && sjis > utf8)
			{
				//SJIS
				return System.Text.Encoding.GetEncoding(932);
			}
			else if (utf8 > euc && utf8 > sjis)
			{
				//UTF8
				return System.Text.Encoding.UTF8;
			}
			return null;
		}

		public static String timestamp()
		{
			DateTime dt = DateTime.Now;
			return String.Format("Time-stamp: <{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00} kahata>",
				dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
		}

		// C Heading 挿入
		public static String CHeading(String path)
		{
			String heading = Lib.File_ReadToEnd("C:\\home\\hidemaru\\template\\c_heading_001.txt");
			if (System.IO.File.Exists(path))
			{
				try
				{
					String localurl = path.Replace("F:", "http://localhost").Replace("\\", "/");
					heading = heading.Replace("%%FILEEXT%%", Path.GetExtension(path)).Replace(".", "");
					heading = heading.Replace("%%FILENAME%%", Path.GetFileName(path));
					heading = heading.Replace("%%FILEPATH%%", path);
					heading = heading.Replace("%%LOCALURL%%", localurl);
					heading = heading.Replace("%%TIMESTAMP%%", timestamp());
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(Lib.OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();		//statusBarにエラーを表示
				}
			}
			return heading;
		}
	}
}
