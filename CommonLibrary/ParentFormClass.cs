using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;

namespace MDIForm
{

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
