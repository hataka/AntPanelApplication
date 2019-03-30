using System;
// 追加 Time-stamp: <2011-01-24 9:29:57 kahata>
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;
using System.Drawing;
//using PluginCore;
//using MDIForm;

//namespace MDIForm
//{

	public class ParentFormClass
	{
		public Form Instance { get; set; }
		//public DockContent containerDockContent { get; set; }

		public ToolStrip toolStrip { get; set; }
		public MenuStrip menuStrip { get; set; }
		public StatusStrip statusStrip { get; set; }
	}

	public class ChildFormControlClass
	{
		public String name { get; set; }
		public RichTextBox TextLog { get; set; }
		//public System.Windows.Forms.ToolStripSpringComboBox selectedPath { get; set; }
		public System.Windows.Forms.ListView fileView { get; set; }
	}

	public interface IMDIForm
	{
		ParentFormClass MainForm { get; set; }
		ChildFormControlClass Instance { get; set; }
		void Dispose();
		void InitializeInterface();
	}

//}


/*
///////////////////////////////////////////////////////////////////////////////////////////////////
// MEMO
 * CustomDockContent → Plugin へのアクセス
 * [OutPutPanel plugin 側]
		public void InitializeInterface()
		{
			this.Instance = new ChildFormControlClass(); // 重要
			this.Instance.name = "OutputPanel";
			this.Instance.TextLog = this.pluginUI.textLog;
	 		this.Instance.PluginPanel = this.PluginPanel;		// 重要
			this.Instance.OutputPanelClass = new OutputPanel_PluginClass(); // 重要
			this.Instance.OutputPanelClass.TextLog = this.pluginUI.textLog; // 重要
			this.Instance.OutputPanelClass.PluginPanel = this.PluginPanel;
		}
 * [PictureBoxControl CustomDockContent 側]
    private void 内容CToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IMDIForm outputpanel = this.MainForm.FindPluginInstanceByName("OutputPanel") as IMDIForm;  // 重要
			RichTextBox textlog
				= outputpanel.Instance.OutputPanelClass.TextLog;  // 重要
			MessageBox.Show(textlog.Text);
			//outputpanel.Instance.PluginPanel.Activate();  // 重要
			outputpanel.Instance.OutputPanelClass.PluginPanel.Activate();
		}


 //////////////////////////////////////////////////////////////////////////////// 
 * CustomDockContent1 → CustomDockContent2 へのアクセス
 * [CustomDockContent1 (Spreadsheet) 側]
  private void ヘルプLToolStripButton_Click(object sender, EventArgs e)
	{
		DockContent document = this.MainForm.GetSingleDockContentByName("PictureBoxControl");;
		//MessageBox.Show(((IMDIForm)(document.Tag)).Instance.toolStripStatusLabel.Text);
		ToolStripStatusLabel toolStripStatusLabel 
			= ((IMDIForm)document.Tag).Instance.PictureBoxControlClass.toolStripStatusLabel;
		MessageBox.Show(toolStripStatusLabel.Text);
		
		document.Activate();
	}	

 * [CustomDockContent2 (PictureBoxControl)側]
 * CustomDockContent2 の 公開する objectを設定する
		public void InitializeInterface()
		{
			this.Instance = new ChildFormControlClass();
			this.Instance.toolStripStatusLabel = this.toolStripStatusLabel1;

			this.Instance.PictureBoxControlClass = new PictureBoxControl_CustomControlClass();
			this.Instance.PictureBoxControlClass.toolStripStatusLabel	= this.toolStripStatusLabel1;// 重要
		}
/////////////////////////////////////////////////////////////////////////////////////
 * Plugin1 → Plugin2 へのアクセス
 * [Plugin1 (DataBaseManager)側]
		private void ヘルプLToolStripButton_Click(object sender, EventArgs e)
		{
			IMDIForm outputpanel = this.pluginMain.MainForm.FindPluginInstanceByName("OutputPanel") as IMDIForm;  // 重要
			RichTextBox textlog
				= outputpanel.Instance.OutputPanelClass.TextLog;  // 重要
			MessageBox.Show(textlog.Text);
			//outputpanel.Instance.PluginPanel.Activate();  // 重要 これでもOK
			outputpanel.Instance.OutputPanelClass.PluginPanel.Activate(); // 重要
		}
/////////////////////////////////////////////////////////////////////////////////////
 * Plugin → CustomDockContent へのアクセス
 * [Plugin1 (XmlTreeView2)側]
		private void ヘルプLToolStripButton_Click(object sender, EventArgs e)
		{
			DockContent document = this.pluginMain.MainForm.GetSingleDockContentByName("PictureBoxControl"); ;
			//MessageBox.Show(((IMDIForm)(document.Tag)).Instance.toolStripStatusLabel.Text);
			ToolStripStatusLabel toolStripStatusLabel
				= ((IMDIForm)document.Tag).Instance.PictureBoxControlClass.toolStripStatusLabel;
			MessageBox.Show(toolStripStatusLabel.Text);
			document.Activate();
		}
/////////////////////////////////////////////////////////////////////////////////////
*/