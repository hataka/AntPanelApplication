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
	public class ProcessHandler
	{
		// コンソール・アプリケーションの出力を取り込む
		// http://msdn.microsoft.com/ja-jp/library/system.diagnostics.processstartinfo(v=vs.80).aspx
		// http://www.atmarkit.co.jp/fdotnet/dotnettips/657redirectstdout/redirectstdout.html
		public static String getStandardOutput(String command, String arguments)
		{
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = command; // 実行するファイル
			startInfo.Arguments = arguments;
			startInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
			startInfo.UseShellExecute = false; // シェル機能を使用しない
			startInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
			System.Diagnostics.Process p = System.Diagnostics.Process.Start(startInfo); // アプリの実行開始
			String output = p.StandardOutput.ReadToEnd(); // 標準出力の読み取り
			output = output.Replace("\r\r\n", "\n"); // 改行コードの修正
			return output;
		}

		//////////////////////////////////////////////////////////////////////////
		// 開発環境 Development Build Execute 操作
		public static String GetProjectFile(String path, String ext)
		{
			String folder = Path.GetDirectoryName(path);
			//"C:\My Documents"以下のファイルをすべて取得
			//folderにあるファイルを取得する
			string[] fs = System.IO.Directory.GetFiles(folder, "*");
			foreach (String file in fs)
			{
				if (Path.GetExtension(file).ToLower() == ext)
				{
					return file;
				}
			}
			return "";
		}

		public static String GetProjectFile(String path)
		{
			//ArrayList files = new ArrayList();
			String folder = Path.GetDirectoryName(path);
			//folderにあるファイルを取得する
			string[] fs = System.IO.Directory.GetFiles(folder, "*");
			foreach (String file in fs)
			{
				if (Path.GetExtension(file).ToLower() == ".vcproj"
					|| Path.GetExtension(file).ToLower() == ".dsp"
					|| Path.GetExtension(file).ToLower() == ".csproj"
					|| Path.GetFileName(file).ToLower() == "makefile"
				)
				{
					return file;
				}
			}
			return "";
		}

		public static String OutputError(string Message)
		{
			StackFrame CallStack = new StackFrame(1, true);

			String SourceFile = CallStack.GetFileName();
			int SourceLine = CallStack.GetFileLineNumber();
			String errMsg = "Error: " + Message + " - File: " + SourceFile + " Line: " + SourceLine.ToString();
			return errMsg;
		}

		public static void Run_Sakura(String path)
		{
			if (File.Exists(path) && Lib.textfile.Contains(Path.GetExtension(path.ToLower())))
			{
				Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", path);
			}
		}

		public static void Run_PSPad(String path)
		{
			if (File.Exists(path) && Lib.textfile.Contains(Path.GetExtension(path.ToLower())))
			{
				Process.Start("F:\\Programs\\PSPad editor\\PSPad.exe", path);
			}
		}

		public static void Run_Explorer(String path)
		{
			if (System.IO.Directory.Exists(path))
			{
				Process.Start(path);
			}
			else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
			{
				Process.Start(Path.GetDirectoryName(path));
			}
		}

		public static void Run_Cmd(String path)
		{
			if (System.IO.Directory.Exists(path))
			{
				System.IO.Directory.SetCurrentDirectory(path);
				Process.Start(@"C:\windows\system32\cmd.exe");
			}
			else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
			{
				System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
				Process.Start(@"C:\windows\system32\cmd.exe");
			}
		}

		public static String Run_Chrome(String path)
		{
			String fileext = Path.GetExtension(path).ToLower();
			String filedir = Path.GetDirectoryName(path).ToLower();
			String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
			String swffile = filedir + "\\" + filebody + ".swf";
			if (File.Exists(swffile))
			{
				try
				{
					if (System.IO.Directory.Exists(filedir))
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						return getStandardOutput(@"C:\Program Files\Google\Chrome\Application\chrome.exe"
							, Lib.Path2Url(swffile));
					}
				}
				catch (System.Exception exc)
				{
					return exc.Message.ToString();
				}
			}
			return "";
		}
	
		//////////////////////////////////////////////////////////////////////////
		// 外部プロセス(アプリケーション) 起動関数
		public static void Run_Process(String appname, String path)
		{
			if (!System.IO.File.Exists(path)) return;

			String AppPath = "";

			if (!File.Exists(path)) return;

			switch (appname.ToLower())
			{
				case "sakura":
					AppPath = @"C:\TiuDevTools\sakura\sakura.exe";
					Process.Start(AppPath, path);
					break;
				case "pspad":
					AppPath = @"F:\Programs\PSPad editor\PSPad.exe";
					Process.Start(AppPath, path);
					break;
				case "explorer":
					if (System.IO.Directory.Exists(path))
					{
						Process.Start(path);
					}
					else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
					{
						Process.Start(Path.GetDirectoryName(path));
					}
					break;
				case "cmd":
					if (System.IO.Directory.Exists(path))
					{
						System.IO.Directory.SetCurrentDirectory(path);
						Process.Start(@"C:\windows\system32\cmd.exe");
					}
					else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
					{
						System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
						Process.Start(@"C:\windows\system32\cmd.exe");
					}
					break;
				default:
					Process.Start(AppPath, path);
					break;
			}
		}

		// Applicationの実行
		public static String Run_Application(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String fileext = Path.GetExtension(path).ToLower();
					String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
					String filedir = Path.GetDirectoryName(path).ToLower();
					String exefile = filebody + ".exe";

					String[] tmpary = Path.GetDirectoryName(path).Split('\\');
					String dirbody = tmpary[tmpary.Length - 1];
					String exefile2 = dirbody + ".exe";
					String projectfile = GetProjectFile(path).ToLower();
					String exefile3 = "";
					if (projectfile != "")
					{
						exefile3 = Path.GetFileNameWithoutExtension(projectfile) + ".exe";
					}
					String exepath = "";
					switch (fileext)
					{
						case ".c":
						case ".cpp":
						case ".cs":
							if (File.Exists(filedir + "\\" + exefile))
							{
								exepath = filedir + "\\" + exefile;
							}
							if (File.Exists(filedir + "\\" + exefile2))
							{
								exepath = filedir + "\\" + exefile2;
							}
							if (File.Exists(filedir + "\\" + exefile3))
							{
								exepath = filedir + "\\" + exefile3;
							}
							else if (File.Exists(filedir + "\\Debug\\" + exefile))
							{
								exepath = filedir + "\\Debug\\" + "\\" + exefile;
							}
							else if (File.Exists(filedir + "\\Debug\\" + exefile2))
							{
								exepath = filedir + "\\Debug\\" + "\\" + exefile2;
							}
							else if (File.Exists(filedir + "\\Debug\\" + exefile3))
							{
								exepath = filedir + "\\Debug\\" + "\\" + exefile3;
							}
							else if (File.Exists(filedir + "\\bin\\Release\\" + exefile))
							{
								exepath = filedir + "\\bin\\Release\\" + "\\" + exefile;
							}
							else if (File.Exists(filedir + "\\bin\\Release\\" + exefile2))
							{
								exepath = filedir + "\\bin\\Release\\" + "\\" + exefile2;
							}
							else if (File.Exists(filedir + "\\bin\\Release\\" + exefile3))
							{
								exepath = filedir + "\\bin\\Release\\" + "\\" + exefile3;
							}
							if (exefile != "")
							{
								return getStandardOutput(exepath, "");
							}
							break;
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Run_Script(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String fileext = Path.GetExtension(path).ToLower();
					String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
					String filedir = Path.GetDirectoryName(path).ToLower();
					String cmd = "";
					String option = "";
					//Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", this.filepath);
					switch (fileext)
					{
						case ".c":
						case ".cpp":
							cmd = "C:\\cygwin\\usr\\local\\cint\\cint.exe";
							break;
						case ".cs":
							cmd = "F:\\Programs\\csscript\\cscs.exe";
							break;
						case ".js":
						case ".vbs":
						case ".wsf":
							cmd = "cscript";
							option = "/nologo ";
							break;
						case ".tcl":
							cmd = "C:\\Tcl\\bin\\wish86.exe";
							break;
						case ".pl":
							cmd = "C:\\cygwin\\usr\\bin\\perl.exe";
							break;
						case ".hta":
							cmd = "mshta";
							break;
						case ".ns":
							cmd = @"F:\c_program\nakka.com\ns003src_double_math\ns.exe";
							break;
						//case ".js":
						//	break;
						case ".php":
							cmd = "php";
							if (System.IO.Directory.Exists(filedir))
							{
								System.IO.Directory.SetCurrentDirectory(filedir);
							}
							break;
					}
					if (cmd != "")
					{
						return getStandardOutput(cmd, option + " " + path);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Run_Java(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
					String filedir = Path.GetDirectoryName(path).ToLower();
					if (File.Exists(filedir + "\\" + filebody + ".class"))
					{
						if (System.IO.Directory.Exists(filedir))
						{
							System.IO.Directory.SetCurrentDirectory(filedir);
							return getStandardOutput("java", filebody);
						}
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Run_Applet(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					//String fileext = Path.GetExtension(path).ToLower();
					String javafile = Path.GetFileName(path).ToLower();
					String filedir = Path.GetDirectoryName(path).ToLower();
					if (System.IO.Directory.Exists(filedir))
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						return getStandardOutput("appletviewer", javafile);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Run_Flex4(String path)
		{
			return null;
		}

		public static String Run_DviOut(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String fileext = Path.GetExtension(path).ToLower();
					String filedir = Path.GetDirectoryName(path).ToLower();
					String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
					String dvifile = filedir + "\\" + filebody + ".dvi";
					if (System.IO.Directory.Exists(filedir) && System.IO.File.Exists(dvifile))
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						return getStandardOutput("C:\\tex\\dviout\\dviout.exe", dvifile);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}



		public static String Run_FlashPlayer9(String path)
		{
			String fileext = Path.GetExtension(path).ToLower();
			String filedir = Path.GetDirectoryName(path).ToLower();
			String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
			String swffile = filedir + "\\" + filebody + ".swf";
			if (File.Exists(swffile))
			{
				try
				{
					if (System.IO.Directory.Exists(filedir))
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						Process.Start(swffile);
						return "";
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static void Run_SystemProcess(String path)
		{
			if (File.Exists(path))
			{
				Process.Start(path);
			}
		}
    /*
		public static String Exec_CSharpCodeProvider01(object frm, String path)
		{
			//計算するためのコード
			String source = Lib.File_ReadToEnd(path);
			String result = "";

			//コンパイルするための準備
			CodeDomProvider cp = new Microsoft.CSharp.CSharpCodeProvider();
			//ICodeCompiler icc = cp.CreateCompiler();
			CompilerParameters cps = new CompilerParameters();

			cps.ReferencedAssemblies.Add("System.dll");
			cps.ReferencedAssemblies.Add("System.Deployment.dll");
			cps.ReferencedAssemblies.Add("System.Data.dll");
			cps.ReferencedAssemblies.Add("System.Drawing.dll");
			cps.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			cps.ReferencedAssemblies.Add("System.Xml.dll");
			cps.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "DockableControls\\azuki.dll"));
			cps.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "DockableControls\\AzukiEditor.dll"));
			cps.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "DockableControls\\PluginCore.dll"));
			cps.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "FlashDevelop.exe"));
			CompilerResults cres = null;
			//メモリ内で出力を生成する
			cps.GenerateInMemory = true;
			try
			{
				//コンパイルする
				cres = cp.CompileAssemblyFromSource(cps, source);
				//コンパイルしたアセンブリを取得
				Assembly asm = cres.CompiledAssembly;
				//MainClassクラスのTypeを取得
				Type t = asm.GetType("CSScript");
				//EValメソッドを実行し、結果を取得
				string d = (string)t.InvokeMember(
				"main", BindingFlags.InvokeMethod, null, null,
					//new object[] { form.azukiForm01 });
				new object[] { frm });

				//結果を表示
				result += String.Format("{0}", d);
				return result;
			}
			catch (System.Exception exc)
			{
				result = "[Compile Error]\n";
				result += exc.Message.ToString();		//statusBarにエラーを表示
				for (int i = 0; i < cres.Errors.Count; i++)
					result += cres.Errors[i].ToString() + "\n";
			}
			return result;
		}
     * */
		/*
			//http://dobon.net/vb/dotnet/programing/eval.html
			public static String Exec_JScriptCodeProvider01(String path)
			{
				//計算式
				String exp = "(1+6)*5/(7-4)";
				//計算するためのコード
				/ *
				string source =
						@"package Evaluator
				{
					class Evaluator
					{
						public function Eval(expr : String) : String
						{ 
							return eval(expr); 
						}
					}
				}";
				* /
				//"F:\\VCSharp\\MDI\\TreeData\\Evaluator.js"
				String source = FileReadToEnd(path);
				//コンパイルするための準備
				CodeDomProvider cp = new Microsoft.JScript.JScriptCodeProvider();
				//ICodeCompiler icc = cp.CreateCompiler();
				CompilerParameters cps = new CompilerParameters();
				CompilerResults cres = null;
				//メモリ内で出力を生成する
				cps.GenerateInMemory = true;
				try
				{
					//コンパイルする
					cres = cp.CompileAssemblyFromSource(cps, source);

					//コンパイルしたアセンブリを取得
					Assembly asm = cres.CompiledAssembly;
					//クラスのTypeを取得
					Type t = asm.GetType("MyClass.MyClass");
					//インスタンスの作成
					object eval = Activator.CreateInstance(t);
					//Evalメソッドを実行し、結果を取得
					String result = (string)t.InvokeMember("Eval",
						BindingFlags.InvokeMethod,
						null,
						eval,
					// koko
					new object[] { exp });
					//結果を表示
					return String.Format("{0}", result);
				}
				catch (System.Exception exc)
				{
					String output = "[Compile Error]\n";
					//String s = exc.Message.ToString();
					//MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					output += exc.Message.ToString();		//statusBarにエラーを表示
					for (int i = 0; i < cres.Errors.Count; i++)
						output += cres.Errors[i].ToString() + "\n";
					return output;
				}
			}
			*/
	
		/*
		public static String Exec_Application(String path)
		{
					//String path = this.GetActiveFilePath();
					try
					{
						String fileext = Path.GetExtension(path).ToLower();
						String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
						String filedir = Path.GetDirectoryName(path).ToLower();
						String exefile = filebody + ".exe";

						String[] tmpary = Path.GetDirectoryName(path).Split('\\');
						String dirbody = tmpary[tmpary.Length - 1];
						String exefile2 = dirbody + ".exe";
						String exepath = "";
						//Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", this.filepath);
						switch (fileext)
						{
							case ".c":
							case ".cpp":
							case ".cs":
								if (File.Exists(filedir + "\\" + exefile))
								{
									exepath = filedir + "\\" + exefile;
								}
								if (File.Exists(filedir + "\\" + exefile2))
								{
									exepath = filedir + "\\" + exefile2;
								}
								else if (File.Exists(filedir + "\\Debug\\" + exefile))
								{
									exepath = filedir + "\\Debug\\" + "\\" + exefile;
								}
								else if (File.Exists(filedir + "\\Debug\\" + exefile2))
								{
									exepath = filedir + "\\Debug\\" + "\\" + exefile2;
								}
								else if (File.Exists(filedir + "\\bin\\Release\\" + exefile))
								{
									exepath = filedir + "\\bin\\Release\\" + "\\" + exefile;
								}
								else if (File.Exists(filedir + "\\bin\\Release\\" + exefile2))
								{
									exepath = filedir + "\\bin\\Release\\" + "\\" + exefile2;
								}
								if (exefile != "")
								{
									return Lib.getStandardOutput(exepath, "");
								}
								else
								{
									return String.Empty;
								}
						}
						return String.Empty;
					}
					catch (System.Exception exc)
					{
						String s = exc.Message.ToString();		//statusBarにエラーを表示
						MessageBox.Show(Lib.OutputError(s));
					}
					return String.Empty;
		}
		*/

		public static String Exec_Application(String path, Boolean conout, Boolean panelout)
		{
			//String path = this.GetActiveFilePath();
			try
			{
				String fileext = Path.GetExtension(path).ToLower();
				String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
				String filedir = Path.GetDirectoryName(path).ToLower();
				String exefile = filebody + ".exe";

				String[] tmpary = Path.GetDirectoryName(path).Split('\\');
				String dirbody = tmpary[tmpary.Length - 1];
				String exefile2 = dirbody + ".exe";
				String exepath = "";
				//Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", this.filepath);
				switch (fileext)
				{
					case ".c":
					case ".cpp":
					case ".cs":
						if (File.Exists(filedir + "\\" + exefile))
						{
							exepath = filedir + "\\" + exefile;
						}
						if (File.Exists(filedir + "\\" + exefile2))
						{
							exepath = filedir + "\\" + exefile2;
						}
						else if (File.Exists(filedir + "\\Debug\\" + exefile))
						{
							exepath = filedir + "\\Debug\\" + "\\" + exefile;
						}
						else if (File.Exists(filedir + "\\Debug\\" + exefile2))
						{
							exepath = filedir + "\\Debug\\" + "\\" + exefile2;
						}
						else if (File.Exists(filedir + "\\bin\\Release\\" + exefile))
						{
							exepath = filedir + "\\bin\\Release\\" + "\\" + exefile;
						}
						else if (File.Exists(filedir + "\\bin\\Release\\" + exefile2))
						{
							exepath = filedir + "\\bin\\Release\\" + "\\" + exefile2;
						}
						if (exefile != "")
						{
							if (conout == true)
							{
								System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(exepath));
								Process.Start(@"C:\windows\system32\cmd.exe", "/k, " + exepath);
							}
							if (panelout == true)
							{
								return getStandardOutput(exepath, "");
							}
							else
							{
								System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(exepath));
								Process.Start(exepath);
								return String.Empty;
							}
						}
						else
						{
							return String.Empty;
						}
				}
				return String.Empty;
			}
			catch (System.Exception exc)
			{
				String s = exc.Message.ToString();		//statusBarにエラーを表示
				MessageBox.Show(Lib.OutputError(s));
			}
			return String.Empty;
		}
		/*		
				public static String Exec_Script(String path)
				{
					//String path = this.GetActiveFilePath();
					if (File.Exists(path))
					{
						try
						{
							String fileext = Path.GetExtension(path).ToLower();
							String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
							String filedir = Path.GetDirectoryName(path).ToLower();
							String cmd = "";
							String option = "";
							//Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", this.filepath);
							switch (fileext)
							{
								case ".c":
								case ".cpp":
									cmd = "C:\\cygwin\\usr\\local\\cint\\cint.exe";
									break;
								case ".cs":
									cmd = "F:\\Programs\\csscript\\cscs.exe";
									break;
								case ".js":
								case ".vbs":
								case ".wsf":
									cmd = "cscript";
									option = "/nologo ";
									break;
								case ".tcl":
									cmd = "C:\\Tcl\\bin\\wish86.exe";
									break;
								case ".pl":
									cmd = "C:\\cygwin\\usr\\bin\\perl.exe";
									break;
								case ".hta":
									cmd = "mshta";
									break;
								//case ".js":
								//	break;
								case ".php":
									cmd = "php";
									if (System.IO.Directory.Exists(filedir))
									{
										System.IO.Directory.SetCurrentDirectory(filedir);
									}
									break;
							}
							if (cmd != "")
							{
								return Lib.getStandardOutput(cmd, option + " " + path);
							}
						}
						catch (System.Exception exc)
						{
							String s = exc.Message.ToString();		//statusBarにエラーを表示
							MessageBox.Show(Lib.OutputError(s));
						}
					}
					return String.Empty;
				}
		*/
		public static String Exec_Script(String path, Boolean conout, Boolean panelout)
		{
			//String path = this.GetActiveFilePath();
			if (File.Exists(path))
			{
				try
				{
					String fileext = Path.GetExtension(path).ToLower();
					String filebody = Path.GetFileNameWithoutExtension(path).ToLower();
					String filedir = Path.GetDirectoryName(path).ToLower();
					String cmd = "";
					String option = "";
					//Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", this.filepath);
					switch (fileext)
					{
						case ".c":
						case ".cpp":
							cmd = "C:\\cygwin\\usr\\local\\cint\\cint.exe";
							break;
						case ".cs":
							cmd = "F:\\Programs\\csscript\\cscs.exe";
							break;
						case ".js":
						case ".vbs":
						case ".wsf":
							cmd = "cscript";
							option = "/nologo ";
							break;
						case ".tcl":
							cmd = "C:\\Tcl\\bin\\wish86.exe";
							break;
						case ".pl":
							cmd = "C:\\cygwin\\usr\\bin\\perl.exe";
							break;
						case ".hta":
							cmd = "mshta";
							break;
						//case ".js":
						//	break;
						case ".php":
							cmd = "php";
							if (System.IO.Directory.Exists(filedir))
							{
								System.IO.Directory.SetCurrentDirectory(filedir);
							}
							break;
					}
					if (cmd != "")
					{
						if (conout == true)
						{
							System.IO.Directory.SetCurrentDirectory(filedir);
							Process.Start(@"C:\windows\system32\cmd.exe", "/k, " + cmd + " " + path);
						}
						if (panelout == true)
						{
							return getStandardOutput(cmd, option + " " + path);
						}
						else return String.Empty;
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();		//statusBarにエラーを表示
					MessageBox.Show(Lib.OutputError(s));
				}
			}
			return String.Empty;
		}

		//////////////////////////////////////////////////////////////////////////
		// 開発環境 Development Build Execute 操作
		// Buildの実行

		public static String Build_LateX(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String filedir = Path.GetDirectoryName(path);
					String filename = Path.GetFileName(path);
					String filebody = Path.GetFileNameWithoutExtension(path);
					String cmd = "platex.exe " + path + " & "
						+ "C:\\tex\\bin\\dvipdfmx.exe " + filebody;

					System.IO.Directory.SetCurrentDirectory(filedir);
					Process.Start("cmd.exe", "/k, " + cmd);
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Build_Java(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String filedir = Path.GetDirectoryName(path).ToLower();
					String filename = Path.GetFileName(path).ToLower();

					// TODO: utf8 Shift_JIS コンパイル選別 javac -encoding utf8 sample.java
					String arg = String.Empty;
					if (Lib.File_GetCode(path) == "UTF-8")
					{
						arg = "/k, javac -encoding utf8 ";
					}
					else
					{
						arg = "/k, javac ";
					}
					arg
						+= "-deprecation -classpath " + "\"" + filedir
						+ ";.;C:\\Program Files\\Apache Software Foundation\\Tomcat 6.0\\lib\\servlet-api.jar;C:\\Program Files\\Apache Software Foundation\\Tomcat 6.0\\lib\\jsp-api.jar;\" "
						+ filename;
					if (System.IO.Directory.Exists(filedir))
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						Process.Start("cmd.exe", arg);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Build_VC6(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String filedir = Path.GetDirectoryName(path).ToLower();
					String filename = Path.GetFileName(path).ToLower();
					String projectfile = ProcessHandler.GetProjectFile(path, ".dsp");
					String arg
						//= "/k, "
						= "/c, "
						+ "\"C:\\Program Files\\Microsoft Visual Studio\\VC98\\Bin\\vcvars32.bat\" & "
						+ "msdev " + projectfile + " /MAKE /REBUILD";
					if (System.IO.Directory.Exists(filedir) && projectfile != "")
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						return getStandardOutput("cmd", arg);
						//Process.Start("cmd.exe", arg);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Build_VC2008(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String filedir = Path.GetDirectoryName(path).ToLower();
					String filename = Path.GetFileName(path).ToLower();
					String projectfile = GetProjectFile(path, ".vcproj");
					//MessageBox.Show(projectfile);
					String arg
						= "/k, "
						//= "/c, "
						+ "\"C:\\Program Files\\Microsoft Visual Studio 9.0\\VC\\bin\\vcvars32.bat\" & "
						+ "\"C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCExpress.exe\" "
						+ projectfile + " /rebuild";
					if (System.IO.Directory.Exists(filedir) && projectfile != "")
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						//return getStandardOutput("cmd", arg);
						Process.Start("cmd.exe", arg);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Build_VCS2008(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String filedir = Path.GetDirectoryName(path).ToLower();
					String filename = Path.GetFileName(path).ToLower();
					String projectfile = GetProjectFile(path, ".csproj");
					String arg
						= "/k, "
						//= "/c, "
						+ "\"C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCSExpress.exe\" "
						+ projectfile + " /rebuild";
					if (System.IO.Directory.Exists(filedir) && projectfile != "")
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						//return getStandardOutput("cmd", arg);
						Process.Start("cmd.exe", arg);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Build_BatchFile(String path)
		{
			if (File.Exists(path))
			{
				try
				{
					String filedir = Path.GetDirectoryName(path).ToLower();
					String filename = Path.GetFileName(path).ToLower();
					String batchfile = filedir + "\\" + "build.bat";
					if (System.IO.File.Exists(batchfile))
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						//return getStandardOutput("cmd.exe", "/k, " + batchfile);
						Process.Start("cmd.exe", "/k, " + batchfile);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return "";
		}

		public static String Build_Flex4(String path)
		{
			String filedir = Path.GetDirectoryName(path);
			String filename = Path.GetFileName(path);
			String fileext = Path.GetExtension(path).ToLower();

			if (File.Exists(path))
			{
				try
				{
					String args = "/k, "
						+ "F:\\Flash\\flex4\\flex_sdk_4.1.0.16076\\bin\\mxmlc.exe "
						+ path;
					if (fileext == ".as" || fileext == ".mxml")
					{
						System.IO.Directory.SetCurrentDirectory(filedir);
						//return getStandardOutput("cmd.exe", "/k, " + batchfile);
						Process.Start("cmd.exe", args);
					}
				}
				catch (System.Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					return exc.Message.ToString();
				}
			}
			return filename + " のビルドに成功しました";
		}

		//////////////////////////////////////////////////////////////////////////
		// Thread 操作関数 検討要
		//外部プロセス処理スレッドの開始＆終了
		/*
			public static void ControlProcess(String program, String argument, bool start)
			{
				if (!start && extThread != null)
				{
					extThread.Abort();
					extThread = null;
				}
				else if (start && extThread == null)
				{
					extThread = new Thread(new ThreadStart(ProcessWorker));
					extThread.Start();
				}
			}
		*/
		//外部プロセスを起動するためのスレッド
		//	public static void ProcessWorker(String program, String argument)
		/*
			重い処理はスレッドを使う
			// 重い処理は「スレッド」にして実行する
		using System.Threading ;  // for Thread
		private void button1_Click(object sender, System.EventArgs e)
		{
			Thread thread_1 = new Thread(new ThreadStart(func_1)) ;  // スレッドの宣言
			thread_1.Start() ;                                       // スレッドの起動
		}
	
		private void func_1()
		{
			// スレッドの本体（重い処理）
		}
	
		// Note: スレッドの中で、Form を生成(new)してはいけない！
		//       たとえ生成しても、スレッド記述の文末で Form が消える。
		//       これは、Form が、生成されたスレッド内でしか有効でない仕様のため。
		// 参考: ms-help『 C# スレッド化チュートリアル 』	
		*/
		/*
		public static void ProcessWorker()
		{
			string program = "C:\\TiuDevTools\\sakura\\sakura.exe";
			string argument = form.filepath;

			//外部プロセスの起動
			try
			{
				extProcess = new Process();
				extProcess.StartInfo.FileName = program;//起動するファイル名
				extProcess.StartInfo.Arguments = argument;
				extProcess.Start();

				//スレッドが終了されるまで待機
				while (true)
				{
					Thread.Sleep(100);
				}
			}
			catch (ThreadAbortException ex)
			{
				MessageBox.Show(ex.Message, form.Text, 
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				if (extProcess != null)
					extProcess.Kill();	//外部プロセスの終了（同時にこのスレッドも終了される）
				extThread.Abort();
				extThread = null;
			}
		}
		*/

    /*
		public void MakeShortCut()
		{
			//[C#]
			//作成するショートカットのパス
			string shortcutPath = System.IO.Path.Combine(
				Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory),
				@"MyApp.lnk");
			//ショートカットのリンク先
			string targetPath = Application.ExecutablePath;

			//WshShellを作成
			IWshRuntimeLibrary.WshShellClass shell = new IWshRuntimeLibrary.WshShellClass();
			//ショートカットのパスを指定して、WshShortcutを作成
			IWshRuntimeLibrary.IWshShortcut shortcut =
				(IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);
			//リンク先
			shortcut.TargetPath = targetPath;
			//コマンドパラメータ 「リンク先」の後ろに付く
			shortcut.Arguments = "/a /b /c";
			//作業フォルダ
			shortcut.WorkingDirectory = Application.StartupPath;
			//ショートカットキー（ホットキー）
			shortcut.Hotkey = "Ctrl+Alt+Shift+F12";
			//実行時の大きさ 1が通常、3が最大化、7が最小化
			shortcut.WindowStyle = 1;
			//コメント
			shortcut.Description = "テストのアプリケーション";
			//アイコンのパス 自分のEXEファイルのインデックス0のアイコン
			shortcut.IconLocation = Application.ExecutablePath + ",0";

			//ショートカットを作成
			shortcut.Save();

			//後始末
			System.Runtime.InteropServices.Marshal.ReleaseComObject(shortcut);
		}

     */
		/// <summary>
		///http://winofsql.jp/sh/html/dotnet_com.htm
		/// .NET で Windows Script Components を呼び出す( スタンバイ状態にする )
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void button1_Click(object sender, EventArgs e)
		{

			Type WshShell;
			WshShell = Type.GetTypeFromProgID("Wscript.Shell");
			Object obj = Activator.CreateInstance(WshShell);

			Object type = 1;
			Object wait = true;

			WshShell.InvokeMember(
					"Run",
					BindingFlags.InvokeMethod,
					null,
					obj,
					new Object[] { "notepad.exe", type, wait }
			);

			Type Standby;
			Standby = Type.GetTypeFromProgID("Lbox.Standby");
			Object obj2 = Activator.CreateInstance(Standby);

			Standby.InvokeMember(
					"Standby",
					BindingFlags.InvokeMethod,
					null,
					obj2,
					new Object[] { 1 }
			);

		}
    /*
		public static void button2_Click(object sender, EventArgs e)
		{
			IWshRuntimeLibrary.WshShell obj = new IWshRuntimeLibrary.WshShell();
			Object type = 1;
			Object wait = true;
			obj.Run("notepad.exe", ref type, ref wait);

		}
    */
		/// <summary>
		/// C#でCreateObjectと同じことをするには？
		/// http://dobon.net/vb/dotnet/vb2cs/createobject.html
		/// COMオブジェクトへの参照を作成および取得する
		/// ただし、C#ではVB.NETと違い、暗黙の遅延バインディングができませんので、
		///		取得したCOMオブジェクトのプロパティやメソッドを呼び出すには、
		///		Type.InvokeMemberメソッドを使用する必要があります。
		/// </summary>
		/// <param name="progId">作成するオブジェクトのプログラムID</param>
		/// <param name="serverName">
		/// オブジェクトが作成されるネットワーク サーバーの名前
		/// </param>
		/// <returns>作成されたCOMオブジェクト</returns>
		public static object CreateObject(string progId, string serverName)
		{
			Type t;
			if (serverName == null || serverName.Length == 0)
				t = Type.GetTypeFromProgID(progId);
			else
				t = Type.GetTypeFromProgID(progId, serverName, true);
			return Activator.CreateInstance(t);
		}

		/// <summary>
		/// COMオブジェクトへの参照を作成および取得する
		/// </summary>
		/// <param name="progId">作成するオブジェクトのプログラムID</param>
		/// <returns>作成されたCOMオブジェクト</returns>
		public static object CreateObject(string progId)
		{
			return CreateObject(progId, null);
		}
	
	
	}
}
