using Microsoft.CSharp;
using PluginCore.Helpers;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AntPlugin.CommonLibrary
{
	public class ProcessHandler
	{
		public static string getStandardOutput(string command, string arguments)
		{
			Process process = Process.Start(new ProcessStartInfo
			{
				FileName = command,
				Arguments = arguments,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true
			});
			string text = process.StandardOutput.ReadToEnd();
			return text.Replace("\r\r\n", "\n");
		}

    public static string getStandardOutput(string command, string arguments, String encoding)
    {
      Process process = Process.Start(new ProcessStartInfo
      {
        FileName = command,
        Arguments = arguments,
        CreateNoWindow = true,
        UseShellExecute = false,
        RedirectStandardOutput = true,
        StandardOutputEncoding = System.Text.Encoding.GetEncoding(encoding)
      });
      string text = process.StandardOutput.ReadToEnd();
      return text.Replace("\r\r\n", "\n");
    }

    public static string GetProjectFile(string path, string ext)
		{
			string directoryName = Path.GetDirectoryName(path);
			string[] files = Directory.GetFiles(directoryName, "*");
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (Path.GetExtension(text).ToLower() == ext)
				{
					return text;
				}
			}
			return "";
		}

		public static string GetProjectFile(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			string[] files = Directory.GetFiles(directoryName, "*");
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (Path.GetExtension(text).ToLower() == ".vcproj" || Path.GetExtension(text).ToLower() == ".dsp" || Path.GetExtension(text).ToLower() == ".csproj" || Path.GetFileName(text).ToLower() == "makefile")
				{
					return text;
				}
			}
			return "";
		}

		public static string OutputError(string Message)
		{
			StackFrame stackFrame = new StackFrame(1, true);
			string fileName = stackFrame.GetFileName();
			return string.Concat(new string[]
			{
				"Error: ",
				Message,
				" - File: ",
				fileName,
				" Line: ",
				stackFrame.GetFileLineNumber().ToString()
			});
		}
		
    public static void Run_Sakura(string path)
		{
			if (File.Exists(path) && Lib.textfile.Contains(Path.GetExtension(path.ToLower())))
			{

        if (File.Exists("C:\\Program Files (x86)\\sakura\\sakura.exe"))
        {
          Process.Start("C:\\Program Files (x86)\\sakura\\sakura.exe", path);
        }
        else if (File.Exists("C:\\TiuDevTools\\sakura\\sakura.exe"))
				{
					Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", path);
					return;
				}
			}
		}
    
		public static void Run_PSPad(string path)
		{
			if (File.Exists(path) && Lib.textfile.Contains(Path.GetExtension(path.ToLower())))
			{
				if (File.Exists("F:\\Programs\\PSPad editor\\PSPad.exe"))
				{
					Process.Start("F:\\Programs\\PSPad editor\\PSPad.exe", path);
					return;
				}
				if (File.Exists("C:\\Program Files (x86)\\PSPad editor\\PSPad.exe"))
				{
					Process.Start("C:\\Program Files (x86)\\PSPad editor\\PSPad.exe", path);
				}
			}
		}

		public static void Run_Explorer(string path)
		{
			if (Directory.Exists(path))
			{
				Process.Start(path);
				return;
			}
			if (Directory.Exists(Path.GetDirectoryName(path)))
			{
				Process.Start(Path.GetDirectoryName(path));
			}
		}

		public static void Run_Cmd(string path)
		{
			if (Directory.Exists(path))
			{
				Directory.SetCurrentDirectory(path);
				Process.Start("C:\\windows\\system32\\cmd.exe");
				return;
			}
			if (Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
				Process.Start("C:\\windows\\system32\\cmd.exe");
			}
		}

    public static string Run_Chrome(string path)
		{
			Path.GetExtension(path).ToLower();
			string text = Path.GetDirectoryName(path).ToLower();
			string str = Path.GetFileNameWithoutExtension(path).ToLower();
			string path2 = text + "\\" + str + ".swf";
			if (File.Exists(path2))
			{
				try
				{
					if (Directory.Exists(text))
					{
						Directory.SetCurrentDirectory(text);
						if (File.Exists("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"))
						{
							//string result = ProcessHandler.getStandardOutput("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", Lib.Path2Url(path2));
              string result = ProcessHandler.getStandardOutput("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", path2);
              return result;
						}
						if (File.Exists("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"))
						{
							//string result = ProcessHandler.getStandardOutput("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", Lib.Path2Url(path2));
              string result = ProcessHandler.getStandardOutput("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", path2);
              return result;
						}
					}
				}
				catch (Exception ex)
				{
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}

		public static void Run_Process(string appname, string path)
		{
			if (!File.Exists(path))
			{
				return;
			}
			string fileName = "";
			if (!File.Exists(path))
			{
				return;
			}
			string a;
			if ((a = appname.ToLower()) != null)
			{
				if (a == "sakura")
				{
					fileName = "C:\\TiuDevTools\\sakura\\sakura.exe";
					Process.Start(fileName, path);
					return;
				}
				if (a == "pspad")
				{
					fileName = "F:\\Programs\\PSPad editor\\PSPad.exe";
					Process.Start(fileName, path);
					return;
				}
				if (!(a == "explorer"))
				{
					if (a == "cmd")
					{
						if (Directory.Exists(path))
						{
							Directory.SetCurrentDirectory(path);
							Process.Start("C:\\windows\\system32\\cmd.exe");
							return;
						}
						if (Directory.Exists(Path.GetDirectoryName(path)))
						{
							Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
							Process.Start("C:\\windows\\system32\\cmd.exe");
							return;
						}
						return;
					}
				}
				else
				{
					if (Directory.Exists(path))
					{
						Process.Start(path);
						return;
					}
					if (Directory.Exists(Path.GetDirectoryName(path)))
					{
						Process.Start(Path.GetDirectoryName(path));
						return;
					}
					return;
				}
			}
			Process.Start(fileName, path);
		}
/*
		public static string Run_Application(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetExtension(path).ToLower();
					string str = Path.GetFileNameWithoutExtension(path).ToLower();
					string str2 = Path.GetDirectoryName(path).ToLower();
					string text2 = str + ".exe";
					string[] array = Path.GetDirectoryName(path).Split(new char[]
					{
						'\\'
					});
					string str3 = array[array.Length - 1];
					string str4 = str3 + ".exe";
					string text3 = ProcessHandler.GetProjectFile(path).ToLower();
					string str5 = "";
					if (text3 != "")
					{
						str5 = Path.GetFileNameWithoutExtension(text3) + ".exe";
					}
					string command = "";
					string a;
					if ((a = text) != null && (a == ".c" || a == ".cpp" || a == ".cs"))
					{
						if (File.Exists(str2 + "\\" + text2))
						{
							command = str2 + "\\" + text2;
						}
						if (File.Exists(str2 + "\\" + str4))
						{
							command = str2 + "\\" + str4;
						}
						if (File.Exists(str2 + "\\" + str5))
						{
							command = str2 + "\\" + str5;
						}
						else if (File.Exists(str2 + "\\Debug\\" + text2))
						{
							command = str2 + "\\Debug\\\\" + text2;
						}
						else if (File.Exists(str2 + "\\Debug\\" + str4))
						{
							command = str2 + "\\Debug\\\\" + str4;
						}
						else if (File.Exists(str2 + "\\Debug\\" + str5))
						{
							command = str2 + "\\Debug\\\\" + str5;
						}
						else if (File.Exists(str2 + "\\bin\\Release\\" + text2))
						{
							command = str2 + "\\bin\\Release\\\\" + text2;
						}
						else if (File.Exists(str2 + "\\bin\\Release\\" + str4))
						{
							command = str2 + "\\bin\\Release\\\\" + str4;
						}
						else if (File.Exists(str2 + "\\bin\\Release\\" + str5))
						{
							command = str2 + "\\bin\\Release\\\\" + str5;
						}
						if (text2 != "")
						{
							string result = ProcessHandler.getStandardOutput(command, "");
							return result;
						}
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}
*/
		
    public static string Run_Script(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetExtension(path).ToLower();
					Path.GetFileNameWithoutExtension(path).ToLower();
					string text2 = Path.GetDirectoryName(path).ToLower();
					string text3 = "";
					string str = "";
					string key;
					switch (key = text)
					{
					case ".c":
					case ".cpp":
						text3 = "C:\\cygwin\\usr\\local\\cint\\cint.exe";
						break;
					case ".cs":
						text3 = "F:\\Programs\\csscript\\cscs.exe";
						break;
					case ".js":
					case ".vbs":
					case ".wsf":
						text3 = "cscript";
						str = "/nologo ";
						break;
					case ".tcl":
						text3 = "C:\\Tcl\\bin\\wish86.exe";
						break;
					case ".pl":
						text3 = "C:\\cygwin\\usr\\bin\\perl.exe";
						break;
					case ".hta":
						text3 = "mshta";
						break;
					case ".ns":
						text3 = "F:\\c_program\\nakka.com\\ns003src_double_math\\ns.exe";
						break;
					case ".php":
						text3 = "php";
						if (Directory.Exists(text2))
						{
							Directory.SetCurrentDirectory(text2);
						}
						break;
					}
					if (text3 != "")
					{
						string result = ProcessHandler.getStandardOutput(text3, str + " " + path);
						return result;
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}

		public static string Run_Java(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetFileNameWithoutExtension(path).ToLower();
					string text2 = Path.GetDirectoryName(path).ToLower();
					if (File.Exists(text2 + "\\" + text + ".class") && Directory.Exists(text2))
					{
						Directory.SetCurrentDirectory(text2);
						string result = ProcessHandler.getStandardOutput("java", text);
						return result;
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}

		public static string Run_Applet(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string arguments = Path.GetFileName(path).ToLower();
					string text = Path.GetDirectoryName(path).ToLower();
					if (Directory.Exists(text))
					{
						Directory.SetCurrentDirectory(text);
						string result = ProcessHandler.getStandardOutput("appletviewer", arguments);
						return result;
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}

		public static string Run_Flex4(string path)
		{
			return null;
		}

		public static string Run_DviOut(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					Path.GetExtension(path).ToLower();
					string text = Path.GetDirectoryName(path).ToLower();
					string str = Path.GetFileNameWithoutExtension(path).ToLower();
					string text2 = text + "\\" + str + ".dvi";
					if (Directory.Exists(text) && File.Exists(text2))
					{
						Directory.SetCurrentDirectory(text);
						string result = ProcessHandler.getStandardOutput("C:\\tex\\dviout\\dviout.exe", text2);
						return result;
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}

		public static string Run_FlashPlayer9(string path)
		{
			Path.GetExtension(path).ToLower();
			string text = Path.GetDirectoryName(path).ToLower();
			string str = Path.GetFileNameWithoutExtension(path).ToLower();
			string text2 = text + "\\" + str + ".swf";
			if (File.Exists(text2))
			{
				try
				{
					if (Directory.Exists(text))
					{
						Directory.SetCurrentDirectory(text);
						Process.Start(text2);
						string result = "";
						return result;
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}

		public static void Run_SystemProcess(string path)
		{
			if (File.Exists(path))
			{
				Process.Start(path);
			}
		}

		public static string Exec_CSharpCodeProvider01(object frm, string path)
		{
			string text = Lib.File_ReadToEnd(path);
			string text2 = "";
			CodeDomProvider codeDomProvider = new CSharpCodeProvider();
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.ReferencedAssemblies.Add("System.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Deployment.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Data.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
			compilerParameters.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "DockableControls\\azuki.dll"));
			compilerParameters.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "DockableControls\\AzukiEditor.dll"));
			compilerParameters.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "DockableControls\\PluginCore.dll"));
			compilerParameters.ReferencedAssemblies.Add(Path.Combine(PathHelper.AppDir, "FlashDevelop.exe"));
			CompilerResults compilerResults = null;
			compilerParameters.GenerateInMemory = true;
			try
			{
				compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, new string[]
				{
					text
				});
				Assembly compiledAssembly = compilerResults.CompiledAssembly;
				Type type = compiledAssembly.GetType("CSScript");
				string arg = (string)type.InvokeMember("main", BindingFlags.InvokeMethod, null, null, new object[]
				{
					frm
				});
				text2 += string.Format("{0}", arg);
				return text2;
			}
			catch (Exception ex)
			{
				text2 = "[Compile Error]\n";
				text2 += ex.Message.ToString();
				for (int i = 0; i < compilerResults.Errors.Count; i++)
				{
					text2 = text2 + compilerResults.Errors[i].ToString() + "\n";
				}
			}
			return text2;
		}

		public static string Exec_Application(string path, bool conout, bool panelout)
		{
			try
			{
				string text = Path.GetExtension(path).ToLower();
				string str = Path.GetFileNameWithoutExtension(path).ToLower();
				string str2 = Path.GetDirectoryName(path).ToLower();
				string text2 = str + ".exe";
				string[] array = Path.GetDirectoryName(path).Split(new char[]
				{
					'\\'
				});
				string str3 = array[array.Length - 1];
				string str4 = str3 + ".exe";
				string text3 = "";
				string a;
				string result;
				if ((a = text) == null || (!(a == ".c") && !(a == ".cpp") && !(a == ".cs")))
				{
					result = string.Empty;
					return result;
				}
				if (File.Exists(str2 + "\\" + text2))
				{
					text3 = str2 + "\\" + text2;
				}
				if (File.Exists(str2 + "\\" + str4))
				{
					text3 = str2 + "\\" + str4;
				}
				else if (File.Exists(str2 + "\\Debug\\" + text2))
				{
					text3 = str2 + "\\Debug\\\\" + text2;
				}
				else if (File.Exists(str2 + "\\Debug\\" + str4))
				{
					text3 = str2 + "\\Debug\\\\" + str4;
				}
				else if (File.Exists(str2 + "\\bin\\Release\\" + text2))
				{
					text3 = str2 + "\\bin\\Release\\\\" + text2;
				}
				else if (File.Exists(str2 + "\\bin\\Release\\" + str4))
				{
					text3 = str2 + "\\bin\\Release\\\\" + str4;
				}
				if (!(text2 != ""))
				{
					result = string.Empty;
					return result;
				}
				if (conout)
				{
					Directory.SetCurrentDirectory(Path.GetDirectoryName(text3));
					Process.Start("C:\\windows\\system32\\cmd.exe", "/k, " + text3);
				}
				if (panelout)
				{
					result = ProcessHandler.getStandardOutput(text3, "");
					return result;
				}
				Directory.SetCurrentDirectory(Path.GetDirectoryName(text3));
				Process.Start(text3);
				result = string.Empty;
				return result;
			}
			catch (Exception ex)
			{
				string message = ex.Message.ToString();
				MessageBox.Show(Lib.OutputError(message));
			}
			return string.Empty;
		}

		public static string Exec_Script(string path, bool conout, bool panelout)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetExtension(path).ToLower();
					Path.GetFileNameWithoutExtension(path).ToLower();
					string text2 = Path.GetDirectoryName(path).ToLower();
					string text3 = "";
					string str = "";
					string key;
					switch (key = text)
					{
					case ".c":
					case ".cpp":
						text3 = "C:\\cygwin\\usr\\local\\cint\\cint.exe";
						break;
					case ".cs":
						text3 = "F:\\Programs\\csscript\\cscs.exe";
						break;
					case ".js":
					case ".vbs":
					case ".wsf":
						text3 = "cscript";
						str = "/nologo ";
						break;
					case ".tcl":
						text3 = "C:\\Tcl\\bin\\wish86.exe";
						break;
					case ".pl":
						text3 = "C:\\cygwin\\usr\\bin\\perl.exe";
						break;
					case ".hta":
						text3 = "mshta";
						break;
					case ".php":
						text3 = "php";
						if (Directory.Exists(text2))
						{
							Directory.SetCurrentDirectory(text2);
						}
						break;
					}
					if (text3 != "")
					{
						if (conout)
						{
							Directory.SetCurrentDirectory(text2);
							Process.Start("C:\\windows\\system32\\cmd.exe", "/k, " + text3 + " " + path);
						}
						string result;
						if (panelout)
						{
							result = ProcessHandler.getStandardOutput(text3, str + " " + path);
							return result;
						}
						result = string.Empty;
						return result;
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(Lib.OutputError(message));
				}
			}
			return string.Empty;
		}

		public static string Build_LateX(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string directoryName = Path.GetDirectoryName(path);
					Path.GetFileName(path);
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
					string str = "platex.exe " + path + " & C:\\tex\\bin\\dvipdfmx.exe " + fileNameWithoutExtension;
					Directory.SetCurrentDirectory(directoryName);
					Process.Start("cmd.exe", "/k, " + str);
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					return ex.Message.ToString();
				}
			}
			return "";
		}

		public static string Build_Java(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetDirectoryName(path).ToLower();
					string text2 = Path.GetFileName(path).ToLower();
					string text3 = string.Empty;
					if (Lib.File_GetCode(path) == "UTF-8")
					{
						text3 = "/k, javac -encoding utf8 ";
					}
					else
					{
						text3 = "/k, javac ";
					}
					string text4 = text3;
					text3 = string.Concat(new string[]
					{
						text4,
						"-deprecation -classpath \"",
						text,
						";.;C:\\Program Files\\Apache Software Foundation\\Tomcat 6.0\\lib\\servlet-api.jar;C:\\Program Files\\Apache Software Foundation\\Tomcat 6.0\\lib\\jsp-api.jar;\" ",
						text2
					});
					if (Directory.Exists(text))
					{
						Directory.SetCurrentDirectory(text);
						Process.Start("cmd.exe", text3);
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					return ex.Message.ToString();
				}
			}
			return "";
		}

		public static string Build_VC6(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetDirectoryName(path).ToLower();
					Path.GetFileName(path).ToLower();
					string projectFile = ProcessHandler.GetProjectFile(path, ".dsp");
					string arguments = "/c, \"C:\\Program Files\\Microsoft Visual Studio\\VC98\\Bin\\vcvars32.bat\" & msdev " + projectFile + " /MAKE /REBUILD";
					if (Directory.Exists(text) && projectFile != "")
					{
						Directory.SetCurrentDirectory(text);
						string result = ProcessHandler.getStandardOutput("cmd", arguments);
						return result;
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					string result = ex.Message.ToString();
					return result;
				}
			}
			return "";
		}

		public static string Build_VC2008(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetDirectoryName(path).ToLower();
					Path.GetFileName(path).ToLower();
					string projectFile = ProcessHandler.GetProjectFile(path, ".vcproj");
					string arguments = "/k, \"C:\\Program Files\\Microsoft Visual Studio 9.0\\VC\\bin\\vcvars32.bat\" & \"C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCExpress.exe\" " + projectFile + " /rebuild";
					if (Directory.Exists(text) && projectFile != "")
					{
						Directory.SetCurrentDirectory(text);
						Process.Start("cmd.exe", arguments);
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					return ex.Message.ToString();
				}
			}
			return "";
		}

		public static string Build_VCS2008(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetDirectoryName(path).ToLower();
					Path.GetFileName(path).ToLower();
					string projectFile = ProcessHandler.GetProjectFile(path, ".csproj");
					string arguments = "/k, \"C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCSExpress.exe\" " + projectFile + " /rebuild";
					if (Directory.Exists(text) && projectFile != "")
					{
						Directory.SetCurrentDirectory(text);
						Process.Start("cmd.exe", arguments);
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					return ex.Message.ToString();
				}
			}
			return "";
		}

		public static string Build_BatchFile(string path)
		{
			if (File.Exists(path))
			{
				try
				{
					string text = Path.GetDirectoryName(path).ToLower();
					Path.GetFileName(path).ToLower();
					string text2 = text + "\\build.bat";
					if (File.Exists(text2))
					{
						Directory.SetCurrentDirectory(text);
						Process.Start("cmd.exe", "/k, " + text2);
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					return ex.Message.ToString();
				}
			}
			return "";
		}

		public static string Build_Flex4(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			string fileName = Path.GetFileName(path);
			string a = Path.GetExtension(path).ToLower();
			if (File.Exists(path))
			{
				try
				{
					string arguments = "/k, F:\\Flash\\flex4\\flex_sdk_4.1.0.16076\\bin\\mxmlc.exe " + path;
					if (a == ".as" || a == ".mxml")
					{
						Directory.SetCurrentDirectory(directoryName);
						Process.Start("cmd.exe", arguments);
					}
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(ProcessHandler.OutputError(message));
					return ex.Message.ToString();
				}
			}
			return fileName + " のビルドに成功しました";
		}

		public static void button1_Click(object sender, EventArgs e)
		{
			Type typeFromProgID = Type.GetTypeFromProgID("Wscript.Shell");
			object target = Activator.CreateInstance(typeFromProgID);
			object obj = 1;
			object obj2 = true;
			typeFromProgID.InvokeMember("Run", BindingFlags.InvokeMethod, null, target, new object[]
			{
				"notepad.exe",
				obj,
				obj2
			});
			Type typeFromProgID2 = Type.GetTypeFromProgID("Lbox.Standby");
			object target2 = Activator.CreateInstance(typeFromProgID2);
			typeFromProgID2.InvokeMember("Standby", BindingFlags.InvokeMethod, null, target2, new object[]
			{
				1
			});
		}

		public static object CreateObject(string progId, string serverName)
		{
			Type typeFromProgID;
			if (serverName == null || serverName.Length == 0)
			{
				typeFromProgID = Type.GetTypeFromProgID(progId);
			}
			else
			{
				typeFromProgID = Type.GetTypeFromProgID(progId, serverName, true);
			}
			return Activator.CreateInstance(typeFromProgID);
		}

		public static object CreateObject(string progId)
		{
			return ProcessHandler.CreateObject(progId, null);
		}
	}
}
