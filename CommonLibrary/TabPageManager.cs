// -*- mode: cs -*-	Time-stamp: <2017-01-28 9:10:19 kahata>
/*================================================================
 * title: 
 * file: TabPageManager.cs
 * path: F:\VCSharp\Flashdevelop5.1.1-LL\External\CustomControl\DataGridViewPanel\TabPageManager.cs
 * url: F:/VCSharp/Flashdevelop5.1.1-LL/External/CustomControl/DataGridViewPanel/TabPageManager.cs
 * created: Time-stamp: <2017-01-28 9:10:19 kahata>
 * revision: $Id$
 * Programmed By: kahata
 * To compile:
 * To run: http://dobon.net/vb/dotnet/control/tabpagehide.html
 * link: 
 * description: 
 *================================================================*/
using MDIForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
	public class TabPageManager
	{
		public class TabPageInfo
		{
			public TabPage TabPage;
			public bool Visible;
      public TabControl Parent;
      public TabPageInfo(TabPage page, TabControl parent, bool v)
			{
				TabPage = page;
        Parent = parent;
        Visible = v;
			}
    }

    private TabPageInfo[] _tabPageInfos = null;
		private TabControl _tabControl = null;

    public static List<TabControl> tabControlList = new List<TabControl>();
    public static List<TabPageInfo> tabPageList = new List<TabPageInfo>();
    public static List<TabPage> tabPages = new List<TabPage>();

    /// <summary>
    /// TabPageManagerクラスのインスタンスを作成する
    /// </summary>
    /// <param name="crl">基になるTabControlオブジェクト</param>
    public TabPageManager(TabControl crl)
		{
			_tabControl = crl;
			_tabPageInfos = new TabPageInfo[_tabControl.TabPages.Count];
			for (int i = 0; i < _tabControl.TabPages.Count; i++)
				_tabPageInfos[i] = new TabPageInfo(_tabControl.TabPages[i], _tabControl, true);
		}

		/// <summary>
		/// TabPageの表示・非表示を変更する
		/// </summary>
		/// <param name="index">変更するTabPageのIndex番号</param>
		/// <param name="v">表示するときはTrue。
		/// 非表示にするときはFalse。</param>
		public void ChangeTabPageVisible(int index, bool v)
		{
			if (_tabPageInfos[index].Visible == v)
				return;

			_tabPageInfos[index].Visible = v;
			_tabControl.SuspendLayout();
			_tabControl.TabPages.Clear();
			for (int i = 0; i < _tabPageInfos.Length; i++)
			{
				if (_tabPageInfos[i].Visible)
					_tabControl.TabPages.Add(_tabPageInfos[i].TabPage);
			}
			_tabControl.ResumeLayout();
		}

    public int GetTabIndexByTabText(String tabText)
    {
      int tabindex = 0;
      for (tabindex = 0; tabindex < this._tabControl.TabCount; tabindex++)
      {
        if (this._tabControl.TabPages[tabindex].Text == tabText) return tabindex;
      }
      return -1;
    }

    public string GetTabTextByTabIndex(int  tabIndex)
    {
      return this._tabControl.TabPages[tabIndex].Text;
    }

    public static void AddTabControl(TabControl tabControl)
    {
      if (!tabControlList.Contains(tabControl)) tabControlList.Add(tabControl);
    }

    public static void RemoveTabControl(TabControl tabControl)
    {
      if (tabControlList.Contains(tabControl)) tabControlList.Remove(tabControl);
    }

    public static void AddTabPage(Control control, TabControl tabControl,Boolean singleton = true)
    {
      try
      {
        TabPage tabPage = new TabPage();
        String path = String.Empty;
        //if (!String.IsNullOrEmpty(control.AccessibleDescription)) path = control.AccessibleDescription;
        if (!String.IsNullOrEmpty(control.AccessibleName)) path = control.AccessibleName;
        else if (!String.IsNullOrEmpty(((Control)control.Tag).Tag as String))
        {
          path = ((Control)control.Tag).Tag as String;
        }
        // tabPage
        // コレクションを指定した文字で連結してくれるstring.Join()
        tabPage.AccessibleDescription = control.GetType().FullName + "@" + path;
        tabPage.Name = tabPage.AccessibleDescription;
        tabPage.Font = new Font("Meiryo UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

        //tabPage.Parent = tabControl;
        tabPage.Tag = new PageInfo(control,tabControl, path, true);
        //tabPage.Tag = control;
        tabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
        tabPage.Text = Path.GetFileName(path.TrimEnd('/'));
        tabPage.UseVisualStyleBackColor = true;


        if (singleton == true)
        {
          for (int i = 0; i < tabControl.TabPages.Count; i++)
          {
            if (tabControl.TabPages[i].AccessibleDescription == tabPage.AccessibleDescription)
            {
              tabControl.SelectedIndex = i;
              return;
            }
          }
          /*
          TabPageInfo info = getInfoByDescription(tabPage.AccessibleDescription);
          if (info != null && info.Visible == false)
          {
            info.Visible = true;
            tabControl.Controls.Add(info.TabPage);
            tabControl.SelectedTab = info.TabPage;
          }
          */

          TabPage tp = getTabPageByDescription(tabPage.AccessibleDescription);
          if (tp != null && ((PageInfo)tp.Tag).Visible == false)
          {
            ((PageInfo)tp.Tag).Visible = true;
            tabControl.Controls.Add(tp);
            tabControl.SelectedTab = tp;
            return;
          }
        }

        if (control != null)
        {
          tabPage.Controls.Add(control);
          tabPageList.Add(new TabPageInfo(tabPage, tabControl, true));
          tabPages.Add(tabPage);

          /*
          MessageBox.Show(control.GetType().Name,"control");
          MessageBox.Show(tabPage.GetType().Name,"tabpage");
          MessageBox.Show(tabControl.GetType().Name,"tabControl");
          */

          tabControl.Controls.Add(tabPage);
          tabControl.SelectedTab = tabPage;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "AddTabPage(Control control, TabControl tabControl)");
      }
    }

    public static void CloseTabPage(object sender, EventArgs e)
    {
      ToolStripMenuItem item = sender as ToolStripMenuItem;
      TabControl tabControl = item.GetCurrentParent().Tag as TabControl;
      TabPage tabpage = tabControl.SelectedTab;
      PageInfo info = tabpage.Tag as PageInfo;
      Control ctrl = info.Child;
      tabControl.TabPages.Remove(tabpage);
      if (ctrl.GetType().Name != "BrowserEx") ctrl.Dispose();
      try { tabPages.Remove(tabpage); } catch { }
      tabpage.Dispose();
    }

    public static void CloseTabPage(TabPage tabPage, TabControl tabControl)
    {
      PageInfo info = tabPage.Tag as PageInfo;
      Control ctrl = info.Child;
      tabControl.TabPages.Remove(tabPage);
      if (ctrl.GetType().Name != "BrowserEx") ctrl.Dispose();
      try { tabPages.Remove(tabPage); } catch { }
      tabPage.Dispose();
    }

    public static void CloseOtherTabPages(TabPage tabPage, TabControl tabControl)
    {
      tabControl.TabPages.Clear();
      tabControl.TabPages.Add(tabPage);
      /*
       * https://www.itmedia.co.jp/enterprise/articles/1903/18/news009.html
      リスト（＝System.Collections.Generic名前空間のList<T> クラスのオブジェクト）に対して、
      foreach文により要素を列挙している最中には、そのリストから要素を削除することはできない。
      これを行おうとするとInvalidOperation例外が発生する。
　    要素の削除により、すべての要素を順に列挙するという動作が保証できなくなるため要素の追加やソートも同様）。
      リスト内の各要素に対して何かの処理を行いながら、不要となる要素を削除したい場合には、
      for文とインデックス番号を使ってリストの要素を列挙する。
      ただし、末尾の要素から先頭方向に向かって処理していく必要がある
      */
      for (int i = tabPages.Count - 1; i >= 0; i--)
      {
        if (((PageInfo)tabPages[i].Tag).Parent == tabControl && tabPages[i] != tabPage) tabPages.Remove(tabPages[i]);
      }
      /*
      foreach (TabPage tp in tabPages)
      {
        //if (((PageInfo)tp.Tag).Parent == tabControl && tp != tabPage) tabPages.Remove(tp);
      }
      */
    }

    public static void CloseAllTabPages(TabControl tabControl)
    {
      tabControl.TabPages.Clear();
      for (int i = tabPages.Count - 1; i >= 0; i--)
      {
        if (((PageInfo)tabPages[i].Tag).Parent == tabControl) tabPages.Remove(tabPages[i]);
      }
      /*
      foreach (TabPage tp in tabPages)
      {
        if (((PageInfo)tp.Tag).Parent == tabControl) tabPages.Remove(tp);
      }
      */
    }

    public static void ShowInDialog(TabPage tabPage, TabControl tabControl)
    {
      Control control = ((PageInfo)tabPage.Tag).Child;
      Form dialog = new Form();
      control.Dock = DockStyle.Fill;
      dialog.Controls.Add(control);
      dialog.Tag = tabPage;
      dialog.Size = new Size(800,600);
      dialog.StartPosition = FormStartPosition.CenterScreen;
      dialog.TopMost = true;
      dialog.FormClosing += new FormClosingEventHandler(parentForm_Closing);
      dialog.Show();
      //tabControl.TabPages.Remove(tabPage);
      //tabPages.Remove(tabPage);
    }
    private static void parentForm_Closing(object sender, CancelEventArgs e)
    {
      Form dialog = sender as Form;
      TabPage tp = (TabPage)dialog.Tag;
      tp.Controls.Add(dialog.Controls[0]);
      PageInfo info = ((TabPage)dialog.Tag).Tag as PageInfo;
      //info.Parent.TabPages.Add((TabPage)dialog.Tag);
      info.Parent.SelectedTab = tp;
    }

    public static void MoveTabPage(TabPage tabPage, TabControl tabControl)
    {
      TabControl org = ((PageInfo)tabPage.Tag).Parent;
      org.TabPages.Remove(tabPage);
      tabControl.TabPages.Add(tabPage);
      ((PageInfo)tabPage.Tag).Parent = tabControl;
    }

    public static List<Control> GetDocuments()
    {
      List<Control> documents = new List<Control>();
      foreach (TabPage tp in tabPages)
      {
        if (((PageInfo)tp.Tag).Parent.Name == "documentTabControl")
        {
          documents.Add(((PageInfo)tp.Tag).Child);
          //documents.Add((Control)tp.Controls[0]);
        }
      }
      return documents;
    }
    /*
    public static void AddTabPage(TabPage tabPage)
    {
      try
      {
        TabControl tabControl = tabPage.Parent as TabControl;
        //TabPage tabPage = new TabPage();
        //String path = control.AccessibleDescription;
        // tabPage
        //tabPage.AccessibleDescription = control.GetType().Name + ";" + path;
        //tabPage.Name = tabPage.AccessibleDescription;
        //tabPage.Tag = tabControl;
        //tabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
        //tabPage.Text = Path.GetFileName(path.TrimEnd('/'));
        //tabPage.UseVisualStyleBackColor = true;
        
        for (int i = 0; i < tabControl.TabPages.Count; i++)
        {
          if (tabControl.TabPages[i].AccessibleDescription == tabPage.AccessibleDescription)
          {
            tabControl.SelectedIndex = i;
            return;
          }
        }
        
        TabPageInfo info = getInfoByDescription(tabPage.AccessibleDescription);
       
        if (info != null && info.Visible == false)
        {
          info.Visible = true;
          tabControl.Controls.Add(info.TabPage);
          tabControl.SelectedTab = info.TabPage;
        }
        tabPageList.Add(new TabPageInfo(tabPage, true));
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "AddTabPage(Control control, TabControl tabControl)");
      }
    }
    */
    public static void AddTabPageList(TabPage tabPage, TabControl parent ,Boolean visible=true)
    {
      try
      {
        String path = String.Empty;
        //try { path = ((Control)((Control)tabPage.Tag).Tag).Tag as String; }catch { }
        //tabPage.Tag = new PageInfo((Control)tabPage.Tag, parent, path, true);
        //tabPages.Add(tabPage);
        tabPageList.Add(new TabPageInfo(tabPage, parent, visible));
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "AddTabPageList(TabPage tabPage, Boolean visible)");
      }
    }

    #region Private Method
    static private TabPageInfo getInfoByDescription(String description)
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


    static private TabPage getTabPageByDescription(String description)
    {
      foreach (TabPage tabPage in tabPages)
      {
        if (tabPage.AccessibleDescription == description)
        {
          return tabPage;
        }
      }
      return null;
    }


    #endregion

  }
}