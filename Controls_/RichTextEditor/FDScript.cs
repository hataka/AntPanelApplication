//css_ref RichTextEditor.dll;

using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using FlashDevelop;
using FlashDevelop.Dialogs;
using PluginCore;
using PluginCore.Utilities;
using PluginCore.Managers;
using PluginCore.Helpers;
using WeifenLuo.WinFormsUI.Docking;
using ScintillaNet;
using CommonLibrary;
using MDIForm;

public class FDScript 
{
	public static DockContent document;
	public static RichTextEditor form;// = new RichTextEditor(); 
	//public static List<string> previousRichTextEditorDocuments;
	
	/**
	 * Entry point of the script.
	 */
	public static void Execute() 
	{
		form = new RichTextEditor();
		string path = Globals.MainForm.CurrentDocument.FileName;
		//form.Dock = System.Windows.Forms.DockStyle.Fill;
		//document = Globals.MainForm.CreateCustomDocument(form);
		//document.Text = "RichTextEditor";
		//IntializeSettings();
		//((Form)document).FormClosing += new FormClosingEventHandler(ParentForm_Closing);
		//form.LoadFile(path);
		//Test();
	}
/*
	public static void IntializeSettings()
	{
		//String ProjectManager_Guid = "30018864-fadd-1122-b2a5-779832cbbf23";
		//String FileExplorer_Guid   = "f534a520-bcc7-4fe4-a4b9-6931948b2686";
		//String FTPClient_Guid      = "42ac7fab-421b-1f38-a985-72a03534f733";		
		String XMLTreeMenu_Guid    = "0538077E-8C37-4A2B-962B-8FB77DC9F325";		
		XMLTreeMenu.PluginMain a
			= (XMLTreeMenu.PluginMain)Globals.MainForm.FindPlugin(XMLTreeMenu_Guid);
		XMLTreeMenu.Settings settings = a.Settings;
		previousRichTextEditorDocuments = (List<string>)settings.PreviousRichTextEditorDocuments;
		form.previousDocuments = previousRichTextEditorDocuments;
		form.PopulatePreviousDocumentsMenu();
	}
	
	public static void ParentForm_Closing(object sender, EventArgs e)
	{
		previousRichTextEditorDocuments = form.previousDocuments;
	}	
*/
	}
