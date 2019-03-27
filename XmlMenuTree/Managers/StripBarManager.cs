using PluginCore;
using PluginCore.Helpers;
using PluginCore.Localization;
using PluginCore.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AntPlugin.XmlTreeMenu.Managers
{
	internal static class StripBarManager
	{
    public static PluginUI pluginUI = null;//

    public static List<ToolStripItem> Items = new List<ToolStripItem>();
    public static ImageList imageList1;

		public static ToolStripItem FindMenuItem(string name)
		{
			for (int i = 0; i < StripBarManager.Items.Count; i++)
			{
				ToolStripItem toolStripItem = StripBarManager.Items[i];
				if (toolStripItem.Name == name)
				{
					return toolStripItem;
				}
			}
			return null;
		}

		public static List<ToolStripItem> FindMenuItems(string name)
		{
			List<ToolStripItem> list = new List<ToolStripItem>();
			for (int i = 0; i < StripBarManager.Items.Count; i++)
			{
				ToolStripItem toolStripItem = StripBarManager.Items[i];
				if (toolStripItem.Name == name)
				{
					list.Add(toolStripItem);
				}
			}
			return list;
		}

		public static ToolStrip GetToolStrip(string file)
		{
			// FIXED
			//ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PluginUI));
			Bitmap value = new Bitmap(Path.Combine(PathHelper.BaseDir, @"SettingData\pspad.bmp"));
      //StripBarManager.imageList1 = XMLTreeMenu.PluginUI.imageList2;
      ToolStrip toolStrip = new ToolStrip();
			XmlNode xmlNode = XmlHelper.LoadXmlDocument(file);
			foreach (XmlNode node in xmlNode.ChildNodes)
			{
				StripBarManager.FillToolItems(toolStrip.Items, node);
			}
			return toolStrip;
		}

		public static ToolStrip GetToolStripFromString(string xml)
		{
      // FIXED
      //Bitmap value = new Bitmap(Path.Combine(PathHelper.BaseDir, @"SettingData\pspad.bmp"));
      //imageList1.Images.AddStrip(value);
      StripBarManager.imageList1 = AntPlugin.PluginUI.imageList2;

      ToolStrip toolStrip = new ToolStrip();

			XmlNode xmlNode = LoadXmlDocumentFromString(xml);
			foreach (XmlNode node in xmlNode.ChildNodes)
			{
				StripBarManager.FillToolItems(toolStrip.Items, node);
			}
			return toolStrip;
		}

		/// <summary>
		/// Reads a xml string and returns it as a XmlNode. Returns null on failure.
		/// Hack added 
		/// </summary>
		public static XmlNode LoadXmlDocumentFromString(String xml)
		{
			try
			{
				XmlDocument document = new XmlDocument();
				document.PreserveWhitespace = false;
				document.LoadXml(xml);
				try
				{
					XmlNode declNode = document.FirstChild;
					XmlNode rootNode = declNode.NextSibling;
					//return rootNode;
					return declNode;
				}
				catch (Exception ex1)
				{
					ErrorManager.ShowError(ex1);
					return null;
				}
			}
			catch (Exception ex2)
			{
				ErrorManager.ShowError(ex2);
				return null;
			}
		}
		
		public static ContextMenuStrip GetContextMenu(string file)
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			XmlNode xmlNode = XmlHelper.LoadXmlDocument(file);
			foreach (XmlNode node in xmlNode.ChildNodes)
			{
				StripBarManager.FillMenuItems(contextMenuStrip.Items, node);
			}
			return contextMenuStrip;
		}

    public static MenuStrip GetMenuStrip(string file)
		{
			MenuStrip menuStrip = new MenuStrip();
			XmlNode xmlNode = XmlHelper.LoadXmlDocument(file);
			foreach (XmlNode node in xmlNode.ChildNodes)
			{
				StripBarManager.FillMenuItems(menuStrip.Items, node);
			}
			return menuStrip;
		}

    public static MenuStrip GetMenuStripFromString(string xml)
		{
      StripBarManager.imageList1 = AntPlugin.PluginUI.imageList2;
      MenuStrip menuStrip = new MenuStrip();
			XmlNode xmlNode = LoadXmlDocumentFromString(xml);
			foreach (XmlNode node in xmlNode.ChildNodes)
			{
				StripBarManager.FillMenuItems(menuStrip.Items, node);
			}
			return menuStrip;
		}

    public static List<ToolStripMenuItem> GetMenuItemsFromString(string xml)
    {
      StripBarManager.imageList1 = AntPlugin.PluginUI.imageList2;
      //MenuStrip menuStrip = new MenuStrip();
      List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
      XmlNode xmlNode = LoadXmlDocumentFromString(xml);
      string name;
      foreach (XmlNode node in xmlNode.ChildNodes)
      {
        //StripBarManager.FillMenuItems(menuStrip.Items, node);

        if ((name = node.Name) != null)
        {
          if (name == "menu")
          {
            menuItems.Add(StripBarManager.GetMenu(node));
          }
          else if (name == "separator")
          {
            //menuItems.Add((ToolStripMenuItem)StripBarManager.GetSeparator(node));
          }
          else if (!(name == "button"))
          {
          }
          else
          {
            ToolStripMenuItem menuItem = StripBarManager.GetMenuItem(node);
            menuItems.Add(menuItem);
            string menuItemId = StripBarManager.GetMenuItemId(menuItem);
            if (menuItemId.IndexOf('.') > -1)
            {
              PluginBase.MainForm.RegisterShortcutItem(StripBarManager.GetMenuItemId(menuItem), menuItem);
            }
          }
        }
      }
      return menuItems;
      //return menuStrip;
    }

    public static void FillMenuItems(ToolStripItemCollection items, XmlNode node)
		{
			string name;
			if ((name = node.Name) != null)
			{
				if (name == "menu")
				{
					items.Add(StripBarManager.GetMenu(node));
					return;
				}
				if (name == "separator")
				{
					items.Add(StripBarManager.GetSeparator(node));
					return;
				}
				if (!(name == "button"))
				{
					return;
				}
				ToolStripMenuItem menuItem = StripBarManager.GetMenuItem(node);
				items.Add(menuItem);
				string menuItemId = StripBarManager.GetMenuItemId(menuItem);
				if (menuItemId.IndexOf('.') > -1)
				{
					PluginBase.MainForm.RegisterShortcutItem(StripBarManager.GetMenuItemId(menuItem), menuItem);
				}
			}
		}

		public static void FillToolItems(ToolStripItemCollection items, XmlNode node)
		{
			string name;
			if ((name = node.Name) != null)
			{
				if (name == "separator")
				{
					items.Add(StripBarManager.GetSeparator(node));
					return;
				}
				if (name == "button")
				{
					items.Add(StripBarManager.GetButtonItem(node));
					return;
				}
				if (!(name == "dropdownbutton"))
				{
					return;
				}
        String attributeInclude = XmlHelper.GetAttribute(node, "include");

        if (attributeInclude != null && File.Exists(attributeInclude))
        {
          if (Path.GetExtension(attributeInclude).ToLower() == ".xml"
            || Path.GetExtension(attributeInclude).ToLower() == ".wsf")
          {
            try
            {
              ToolStripItem antDropDownButton = StripBarManager.GetAntDropDownButton(attributeInclude);
              items.Add(antDropDownButton);
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message.ToString(),"FillToolItems");
            }
          }
          else
          {
            ToolStripButton toolButton = new ToolStripButton();
            toolButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolButton.Name = Path.GetFileName(attributeInclude);
            toolButton.Text = Path.GetFileName(attributeInclude);
            //toolButton.Image = pluginUI.imageList.Images[10];
            toolButton.Image = pluginUI.imageList.Images[pluginUI.menuTree.GetIconImageIndex(attributeInclude)];
            toolButton.ToolTipText = attributeInclude;
            toolButton.Tag = attributeInclude;
            toolButton.Click += new System.EventHandler(antToolButton_Click);
            items.Add(toolButton);
          }
        }
        else
        {
          items.Add(StripBarManager.GetDropDownButtonItem(node));
        }
      }
		}

    public static void antToolButton_Click(object sender, EventArgs e)
    {
      ToolStripButton toolButton = sender as ToolStripButton;
      String path = toolButton.Tag as String;
      //MessageBox.Show(path);
      Process.Start(path);
    }

    public static ToolStripMenuItem GetMenu(XmlNode node)
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			string attribute = XmlHelper.GetAttribute(node, "name");
			string attribute2 = XmlHelper.GetAttribute(node, "image");
			string attribute3 = XmlHelper.GetAttribute(node, "imagelist");
			string attribute4 = XmlHelper.GetAttribute(node, "label");
			string attribute5 = XmlHelper.GetAttribute(node, "click");
			string attribute6 = XmlHelper.GetAttribute(node, "flags");
			string attribute7 = XmlHelper.GetAttribute(node, "enabled");
			string attribute8 = XmlHelper.GetAttribute(node, "tag");
			toolStripMenuItem.Tag = new ItemData(attribute4, attribute8, attribute6);
			toolStripMenuItem.Text = StripBarManager.GetLocalizedString(attribute4);
			if (attribute != null)
			{
				toolStripMenuItem.Name = attribute;
			}
			else
			{
				toolStripMenuItem.Name = attribute4.Replace("Label.", "");
			}
			if (attribute7 != null)
			{
				toolStripMenuItem.Enabled = Convert.ToBoolean(attribute7);
			}
			if (attribute2 != null)
			{
				toolStripMenuItem.Image = PluginBase.MainForm.FindImage(attribute2);
			}
			else if (attribute3 != null && StripBarManager.imageList1 != null)
			{
				int index;
				int.TryParse(attribute3, out index);
        toolStripMenuItem.Image = StripBarManager.imageList1.Images[index];
			}
			if (attribute5 != null)
			{
				toolStripMenuItem.Click += StripBarManager.GetEventHandler(attribute5);
			}
			foreach (XmlNode node2 in node.ChildNodes)
			{
				StripBarManager.FillMenuItems(toolStripMenuItem.DropDownItems, node2);
			}
			StripBarManager.Items.Add(toolStripMenuItem);
			return toolStripMenuItem;
		}

		public static ToolStripItem GetButtonItem(XmlNode node)
		{
			ToolStripButton toolStripButton = new ToolStripButton();
			string attribute = XmlHelper.GetAttribute(node, "name");
			string attribute2 = XmlHelper.GetAttribute(node, "image");
			string attribute3 = XmlHelper.GetAttribute(node, "imagelist");
			string attribute4 = XmlHelper.GetAttribute(node, "label");
			string attribute5 = XmlHelper.GetAttribute(node, "click");
			string attribute6 = XmlHelper.GetAttribute(node, "flags");
			string attribute7 = XmlHelper.GetAttribute(node, "enabled");
			string attribute8 = XmlHelper.GetAttribute(node, "tag");
			toolStripButton.Tag = new ItemData(attribute4, attribute8, attribute6);
			if (attribute != null)
			{
				toolStripButton.Name = attribute;
			}
			else
			{
				toolStripButton.Name = attribute4.Replace("Label.", "");
			}
			string strippedString = StripBarManager.GetStrippedString(StripBarManager.GetLocalizedString(attribute4), false);
			if (attribute2 != null)
			{
				toolStripButton.ToolTipText = strippedString;
			}
			else if (attribute3 != null && StripBarManager.imageList1 != null)
			{
				int index;
				int.TryParse(attribute3, out index);
        toolStripButton.Image = StripBarManager.imageList1.Images[index];
			}
			else
			{
				toolStripButton.Text = strippedString;
			}
			if (attribute7 != null)
			{
				toolStripButton.Enabled = Convert.ToBoolean(attribute7);
			}
			if (attribute2 != null)
			{
				toolStripButton.Image = PluginBase.MainForm.FindImage(attribute2);
			}
			if (attribute5 != null)
			{
				toolStripButton.Click += StripBarManager.GetEventHandler(attribute5);
			}
			StripBarManager.Items.Add(toolStripButton);
			return toolStripButton;
		}

		public static ToolStripItem GetDropDownButtonItem(XmlNode node)
		{
			ToolStripDropDownButton toolStripDropDownButton = new ToolStripDropDownButton();
			toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			string attributeName = XmlHelper.GetAttribute(node, "name");
			string attributeImage = XmlHelper.GetAttribute(node, "image");
			string attributeImageList = XmlHelper.GetAttribute(node, "imagelist");
			string attributeLabel = XmlHelper.GetAttribute(node, "label");
			string attributeClick = XmlHelper.GetAttribute(node, "click");
			string attributeFlags = XmlHelper.GetAttribute(node, "flags");
			string attributeEnabled = XmlHelper.GetAttribute(node, "enabled");
			string attributeTag = XmlHelper.GetAttribute(node, "tag");
      string attributeTooltip = XmlHelper.GetAttribute(node, "tooltip");

      toolStripDropDownButton.Tag = new ItemData(attributeLabel, attributeTag, attributeFlags);
			if (attributeName != null)
			{
				toolStripDropDownButton.Name = attributeName;
			}
			else
			{
				toolStripDropDownButton.Name = attributeLabel.Replace("Label.", "");
			}
			if (attributeImage != null)
			{
				toolStripDropDownButton.ToolTipText = attributeLabel;
			}
			else
			{
				toolStripDropDownButton.Text = attributeLabel;
			}
			if (attributeName != null)
			{
				toolStripDropDownButton.Name = attributeName;
			}
			else
			{
				toolStripDropDownButton.Name = attributeLabel;
			}
			if (attributeEnabled != null)
			{
				toolStripDropDownButton.Enabled = Convert.ToBoolean(attributeEnabled);
			}
			if (attributeImage != null)
			{
				toolStripDropDownButton.Image = PluginBase.MainForm.FindImage(attributeImage);
			}
			else if (attributeImageList != null && StripBarManager.imageList1 != null)
			{
				int index;
				int.TryParse(attributeImageList, out index);
        toolStripDropDownButton.Image = StripBarManager.imageList1.Images[index];
			}
      if (attributeTooltip != null)
      {
        toolStripDropDownButton.ToolTipText = attributeTooltip;
      }

      if (attributeClick != null)
			{
				toolStripDropDownButton.Click += StripBarManager.GetEventHandler(attributeClick);
			}
      
      foreach (XmlNode node2 in node.ChildNodes)
			{
				StripBarManager.FillMenuItems(toolStripDropDownButton.DropDownItems, node2);
			}
      StripBarManager.Items.Add(toolStripDropDownButton);
			return toolStripDropDownButton;
		}

    public static ToolStripItem GetAntDropDownButton(string file)
    {
      XmlDocument xml = new XmlDocument();
      xml.PreserveWhitespace = true;
      xml.Load(file);

      XmlAttribute defTargetAttr = xml.DocumentElement.Attributes["default"];
      String defaultTarget = (defTargetAttr != null) ? defTargetAttr.InnerText : "";
      ToolStripMenuItem targetItem;

      XmlAttribute nameAttr = xml.DocumentElement.Attributes["name"];
      String projectName = (nameAttr != null) ? nameAttr.InnerText : Path.GetFileName(file);

      XmlAttribute descrAttr = xml.DocumentElement.Attributes["description"];
      String description = (descrAttr != null) ? descrAttr.InnerText : "";

      XmlNodeList elm = xml.GetElementsByTagName("description");

      if (elm.Count > 0) description = elm.Item(0).InnerText.Trim();
      else description = file;
      if (projectName.Length == 0) projectName = Path.GetFileName(file);

      ToolStripDropDownButton toolStripDropDownButton = new ToolStripDropDownButton();
      toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      toolStripDropDownButton.Image = pluginUI.imageList.Images[0];
      toolStripDropDownButton.Text = projectName;

      if (Path.GetExtension(file).ToLower() == ".wsf")
      {
        toolStripDropDownButton = new ToolStripDropDownButton();
        toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripDropDownButton.Image = pluginUI.imageList.Images[36];
        toolStripDropDownButton.Name = projectName;
        toolStripDropDownButton.Text = projectName;
        toolStripDropDownButton.ToolTipText = description;
        toolStripDropDownButton.AccessibleDescription = file;
        toolStripDropDownButton.Tag = file;
      }

      if (Path.GetExtension(file).ToLower() == ".fdp")
      {
        toolStripDropDownButton = new ToolStripDropDownButton();
        toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        toolStripDropDownButton.Image = pluginUI.imageList.Images[37];
        toolStripDropDownButton.Name = projectName;
        toolStripDropDownButton.Text = projectName;
        toolStripDropDownButton.ToolTipText = description;
        toolStripDropDownButton.AccessibleDescription = file;
        toolStripDropDownButton.Tag = file;
      }

      XmlNodeList nodes = xml.DocumentElement.ChildNodes;
      int nodeCount = nodes.Count;
      for (int i = 0; i < nodeCount; i++)
      {
        XmlNode child = nodes[i];
        switch (child.Name)
        {
          case "target":
            String targetName=String.Empty;
            // skip private targets
            XmlAttribute targetNameAttr = child.Attributes["name"];
            if (targetNameAttr != null)
            {
              targetName = targetNameAttr.InnerText;
              if (!String.IsNullOrEmpty(targetName) && (targetName[0] == '-')) continue;
            }
            else targetName = defaultTarget;
            targetItem = GetBuildTargetMenuItem(child, defaultTarget);
            targetItem.AccessibleDescription = file;
            targetItem.Tag = targetName; 
            targetItem.Click += new System.EventHandler(pluginUI.antDropDownMenuItem_Click);

            if(toolStripDropDownButton!=null) toolStripDropDownButton.DropDownItems.Add(targetItem);
            break;

          //Kahata TimeStamp: 2018-02-08
          case "job":
            String jobName;
            XmlAttribute jobNameAttr = child.Attributes["id"];
            if (jobNameAttr != null)
            {
              jobName = jobNameAttr.InnerText;
              if (!String.IsNullOrEmpty(jobName) && (jobName[0] == '-')) continue;
            }
            else jobName = "default";
            targetItem = GetBuildTargetMenuItem(child, defaultTarget);
            targetItem.AccessibleDescription = file;
            targetItem.Tag = jobName;
            targetItem.Click += new System.EventHandler(pluginUI.antDropDownMenuItem_Click);
            if (toolStripDropDownButton != null) toolStripDropDownButton.DropDownItems.Add(targetItem);
            break;
          default:
            break;
        }
      }
      return toolStripDropDownButton;
    }

    public static ToolStripMenuItem GetBuildTargetMenuItem(XmlNode node, string defaultTarget)
    {
      XmlAttribute nameAttr = node.Attributes["name"];
      XmlAttribute resourceAttr = node.Attributes["resource"];
      String targetName = "";

      // fix 2017-01-07
      try
      {
        targetName = (nameAttr != null) ? nameAttr.InnerText : resourceAttr.InnerText;
      }
      catch
      {
        targetName = (nameAttr != null) ? nameAttr.InnerText : "";
      }


      XmlAttribute descrAttr = node.Attributes["description"];
      String description = (descrAttr != null) ? descrAttr.InnerText : "";

      ToolStripMenuItem targetMenuItem;
      //int access;

      switch (node.Name)
      {
        case "taskdef":
          targetMenuItem = new ToolStripMenuItem();
          targetMenuItem.Name = targetName;
          targetMenuItem.Text = targetName;
          targetMenuItem.ForeColor = Color.Blue;
          targetMenuItem.Image = pluginUI.imageList.Images[16];
          break;
        case "target":
        case "job":
          if (targetName == defaultTarget)
          {
            targetMenuItem = new ToolStripMenuItem();
            targetMenuItem.Name = targetName;
            targetMenuItem.Text = targetName;
            targetMenuItem.Image = pluginUI.imageList.Images[1];
            targetMenuItem.Font = new Font(
                pluginUI.treeView.Font.Name,
                pluginUI.treeView.Font.Size,
                FontStyle.Bold);
          }
          else if (description.Length > 0)
          {
            targetMenuItem = new ToolStripMenuItem();
            targetMenuItem.Name = targetName;
            targetMenuItem.Text = targetName;
            targetMenuItem.Image = pluginUI.imageList.Images[3];
            targetMenuItem.ToolTipText = description;
            if (description.StartsWith("Click"))
            {
              targetMenuItem.Font = new Font(
                  pluginUI.treeView.Font.Name,
                  pluginUI.treeView.Font.Size,
                  FontStyle.Bold);
              targetMenuItem.ForeColor = Color.Blue;
            }
            else if (description.StartsWith("Plugin"))
            {
              targetMenuItem.Font = new Font(
                  pluginUI.treeView.Font.Name,
                  pluginUI.treeView.Font.Size,
                  FontStyle.Bold);
              targetMenuItem.ForeColor = Color.Green;
            }
          }
          else
          {
            targetMenuItem = new ToolStripMenuItem();
            targetMenuItem.Name = targetName;
            targetMenuItem.Text = targetName;
            targetMenuItem.Image = pluginUI.imageList.Images[2];

          }
          break;
        default:
           targetMenuItem = new ToolStripMenuItem();
          targetMenuItem.Name = targetName;
          targetMenuItem.Text = targetName;
          targetMenuItem.Image = pluginUI.imageList.Images[16];
          break;
      }
      targetMenuItem.AccessibleDescription = targetName;
      targetMenuItem.Tag = targetName;
      targetMenuItem.ToolTipText = description;
      return targetMenuItem;
    }

    public static ToolStripMenuItem GetMenuItem(XmlNode node)
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			string attribute = XmlHelper.GetAttribute(node, "name");
			string attribute2 = XmlHelper.GetAttribute(node, "image");
			string attribute3 = XmlHelper.GetAttribute(node, "imagelist");
			string attribute4 = XmlHelper.GetAttribute(node, "label");
			string attribute5 = XmlHelper.GetAttribute(node, "click");
			string attribute6 = XmlHelper.GetAttribute(node, "enabled");
			string attribute7 = XmlHelper.GetAttribute(node, "shortcut");
			string attribute8 = XmlHelper.GetAttribute(node, "keytext");
			string attribute9 = XmlHelper.GetAttribute(node, "flags");
			string attribute10 = XmlHelper.GetAttribute(node, "tag");
      string attributeTooltip = XmlHelper.GetAttribute(node, "tooltip");
      toolStripMenuItem.Tag = new ItemData(attribute4, attribute10, attribute9);
			toolStripMenuItem.Text = StripBarManager.GetLocalizedString(attribute4);
			if (attribute != null)
			{
				toolStripMenuItem.Name = attribute;
			}
			else
			{
				toolStripMenuItem.Name = attribute4.Replace("Label.", "");
			}
			if (attribute2 != null)
			{
				toolStripMenuItem.Image = PluginBase.MainForm.FindImage(attribute2);
			}
			else if (attribute3 != null && StripBarManager.imageList1 != null)
			{
				int index;
				int.TryParse(attribute3, out index);
        toolStripMenuItem.Image = StripBarManager.imageList1.Images[index];
			}
			if (attribute6 != null)
			{
				toolStripMenuItem.Enabled = Convert.ToBoolean(attribute6);
			}
			if (attribute8 != null)
			{
				toolStripMenuItem.ShortcutKeyDisplayString = StripBarManager.GetKeyText(attribute8);
			}
      if (attributeTooltip != null)
      {
        toolStripMenuItem.ToolTipText = attributeTooltip;
      }
      if (attribute5 != null)
			{
				toolStripMenuItem.Click += StripBarManager.GetEventHandler(attribute5);
			}
			if (attribute7 != null)
			{
				toolStripMenuItem.ShortcutKeys = StripBarManager.GetKeys(attribute7);
			}
			StripBarManager.Items.Add(toolStripMenuItem);
			return toolStripMenuItem;
		}

		public static ToolStripSeparator GetSeparator(XmlNode node)
		{
			return new ToolStripSeparator();
		}

		private static string GetStrippedString(string text, bool removeWhite)
		{
			text = text.Replace("&", "");
			text = text.Replace("...", "");
			if (removeWhite)
			{
				text = text.Replace(" ", "");
				text = text.Replace("\t", "");
			}
			return text;
		}

		private static string GetLocalizedString(string key)
		{
			string result;
			try
			{
				if (!key.StartsWith("Label."))
				{
					result = key;
				}
				else
				{
					result = TextHelper.GetString(key);
				}
			}
			catch (Exception ex)
			{
				ErrorManager.ShowError(ex);
				result = string.Empty;
			}
			return result;
		}

		public static string GetMenuItemId(ToolStripMenuItem menu)
		{
			if (menu.OwnerItem != null)
			{
				ToolStripMenuItem menu2 = menu.OwnerItem as ToolStripMenuItem;
				return StripBarManager.GetMenuItemId(menu2) + "." + StripBarManager.GetStrippedString(menu.Name, true);
			}
			return StripBarManager.GetStrippedString(menu.Name, true);
		}

		private static string GetKeyText(string data)
		{
			data = data.Replace("|", "+");
			data = data.Replace("Control", "Ctrl");
			return data;
		}

		private static Keys GetKeys(string data)
		{
			Keys result;
			try
			{
				Keys keys = Keys.None;
				string[] array = data.Split(new char[]
				{
					'|'
				});
				for (int i = 0; i < array.Length; i++)
				{
					keys |= (Keys)Enum.Parse(typeof(Keys), array[i]);
				}
				result = keys;
			}
			catch (Exception ex)
			{
				ErrorManager.ShowError(ex);
				result = Keys.None;
			}
			return result;
		}

		private static EventHandler GetEventHandler(string method)
		{
			EventHandler result;
			try
			{
				result = (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), PluginBase.MainForm, method);
			}
			catch (Exception ex)
			{
				ErrorManager.ShowError(ex);
				result = null;
			}
			return result;
		}
    
    /*
		public static void AddIcon(string name)
		{
			string fileName = Path.Combine(PathHelper.BaseDir, "SettingData\\icons\\" + name);
			Icon icon = new Icon(fileName, 16, 16);
			Bitmap value = icon.ToBitmap();
			icon.Dispose();
			StripBarManager.imageList1.Images.Add(value);
		}
    */
  }
}
