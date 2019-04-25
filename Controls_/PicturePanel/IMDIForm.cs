using System;

//namespace MDIForm
//{
	public interface IMDIForm
	{
		ParentFormClass MainForm
		{
			get;
			set;
		}

		ChildFormControlClass Instance
		{
			get;
			set;
		}

		void Dispose();

		void InitializeInterface();
	}
//}
