using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MDIForm
{
	// 各ノードの情報用クラス
	public class NodeInfo
	{
		// タグ名称 (この名称を元にタグの種類を判断)
		public string type;
		// 「title」属性 (TreeViewのラベルはこの値を使用する)
		public string title;
		// 「price」属性
		//public string price;
		// 「expand」属性 (実はXML読み込み時以外必要ありませんが)
		public bool expand;

		public String pathbase;
		public String action;
		public String command;
		public String path;
		public String icon;
		public String args;
		public String option;
		public String innerText;
		public String comment;
		
		public NodeInfo()
		{
			type = "null";
			title = "new title";
			//price = "0";
			expand = false;

			pathbase = "";
			action = "";
			command = "";
			path = "";
			args = "";
			option = "";
		}

		[ReadOnly(true)]
		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		//public string Price
		//{
		//	get { return price; }
		//	set { price = value; }
		//}

		[ReadOnly(true)]
		public bool Expand
		{
			get { return expand; }
			set { expand = value; }
		}

		public string PathBase
		{
			get { return pathbase; }
			set { pathbase = value; }
		}
		public string Action
		{
			get { return action; }
			set { action = value; }
		}
		public string Command
		{
			get { return command; }
			set { command = value; }
		}
    /*
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor),
						typeof(System.Drawing.Design.UITypeEditor))]
		public string Path
		{
			get { return path; }
			set { path = value; }
		}

		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor),
						typeof(System.Drawing.Design.UITypeEditor))]
		public string Icon
		{
			get { return icon; }
			set { icon = value; }
		}
	*/
		public string Args
		{
			get { return args; }
			set { args = value; }
		}
		public string Option
		{
			get { return option; }
			set { option = value; }
		}

		public string InnerText
		{
			get { return innerText; }
			set { innerText = value; }
		}

		public string Comment
		{
			get { return comment; }
			set { comment = value; }
		}
	
	}
}
