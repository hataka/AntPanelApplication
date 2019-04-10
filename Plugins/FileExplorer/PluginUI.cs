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
    public PluginUI()
    {
      InitializeComponent();
      this.AccessibleName = @"F:\VirtualBox\ShareFolder\mono\AntPanelApplication\bin\Debug\Nashorn.fdp";
    }

    private void PluginUI_Load(object sender, EventArgs e)
    {
    }
  }
}
