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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
	public class TabPageManager
	{
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
		private TabControl _tabControl = null;

		/// <summary>
		/// TabPageManagerクラスのインスタンスを作成する
		/// </summary>
		/// <param name="crl">基になるTabControlオブジェクト</param>
		public TabPageManager(TabControl crl)
		{
			_tabControl = crl;
			_tabPageInfos = new TabPageInfo[_tabControl.TabPages.Count];
			for (int i = 0; i < _tabControl.TabPages.Count; i++)
				_tabPageInfos[i] = new TabPageInfo(_tabControl.TabPages[i], true);
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




  }
}