using AntPlugin.CommonLibrary;
using PluginCore;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AntPlugin.XMLTreeMenu.Dialogs
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
			this.cancelButton = new Button();
			this.replacedHeader = new ColumnHeader();
			this.executeDirectoryComboBox = new ComboBox();
			this.executedirectoryLabel = new Label();
			this.settingButton = new Button();
			this.optionsGroupBox = new GroupBox();
			this.panelOutputCheckBox = new CheckBox();
			this.consoleOutputCheckBox = new CheckBox();
			this.argumentComboBox = new ComboBox();
			this.argumentLabel = new Label();
			this.commandComboBox = new ComboBox();
			this.commandLabel = new Label();
			this.executeButton = new Button();
			this.closeButton = new Button();
			this.commandButton = new Button();
			this.executedirectoryButton = new Button();
			this.argumentButton = new Button();
			this.optionsGroupBox.SuspendLayout();
			base.SuspendLayout();
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(413, 138);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new Size(90, 21);
			this.cancelButton.TabIndex = 41;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
			this.replacedHeader.Text = "Replacements";
			this.replacedHeader.Width = 120;
			this.executeDirectoryComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.executeDirectoryComboBox.FlatStyle = FlatStyle.System;
			this.executeDirectoryComboBox.Location = new Point(17, 98);
			this.executeDirectoryComboBox.Name = "executeDirectoryComboBox";
			this.executeDirectoryComboBox.Size = new Size(293, 20);
			this.executeDirectoryComboBox.TabIndex = 34;
			this.executeDirectoryComboBox.Text = ".ext";
			this.executedirectoryLabel.AutoSize = true;
			this.executedirectoryLabel.FlatStyle = FlatStyle.System;
			this.executedirectoryLabel.Location = new Point(18, 84);
			this.executedirectoryLabel.Name = "executedirectoryLabel";
			this.executedirectoryLabel.Size = new Size(84, 12);
			this.executedirectoryLabel.TabIndex = 45;
			this.executedirectoryLabel.Text = "起動ディレクトリ：";
			this.settingButton.DialogResult = DialogResult.Yes;
			this.settingButton.Enabled = false;
			this.settingButton.FlatStyle = FlatStyle.System;
			this.settingButton.Location = new Point(311, 138);
			this.settingButton.Name = "settingButton";
			this.settingButton.Size = new Size(90, 21);
			this.settingButton.TabIndex = 39;
			this.settingButton.Text = "Option設定";
			this.settingButton.Click += new EventHandler(this.replaceButton_Click);
			this.optionsGroupBox.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.optionsGroupBox.Controls.Add(this.panelOutputCheckBox);
			this.optionsGroupBox.Controls.Add(this.consoleOutputCheckBox);
			this.optionsGroupBox.FlatStyle = FlatStyle.System;
			this.optionsGroupBox.Location = new Point(352, 20);
			this.optionsGroupBox.Name = "optionsGroupBox";
			this.optionsGroupBox.Size = new Size(151, 100);
			this.optionsGroupBox.TabIndex = 37;
			this.optionsGroupBox.TabStop = false;
			this.optionsGroupBox.Text = " オプション";
			this.panelOutputCheckBox.AutoSize = true;
			this.panelOutputCheckBox.FlatStyle = FlatStyle.System;
			this.panelOutputCheckBox.Location = new Point(12, 18);
			this.panelOutputCheckBox.Name = "panelOutputCheckBox";
			this.panelOutputCheckBox.Size = new Size(92, 17);
			this.panelOutputCheckBox.TabIndex = 1;
			this.panelOutputCheckBox.Text = "PanelOutput";
			this.consoleOutputCheckBox.AutoSize = true;
			this.consoleOutputCheckBox.FlatStyle = FlatStyle.System;
			this.consoleOutputCheckBox.Location = new Point(12, 38);
			this.consoleOutputCheckBox.Name = "consoleOutputCheckBox";
			this.consoleOutputCheckBox.Size = new Size(105, 17);
			this.consoleOutputCheckBox.TabIndex = 2;
			this.consoleOutputCheckBox.Text = "ConsoleOutput";
			this.argumentComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.argumentComboBox.FlatStyle = FlatStyle.System;
			this.argumentComboBox.Items.AddRange(new object[]
			{
				"/k ",
				"/c ",
				"//nologo "
			});
			this.argumentComboBox.Location = new Point(17, 62);
			this.argumentComboBox.Name = "argumentComboBox";
			this.argumentComboBox.Size = new Size(293, 20);
			this.argumentComboBox.TabIndex = 33;
			this.argumentLabel.AutoSize = true;
			this.argumentLabel.FlatStyle = FlatStyle.System;
			this.argumentLabel.Location = new Point(18, 48);
			this.argumentLabel.Name = "argumentLabel";
			this.argumentLabel.Size = new Size(73, 12);
			this.argumentLabel.TabIndex = 44;
			this.argumentLabel.Text = "アーギュメント：";
			this.commandComboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.commandComboBox.FlatStyle = FlatStyle.System;
			this.commandComboBox.Items.AddRange(new object[]
			{
				"C:\\Windows\\System32\\cmd.exe",
				"C:\\Windows\\System32\\Wscript.exe",
				"C:\\Windows\\System32\\Cscript.exe",
				"java",
				"appletviewer",
				"F:\\Programs\\csscript\\cscs.exe",
				"F:\\Programs\\csscript\\csws.exe"
			});
			this.commandComboBox.Location = new Point(17, 26);
			this.commandComboBox.Name = "commandComboBox";
			this.commandComboBox.Size = new Size(293, 20);
			this.commandComboBox.TabIndex = 32;
			this.commandComboBox.Text = "C:\\Windows\\System32\\cmd.exe";
			this.commandLabel.AutoSize = true;
			this.commandLabel.FlatStyle = FlatStyle.System;
			this.commandLabel.Location = new Point(18, 12);
			this.commandLabel.Name = "commandLabel";
			this.commandLabel.Size = new Size(46, 12);
			this.commandLabel.TabIndex = 43;
			this.commandLabel.Text = "コマンド：";
			this.executeButton.DialogResult = DialogResult.OK;
			this.executeButton.FlatStyle = FlatStyle.System;
			this.executeButton.Location = new Point(209, 138);
			this.executeButton.Name = "executeButton";
			this.executeButton.Size = new Size(90, 21);
			this.executeButton.TabIndex = 38;
			this.executeButton.Text = "実行";
			this.executeButton.Click += new EventHandler(this.findButton_Click);
			this.closeButton.DialogResult = DialogResult.Cancel;
			this.closeButton.FlatStyle = FlatStyle.System;
			this.closeButton.Location = new Point(5, 6);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new Size(0, 0);
			this.closeButton.TabIndex = 47;
			this.closeButton.TabStop = false;
			this.commandButton.Location = new Point(316, 26);
			this.commandButton.Name = "commandButton";
			this.commandButton.Size = new Size(26, 22);
			this.commandButton.TabIndex = 48;
			this.commandButton.Click += new EventHandler(this.commandButton_Click);
			this.executedirectoryButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.executedirectoryButton.Location = new Point(315, 98);
			this.executedirectoryButton.Name = "executedirectoryButton";
			this.executedirectoryButton.Size = new Size(26, 22);
			this.executedirectoryButton.TabIndex = 49;
			this.executedirectoryButton.Click += new EventHandler(this.executedirectoryButton_Click);
			this.argumentButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.argumentButton.Location = new Point(315, 60);
			this.argumentButton.Name = "argumentButton";
			this.argumentButton.Size = new Size(26, 22);
			this.argumentButton.TabIndex = 50;
			this.argumentButton.Click += new EventHandler(this.argumentButton_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(515, 171);
			base.Controls.Add(this.argumentButton);
			base.Controls.Add(this.executedirectoryButton);
			base.Controls.Add(this.commandButton);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.executeDirectoryComboBox);
			base.Controls.Add(this.executedirectoryLabel);
			base.Controls.Add(this.settingButton);
			base.Controls.Add(this.optionsGroupBox);
			base.Controls.Add(this.argumentComboBox);
			base.Controls.Add(this.argumentLabel);
			base.Controls.Add(this.commandComboBox);
			base.Controls.Add(this.commandLabel);
			base.Controls.Add(this.executeButton);
			base.Controls.Add(this.closeButton);
			base.Name = "RunProcessDialog";
			this.Text = "ファイル名を指定して実行";
			base.Load += new EventHandler(this.RunProcessDialog_Load);
			this.optionsGroupBox.ResumeLayout(false);
			this.optionsGroupBox.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public RunProcessDialog()
		{
			this.InitializeComponent();
			Image image = PluginBase.MainForm.FindImage("203");
			this.commandButton.Image = image;
			this.executedirectoryButton.Image = image;
			this.argumentButton.Image = image;
		}

		private void RunProcessDialog_Load(object sender, EventArgs e)
		{
			if (PluginBase.MainForm.CurrentDocument.FileName != null)
			{
				this.executeDirectoryComboBox.Text = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName);
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
			if (PluginBase.MainForm.CurrentDocument.FileName != null)
			{
				initialDirectory = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName);
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
			if (PluginBase.MainForm.CurrentDocument.FileName != null)
			{
				initialDirectory = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName);
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
