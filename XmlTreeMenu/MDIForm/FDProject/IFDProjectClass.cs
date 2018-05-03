using System;
using System.Drawing;

namespace MDIForm.FDProject
{
	public interface IFDProjectClass
	{
		string Name
		{
			get;
			set;
		}

		string Path
		{
			get;
			set;
		}

		Image bImage
		{
			get;
			set;
		}

		Image lImage
		{
			get;
			set;
		}

		string Args
		{
			get;
			set;
		}

		string Output
		{
			get;
			set;
		}

		string Option
		{
			get;
			set;
		}

		string DefaultDir
		{
			get;
			set;
		}

		bool SaveAll
		{
			get;
			set;
		}

		bool Capture
		{
			get;
			set;
		}

		bool HideOutput
		{
			get;
			set;
		}

		bool LogType
		{
			get;
			set;
		}
	}
}
