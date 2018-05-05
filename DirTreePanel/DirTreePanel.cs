using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using AntPlugin;
using AntPlugin.CommonLibrary;
using AntPlugin.XmlTreeMenu;
using System.Diagnostics;
using AntPanelApplication;
using CommonLibrary;

namespace AntPlugin.Controls
{
	public class DirTreePanel : UserControl
	{
		private ToolStrip toolStrip1;
		private ToolStripButton �w���vLToolStripButton;
		private Panel panel1;
		//private PluginMain pluginMain;
		private AntPanel antPanel;
    global::AntPanelApplication.Properties.Settings settings;

    private ToolStripButton �V�K�쐬NToolStripButton;
		private ToolStripButton �J��OToolStripButton;
		private ToolStripButton �㏑���ۑ�SToolStripButton;
		private ToolStripButton ���PToolStripButton;
		private ToolStripSeparator toolStripSeparator;
		private ToolStripButton �؂���UToolStripButton;
		private ToolStripButton �R�s�[CToolStripButton;
		private ToolStripButton �\��t��PToolStripButton;
		private ToolStripSeparator toolStripSeparator2;
		public DirTreeView dirTreeView1;
		private SplitContainer splitContainer1;
		public PropertyGrid propertyGrid1;
		private ToolStripButton toolStripButton1;
		private TreeView treeView1;
		private Panel panel2;
		private ListView listView1;
		private Panel panel3;
		private TreeView treeView2;
		private ToolStripButton homeStripButton;
		private string currentProjectPath = String.Empty;
    private ToolStripButton upButton;
    private ToolStripButton downButton;
    private ToolStripButton projectButton;
    private ToolStripButton itemButton;
    private string currentRootDir = String.Empty;

    /*
		public DirTreePanel(PluginMain pluginMain)
		{
			this.pluginMain = pluginMain;
			this.InitializeComponent();
		}
		*/
    public DirTreePanel(AntPanel ui)
		{
			this.antPanel = ui;
			this.InitializeComponent();
      this.settings = new global::AntPanelApplication.Properties.Settings();
    }

		public DirTreePanel(AntPanel ui, String rootPath)
		{
			this.InitializeComponent();
			this.antPanel = ui;
			this.currentProjectPath = rootPath;
      this.currentRootDir = Path.GetDirectoryName(rootPath);
      InitializDirTreeView();
      this.settings = new global::AntPanelApplication.Properties.Settings();
    }

    /// <summary>
    /// Accessor to the RichTextBox
    /// </summary>
    /*    
		public RichTextBox Output
				{
						get { return this.richTextBox; }
				}
		*/

    #region Windows Forms Designer Generated Code

    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirTreePanel));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.�V�K�쐬NToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.�J��OToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.�㏑���ۑ�SToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.���PToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.�؂���UToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.�R�s�[CToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.�\��t��PToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.�w���vLToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.homeStripButton = new System.Windows.Forms.ToolStripButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.panel2 = new System.Windows.Forms.Panel();
      this.listView1 = new System.Windows.Forms.ListView();
      this.panel3 = new System.Windows.Forms.Panel();
      this.treeView2 = new System.Windows.Forms.TreeView();
      this.upButton = new System.Windows.Forms.ToolStripButton();
      this.downButton = new System.Windows.Forms.ToolStripButton();
      this.projectButton = new System.Windows.Forms.ToolStripButton();
      this.itemButton = new System.Windows.Forms.ToolStripButton();
      this.toolStrip1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.�V�K�쐬NToolStripButton,
            this.�J��OToolStripButton,
            this.�㏑���ۑ�SToolStripButton,
            this.���PToolStripButton,
            this.toolStripSeparator,
            this.�؂���UToolStripButton,
            this.�R�s�[CToolStripButton,
            this.�\��t��PToolStripButton,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.�w���vLToolStripButton,
            this.homeStripButton,
            this.upButton,
            this.downButton,
            this.projectButton,
            this.itemButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(787, 27);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // �V�K�쐬NToolStripButton
      // 
      this.�V�K�쐬NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.�V�K�쐬NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("�V�K�쐬NToolStripButton.Image")));
      this.�V�K�쐬NToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.�V�K�쐬NToolStripButton.Name = "�V�K�쐬NToolStripButton";
      this.�V�K�쐬NToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.�V�K�쐬NToolStripButton.Text = "�V�K�쐬(&N)";
      // 
      // �J��OToolStripButton
      // 
      this.�J��OToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.�J��OToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("�J��OToolStripButton.Image")));
      this.�J��OToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.�J��OToolStripButton.Name = "�J��OToolStripButton";
      this.�J��OToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.�J��OToolStripButton.Text = "�J��(&O)";
      this.�J��OToolStripButton.Click += new System.EventHandler(this.�J��OToolStripButton_Click);
      // 
      // �㏑���ۑ�SToolStripButton
      // 
      this.�㏑���ۑ�SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.�㏑���ۑ�SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("�㏑���ۑ�SToolStripButton.Image")));
      this.�㏑���ۑ�SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.�㏑���ۑ�SToolStripButton.Name = "�㏑���ۑ�SToolStripButton";
      this.�㏑���ۑ�SToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.�㏑���ۑ�SToolStripButton.Text = "�㏑���ۑ�(&S)";
      // 
      // ���PToolStripButton
      // 
      this.���PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.���PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("���PToolStripButton.Image")));
      this.���PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.���PToolStripButton.Name = "���PToolStripButton";
      this.���PToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.���PToolStripButton.Text = "���(&P)";
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
      // 
      // �؂���UToolStripButton
      // 
      this.�؂���UToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.�؂���UToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("�؂���UToolStripButton.Image")));
      this.�؂���UToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.�؂���UToolStripButton.Name = "�؂���UToolStripButton";
      this.�؂���UToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.�؂���UToolStripButton.Text = "�؂���(&U)";
      // 
      // �R�s�[CToolStripButton
      // 
      this.�R�s�[CToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.�R�s�[CToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("�R�s�[CToolStripButton.Image")));
      this.�R�s�[CToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.�R�s�[CToolStripButton.Name = "�R�s�[CToolStripButton";
      this.�R�s�[CToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.�R�s�[CToolStripButton.Text = "�R�s�[(&C)";
      // 
      // �\��t��PToolStripButton
      // 
      this.�\��t��PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.�\��t��PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("�\��t��PToolStripButton.Image")));
      this.�\��t��PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.�\��t��PToolStripButton.Name = "�\��t��PToolStripButton";
      this.�\��t��PToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.�\��t��PToolStripButton.Text = "�\��t��(&P)";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton1.Text = "toolStripButton1";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // �w���vLToolStripButton
      // 
      this.�w���vLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.�w���vLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("�w���vLToolStripButton.Image")));
      this.�w���vLToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.�w���vLToolStripButton.Name = "�w���vLToolStripButton";
      this.�w���vLToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.�w���vLToolStripButton.Text = "�w���v(&L)";
      this.�w���vLToolStripButton.Click += new System.EventHandler(this.�w���vLToolStripButton_Click);
      // 
      // homeStripButton
      // 
      this.homeStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.homeStripButton.Image = ((System.Drawing.Image)(resources.GetObject("homeStripButton.Image")));
      this.homeStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.homeStripButton.Name = "homeStripButton";
      this.homeStripButton.Size = new System.Drawing.Size(24, 24);
      this.homeStripButton.Text = "homeStripButton";
      this.homeStripButton.Click += new System.EventHandler(this.homeStripButton_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.treeView1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(787, 363);
      this.panel1.TabIndex = 1;
      // 
      // treeView1
      // 
      this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView1.Location = new System.Drawing.Point(0, 0);
      this.treeView1.Name = "treeView1";
      this.treeView1.Size = new System.Drawing.Size(787, 363);
      this.treeView1.TabIndex = 0;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 27);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
      this.splitContainer1.Size = new System.Drawing.Size(787, 612);
      this.splitContainer1.SplitterDistance = 363;
      this.splitContainer1.TabIndex = 2;
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(787, 245);
      this.propertyGrid1.TabIndex = 0;
      this.propertyGrid1.ToolbarVisible = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.listView1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 27);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(787, 612);
      this.panel2.TabIndex = 2;
      // 
      // listView1
      // 
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.Location = new System.Drawing.Point(0, 0);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(787, 612);
      this.listView1.TabIndex = 0;
      this.listView1.UseCompatibleStateImageBehavior = false;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.treeView2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 27);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(787, 612);
      this.panel3.TabIndex = 3;
      // 
      // treeView2
      // 
      this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView2.Location = new System.Drawing.Point(0, 0);
      this.treeView2.Name = "treeView2";
      this.treeView2.Size = new System.Drawing.Size(787, 612);
      this.treeView2.TabIndex = 0;
      // 
      // upButton
      // 
      this.upButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
      this.upButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.upButton.Name = "upButton";
      this.upButton.Size = new System.Drawing.Size(24, 24);
      this.upButton.Text = "upButton";
      this.upButton.ToolTipText = "��̃f�B���N�g���Ɉړ����܂�";
      this.upButton.Click += new System.EventHandler(this.upButton_Click);
      // 
      // downButton
      // 
      this.downButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
      this.downButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.downButton.Name = "downButton";
      this.downButton.Size = new System.Drawing.Size(24, 24);
      this.downButton.Text = "downButton";
      this.downButton.ToolTipText = "�I�����ڂ̃t�H���_�Ɉړ����܂�";
      this.downButton.Click += new System.EventHandler(this.downButton_Click);
      // 
      // projectButton
      // 
      this.projectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.projectButton.Image = ((System.Drawing.Image)(resources.GetObject("projectButton.Image")));
      this.projectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.projectButton.Name = "projectButton";
      this.projectButton.Size = new System.Drawing.Size(24, 24);
      this.projectButton.Text = "projectButton";
      this.projectButton.ToolTipText = "�v���W�F�N�g�t�H���_�Ɉړ����܂�";
      this.projectButton.Click += new System.EventHandler(this.projectButton_Click);
      // 
      // itemButton
      // 
      this.itemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.itemButton.Image = ((System.Drawing.Image)(resources.GetObject("itemButton.Image")));
      this.itemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.itemButton.Name = "itemButton";
      this.itemButton.Size = new System.Drawing.Size(24, 24);
      this.itemButton.Text = "itemButton";
      this.itemButton.ToolTipText = "�A�C�e���t�H���_�Ɉړ����܂�";
      this.itemButton.Click += new System.EventHandler(this.itemButton_Click);
      // 
      // DirTreePanel
      // 
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.toolStrip1);
      this.Name = "DirTreePanel";
      this.Size = new System.Drawing.Size(787, 639);
      this.Load += new System.EventHandler(this.PluginUI_Load);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private void PluginUI_Load(object sender, EventArgs e)
		{
			InitializDirTreeView();
		}
		#region InitializDirTreeView

		public void InitializDirTreeView()
		{
      Boolean projectExists = (File.Exists(this.currentProjectPath));
      Enabled = projectExists;

      if (this.dirTreeView1 == null)
      {
        //Project�Ɠ����p�~ �e�f�B���N�g���Ɉړ��ł��Ȃ� FileExplorer���g�p����
        if (projectExists)
        {
          this.dirTreeView1 = new DirTreeView(Path.GetDirectoryName(this.antPanel.projectPath));
        }
        else
        {
          this.dirTreeView1 = new DirTreeView();
        }
      }

      this.SuspendLayout();

			this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Panel1Collapsed = false;
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.ToolbarVisible = false;

			// 
			// dirTreeView1
			// 
			this.dirTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dirTreeView1.ImageIndex = 0;
			this.dirTreeView1.Location = new System.Drawing.Point(0, 2);
			this.dirTreeView1.Name = "dirTreeView1";
			this.dirTreeView1.SelectedImageIndex = 0;
			this.dirTreeView1.Size = new System.Drawing.Size(221, 361);
			this.dirTreeView1.TabIndex = 0;
			this.dirTreeView1.DoubleClick += new System.EventHandler(this.dirTreeView1_DoubleClick);
			this.dirTreeView1.BeforeExpand
				+= new System.Windows.Forms.TreeViewCancelEventHandler(this.dirTreeView1_BeforeExpand);
			this.dirTreeView1.AfterSelect
				+= new System.Windows.Forms.TreeViewEventHandler(this.dirTreeView1_AfterSelect);
			this.dirTreeView1.�T�N���G�f�B�^ToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_�T�N���G�f�B�^ToolStripMenuItem1_Click);
			this.dirTreeView1.pSPadToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_pSPadToolStripMenuItem1_Click);
			this.dirTreeView1.azukiControlToolStripMenuItem3.Click
				+= new System.EventHandler(this.dirTreeView1_azukiControlToolStripMenuItem3_Click);
			this.dirTreeView1.richTextEditorToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_richTextEditorToolStripMenuItem1_Click);
			this.dirTreeView1.�G�N�X�v���[��ToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_�G�N�X�v���[��ToolStripMenuItem1_Click);
			this.dirTreeView1.�R�}���h�v�����v�gToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_�R�}���h�v�����v�gToolStripMenuItem1_Click);
			//this.viewerToolStripMenuItem.Click
			//	+= new System.EventHandler(this.viewerToolStripMenuItem_Click);
			this.dirTreeView1.viewerToolStripMenuItem.Click
				+= new System.EventHandler(this.dirTreeView1_viewerToolStripMenuItem_Click);
			this.dirTreeView1.�V�X�e���v���O����ToolStripMenuItem.Click
				+= new System.EventHandler(this.dirTreeView1_�V�X�e���v���O����ToolStripMenuItem_Click);
			this.dirTreeView1.�J��toolStripMenuItem1.Click
			 += new System.EventHandler(this.dirTreeView1_�J��toolStripMenuItem1_Click);
			this.dirTreeView1.�u���E�U�ŊJ��toolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_�u���E�U�ŊJ��toolStripMenuItem1_Click);
			this.dirTreeView1.�R���e�L�X�g���j���[toolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_�R���e�L�X�g���j���[toolStripMenuItem1_Click);
			this.dirTreeView1.�t�@�C���G�N�X�v���[���Ɠ���toolStripMenuItem.Click += new System.EventHandler(this.�t�@�C���G�N�X�v���[���Ɠ���toolStripMenuItem_Click);
			this.dirTreeView1.Ant�c���[�ɒǉ�toolStripMenuItem.Click += new System.EventHandler(this.Ant�c���[�ɒǉ�toolStripMenuItem_Click);

			this.panel1.Controls.Add(dirTreeView1);
			this.dirTreeView1.BringToFront();
			this.ResumeLayout(false);
		}

		// �ꉞ���� 
		private void dirTreeView1_DoubleClick(object sender, System.EventArgs e)
		{
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			DirectoryInfo selectedPath = new DirectoryInfo(path);//using System.IO
			try
			{
        // TODO ����
        //PluginBase.MainForm.OpenEditableDocument(path);
        antPanel.OpenDocument(path);
      }
			catch (Exception exc)
			{
				String errMsg = exc.Message.ToString();
				//MessageBox.Show(exc.Message.ToString());
			}
		}

		private void dirTreeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeNode selectedNode = e.Node;   //�����̃C�x���g�����m�[�h�ɑ΂��ǉ��E�폜�E����
			//DirectoryInfo selectedDir = new DirectoryInfo(selectedNode.FullPath);//using System.IO
			//this.dirTreeView1.Nodes.Clear();
			//FolderTreeBox.dirTreeView1.InitializeDirTreeView(selectedNode.FullPath);
		}

		private void dirTreeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			String path = this.dirTreeView1.filepath;

			this.ShowNodeInfo(path);
			//ListView�Ƀt�@�C���ꗗ��\��
			DirectoryInfo di = new DirectoryInfo(e.Node.FullPath);
			if ((di.Attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
			{
				//this.listView1.Clear();//���ꂪ�Ȃ��ƃt�@�C���\�����ݐ�
			}
		}

		// dirTreeView1 
		private void dirTreeView1_�T�N���G�f�B�^ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_Sakura(this.dirTreeView1.filepath);
		}

		// dirTreeView1 
		private void dirTreeView1_pSPadToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_PSPad(this.dirTreeView1.filepath);
		}

		// dirTreeView1 
		private void dirTreeView1_viewerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//MainForm.Run_PHPViewer(DirTreeBox.dirTreeView1.filepath);
		}
		// dirTreeView1 
		private void dirTreeView1_�R�}���h�v�����v�gToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_Cmd(this.dirTreeView1.filepath);
		}
		// dirTreeView1 
		private void dirTreeView1_richTextEditorToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			try
			{
				// HACK - dirTreeView1_richTextEditorToolStripMenuItem1_Click
				//this.OpenDocument(path);
				//TODO ����
				//PluginBase.MainForm.CallCommand("PluginCommand",
				//  "XMLTreeMenu.CreateCustomDocument;RichTextEditor|" + path);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}


		}
		// dirTreeView1 
		private void dirTreeView1_azukiControlToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			//MainForm.Run_AzukiControl(DirTreeBox.dirTreeView1.filepath);
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			try
			{
				// HACK - dirTreeView1_azukiControlToolStripMenuItem3_Click
				//this.OpenDocument(path);

				// TODO ����
				//PluginBase.MainForm.CallCommand("PluginCommand",
				//  "XMLTreeMenu.CreateCustomDocument;AzukiEditor|" + path);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}
		}
		// dirTreeView1 
		private void dirTreeView1_�G�N�X�v���[��ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			//ProcessHandler.Run_Explorer(this.dirTreeView1.filepath);
			Process.Start(@"C:\windows\explorer.exe", "/select," + this.dirTreeView1.filepath);
		}
		// dirTreeView1 
		private void dirTreeView1_�V�X�e���v���O����ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_SystemProcess(this.dirTreeView1.filepath);
		}


		private void dirTreeView1_�J��toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			try
			{
        // TODO ����
        //PluginBase.MainForm.OpenEditableDocument(path);
        antPanel.OpenDocument(path);
      }
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}
		}

		private void dirTreeView1_�u���E�U�ŊJ��toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			String path = this.dirTreeView1.filepath.ToLower();
			String url = path.Replace(@"f:\", "http://localhost/f/");
			url = url.Replace(@"c:\apache2.2\htdocs", "http://localhost");
			url = url.Replace("c:", "file:///c");
			url = url.Replace("\\", "/");
      // TODO ����
      //PluginBase.MainForm.CallCommand("Browse", url);
      Process.Start(settings.ChromePath, url);
    }

		private void dirTreeView1_�R���e�L�X�g���j���[toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				FileInfo[] selectedPathsAndFiles = new FileInfo[1];
				// �I���m�[�h��Path���擾
				String path = this.dirTreeView1.filepath;
				if (!File.Exists(path)) return;
				ShellContextMenu scm = new ShellContextMenu();
				Point location = new Point(this.dirTreeView1.contextMenuStrip1.Bounds.Left,
																		this.dirTreeView1.contextMenuStrip1.Bounds.Top);
				selectedPathsAndFiles[0] = new FileInfo(path);
				this.dirTreeView1.contextMenuStrip1.Hide(); // Hide default menu
				scm.ShowContextMenu(selectedPathsAndFiles, location);
			}
			catch
			{
				MessageBox.Show("�m�[�h��I�����Ă�������");
			}
		}


		private void �t�@�C���G�N�X�v���[���Ɠ���toolStripMenuItem_Click(object sender, EventArgs e)
		{
			String path = Path.GetDirectoryName(this.dirTreeView1.filepath);
			if (System.IO.Directory.Exists(this.dirTreeView1.filepath)) path = this.dirTreeView1.filepath;
			try
			{
				//PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + path);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}

		}

		private void Ant�c���[�ɒǉ�toolStripMenuItem_Click(object sender, EventArgs e)
		{
			string[] files = new String[1] { this.dirTreeView1.filepath };
			try
			{
				this.antPanel.AddBuildFiles(files);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());

			}
		}

    private void �w���vLToolStripButton_Click(object sender, EventArgs e)
		{
			String path = "http://localhost/f/VCSharp/Flashdevelop5.1.1-LL/External/Plugins/DirTreePanel/doxygen/html/index.html";
      //PluginBase.MainForm.CallCommand("Browse", path);
      Process.Start(settings.ChromePath, path);
        
    }

		public TreeNode RecursiveCreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			NodeInfo ni = new NodeInfo();
			ni.Path = directoryInfo.FullName;
			ni.Type = "folder";
			if (String.IsNullOrEmpty(ni.Title)) ni.Title = Path.GetFileName(directoryInfo.Name);
			// FIXME
			int imageIndex = this.antPanel.menuTree.GetIconImageIndex(@"C:\windows");
			//this.getImageIndexFromNodeInfo_safe(ni);
			TreeNode directoryNode = new TreeNode(ni.Title, imageIndex, imageIndex);

			// �m�[�h�ɑ�����ݒ�
			directoryNode.Tag = ni;
			directoryNode.ToolTipText = ni.Path;

			foreach (var directory in directoryInfo.GetDirectories())
				directoryNode.Nodes.Add(RecursiveCreateDirectoryNode(directory));
			foreach (var file in directoryInfo.GetFiles())
			{
				ni = new NodeInfo();
				ni.Type = "record";
				ni.Path = file.FullName;
				ni.Title = Path.GetFileName(file.Name);
				//MessageBox.Show(ni.Path);
				imageIndex = this.antPanel.menuTree.GetIconImageIndex(ni.Path);
				TreeNode fileNode = new TreeNode(ni.Title, imageIndex, imageIndex);

				fileNode.Tag = ni;
				fileNode.ToolTipText = ni.Path;

				//directoryNode.Nodes.Add(new TreeNode(file.Name));
				directoryNode.Nodes.Add(fileNode);
			}
			return directoryNode;
		}
		//////////////////////////////////////////////////	
		#endregion

		private void ShowNodeInfo(String path)
		{
			if (Directory.Exists(path))
			{
				DirectoryInfo di = new DirectoryInfo(path);
				this.propertyGrid1.SelectedObject = di;
			}
			else if (File.Exists(path))
			{
				FileInfo fi = new FileInfo(path);
				this.propertyGrid1.SelectedObject = fi;
			}
		}

    // no refer
		private void ShowNodeInfo(TreeNode treeNode)
		{
			if (treeNode != null && treeNode.Tag != null)
			{
					NodeInfo selectedObject = new NodeInfo();
				selectedObject = (NodeInfo)treeNode.Tag;
				this.propertyGrid1.SelectedObject = selectedObject;
			}
		}

		private int toggleIndex = 1;
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			int num = this.toggleIndex % 3;
			if (num == 0)
			{
				this.splitContainer1.Panel2Collapsed = true;
				this.splitContainer1.Panel1Collapsed = false;
				this.propertyGrid1.HelpVisible = false;
				this.propertyGrid1.ToolbarVisible = false;
			}
			else if (num == 1)
			{
				this.splitContainer1.Panel2Collapsed = false;
				this.splitContainer1.Panel1Collapsed = true;
				this.propertyGrid1.HelpVisible = true;
				this.propertyGrid1.ToolbarVisible = true;
			}
			else if (num == 2)
			{
				this.splitContainer1.Panel2Collapsed = false;
				this.splitContainer1.Panel1Collapsed = false;
				this.propertyGrid1.HelpVisible = false;
				this.propertyGrid1.ToolbarVisible = false;
			}
			this.toggleIndex++;
		}

		private void �J��OToolStripButton_Click(object sender, EventArgs e)
		{
      String filepath = @"F:\codingground\java\swt.snippets8\Snippet005\Snippet5.java";
      //FolderBrowserDialog�N���X�̃C���X�^���X���쐬
      FolderBrowserDialog fbd = new FolderBrowserDialog();

      //�㕔�ɕ\����������e�L�X�g���w�肷��
      fbd.Description = "�t�H���_���w�肵�Ă��������B";
      //���[�g�t�H���_���w�肷��
      //�f�t�H���g��Desktop
      fbd.RootFolder = Environment.SpecialFolder.Desktop;
      //�ŏ��ɑI������t�H���_���w�肷��
      //RootFolder�ȉ��ɂ���t�H���_�ł���K�v������
      fbd.SelectedPath = @"F:\";
      //���[�U�[���V�����t�H���_���쐬�ł���悤�ɂ���
      //�f�t�H���g��True
      fbd.ShowNewFolderButton = true;

      //�_�C�A���O��\������
      if (fbd.ShowDialog(this) == DialogResult.OK)
      {
        //�I�����ꂽ�t�H���_��\������
        Console.WriteLine(fbd.SelectedPath);
        filepath = fbd.SelectedPath;
      }

      if (Directory.Exists(filepath)) this.currentRootDir = filepath;
      //else if(File.Exists(filepath)) this.currentRootDir = Path.GetDirectoryName(antPanel.projectPath);

      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
		}

		private void homeStripButton_Click(object sender, EventArgs e)
		{
			this.Controls.Remove(this.dirTreeView1);
			this.dirTreeView1.Dispose();
			this.dirTreeView1 = new DirTreeView();
      InitializDirTreeView();
		}

    private void upButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      this.currentRootDir = Path.GetDirectoryName(this.currentRootDir);
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
    }

    private void downButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      if(Directory.Exists(this.dirTreeView1.filepath))
      {
        this.currentRootDir = this.dirTreeView1.filepath;
      }
      else if(File.Exists(this.dirTreeView1.filepath))
      {
        this.currentRootDir = Path.GetDirectoryName(this.dirTreeView1.filepath);
      }
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
    }

    private void itemButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      if (Directory.Exists(antPanel.itemPath))
      {
        this.currentRootDir = antPanel.itemPath;
      }
      else if (File.Exists(antPanel.itemPath))
      {
        this.currentRootDir = Path.GetDirectoryName(antPanel.itemPath);
      }
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
    }

    private void projectButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      if (File.Exists(antPanel.projectPath))
      {
        this.currentRootDir = Path.GetDirectoryName(antPanel.projectPath);
      }
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();

    }
  }
}
