using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using XMLTreeMenu;

namespace AntPlugin.CommonLibrary
{
	public class PSPadProject
	{
    public static String templateDir = @"F:\Programs\PSPad editor\Template_01";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basefilePath"></param>
    /// <param name="templatefile"></param>
    /// <param name="outputfile"></param>
    public static void make_from_template(string basefilePath, string templatefile, string outputfile)
		{
			string target = Path.GetFileNameWithoutExtension(basefilePath);
			//Path.GetExtension(basefile).Replace(".", "");
			string templatefilePath = Path.Combine(templateDir, templatefile);
			if (!File.Exists(templatefilePath))
			{
				return;
			}
			string content = Lib.File_ReadToEnd(templatefilePath);
			string outputPath = Path.Combine(Path.GetDirectoryName(basefilePath), outputfile);
			content = content.Replace("%%TARGET%%", target);
			content = content.Replace("%%FILENAME%%", Path.GetFileName(basefilePath));
			content = content.Replace("%%TIMESTAMP%%", StringHandler.timestamp());
			content = content.Replace("%%FILEDIR%%", Path.GetDirectoryName(basefilePath));
			content = content.Replace("%%DIR_NAME%%", Path.GetFileName(Path.GetDirectoryName(basefilePath)));
			content = content.Replace("%%PATH%%", basefilePath);
			//text2 = text2.Replace("%%LOCALDIR%%", directoryName2);
			StreamWriter streamWriter = new StreamWriter(outputPath, false, Encoding.UTF8);
			streamWriter.Write(content);
			streamWriter.Close();
		}

		public static void make_VC2008_CLR_ConsoleApplication_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\ConsoleTemplate01.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\build_msvc2008.bat", "build_msvc2008.bat");
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\RunCint.wsf", "RunCint.wsf");
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".vcproj"))
			{
				PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\ConsoleTemplate01.vcproj", fileNameWithoutExtension + ".vcproj");
				PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\ConsoleTemplate01.sln", fileNameWithoutExtension + ".sln");
			}
			if (!File.Exists(directoryName + "\\Makefile"))
			{
				PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\Makefile", "Makefile");
			}
			File.Copy(str + "Template_01\\VC2008CLRConsoleTemplate01\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy(str + "Template_01\\VC2008CLRConsoleTemplate01\\ConsoleTemplate01.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\doxdoc.hta", "doxdoc.hta");
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\make_doxydoc.bat", "make_doxydoc.bat");
			PSPadProject.make_from_template(filepath, "VC2008CLRConsoleTemplate01\\Doxyfile", "Doxyfile");
		}

		public static void make_VC2008_CLR_WindowsApplication_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\WindowsApplication.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\build_msvc2008.bat", "build_msvc2008.bat");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\RunApp.wsf", "RunApp.wsf");
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".vcproj"))
			{
				PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\WindowsApplication.vcproj", fileNameWithoutExtension + ".vcproj");
				PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\WindowsApplication.sln", fileNameWithoutExtension + ".sln");
			}
			if (!File.Exists(directoryName + "\\Makefile"))
			{
				PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\Makefile", "Makefile");
			}
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\doxdoc.hta", "doxdoc.hta");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\make_doxydoc.bat", "make_doxydoc.bat");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\Doxyfile", "Doxyfile");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\WindowsApplication.fdp", fileNameWithoutExtension + ".fdp");
			PSPadProject.make_from_template(filepath, "VC2008CLR_WindowsApplication01\\FDTreeMenu.xml", "FDTreeMenu.xml");
		}

		public static void make_VCS2008_ConsoleApplication_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\ConsoleApplication1.ppr", fileNameWithoutExtension + ".ppr");
			if (!File.Exists(directoryName + "\\build.bat"))
			{
				PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\build.bat", "build.bat");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".csproj"))
			{
				PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\ConsoleApplication1.csproj", fileNameWithoutExtension + ".csproj");
				PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\ConsoleApplication1.sln", fileNameWithoutExtension + ".sln");
			}
			PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\Makefile", "Makefile");
			PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "VCS2008_ConsoleApplication\\RunCsScript.wsf", "RunCsScript.wsf");
			File.Copy(str + "Template_01\\VCS2008_ConsoleApplication\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy(str + "Template_01\\VCS2008_ConsoleApplication\\ConsoleApplication1.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
		}

		public static void make_VCS2008_WindowsApplication_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\MainForm.ppr", fileNameWithoutExtension + ".ppr");
			if (!File.Exists(directoryName + "\\build.bat"))
			{
				PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\build.bat", "build.bat");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".csproj"))
			{
				PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\MainForm.csproj", fileNameWithoutExtension + ".csproj");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".sln"))
			{
				PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\MainForm.sln", fileNameWithoutExtension + ".sln");
			}
			PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\Makefile", "Makefile");
			PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "VCS2008_WindowsApplication\\RunWsScript.wsf", "RunWsScript.wsf");
			MessageBox.Show(str + "Template_01\\VCS2008_WindowsApplication\\FDTreeMenu.xml");
			MessageBox.Show(directoryName + "\\FDTreeMenu.xml");
		}

		public static void make_VC6_ConsoleApplication_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "VC6_ConsoleApplication01\\VC6_ConsoleApplication002.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "VC6_ConsoleApplication01\\VC6_build002.bat", "build_msvc6.bat");
			PSPadProject.make_from_template(filepath, "VC6_ConsoleApplication01\\VC2008_build002.bat", "build_msvc2008.bat");
			PSPadProject.make_from_template(filepath, "VC6_ConsoleApplication01\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "VC6_ConsoleApplication01\\RunCint.wsf", "RunCint.wsf");
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".dsp"))
			{
				PSPadProject.make_from_template(filepath, "VC6_ConsoleApplication01\\VC6_ConsoleApplication002.dsw", fileNameWithoutExtension + ".dsw");
				PSPadProject.make_from_template(filepath, "VC6_ConsoleApplication01\\VC6_ConsoleApplication002.dsp", fileNameWithoutExtension + ".dsp");
			}
			if (!File.Exists(directoryName + "\\Makefile"))
			{
				PSPadProject.make_from_template(filepath, "Makefile.gcc_console001", "Makefile");
			}
			PSPadProject.make_from_template(filepath, "main.fdp", fileNameWithoutExtension + ".fdp");
			File.Copy(str + "Template_01\\VC6_ConsoleApplication01\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
		}

		public static void make_VC6_WindowApplication_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\VC6_002.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\VC6_build002.bat", "build_msvc6.bat");
			PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\build_msvc2008.bat", "build_vc2008.bat");
			PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\Console_output001.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".dsp"))
			{
				PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\VC6_WindowApplication002.dsw", fileNameWithoutExtension + ".dsw");
				PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\VC6_WindowApplication002.dsp", fileNameWithoutExtension + ".dsp");
			}
			if (!File.Exists(directoryName + "\\Makefile"))
			{
				PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\Makefile_gcc_winexe_002", "Makefile");
			}
			PSPadProject.make_from_template(filepath, "VC6_WindowsApplication01\\main.fdp", fileNameWithoutExtension + ".fdp");
			File.Copy(str + "Template_01\\VC6_WindowsApplication01\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
		}

		public static void make_java_ConApp_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\j121b.ppr", fileNameWithoutExtension + ".ppr");
			if (!File.Exists(directoryName + "\\build.bat"))
			{
				PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\build.bat", "build.bat");
			}
			PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\Makefile", "Makefile");
			PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			File.Copy(str + "Template_01\\Java_ConsoleApplication01\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy(str + "Template_01\\Java_ConsoleApplication01\\j121b.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
			PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\Manifest.txt", "Manifest.txt");
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".java.php"))
			{
				PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\j121b.java.php", fileNameWithoutExtension + ".java.php");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".jnlp"))
			{
				PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\j121b.jnlp", fileNameWithoutExtension + ".jnlp");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + "_ui.tab.php"))
			{
				PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\j121b_ui.tab.php", fileNameWithoutExtension + "_ui.tab.php");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + "_view.html"))
			{
				PSPadProject.make_from_template(filepath, "Java_ConsoleApplication01\\j121b_view.html", fileNameWithoutExtension + "_view.html");
			}
		}

		public static void make_java_Applet_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\j343a.ppr", fileNameWithoutExtension + ".ppr");
			if (!File.Exists(directoryName + "\\build.bat"))
			{
				PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\build.bat", "build.bat");
			}
			PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\Makefile", "Makefile");
			PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\Manifest.txt", "Manifest.txt");
			File.Copy(str + "Template_01\\Java_AppletApplication01\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy(str + "Template_01\\Java_AppletApplication01\\j343a.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".htm"))
			{
				PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\j343a.htm", fileNameWithoutExtension + ".htm");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".html"))
			{
				PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\j343a.htm", fileNameWithoutExtension + ".html");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".java.html"))
			{
				PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\j343a.java.html", fileNameWithoutExtension + ".java.html");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".java.php"))
			{
				PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\j343a.java.php", fileNameWithoutExtension + ".java.php");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + "_ui.tab.php"))
			{
				PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\j343a_ui.tab.php", fileNameWithoutExtension + "_ui.tab.php");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + "_view.html"))
			{
				PSPadProject.make_from_template(filepath, "Java_AppletApplication01\\j343a_view.html", fileNameWithoutExtension + "_view.html");
			}
		}

		public static void make_java_GUIApp_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\s123.ppr", fileNameWithoutExtension + ".ppr");
			if (!File.Exists(directoryName + '\\' + "build.bat"))
			{
				PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\build.bat", "build.bat");
			}
			PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\Makefile", "Makefile");
			PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\Manifest.txt", "Manifest.txt");
			File.Copy(str + "Template_01\\Java_WindowsApplication01\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy(str + "Template_01\\Java_WindowsApplication01\\s123.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".jnlp"))
			{
				PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\s123.jnlp", fileNameWithoutExtension + ".jnlp");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".java.html"))
			{
				PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\s123.java.html", fileNameWithoutExtension + ".java.html");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".java.php"))
			{
				PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\s123.java.php", fileNameWithoutExtension + ".java.php");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + "_ui.tab.php"))
			{
				PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\s123_ui.tab.php", fileNameWithoutExtension + "_ui.tab.php");
			}
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + "_view.html"))
			{
				PSPadProject.make_from_template(filepath, "Java_WindowsApplication01\\s123_view.html", fileNameWithoutExtension + "_view.html");
			}
		}

		public static void make_Qt4_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "Qt443\\build.bat", "build.bat");
			PSPadProject.make_from_template(filepath, "Qt443\\menus.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "Qt443\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "Qt443\\RunApp.wsf", "RunApp.wsf");
			PSPadProject.make_from_template(filepath, "Qt443\\Doxyfile", "Doxyfile");
			if (!File.Exists(directoryName + "\\" + fileNameWithoutExtension + ".pro"))
			{
				PSPadProject.make_from_template(filepath, "Qt443\\menus.pro", fileNameWithoutExtension + ".pro");
			}
			File.Copy(str + "Template_01\\Qt443\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy(str + "Template_01\\Qt443\\menus.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
		}

		public static void make_svg_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "svg_002.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "svg_test001.html", fileNameWithoutExtension + ".html");
			PSPadProject.make_from_template(filepath, "svg_test001.php", fileNameWithoutExtension + ".php");
		}

		public static void make_appletviewer_wsf(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "Appletviewer.wsf", fileNameWithoutExtension + ".wsf");
			PSPadProject.make_from_template(filepath, "Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
		}

		public static void make_flex3_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			File.Copy("F:\\Programs\\PSPad editor\\Template_01\\Flex3.4\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy("F:\\Programs\\PSPad editor\\Template_01\\Flex3.4\\Accordion.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
			PSPadProject.make_from_template(filepath, "Flex3.4\\Accordion.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "Flex3.4\\Accordion-app.xml", fileNameWithoutExtension + "-app.xml");
			PSPadProject.make_from_template(filepath, "Flex3.4\\Accordion.as3proj", fileNameWithoutExtension + ".as3proj");
			PSPadProject.make_from_template(filepath, "Flex3.4\\LaunchChrome.wsf", "LaunchChrome.wsf");
			PSPadProject.make_from_template(filepath, "Flex3.4\\LaunchFireFox.wsf", "LaunchFireFox.wsf");
			PSPadProject.make_from_template(filepath, "Flex3.4\\LaunchIE.wsf", "LaunchIE.wsf");
			PSPadProject.make_from_template(filepath, "Flex3.4\\RunAir.wsf", "RunAir.wsf");
			PSPadProject.make_from_template(filepath, "Flex3.4\\RunPlayer.wsf", "RunPlayer.wsf");
			PSPadProject.make_from_template(filepath, "Flex3.4\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "Flex3.4\\Viewer_mxml.php", "Viewer_mxml.php");
			PSPadProject.make_from_template(filepath, "Flex3.4\\Viewer_mxml_ui.tab.php", "Viewer_mxml_ui.tab.php");
			PSPadProject.make_from_template(filepath, "Flex3.4\\build.bat", "build.bat");
			PSPadProject.make_from_template(filepath, "Flex3.4\\index.html", "index.html");
			PSPadProject.make_from_template(filepath, "Flex3.4\\index.html", "index.hta");
		}

		public static void make_flex4_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			File.Copy("F:\\Programs\\PSPad editor\\Template_01\\Flex4.1\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy("F:\\Programs\\PSPad editor\\Template_01\\Flex4.1\\Accordion.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
			PSPadProject.make_from_template(filepath, "Flex4.1\\Accordion.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "Flex4.1\\Accordion-app.xml", fileNameWithoutExtension + "-app.xml");
			PSPadProject.make_from_template(filepath, "Flex4.1\\Accordion.as3proj", fileNameWithoutExtension + ".as3proj");
			PSPadProject.make_from_template(filepath, "Flex4.1\\LaunchChrome.wsf", "LaunchChrome.wsf");
			PSPadProject.make_from_template(filepath, "Flex4.1\\LaunchFireFox.wsf", "LaunchFireFox.wsf");
			PSPadProject.make_from_template(filepath, "Flex4.1\\LaunchIE.wsf", "LaunchIE.wsf");
			PSPadProject.make_from_template(filepath, "Flex4.1\\RunAir.wsf", "RunAir.wsf");
			PSPadProject.make_from_template(filepath, "Flex4.1\\RunPlayer.wsf", "RunPlayer.wsf");
			PSPadProject.make_from_template(filepath, "Flex4.1\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "Flex4.1\\Viewer_mxml.php", "Viewer_mxml.php");
			PSPadProject.make_from_template(filepath, "Flex4.1\\Viewer_mxml_ui.tab.php", "Viewer_mxml_ui.tab.php");
			PSPadProject.make_from_template(filepath, "Flex4.1\\build.bat", "build.bat");
			PSPadProject.make_from_template(filepath, "Flex4.1\\index.html", "index.html");
		}

		public static void make_air4_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string str = "F:\\Programs\\PSPad editor\\";
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			string directoryName = Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "Air4\\MyBrowser01.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "Air4\\MyBrowser01-app.xml", fileNameWithoutExtension + "-app.xml");
			if (!File.Exists(directoryName + "\\build.bat"))
			{
				PSPadProject.make_from_template(filepath, "Air4\\build.bat", "build.bat");
			}
			PSPadProject.make_from_template(filepath, "Air4\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "Air4\\RunAir.wsf", "RunAir.wsf");
			PSPadProject.make_from_template(filepath, "Air4\\CreateCertificate.bat", "CreateCertificate.bat");
			PSPadProject.make_from_template(filepath, "Air4\\PackageApplication.bat", "PackageApplication.bat");
			PSPadProject.make_from_template(filepath, "Air4\\application.xml", "application.xml");
			File.Copy(str + "Template_01\\Air4\\FDTreeMenu.xml", directoryName + "\\FDTreeMenu.xml", true);
			File.Copy(str + "Template_01\\Air4\\MyBrowser01.fdp", directoryName + "\\" + fileNameWithoutExtension + ".fdp", true);
			File.Copy(str + "Template_01\\Air4\\AIR_readme.txt", directoryName + "\\AIR_readme.txt", true);
		}

		public static void make_latex_ppr(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "Tex\\build.bat", "build.bat");
			PSPadProject.make_from_template(filepath, "Tex\\Runapp.wsf", "Runapp.wsf");
			PSPadProject.make_from_template(filepath, "Tex\\latex.ppr", fileNameWithoutExtension + ".ppr");
			PSPadProject.make_from_template(filepath, "Tex\\Sakura_MultiOpen.wsf", "Sakura_MultiOpen.wsf");
			PSPadProject.make_from_template(filepath, "Tex\\embed_pdf.html", fileNameWithoutExtension + ".html");
			PSPadProject.make_from_template(filepath, "Tex\\dviout.wsf", fileNameWithoutExtension + ".wsf");
		}

		public static void make_redirect_refresh_html(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "redirect_refresh.html", fileNameWithoutExtension + "_refresh.html");
		}

		public static void make_redirect_frame_html(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "redirect_iframe.html", fileNameWithoutExtension + "_reframe.html");
		}

		public static void make_redirect_reopen_html(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filepath);
			Path.GetDirectoryName(filepath);
			Path.GetFileName(filepath);
			Path.GetExtension(filepath).Replace(".", "");
			PSPadProject.make_from_template(filepath, "redirect_reopen.html", fileNameWithoutExtension + "_reopen.html");
		}
	}
}
