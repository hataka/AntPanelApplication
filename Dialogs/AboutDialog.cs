using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//using PluginCore.Localization;
//using FlashDevelop.Helpers;
//using PluginCore.Helpers;
//using PluginCore;

namespace AntPanelApplication.Dialogs
{
    public class AboutDialog : Form
    {
        private System.Windows.Forms.Label copyLabel;
        private System.Windows.Forms.LinkLabel versionLabel;
        private System.Windows.Forms.PictureBox imageBox;
    global::AntPanelApplication.Properties.Settings settings
          = new global::AntPanelApplication.Properties.Settings();

    public AboutDialog()
        {
            this.Owner = Globals.MainForm;
            this.Font = this.settings.DefaultFont;
            this.InitializeComponent();
            this.ApplyLocalizedTexts();
            this.InitializeGraphics();
        }

        #region Windows Forms Designer Generated Code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent() 
        {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
      this.imageBox = new System.Windows.Forms.PictureBox();
      this.copyLabel = new System.Windows.Forms.Label();
      this.versionLabel = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
      this.SuspendLayout();
      // 
      // imageBox
      // 
      this.imageBox.Image = ((System.Drawing.Image)(resources.GetObject("imageBox.Image")));
      this.imageBox.Location = new System.Drawing.Point(0, 0);
      this.imageBox.Name = "imageBox";
      this.imageBox.Size = new System.Drawing.Size(514, 244);
      this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.imageBox.TabIndex = 0;
      this.imageBox.TabStop = false;
      this.imageBox.Click += new System.EventHandler(this.DialogCloseClick);
      // 
      // copyLabel
      // 
      this.copyLabel.AutoSize = true;
      this.copyLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
      this.copyLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.copyLabel.ForeColor = System.Drawing.Color.DarkGray;
      this.copyLabel.Location = new System.Drawing.Point(29, 192);
      this.copyLabel.Name = "copyLabel";
      this.copyLabel.Size = new System.Drawing.Size(451, 30);
      this.copyLabel.TabIndex = 0;
      this.copyLabel.Text = "FlashDevelop logo, domain and the name are copyright of Mika Palmu.\r\nDevelopment:" +
    " Mika Palmu, Philippe Elsass and all helpful contributors.";
      this.copyLabel.Click += new System.EventHandler(this.DialogCloseClick);
      // 
      // versionLabel
      // 
      this.versionLabel.AutoSize = true;
      this.versionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
      this.versionLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.versionLabel.ForeColor = System.Drawing.Color.DarkGray;
      this.versionLabel.LinkColor = System.Drawing.Color.DarkGray;
      this.versionLabel.Location = new System.Drawing.Point(26, 172);
      this.versionLabel.Name = "versionLabel";
      this.versionLabel.Size = new System.Drawing.Size(373, 15);
      this.versionLabel.TabIndex = 0;
      this.versionLabel.TabStop = true;
      this.versionLabel.Text = "FlashDevelop 5.0.0.99 for .NET 3.5 (master#1234567890)";
      this.versionLabel.Click += new System.EventHandler(this.DialogCloseClick);
      // 
      // AboutDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(514, 244);
      this.Controls.Add(this.copyLabel);
      this.Controls.Add(this.versionLabel);
      this.Controls.Add(this.imageBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = " About FlashDevelop";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DialogKeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        #region Methods And Event Handlers

        /// <summary>
        /// Attaches the image to the imagebox
        /// </summary>
        private void InitializeGraphics()
        {
      //FIXME      
      //Stream stream = ResourceHelper.GetStream("AboutDialog.jpg");
            //this.imageBox.Image = Image.FromStream(stream);
        }

        /// <summary>
        /// Applies the localized texts to the form
        /// </summary>
        private void ApplyLocalizedTexts()
        {
            String name = Application.ProductName;
            this.Text = " " + "Title.AboutDialog";
            this.versionLabel.Font = new Font(this.Font, FontStyle.Bold);
            this.versionLabel.Text = name;
            Regex shaRegex = new Regex("#([a-f0-9]*)");
      String sha = String.Empty;// shaRegex.Match(name).Captures[0].ToString().Remove(0, 1);
            String link = "www.github.com/fdorg/flashdevelop/commit/" + sha;
            this.versionLabel.Links.Add(new LinkLabel.Link(name.IndexOf('('), versionLabel.Text.Length, link));
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(versionLabel, link);
        }

        /// <summary>
        /// When user clicks the link, open the Github commit in the browser.
        /// </summary>
        private void VersionLabelLinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as String;
            if (target != null) System.Diagnostics.Process.Start(target);
        }

        /// <summary>
        /// Closes the about dialog
        /// </summary>
        private void DialogKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Closes the about dialog
        /// </summary>
        private void DialogCloseClick(Object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Shows the about dialog
        /// </summary>
        public static new void Show()
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();
        }

        #endregion

    }
    
}
