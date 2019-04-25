using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace XMLSettings
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Settings settings;
		private System.Windows.Forms.Button serialize;
		private System.Windows.Forms.Button deserialize;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			settings = new Settings();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.propertyGrid = new System.Windows.Forms.PropertyGrid();
      this.serialize = new System.Windows.Forms.Button();
      this.deserialize = new System.Windows.Forms.Button();
      this.ofd = new System.Windows.Forms.OpenFileDialog();
      this.sfd = new System.Windows.Forms.SaveFileDialog();
      this.SuspendLayout();
      // 
      // propertyGrid
      // 
      this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
      this.propertyGrid.Location = new System.Drawing.Point(11, 9);
      this.propertyGrid.Name = "propertyGrid";
      this.propertyGrid.Size = new System.Drawing.Size(218, 264);
      this.propertyGrid.TabIndex = 0;
      // 
      // serialize
      // 
      this.serialize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.serialize.Location = new System.Drawing.Point(5, 282);
      this.serialize.Name = "serialize";
      this.serialize.Size = new System.Drawing.Size(105, 27);
      this.serialize.TabIndex = 1;
      this.serialize.Text = "Serialize";
      this.serialize.Click += new System.EventHandler(this.serialize_Click);
      // 
      // deserialize
      // 
      this.deserialize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.deserialize.Location = new System.Drawing.Point(128, 282);
      this.deserialize.Name = "deserialize";
      this.deserialize.Size = new System.Drawing.Size(105, 27);
      this.deserialize.TabIndex = 2;
      this.deserialize.Text = "Deserialize";
      this.deserialize.Click += new System.EventHandler(this.deserialize_Click);
      // 
      // ofd
      // 
      this.ofd.DefaultExt = "xml";
      this.ofd.Filter = "Settings Files (*.xml)|*.xml";
      this.ofd.Title = "Load Settings";
      // 
      // sfd
      // 
      this.sfd.DefaultExt = "xml";
      this.sfd.FileName = "settings";
      this.sfd.Filter = "Settings Files (*.xml)|*.xml";
      this.sfd.Title = "Save Settings";
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(11, 26);
      this.ClientSize = new System.Drawing.Size(240, 323);
      this.Controls.Add(this.deserialize);
      this.Controls.Add(this.serialize);
      this.Controls.Add(this.propertyGrid);
      this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Name = "Form1";
      this.Text = "XML Settings Demo";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			propertyGrid.SelectedObject = settings;
		}

		private void serialize_Click(object sender, System.EventArgs e)
		{
			string filename = Settings.SettingsDirectory + "\\settings.xml";

			if( MessageBox.Show("Would you like to save the settings in \r\n" + 
				"\"" + filename + "\"",
				"Save in default location?", 
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{

				if( sfd.ShowDialog() == DialogResult.OK )
				{
					filename = sfd.FileName;
				}
				else
				{
					return ;
				}
			}

			Settings.SaveSettingsToFile(filename, settings);
		}

		private void deserialize_Click(object sender, System.EventArgs e)
		{
			if( ofd.InitialDirectory == "" )
				ofd.InitialDirectory = Settings.SettingsDirectory;

			if( ofd.ShowDialog() == DialogResult.OK )
			{
				settings = Settings.LoadSettingsFromFile(ofd.FileName);
				propertyGrid.SelectedObject = settings;
			}
		}
	}
}
