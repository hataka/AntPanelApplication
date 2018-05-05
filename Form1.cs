using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntPanelApplication
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel();
      antPanel.Dock = DockStyle.Fill;
      this.Controls.Add(antPanel);
      this.Text = "AntPanel";
      this.Size = new Size(1200 ,800);
      this.StartPosition = FormStartPosition.CenterScreen;
    }

    public Form1(String path)
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel(path);
      antPanel.Dock = DockStyle.Fill;
      this.Controls.Add(antPanel);
      this.Text = "AntPanel : " + path;
      this.Size = new Size(1200, 800);
      this.StartPosition = FormStartPosition.CenterScreen;
    }
  }
}
