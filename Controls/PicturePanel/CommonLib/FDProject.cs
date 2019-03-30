using System;
// 追加 Time-stamp: <2011-01-24 9:29:57 kahata>
using System.Windows.Forms;
using System.Drawing;
//using WeifenLuo.WinFormsUI.Docking;
//using System.Drawing;
//using MDIForm;

//namespace MDIForm.FDProject
//{
	/// <summary>
	/// MDIParent1 Methodのデリゲート
	/// </summary>
//	public delegate void ShowMessageHandler(String name);
	
	/// <summary>
	/// ChildControlのデリゲート
	/// </summary>
	public interface IFDProjectClass
	{
		String Name { get; set; }
		String Path { get; set; }
		Image bImage { get; set; }
		Image lImage { get; set; }
		String Args { get; set; }
		String Output { get; set; }
		String Option { get; set; }
		String DefaultDir { get; set; }
		Boolean SaveAll { get; set; }
		Boolean Capture { get; set; }
		Boolean HideOutput { get; set; }
		Boolean LogType { get; set; }
	}

	public class CompileClass : IFDProjectClass
	{
		public String Name { get; set; }
		public String Path { get; set; }
		public Image  bImage { get; set; }
		public Image  lImage { get; set; }
		public String Args { get; set; }
		public String Output { get; set; }
		public String Option { get; set; }
		public String DefaultDir { get; set; }
		public Boolean SaveAll { get; set; }
		public Boolean Capture { get; set; }
		public Boolean HideOutput { get; set; }
		public Boolean LogType { get; set; }
	}

	public class ExecuteClass : IFDProjectClass
	{
		public String Name { get; set; }
		public Image  bImage { get; set; }
		public Image  lImage { get; set; }
		public String Path { get; set; }
		public String Args { get; set; }
		public String Output { get; set; }
		public String Option { get; set; }
		public String DefaultDir { get; set; }
		public Boolean SaveAll { get; set; }
		public Boolean Capture { get; set; }
		public Boolean HideOutput { get; set; }
		public Boolean LogType { get; set; }
	}

	public class ProjectClass
	{
		public String Name { get; set; }
		public CompileClass Compilator1 { get; set; }
		public CompileClass Compilator2 { get; set; }
		public CompileClass Compilator3 { get; set; }
		public CompileClass Compilator4 { get; set; }
		public ExecuteClass Prog1 { get; set; }
		public ExecuteClass Prog2 { get; set; }
		public ExecuteClass Prog3 { get; set; }
		public ExecuteClass Prog4 { get; set; }
	}
//}
