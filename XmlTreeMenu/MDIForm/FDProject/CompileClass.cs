using MDIForm.FDProject;
using System;
using System.Drawing;

namespace MDIForm.FDProject
{
	public class CompileClass : IFDProjectClass
	{
		public string Name
		{
			get;
			set;
		}

		public string Path
		{
			get;
			set;
		}

		public Image bImage
		{
			get;
			set;
		}

		public Image lImage
		{
			get;
			set;
		}

		public string Args
		{
			get;
			set;
		}

		public string Output
		{
			get;
			set;
		}

		public string Option
		{
			get;
			set;
		}

		public string DefaultDir
		{
			get;
			set;
		}

		public bool SaveAll
		{
			get;
			set;
		}

		public bool Capture
		{
			get;
			set;
		}

		public bool HideOutput
		{
			get;
			set;
		}

		public bool LogType
		{
			get;
			set;
		}
	}
}
