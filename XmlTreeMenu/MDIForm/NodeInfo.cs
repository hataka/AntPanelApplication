using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Xml;

namespace MDIForm
{
	public class NodeInfo
	{
		public string type;
		public string title;
    public string tooltip;
    public bool expand;
		public string pathbase;
		public string action;
		public string command;
		public string path;
		public string icon;
		public string args;
		public string option;
		public string innerText;
		public string comment;
    public XmlNode xmlNode;


    [ReadOnly(true)]
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

    public string Tooltip
    {
      get
      {
        return this.tooltip;
      }
      set
      {
        this.tooltip = value;
      }
    }


    [ReadOnly(true)]
		public bool Expand
		{
			get
			{
				return this.expand;
			}
			set
			{
				this.expand = value;
			}
		}

    [ReadOnly(true)]
    public XmlNode XmlNode
    {
      get
      {
        return this.xmlNode;
      }
      set
      {
        this.xmlNode = value;
      }
    }

    public string PathBase
		{
			get
			{
				return this.pathbase;
			}
			set
			{
				this.pathbase = value;
			}
		}

		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		public string Command
		{
			get
			{
				return this.command;
			}
			set
			{
				this.command = value;
			}
		}

		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		public string Args
		{
			get
			{
				return this.args;
			}
			set
			{
				this.args = value;
			}
		}

		public string Option
		{
			get
			{
				return this.option;
			}
			set
			{
				this.option = value;
			}
		}

		public string InnerText
		{
			get
			{
				return this.innerText;
			}
			set
			{
				this.innerText = value;
			}
		}

		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		public NodeInfo()
		{
			this.type = "null";
      this.title = String.Empty;
      this.tooltip = "";
      this.expand = false;
			this.pathbase = "";
			this.action = "";
			this.command = "";
			this.path = "";
			this.args = "";
			this.option = "";
      this.innerText = String.Empty;
      this.comment = String.Empty;
      this.xmlNode = null;
    }
	}
}
