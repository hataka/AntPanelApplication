using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Xml;

namespace CommonLibrary
{
	public class NodeInfo
	{
		public string type;
		public string title;
    public string tooltip;
    public bool expand;

    /*
       tn.BackColor;
       //tn.Checked;
       //tn.Expand;
       tn.ForeColor;
       //tn.IsVisible;
      tn.NodeFont;
      //tn.TreeView; //åªç›äÑÇËìñÇƒÇÁÇÍÇƒÇ¢ÇÈêeTreeViewÇéÊìæ
      //tn.IsVisible;
      //tn.FullPath;
      //tn.ContextMenu;
    */
    public string backColor;
    public string foreColor;
    public string nodeFont;
    public string nodeChecked;

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


    /*
       tn.BackColor;
       //tn.Checked;
       //tn.Expand;
       tn.ForeColor;
       //tn.IsVisible;
      tn.NodeFont;
      //tn.TreeView; //åªç›äÑÇËìñÇƒÇÁÇÍÇƒÇ¢ÇÈêeTreeViewÇéÊìæ
      //tn.IsVisible;
      //tn.FullPath;
      //tn.ContextMenu;
    */
    public string BackColor
    {
      get
      {
        return this.backColor;
      }
      set
      {
        this.backColor = value;
      }
    }

    public string ForeColor
    {
      get
      {
        return this.foreColor;
      }
      set
      {
        this.foreColor = value;
      }
    }

    public string NodeFont
    {
      get
      {
        return this.nodeFont;
      }
      set
      {
        this.nodeFont = value;
      }
    }

    public string NodeChecked
    {
      get
      {
        return this.nodeChecked;
      }
      set
      {
        this.nodeChecked = value;
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
      this.backColor = String.Empty;
      this.foreColor = String.Empty;
      this.nodeFont  = String.Empty;
      this.nodeChecked = String.Empty;
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
