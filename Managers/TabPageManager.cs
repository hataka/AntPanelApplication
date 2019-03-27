using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntPanelApplication.Managers
{
  public class TabPageManager : UserControl
  {
    #region Private Variables
    private class TabPageInfo
    {
      public TabPage TabPage;
      public bool Visible;
      public TabPageInfo(TabPage page, bool v)
      {
        TabPage = page;
        Visible = v;
      }
    }
    private TabPageInfo[] _tabPageInfos = null;
    private  List<TabPageInfo> tabPageList = new List<TabPageInfo>();
    private ContextMenuStrip antPanelTabcontextMenuStrip;
    private System.ComponentModel.IContainer components;
    private ToolStripMenuItem 非表示ToolStripMenuItem;
    private ToolStripMenuItem 他を非表示toolStripMenuItem1;
    private ToolStripMenuItem 全て非表示ToolStripMenuItem;
    private ToolStripMenuItem 再読込みToolStripMenuItem;
    private ToolStripMenuItem 複製ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem コマンドプロンプトToolStripMenuItem;
    private ToolStripMenuItem エクスプローラToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem 表示ToolStripMenuItem;
    private ToolStripMenuItem 追加ToolStripMenuItem;
    private ToolStripMenuItem 全て閉じるToolStripMenuItem2;
    private ToolStripMenuItem 全て表示ToolStripMenuItem;
    private TabControl _tabControl = null;
    #endregion

    #region Constructor
    public TabPageManager(TabControl crl)
    {
      InitializeComponent();
      _tabControl = crl;

      _tabPageInfos = new TabPageInfo[_tabControl.TabPages.Count];
      for (int i = 0; i < _tabControl.TabPages.Count; i++)
      {
        _tabPageInfos[i] =
            new TabPageInfo(_tabControl.TabPages[i], true);
        tabPageList.Add(new TabPageInfo(_tabControl.TabPages[i], true));
      }
      PopulateShowsMenu();
    }
    #endregion

    #region Private Method
    private TabPageInfo getInfoByDescription(String description)
    {
      foreach (TabPageInfo info in tabPageList)
      {
        if (info.TabPage.AccessibleDescription == description)
        {
          return info;
        }
      }
      return null;
    }

    private TabPageInfo getInfoByName(String name)
    {
      foreach (TabPageInfo info in tabPageList)
      {
        if (info.TabPage.Name == name)
        {
          return info;
        }
      }
      return null;
    }

    private Int32 GetSelectedInfoIndex()
    {
      for (int i = 0; i < _tabPageInfos.Length; i++)
      {
        if (_tabControl.TabPages[_tabControl.SelectedIndex].Name == _tabPageInfos[i].TabPage.Name)
        {
          return i;
        }
      }
      return -1;
    }

    private Int32 GetInfoIndexByName(String name)
    {
      for (int i = 0; i < _tabPageInfos.Length; i++)
      {
        if (_tabPageInfos[i].TabPage.Name == name)
        {
          return i;
        }
      }
      return -1;
    }
    #endregion

    #region Public Method
    /// <summary>
    /// TabPageの表示・非表示を変更する
    /// </summary>
    /// <param name="index">変更するTabPageのIndex番号</param>
    /// <param name="v">表示するときはTrue。
    /// 非表示にするときはFalse。</param>
    public void ChangeTabPageVisible(int index, bool v)
    {
      if (tabPageList[index].Visible == v) return;

      tabPageList[index].Visible = v;
      _tabControl.SuspendLayout();
      _tabControl.TabPages.Clear();
      foreach(TabPageInfo tabpageInfo in tabPageList)
      {
        if (tabpageInfo.Visible) _tabControl.TabPages.Add(tabpageInfo.TabPage);
      }
      _tabControl.ResumeLayout();
     }

    public void UpdateInfo()
    {
      _tabPageInfos = new TabPageInfo[_tabControl.TabPages.Count];
      for (int i = 0; i < _tabControl.TabPages.Count; i++)
        _tabPageInfos[i] =
            new TabPageInfo(_tabControl.TabPages[i], true);
      PopulateShowsMenu();
      _tabControl.MouseClick += new MouseEventHandler(tabControl_MouseClick);
    }

    public void Remove(Int32 index)
    {
      //Int32 index = GetSelectedInfoIndex();
      if (index == 0) return;
      // http://kan-kikuchi.hatenablog.com/entry/C%23Tip3
      //List<TabPageInfo> tabPagelist = new List<TabPageInfo>();
      //tabPagelist.AddRange(_tabPageInfos);
      //if (index > 0) tabPagelist.RemoveAt(index);
      //_tabPageInfos = tabPagelist.ToArray();
      //MessageBox.Show(_tabControl.TabPages[_tabControl.SelectedIndex].Text);
      _tabControl.TabPages.RemoveAt(index);
      _tabPageInfos = new TabPageInfo[_tabControl.TabPages.Count];
      for (int i = 0; i < _tabControl.TabPages.Count; i++)
        _tabPageInfos[i] =
            new TabPageInfo(_tabControl.TabPages[i], true);
      PopulateShowsMenu();
    }

    public void AddTabPage(Control control, String path)
    {
      TabPage tabPage = new TabPage();
      // tabPage
      tabPage.AccessibleDescription = control.GetType().Name + ";" + path;
      //tabPage.Name  "tabPage" + n;
      tabPage.Name = tabPage.AccessibleDescription;
      tabPage.Tag = control;
      tabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      tabPage.Size = new System.Drawing.Size(1185, 491);
      tabPage.Text = Path.GetFileName(path.TrimEnd('/'));
      tabPage.UseVisualStyleBackColor = true;
      for (int i = 0; i < _tabControl.TabPages.Count; i++)
      {
        if (_tabControl.TabPages[i].AccessibleDescription == tabPage.AccessibleDescription)
        {
          _tabControl.SelectedIndex = i;
          return;
        }
      }
      TabPageInfo info = this.getInfoByDescription(tabPage.AccessibleDescription);
      if (info != null && info.Visible == false)
      {
        info.Visible = true;
        _tabControl.Controls.Add(info.TabPage);
        _tabControl.SelectedTab = info.TabPage;
      }
      if (control != null)
      {
        tabPage.Controls.Add(control);
        tabPageList.Add(new TabPageInfo(tabPage, true));
        _tabControl.Controls.Add(tabPage);
        _tabControl.SelectedTab = tabPage;
      }
    }

    public void CloseTabPage(object sender, EventArgs e)
    {
      string msgboxString = _tabControl.SelectedTab.Text + " タブを削除します\nよろしいですか?";
      TabPage tabpage = _tabControl.SelectedTab;
      TabPageInfo info = this.getInfoByDescription(tabpage.AccessibleDescription);
      Control ctrl = (Control)tabpage.Tag as Control;
      tabpage.Controls.Remove(ctrl);
      //MessageBox.Show(ctrl.GetType().Name);
      _tabControl.TabPages.RemoveAt(_tabControl.SelectedIndex);
      if(ctrl.GetType().Name != "BrowserEx") ctrl.Dispose();
      try { tabPageList.Remove(info); } catch { }
      tabpage.Dispose();
    }
    
    public List<Control> GetDocuments()
    {
      List<Control> documents = new List<Control>();
      foreach(TabPageInfo info in tabPageList)
      {
        documents.Add((Control)info.TabPage.Tag);
      }
      return documents;
    }

    public Control GetCurrentDocument()
    {
      return (Control)_tabControl.SelectedTab.Tag;
    }

    #endregion

    #region ContextMenu Click Handler
    public void tabControl_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        //http://note.phyllo.net/?eid=517117
        for (int i = 0; i < this._tabControl.TabCount; i++)
        {
          //タブとマウス位置を比較し、クリックしたタブを選択
          if (this._tabControl.GetTabRect(i).Contains(e.X, e.Y))
          {
            this._tabControl.SelectedTab = this._tabControl.TabPages[i];
            this.antPanelTabcontextMenuStrip.Show(_tabControl, e.Location);
            break;
          }
        }
      }
    }

    private void 非表示ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Int32 index = GetSelectedInfoIndex();
      if (index> 0) ChangeTabPageVisible(index, false);
    }

    private void 他を非表示ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Int32 index = GetSelectedInfoIndex();
      _tabControl.SuspendLayout();
      _tabControl.TabPages.Clear();
      _tabControl.TabPages.Add(_tabPageInfos[0].TabPage);
      if(index>0) _tabControl.TabPages.Add(_tabPageInfos[index].TabPage);
      _tabControl.ResumeLayout();
    }

    private void 全て表示ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _tabControl.SuspendLayout();
      _tabControl.TabPages.Clear();
      for (int i = 0; i < _tabPageInfos.Length; i++)
      {
          _tabControl.TabPages.Add(_tabPageInfos[i].TabPage);
      }
      _tabControl.ResumeLayout();
      if (AntPanel.IsRunningWindows)
      {
        ChangeTabPageVisible(6, true);
        ChangeTabPageVisible(7, true);
        AntPanel.webBrowser1.Navigate("http://www.google.co.jp");
      }
      else
      {
        ChangeTabPageVisible(6, false);
        ChangeTabPageVisible(7, false);
      }
    }

    private void 追加ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //this.Add(AntPanel.webBrowser1, "新規");
    }

    private void 全て非表示ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _tabControl.SuspendLayout();
      _tabControl.TabPages.Clear();
      _tabControl.TabPages.Add(_tabPageInfos[0].TabPage);
      _tabControl.ResumeLayout();
    }

    private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void コマンドプロンプトToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void エクスプローラToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    public void PopulateShowsMenu()
    {
      try
      {
        ToolStripMenuItem toolStripMenuItem = this.表示ToolStripMenuItem;
        toolStripMenuItem.DropDownItems.Clear();
        for (int i = 0; i < _tabControl.TabPages.Count; i++)
        {
          //string text = this.settings.PreviousCustomDocuments[i];
          ToolStripMenuItem tabPageMenuItem = new ToolStripMenuItem();

          tabPageMenuItem.Click += new EventHandler(this.tabPageMenuItem_Click);
          tabPageMenuItem.Tag = _tabControl.TabPages[i];
          tabPageMenuItem.Name = _tabControl.TabPages[i].Name;
          tabPageMenuItem.CheckOnClick = true;
          tabPageMenuItem.Text = _tabControl.TabPages[i].Text;
          if (_tabPageInfos[i].Visible == true) tabPageMenuItem.Checked = true;
          else tabPageMenuItem.Checked = false;
          toolStripMenuItem.DropDownItems.Add(tabPageMenuItem);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString(), "PluginUI:PopulatePreviousCustomDocumentsMenu:1599");
      }
    }

    private void tabPageMenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem item = sender as ToolStripMenuItem;
      //MessageBox.Show(item.Name);
      ChangeTabPageVisible(this.GetInfoIndexByName(item.Name), item.Checked);
    }
    #endregion

    #region Designer Generated Code
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.antPanelTabcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.非表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.他を非表示toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.全て非表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.全て表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.再読込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.複製ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.コマンドプロンプトToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.エクスプローラToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.全て閉じるToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.antPanelTabcontextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // antPanelTabcontextMenuStrip
      // 
      this.antPanelTabcontextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.antPanelTabcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.非表示ToolStripMenuItem,
            this.他を非表示toolStripMenuItem1,
            this.全て非表示ToolStripMenuItem,
            this.toolStripSeparator5,
            this.表示ToolStripMenuItem,
            this.全て表示ToolStripMenuItem,
            this.追加ToolStripMenuItem,
            this.toolStripSeparator3,
            this.再読込みToolStripMenuItem,
            this.複製ToolStripMenuItem,
            this.toolStripSeparator4,
            this.コマンドプロンプトToolStripMenuItem,
            this.エクスプローラToolStripMenuItem});
      this.antPanelTabcontextMenuStrip.Name = "contextMenuStrip1";
      this.antPanelTabcontextMenuStrip.Size = new System.Drawing.Size(211, 290);
      // 
      // 非表示ToolStripMenuItem
      // 
      this.非表示ToolStripMenuItem.Name = "非表示ToolStripMenuItem";
      this.非表示ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.非表示ToolStripMenuItem.Text = "非表示";
      this.非表示ToolStripMenuItem.Click += new System.EventHandler(this.非表示ToolStripMenuItem_Click);
      // 
      // 他を非表示toolStripMenuItem1
      // 
      this.他を非表示toolStripMenuItem1.CheckOnClick = true;
      this.他を非表示toolStripMenuItem1.Name = "他を非表示toolStripMenuItem1";
      this.他を非表示toolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
      this.他を非表示toolStripMenuItem1.Text = "他を非表示";
      this.他を非表示toolStripMenuItem1.Click += new System.EventHandler(this.他を非表示ToolStripMenuItem_Click);
      // 
      // 全て非表示ToolStripMenuItem
      // 
      this.全て非表示ToolStripMenuItem.Name = "全て非表示ToolStripMenuItem";
      this.全て非表示ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.全て非表示ToolStripMenuItem.Text = "全非表示";
      this.全て非表示ToolStripMenuItem.Click += new System.EventHandler(this.全て非表示ToolStripMenuItem_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(207, 6);
      // 
      // 表示ToolStripMenuItem
      // 
      this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
      this.表示ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.表示ToolStripMenuItem.Text = "表示";
      // 
      // 全て表示ToolStripMenuItem
      // 
      this.全て表示ToolStripMenuItem.Name = "全て表示ToolStripMenuItem";
      this.全て表示ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.全て表示ToolStripMenuItem.Text = "全て表示";
      this.全て表示ToolStripMenuItem.Click += new System.EventHandler(this.全て表示ToolStripMenuItem_Click);
      // 
      // 追加ToolStripMenuItem
      // 
      this.追加ToolStripMenuItem.Name = "追加ToolStripMenuItem";
      this.追加ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.追加ToolStripMenuItem.Text = "追加";
      this.追加ToolStripMenuItem.Click += new System.EventHandler(this.追加ToolStripMenuItem_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(207, 6);
      // 
      // 再読込みToolStripMenuItem
      // 
      this.再読込みToolStripMenuItem.Name = "再読込みToolStripMenuItem";
      this.再読込みToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.再読込みToolStripMenuItem.Text = "再読込み";
      // 
      // 複製ToolStripMenuItem
      // 
      this.複製ToolStripMenuItem.Name = "複製ToolStripMenuItem";
      this.複製ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.複製ToolStripMenuItem.Text = "複製";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(207, 6);
      // 
      // コマンドプロンプトToolStripMenuItem
      // 
      this.コマンドプロンプトToolStripMenuItem.Name = "コマンドプロンプトToolStripMenuItem";
      this.コマンドプロンプトToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.コマンドプロンプトToolStripMenuItem.Text = "コマンドプロンプト";
      this.コマンドプロンプトToolStripMenuItem.Click += new System.EventHandler(this.コマンドプロンプトToolStripMenuItem_Click);
      // 
      // エクスプローラToolStripMenuItem
      // 
      this.エクスプローラToolStripMenuItem.Name = "エクスプローラToolStripMenuItem";
      this.エクスプローラToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
      this.エクスプローラToolStripMenuItem.Text = "エクスプローラ";
      this.エクスプローラToolStripMenuItem.Click += new System.EventHandler(this.エクスプローラToolStripMenuItem_Click);
      // 
      // 全て閉じるToolStripMenuItem2
      // 
      this.全て閉じるToolStripMenuItem2.CheckOnClick = true;
      this.全て閉じるToolStripMenuItem2.Name = "全て閉じるToolStripMenuItem2";
      this.全て閉じるToolStripMenuItem2.Size = new System.Drawing.Size(210, 24);
      this.全て閉じるToolStripMenuItem2.Text = "全て表示";
      this.全て閉じるToolStripMenuItem2.Click += new System.EventHandler(this.全て表示ToolStripMenuItem_Click);
      // 
      // TabPageManager
      // 
      this.Name = "TabPageManager";
      this.antPanelTabcontextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion
  }
}
