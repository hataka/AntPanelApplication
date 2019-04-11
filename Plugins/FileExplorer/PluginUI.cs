using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonInterface;
using CommonInterface.Managers;

namespace FileExplorer
{
  public partial class PluginUI : UserControl
  {
    private PluginMain pluginMain;

    public PluginUI(PluginMain pluginMain)
    {
      //this.AutoKeyHandling = true;
      this.pluginMain = pluginMain;
      //this.listViewSorter = new ListViewSorter();
      this.InitializeComponent();
      //this.InitializeGraphics();
      //this.InitializeContextMenu();
      //this.InitializeLayout();
      //this.InitializeTexts();
      //ScrollBarEx.Attach(fileView);

      this.AccessibleName = @"F:\VirtualBox\ShareFolder\mono\AntPanelApplication\bin\Debug\Nashorn.fdp";
      this.AccessibleDescription = System.Reflection.MethodBase.GetCurrentMethod().Name
                                  + "@" + System.Reflection.Assembly.GetExecutingAssembly().Location;
    }

    private void PluginUI_Load(object sender, EventArgs e)
    {
    }
  }
}
