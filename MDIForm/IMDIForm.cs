using MDIForm;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MDIForm
{
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
  /*
  public class PageInfo
  {
    public Control Child;
    public TabControl Parent;
    public String Path;
    public bool Visible;
    public PageInfo(Control child, TabControl parent, String path, bool v)
    {
      Child = child;
      Parent = parent;
      Path = path;
      Visible = v;
    }
  }
  */
  public class ChildFormControlClass
  {
    public delegate Boolean CallPluginCommand(String command, String arguments);

    public string name
    {
      get;
      set;
    }

    public string filepath
    {
      get;
      set;
    }

    public Control MainForm
    {
      get;
      set;
    }

    public Form containerForm
    {
      get;
      set;
    }

    public ToolStrip toolStrip
    {
      get;
      set;
    }

    public MenuStrip menuStrip
    {
      get;
      set;
    }

    public StatusStrip statusStrip
    {
      get;
      set;
    }

    public BindingNavigator bindingNavigator
    {
      get;
      set;
    }
    public DataGridView dataGridView1
    {
      get;
      set;
    }
    public RichTextBox richTextBox
    {
      get;
      set;
    }
    public WebBrowser webBrowser
    {
      get;
      set;
    }
    public ListView listView
    {
      get;
      set;
    }
    public DataGridView dataGridView2
    {
      get;
      set;
    }
    public Panel panel
    {
      get;
      set;
    }
    public TreeView treeView
    {
      get;
      set;
    }

    public ToolStripStatusLabel toolStripStatusLabel
    {
      get;
      set;
    }

    public ToolStripMenuItem スクリプトToolStripMenuItem
    {
      get;
      set;
    }

    public List<string> PreviousDocuments
    {
      get;
      set;
    }
    /*
    public AxWMPLib.AxWindowsMediaPlayer mediaPlayer
    {
      get;
      set;
    }
    */
    public PictureBox pictureBox
    {
      get;
      set;
    }

    public ImageList imageList
    {
      get;
      set;
    }

    public CallPluginCommand callPluginCommand
    {
      get;
      set;
    }

  }

  public class ParentFormClass
  {
    public delegate Boolean CallPluginCommand(String command, String arguments);

    public List<string> SelectedPathes;

    public string[] ProjectSelectedPaths;

    public Form Instance
    {
      get;
      set;
    }
    /*
		public DockContent containerDockContent
		{
			get;
			set;
		}
    */
    public ToolStrip toolStrip
    {
      get;
      set;
    }

    public MenuStrip menuStrip
    {
      get;
      set;
    }

    public TreeView treeView
    {
      get;
      set;
    }

    public StatusStrip statusStrip
    {
      get;
      set;
    }
    /*
		public ToolStripSpringComboBox selectedPath
		{
			get;
			set;
		}
    */
    public ImageList imageList1
    {
      get;
      set;
    }

    public ImageList imageList2
    {
      get;
      set;
    }

    public ContextMenuStrip contextMenuStrip1
    {
      get;
      set;
    }

    public PropertyGrid propertyGrid1
    {
      get;
      set;
    }

    public ListView FileView
    {
      get;
      set;
    }

    public UserControl xmlTreeMenu_pluginUI
    {
      get;
      set;
    }

    public Object settings
    {
      get;
      set;
    }

    public CallPluginCommand callPluginCommand
    {
      get;
      set;
    }
  }
}
