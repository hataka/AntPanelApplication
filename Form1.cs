using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AntPanelApplication
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel();
      InitializeForm(antPanel);
    }

    public Form1(String path)
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel(path);
      antPanel.AccessibleDescription = path;
      InitializeForm(antPanel);
    }

    public Form1(String[] args)
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel(args);
      if (args.Length > 1 && !String.IsNullOrEmpty(args[0]))
      {
        antPanel.AccessibleDescription = args[0];
      }
      InitializeForm(antPanel);
    }

    private void InitializeForm(AntPanel antpanel)
    {
      antpanel.Dock = DockStyle.Fill;

      this.Controls.Add(antpanel);

      this.Text = "AntPanel : " + Path.GetFileName(antpanel.AccessibleDescription);
      this.Size = new Size(1200, 800);
      this.StartPosition = FormStartPosition.CenterScreen;
    }
  }
}
