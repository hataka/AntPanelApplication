using CommonLibrary;
using CommonInterface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AntPanelApplication.Dialogs
{
	public class RunProcessDialog : Form
	{
		private IContainer components=null;
		private Button cancelButton;
		private ColumnHeader replacedHeader;
		public ComboBox executeDirectoryComboBox;
		private Label executedirectoryLabel;
		private Button settingButton;
		private GroupBox optionsGroupBox;
		public CheckBox panelOutputCheckBox;
		public CheckBox consoleOutputCheckBox;
		public ComboBox argumentComboBox;
		private Label argumentLabel;
		public ComboBox commandComboBox;
		private Label commandLabel;
		private Button executeButton;
		private Button closeButton;
		private Button commandButton;
		private Button executedirectoryButton;
		private Button argumentButton;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunProcessDialog));
      this.cancelButton = new System.Windows.Forms.Button();
      this.replacedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.executeDirectoryComboBox = new System.Windows.Forms.ComboBox();
      this.executedirectoryLabel = new System.Windows.Forms.Label();
      this.settingButton = new System.Windows.Forms.Button();
      this.optionsGroupBox = new System.Windows.Forms.GroupBox();
      this.panelOutputCheckBox = new System.Windows.Forms.CheckBox();
      this.consoleOutputCheckBox = new System.Windows.Forms.CheckBox();
      this.argumentComboBox = new System.Windows.Forms.ComboBox();
      this.argumentLabel = new System.Windows.Forms.Label();
      this.commandComboBox = new System.Windows.Forms.ComboBox();
      this.commandLabel = new System.Windows.Forms.Label();
      this.executeButton = new System.Windows.Forms.Button();
      this.closeButton = new System.Windows.Forms.Button();
      this.commandButton = new System.Windows.Forms.Button();
      this.executedirectoryButton = new System.Windows.Forms.Button();
      this.argumentButton = new System.Windows.Forms.Button();
      this.optionsGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cancelButton.Location = new System.Drawing.Point(551, 172);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(120, 26);
      this.cancelButton.TabIndex = 41;
      this.cancelButton.Text = "キャンセル";
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // replacedHeader
      // 
      this.replacedHeader.Text = "Replacements";
      this.replacedHeader.Width = 120;
      // 
      // executeDirectoryComboBox
      // 
      this.executeDirectoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.executeDirectoryComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.executeDirectoryComboBox.Location = new System.Drawing.Point(23, 122);
      this.executeDirectoryComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.executeDirectoryComboBox.Name = "executeDirectoryComboBox";
      this.executeDirectoryComboBox.Size = new System.Drawing.Size(389, 23);
      this.executeDirectoryComboBox.TabIndex = 34;
      this.executeDirectoryComboBox.Text = ".ext";
      // 
      // executedirectoryLabel
      // 
      this.executedirectoryLabel.AutoSize = true;
      this.executedirectoryLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.executedirectoryLabel.Location = new System.Drawing.Point(24, 105);
      this.executedirectoryLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.executedirectoryLabel.Name = "executedirectoryLabel";
      this.executedirectoryLabel.Size = new System.Drawing.Size(106, 15);
      this.executedirectoryLabel.TabIndex = 45;
      this.executedirectoryLabel.Text = "起動ディレクトリ：";
      // 
      // settingButton
      // 
      this.settingButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.settingButton.Enabled = false;
      this.settingButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.settingButton.Location = new System.Drawing.Point(415, 172);
      this.settingButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.settingButton.Name = "settingButton";
      this.settingButton.Size = new System.Drawing.Size(120, 26);
      this.settingButton.TabIndex = 39;
      this.settingButton.Text = "Option設定";
      this.settingButton.Click += new System.EventHandler(this.replaceButton_Click);
      // 
      // optionsGroupBox
      // 
      this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.optionsGroupBox.Controls.Add(this.panelOutputCheckBox);
      this.optionsGroupBox.Controls.Add(this.consoleOutputCheckBox);
      this.optionsGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.optionsGroupBox.Location = new System.Drawing.Point(469, 25);
      this.optionsGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.optionsGroupBox.Name = "optionsGroupBox";
      this.optionsGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.optionsGroupBox.Size = new System.Drawing.Size(201, 125);
      this.optionsGroupBox.TabIndex = 37;
      this.optionsGroupBox.TabStop = false;
      this.optionsGroupBox.Text = " オプション";
      // 
      // panelOutputCheckBox
      // 
      this.panelOutputCheckBox.AutoSize = true;
      this.panelOutputCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.panelOutputCheckBox.Location = new System.Drawing.Point(16, 22);
      this.panelOutputCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.panelOutputCheckBox.Name = "panelOutputCheckBox";
      this.panelOutputCheckBox.Size = new System.Drawing.Size(111, 20);
      this.panelOutputCheckBox.TabIndex = 1;
      this.panelOutputCheckBox.Text = "PanelOutput";
      // 
      // consoleOutputCheckBox
      // 
      this.consoleOutputCheckBox.AutoSize = true;
      this.consoleOutputCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.consoleOutputCheckBox.Location = new System.Drawing.Point(16, 48);
      this.consoleOutputCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.consoleOutputCheckBox.Name = "consoleOutputCheckBox";
      this.consoleOutputCheckBox.Size = new System.Drawing.Size(128, 20);
      this.consoleOutputCheckBox.TabIndex = 2;
      this.consoleOutputCheckBox.Text = "ConsoleOutput";
      // 
      // argumentComboBox
      // 
      this.argumentComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.argumentComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.argumentComboBox.Items.AddRange(new object[] {
            "/k ",
            "/c ",
            "//nologo "});
      this.argumentComboBox.Location = new System.Drawing.Point(23, 78);
      this.argumentComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.argumentComboBox.Name = "argumentComboBox";
      this.argumentComboBox.Size = new System.Drawing.Size(389, 23);
      this.argumentComboBox.TabIndex = 33;
      // 
      // argumentLabel
      // 
      this.argumentLabel.AutoSize = true;
      this.argumentLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.argumentLabel.Location = new System.Drawing.Point(24, 60);
      this.argumentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.argumentLabel.Name = "argumentLabel";
      this.argumentLabel.Size = new System.Drawing.Size(92, 15);
      this.argumentLabel.TabIndex = 44;
      this.argumentLabel.Text = "アーギュメント：";
      // 
      // commandComboBox
      // 
      this.commandComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.commandComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.commandComboBox.Items.AddRange(new object[] {
            "C:\\Windows\\System32\\cmd.exe",
            "C:\\Windows\\System32\\Wscript.exe",
            "C:\\Windows\\System32\\Cscript.exe",
            "java",
            "appletviewer",
            "F:\\Programs\\csscript\\cscs.exe",
            "F:\\Programs\\csscript\\csws.exe"});
      this.commandComboBox.Location = new System.Drawing.Point(23, 32);
      this.commandComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.commandComboBox.Name = "commandComboBox";
      this.commandComboBox.Size = new System.Drawing.Size(389, 23);
      this.commandComboBox.TabIndex = 32;
      this.commandComboBox.Text = "C:\\Windows\\System32\\cmd.exe";
      // 
      // commandLabel
      // 
      this.commandLabel.AutoSize = true;
      this.commandLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.commandLabel.Location = new System.Drawing.Point(24, 15);
      this.commandLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.commandLabel.Name = "commandLabel";
      this.commandLabel.Size = new System.Drawing.Size(58, 15);
      this.commandLabel.TabIndex = 43;
      this.commandLabel.Text = "コマンド：";
      // 
      // executeButton
      // 
      this.executeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.executeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.executeButton.Location = new System.Drawing.Point(279, 172);
      this.executeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.executeButton.Name = "executeButton";
      this.executeButton.Size = new System.Drawing.Size(120, 26);
      this.executeButton.TabIndex = 38;
      this.executeButton.Text = "実行";
      this.executeButton.Click += new System.EventHandler(this.findButton_Click);
      // 
      // closeButton
      // 
      this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.closeButton.Location = new System.Drawing.Point(7, 8);
      this.closeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(0, 0);
      this.closeButton.TabIndex = 47;
      this.closeButton.TabStop = false;
      // 
      // commandButton
      // 
      this.commandButton.Image = ((System.Drawing.Image)(resources.GetObject("commandButton.Image")));
      this.commandButton.Location = new System.Drawing.Point(421, 32);
      this.commandButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.commandButton.Name = "commandButton";
      this.commandButton.Size = new System.Drawing.Size(35, 28);
      this.commandButton.TabIndex = 48;
      this.commandButton.Click += new System.EventHandler(this.commandButton_Click);
      // 
      // executedirectoryButton
      // 
      this.executedirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.executedirectoryButton.Image = ((System.Drawing.Image)(resources.GetObject("executedirectoryButton.Image")));
      this.executedirectoryButton.Location = new System.Drawing.Point(420, 122);
      this.executedirectoryButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.executedirectoryButton.Name = "executedirectoryButton";
      this.executedirectoryButton.Size = new System.Drawing.Size(35, 28);
      this.executedirectoryButton.TabIndex = 49;
      this.executedirectoryButton.Click += new System.EventHandler(this.executedirectoryButton_Click);
      // 
      // argumentButton
      // 
      this.argumentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.argumentButton.Image = ((System.Drawing.Image)(resources.GetObject("argumentButton.Image")));
      this.argumentButton.Location = new System.Drawing.Point(420, 75);
      this.argumentButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.argumentButton.Name = "argumentButton";
      this.argumentButton.Size = new System.Drawing.Size(35, 28);
      this.argumentButton.TabIndex = 50;
      this.argumentButton.Click += new System.EventHandler(this.argumentButton_Click);
      // 
      // RunProcessDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(687, 214);
      this.Controls.Add(this.argumentButton);
      this.Controls.Add(this.executedirectoryButton);
      this.Controls.Add(this.commandButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.executeDirectoryComboBox);
      this.Controls.Add(this.executedirectoryLabel);
      this.Controls.Add(this.settingButton);
      this.Controls.Add(this.optionsGroupBox);
      this.Controls.Add(this.argumentComboBox);
      this.Controls.Add(this.argumentLabel);
      this.Controls.Add(this.commandComboBox);
      this.Controls.Add(this.commandLabel);
      this.Controls.Add(this.executeButton);
      this.Controls.Add(this.closeButton);
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.Name = "RunProcessDialog";
      this.Text = "ファイル名を指定して実行";
      this.Load += new System.EventHandler(this.RunProcessDialog_Load);
      this.optionsGroupBox.ResumeLayout(false);
      this.optionsGroupBox.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		public RunProcessDialog()
		{
			this.InitializeComponent();
			//Image image = Globals.MainForm.FindImage("203");
			//this.commandButton.Image = image;
			//this.executedirectoryButton.Image = image;
			//this.argumentButton.Image = image;
		}

		private void RunProcessDialog_Load(object sender, EventArgs e)
		{
			if (Globals.MainForm.CurrentDocumentPath != null)
			{
				this.executeDirectoryComboBox.Text = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocumentPath);
			}
			else
			{
				this.executeDirectoryComboBox.Text = PluginBase.MainForm.WorkingDirectory;
			}
			this.argumentComboBox.Text = "/k ";
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void findButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void replaceButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void executedirectoryButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (Directory.Exists(this.executeDirectoryComboBox.Text))
			{
				folderBrowserDialog.SelectedPath = this.executeDirectoryComboBox.Text;
			}
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK && Directory.Exists(folderBrowserDialog.SelectedPath))
			{
				this.executeDirectoryComboBox.Text = folderBrowserDialog.SelectedPath;
				this.executeDirectoryComboBox.SelectionStart = this.executeDirectoryComboBox.Text.Length;
			}
		}

		private void commandButton_Click(object sender, EventArgs e)
		{
			string initialDirectory;
			if (Globals.MainForm.CurrentDocumentPath != null)
			{
				initialDirectory = Path.GetDirectoryName(Globals.MainForm.CurrentDocumentPath);
			}
			else
			{
				initialDirectory = PluginBase.MainForm.WorkingDirectory;
			}
			string filter = "実行ファイル(*.exe;*.bat;*.wsf)|*.exe;*.bat;*.wsf|すべてのファイル(*.*)|*.*";
			string fileName = "test.exe";
			this.commandComboBox.Text = Lib.File_OpenDialog(fileName, initialDirectory, filter);
		}

		private void argumentButton_Click(object sender, EventArgs e)
		{
			string initialDirectory;
			if (Globals.MainForm.CurrentDocumentPath != null)
			{
				initialDirectory = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocumentPath);
			}
			else
			{
				initialDirectory = PluginBase.MainForm.WorkingDirectory;
			}
			string filter = "実行ファイル(*.exe;*.bat;*.wsf)|*.exe;*.bat;*.wsf|textファイル(*.c;*.cpp;*.cs;*.java)|*.c;*.cpp;*.cs;*.java|すべてのファイル(*.*)|*.*";
			string fileName = "test.cs";
			this.argumentComboBox.Text = this.argumentComboBox.Text + " " + Lib.File_OpenDialog(fileName, initialDirectory, filter);
		}
	}
}
