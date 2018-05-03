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
namespace CommonLibrary
{
	public class IniFileHandler
	{
		// http://www.atmarkit.co.jp/fdotnet/dotnettips/039inifile/inifile.html
		[DllImport("KERNEL32.DLL")]
		public static extern uint 
			GetPrivateProfileString(string lpAppName, 
			string lpKeyName, string lpDefault, 
			StringBuilder lpReturnedString, uint nSize, 
			string lpFileName);

		[DllImport("KERNEL32.DLL",EntryPoint="GetPrivateProfileStringA")]
		public static extern uint 
			GetPrivateProfileStringByByteArray(string lpAppName, 
			string lpKeyName, string lpDefault, 
			byte [] lpReturnedString, uint nSize, 
			string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint 
			GetPrivateProfileInt( string lpAppName, 
			string lpKeyName, int nDefault, string lpFileName );

		[DllImport("KERNEL32.DLL")]
			public static extern uint WritePrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpString,
			string lpFileName);

		/*
		// http://uchukamen.com/Programming/iniFile/#SEC5
		//uint entryLength;
		//string strEntryStringValue;
		//System.Text.StringBuilder strEntryString = new System.Text.StringBuilder( 256 );
		//entryLength = GetPrivateProfileString( "SECTION", "ENTRY", "Nothing", strEntryString, (uint)(strEntryString.Capacity), "C:\\TEMP\\TEST.ini" );
		//strEntryStringValue = strEntryString.ToString();

		// この例では、C:\TEMP\Test.ini 
		// ファイルのセクション名 "SECTION" に、キーネーム"ENTRY" に値 "Hello" が設定されます。
		//[DEFAULT]
		//BASEURL=http://msdn.microsoft.com/en-us/library/ms533049.aspx
		//[InternetShortcut]
		//URL=http://msdn.microsoft.com/en-us/library/ms533049.aspx
		*/

		static String GetValueFromIniFile(String lpAppName, string lpKeyName, 
			string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName)
		{
			uint entryLength;
			string strEntryStringValue;

			System.Text.StringBuilder strEntryString = new System.Text.StringBuilder( 256 );
			entryLength = GetPrivateProfileString(
	 			lpAppName, lpKeyName, lpDefault, strEntryString,
				(uint)(strEntryString.Capacity), lpFileName);
			strEntryStringValue = strEntryString.ToString();
			return strEntryStringValue;
		}

		/*
		 * 書き込み
		string str = this.textBox1.Text;

		uint entryLength;
		entryLength = WritePrivateProfileString( "SECTION", "ENTRY", str, "C:\\TEMP\\TEST.ini" );
		この例では、C:\TEMP\Test.ini ファイルのセクション名 "SECTION" に、
		キーネーム"ENTRY" に値 "Hello" が設定されます。
		*/
		static void Sample()
		{

			// キーと値を書き加える
			IniFileHandler.WritePrivateProfileString("アプリ1", "キー1", "ハロー", @"c:\sample.ini");
			IniFileHandler.WritePrivateProfileString("アプリ1", "キー2", "1234", @"c:\sample.ini");
			IniFileHandler.WritePrivateProfileString("アプリ2", "キー1", "good morning", @"c:\sample.ini");

			// 文字列を読み出す
			StringBuilder sb = new StringBuilder(1024);
			IniFileHandler.GetPrivateProfileString("アプリ1", "キー1", "default", sb, (uint)sb.Capacity, @"c:\sample.ini");
			Console.WriteLine("アプリ1セクションに含まれるキー1の値: {0}", sb.ToString());

			// 整数値を読み出す
			uint resultValue = IniFileHandler.GetPrivateProfileInt("アプリ1", "キー2", 0, @"c:\sample.ini");
			Console.WriteLine("アプリ1セクションに含まれるキー2の値: {0}", resultValue);

			// 指定セクションのキーの一覧を得る
			byte[] ar1 = new byte[1024];
			uint resultSize1 = IniFileHandler.GetPrivateProfileStringByByteArray("アプリ1", null, "default", ar1, (uint)ar1.Length, @"c:\sample.ini");
			string result1 = System.Text.Encoding.Default.GetString(ar1, 0, (int)resultSize1 - 1);
			string[] keys = result1.Split('\0');
			foreach (string key in keys)
			{
				Console.WriteLine("アプリ1セクションに含まれるキー名: {0}", key);
			}

			// 指定ファイルのセクションの一覧を得る
			byte[] ar2 = new byte[1024];
			uint resultSize2 = IniFileHandler.GetPrivateProfileStringByByteArray(null, null, "default", ar2, (uint)ar2.Length, @"c:\sample.ini");
			string result2 = System.Text.Encoding.Default.GetString(ar2, 0, (int)resultSize2 - 1);
			string[] sections = result2.Split('\0');
			foreach (string section in sections)
			{
				Console.WriteLine("このファイルに含まれるセクション名: {0}", section);
			}

			// 1つのキーと値のペアを削除する
			IniFileHandler.WritePrivateProfileString("アプリ2", "キー1", null, @"c:\sample.ini");

			// 指定セクション内の全てのキーと値のペアを削除する
			IniFileHandler.WritePrivateProfileString("アプリ1", null, null, @"c:\sample.ini");
		}
	
	
	
	
	}
}


/*
//////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace WindowsApplication11
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(184, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 40);
			this.button1.TabIndex = 0;
			this.button1.Text = "ini ファイルからの読み込み";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(184, 88);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 32);
			this.button2.TabIndex = 2;
			this.button2.Text = "ini ファイルへの書き込み";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(32, 88);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(128, 19);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "textBox1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(280, 166);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
			this.textBox1,
			this.button2,
			this.label1,
			this.button1});
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		/// <summary>
		/// ini ファイルの読み込み用の関数(GetPrivateProfileString)の宣言部分
		/// </summary>
		[DllImport("kernel32.dll", EntryPoint="GetPrivateProfileString")]
		private static extern uint GetPrivateProfileString( string lpApplicationName, 
		string lpEntryName, string lpDefault, System.Text.StringBuilder lpReturnedString, 
		uint nSize, string lpFileName ); 

		/// <summary>
		/// ini ファイルの書き込み用の関数(WritePrivateProfileString)の宣言部分
		/// </summary>
		[DllImport("kernel32.dll", EntryPoint="WritePrivateProfileString")] 
		private static extern uint WritePrivateProfileString( string lpApplicationName, 
		string lpEntryName, string lpEntryString, string lpFileName ); 


		private void button1_Click(object sender, System.EventArgs e)
		{
			
			uint entryLength;
			string strEntryStringValue;

			// 読み込むためのバッファ
			// string は、immutable （変更できない）ので、StringBuilder を使って、
			// 次のようにバッファを渡してあげる必要があります。
			System.Text.StringBuilder strEntryString = new System.Text.StringBuilder( 256 );
			entryLength = GetPrivateProfileString( "SECTION", "ENTRY", "Nothing", 
			strEntryString, (uint)(strEntryString.Capacity), "C:\\TEMP\\TEST.ini" );
			strEntryStringValue = strEntryString.ToString();

			// 結果をラベルに書く。
			this.label1.Text = strEntryStringValue;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			// ini ファイルの SECTION に書き込む値を TextBox からとってくる。
			string str = this.textBox1.Text;

			uint entryLength;
			entryLength = WritePrivateProfileString( "SECTION", "ENTRY", str, "C:\\TEMP\\TEST.ini" );
		}
	}
}
*/

