using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSParser.Model;
using System.IO;
using AntPlugin.CommonLibrary;

namespace CSParser.BuildTree
{
	public class BuildTree : UserControl
	{
		private ImageList imageList;
		private String path;
		private string src;
		public   List<string> MemberId = new List<string>();
		public static bool ANT = false;
		/*   
		this.imageList.Images.SetKeyName(0, "ant_buildfile.png");
		this.imageList.Images.SetKeyName(1, "defaulttarget_obj.png");
		this.imageList.Images.SetKeyName(2, "targetinternal_obj.png");
		this.imageList.Images.SetKeyName(3, "targetpublic_obj.png");
		ImageList.ImageCollection.IndexOfKey(grade); 
		pictureBox1.Image = imageList1.ImageCollection.IndexOfKey(grade);
		pictureBox1.Image = ImageList.ImageCollection.IndexOfKey(grade); 
		*/
		public const int ICON_FILE = 0;//imageList.Images.SetKeyName(10, "FilePlain.png");
		public const int ICON_FOLDER_CLOSED = 1; //Images.SetKeyName(11, "FolderClosed.png");
		public const int ICON_FOLDER_OPEN = 2;//Images.SetKeyName(12, "FolderOpen.png");
		public const int ICON_CHECK_SYNTAX = 3;//imageList.Images.SetKeyName(4, "CheckAS.png");
		public const int ICON_QUICK_BUILD = 4;//imageList.Images.SetKeyName(28, "QuickBuild.png");
		public const int ICON_PACKAGE = 5;//imageList.Images.SetKeyName(21, "Package.png");
		public const int ICON_INTERFACE = 6;//imageList.Images.SetKeyName(13, "Interface.png");
		public const int ICON_INTRINSIC_TYPE = 7;//imageList.Images.SetKeyName(14, "Intrinsic.png");
		public const int ICON_TYPE = 8;//imageList.Images.SetKeyName(5, "Class.png")
		public const int ICON_VAR = 9;//imageList.Images.SetKeyName(30, "Variable.png");
		public const int ICON_PROTECTED_VAR = 10;//imageList.Images.SetKeyName(32, "VariableProtected.png");
		public const int ICON_PRIVATE_VAR = 11;//imageList.Images.SetKeyName(31, "VariablePrivate.png");
		public const int ICON_STATIC_VAR = 12;//imageList.Images.SetKeyName(33, "VariableStatic.png");
		public const int ICON_STATIC_PROTECTED_VAR = 13;//imageList.Images.SetKeyName(35, "VariableStaticProtected.png");
		public const int ICON_STATIC_PRIVATE_VAR = 14;//imageList.Images.SetKeyName(34, "VariableStaticPrivate.png");
		public const int ICON_CONST = 15;//imageList.Images.SetKeyName(6, "Const.png");
		public const int ICON_PROTECTED_CONST = 16;//imageList.Images.SetKeyName(8, "ConstProtected.png");
		public const int ICON_PRIVATE_CONST = 17;//imageList.Images.SetKeyName(7, "ConstPrivate.png");
		public const int ICON_STATIC_CONST = 18;
		public const int ICON_STATIC_PROTECTED_CONST = 19;
		public const int ICON_STATIC_PRIVATE_CONST = 20;
		public const int ICON_FUNCTION = 21;//imageList.Images.SetKeyName(15, "Method.png");
		public const int ICON_PROTECTED_FUNCTION = 22;//imageList.Images.SetKeyName(17, "MethodProtected.png");
		public const int ICON_PRIVATE_FUNCTION = 23;//imageList.Images.SetKeyName(16, "MethodPrivate.png");
		public const int ICON_STATIC_FUNCTION = 24;//imageList.Images.SetKeyName(18, "MethodStatic.png");
		public const int ICON_STATIC_PROTECTED_FUNCTION = 25;//imageList.Images.SetKeyName(20, "MethodStaticProtected.png");
		public const int ICON_STATIC_PRIVATE_FUNCTION = 26;//imageList.Images.SetKeyName(19, "MethodStaticPrivate.png");
		public const int ICON_PROPERTY = 27;//imageList.Images.SetKeyName(22, "Property.png");
		public const int ICON_PROTECTED_PROPERTY = 28;//imageList.Images.SetKeyName(24, "PropertyProtected.png");
		public const int ICON_PRIVATE_PROPERTY = 29;//
		public const int ICON_STATIC_PROPERTY = 30;//imageList.Images.SetKeyName(25, "PropertyStatic.png");
		public const int ICON_STATIC_PROTECTED_PROPERTY = 31;//imageList.Images.SetKeyName(27, "PropertyStaticProtected.png");
		public const int ICON_STATIC_PRIVATE_PROPERTY = 32;//imageList.Images.SetKeyName(26, "PropertyStaticPrivate.png");
		public const int ICON_TEMPLATE = 33;//imageList.Images.SetKeyName(29, "Template.png");
    private ImageList imageList1;
    private System.ComponentModel.IContainer components;
    public const int ICON_DECLARATION = 34;//imageList.Images.SetKeyName(9, "Declaration.png");

		public TreeNode CsOutlineTreeNode(string path, ImageList imageList)
		{
			this.path = path;
			this.imageList = imageList;
			return GetCsOutlineTreeNode(path);
		}
    
    public TreeNode CsOutlineTreeNode(string path)
    {
      this.path = path;
      this.imageList = this.imageList1;
      this.MemberId = new List<String>();
      if (this.imageList.Tag.ToString() == "Ant") ANT = true;
      else ANT = false;
      return GetCsOutlineTreeNode(path);
    }
 
    public TreeNode CsOutlineTreeNode(string path, ImageList imageList, List<string> MemberId)
		{
			this.path = path;
			this.imageList = imageList;
			this.MemberId = MemberId;
      if (this.imageList.Tag.ToString() == "Ant") ANT = true;
			else ANT = false;
			return GetCsOutlineTreeNode(path);
		}

		private int GetIcon(CSParser.Model.MemberModel model)
		{
			switch (model.kind)
			{
				case "import": return ANT ? imageList.Images.IndexOfKey("Package.png") : ICON_PACKAGE;
				case "namespace": return ANT ? imageList.Images.IndexOfKey("Package.png") : ICON_PACKAGE;//21
				case "class":
          //MessageBox.Show(imageList.Images.IndexOfKey("Class.png").ToString());
          //return 8;
          return ANT ? imageList.Images.IndexOfKey("Class.png") : ICON_TYPE;// 5
				case "struct": return  ANT ? imageList.Images.IndexOfKey("Template.png") : ICON_TEMPLATE; ;
				case "interface": return  ANT ? imageList.Images.IndexOfKey("Interface.png") : ICON_INTERFACE;//13
				case "delegate": return  ANT ? imageList.Images.IndexOfKey("Intrinsic.png") : ICON_INTRINSIC_TYPE;
				case "enum": return ANT ? imageList.Images.IndexOfKey("Const.png") : ICON_CONST;
			}
			if ((model.kind == "field" || model.kind == "variable") && model.access == "public")
			{
				if (model.modifier == "static") return ANT ? imageList.Images.IndexOfKey("VariableStatic.png") : ICON_STATIC_VAR;//18
				else return ANT ? imageList.Images.IndexOfKey("Variable.png") : ICON_VAR;//15
			}
			if ((model.kind == "field" || model.kind == "variable") && model.access == "protected")
			{
				if (model.modifier == "static") return  ANT ? imageList.Images.IndexOfKey("VariableStaticProtected.png"):ICON_STATIC_PROTECTED_VAR;//20
				else return ANT ? imageList.Images.IndexOfKey("VariableProtected.png") : ICON_PROTECTED_VAR;//
			}
			if ((model.kind == "field" || model.kind == "variable")
					&& (model.access == "private" || model.access == "default" || model.access == String.Empty))
			{
				if (model.modifier == "static") return ANT ? imageList.Images.IndexOfKey("VariableStaticPrivate.png") : ICON_STATIC_PRIVATE_VAR;
				else return ANT ? imageList.Images.IndexOfKey("VariablePrivate.png"):ICON_PRIVATE_VAR;
			}
			if (model.kind == "property" && model.access == "public")
			{
				if (model.modifier == "static") return  ANT ? imageList.Images.IndexOfKey("PropertyStatic.png"):ICON_STATIC_PROPERTY;//18
				else return ANT ? imageList.Images.IndexOfKey("Property.png") : ICON_PROPERTY;//15
			}
			if (model.kind == "property" && model.access == "protected")
			{
				if (model.modifier == "static") return　ANT ?  imageList.Images.IndexOfKey("PropertyStaticProtected.png"):ICON_STATIC_PROTECTED_PROPERTY;//20
				else return ANT ?  imageList.Images.IndexOfKey("PropertyProtected.png") :ICON_PROTECTED_PROPERTY;//
			}
			if (model.kind == "property" && (model.access == "private" || model.access == "default"))
			{
				if (model.modifier == "static") return ANT ? imageList.Images.IndexOfKey("PropertyStaticPrivate.png"):ICON_STATIC_PRIVATE_PROPERTY;
				else return ANT ? imageList.Images.IndexOfKey("PropertyPrivate.png"):ICON_PRIVATE_PROPERTY;
			}
			if (model.kind == "method" && model.access == "public")
			{
				if (model.modifier == "static") return ANT ? imageList.Images.IndexOfKey("MethodStatic.png"):ICON_STATIC_FUNCTION;//18
				else return ANT ? imageList.Images.IndexOfKey("Method.png") : ICON_FUNCTION;//15
			}
			if (model.kind == "method" && model.access == "protected")
			{
				if (model.modifier == "static") return  ANT ? imageList.Images.IndexOfKey("MethodStaticProtected.png"):ICON_STATIC_PROTECTED_FUNCTION;//20
				else return ANT ? imageList.Images.IndexOfKey("MethodProtected.png") : ICON_PROTECTED_FUNCTION;//
			}
			if (model.kind == "method" && (model.access == "private" || model.access == "default" || model.access == String.Empty))
			{
				if (model.modifier == "static") return ANT ? imageList.Images.IndexOfKey("MethodStaticPrivate.png"):ICON_STATIC_PRIVATE_FUNCTION;
				else return ANT ? imageList.Images.IndexOfKey("MethodPrivate.png") : ICON_PRIVATE_FUNCTION;
			}
			return 1;
		}
	
		private List<MemberModel> ScanMembers(String text, string kind)
		{
			return CSParserRegexes.ScanMembers(text, CSParserRegexes.RegExpDict[kind], this.src, this.path);
		}

		public TreeNode GetCsOutlineTreeNode(string path)
		{
      using (StreamReader sr = new StreamReader(
					path, Encoding.GetEncoding("UTF-8")))
			{
				this.src = sr.ReadToEnd();


        var cc = new DeComment();
        //var text = File.ReadAllText("Sample.cs");
        this.src = cc.Execute(this.src);

      }
			//TreeNode rootNode = new TreeNode(path, 10, 10);
			// 2017-01-10 pathは冗長でうるさい
			TreeNode rootNode = new TreeNode(Path.GetFileName(path), 10, 10);
			rootNode.Tag = this.path;
			rootNode.ToolTipText = this.path;

			// java Package 追加
			if (src.IndexOf("package") > -1)
			{
				int imageindex = ANT ? imageList.Images.IndexOfKey("Package.png") : ICON_PACKAGE;//("Declaration.png") : ICON_DECLARATION;
				TreeNode packageNode = new TreeNode("package", imageindex, imageindex);
				List<MemberModel> importList = this.ScanMembers(this.src, "package");
				foreach (MemberModel importitem in importList)
				{
					if (CheckId(importitem)) packageNode.Nodes.Add(GetMemberNode(importitem));
				}
				rootNode.Nodes.Add(packageNode);
			}
			
			if (src.IndexOf("using") > -1)
			{
				int imageindex = ANT ? imageList.Images.IndexOfKey("Declaration.png") : ICON_DECLARATION;
				TreeNode packageNode = new TreeNode("using", imageindex, imageindex);
				
				List<MemberModel> importList = this.ScanMembers(this.src, "import");
				foreach (MemberModel importitem in importList)
				{
					if (CheckId(importitem)) packageNode.Nodes.Add(GetMemberNode(importitem));
				}
				rootNode.Nodes.Add(packageNode);
			}

			if (src.IndexOf("import") > -1)
			{
				int imageindex = ANT ? imageList.Images.IndexOfKey("Declaration.png") : ICON_DECLARATION;
				TreeNode packageNode = new TreeNode("import", imageindex, imageindex);

				List<MemberModel> importList = this.ScanMembers(this.src, "import");
				foreach (MemberModel importitem in importList)
				{
					if (CheckId(importitem)) packageNode.Nodes.Add(GetMemberNode(importitem));
				}
				rootNode.Nodes.Add(packageNode);
			}
			
			if (this.src.IndexOf("namespace") < 0)
			{
				List<MemberModel> interfaceList = this.ScanMembers(this.src, "interface");
				if (interfaceList.Count > 0)
				{
					int imgIndex = ANT ? 13 : ICON_INTERFACE;// = 5;//imageList.Images.SetKeyName(13, "Interface.png");
					TreeNode interfacefolderNode = new TreeNode("interface", imgIndex, imgIndex);
					//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
					//interfacefolderNode.Tag = namespaceitem;
					rootNode.Nodes.Add(interfacefolderNode);
					foreach (MemberModel interfaceitem in interfaceList)
					{
						if (CheckId(interfaceitem)) interfacefolderNode.Nodes.Add(GetInterfaceNode(interfaceitem));
					}
				}

				/// KAHATA delegate 追加 Time-stamp: <2016-05-13 20:00:27 kahata>
				List<MemberModel> delegateList = this.ScanMembers(CSParserUtils.GetOuterCode(this.src,0), "delegate");
				if (delegateList.Count > 0)
				{
					TreeNode delegatefolderNode = new TreeNode("delegate", 11, 11);
					//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
					//interfacefolderNode.Tag = namespaceitem;
					rootNode.Nodes.Add(delegatefolderNode);
					foreach (MemberModel delegateitem in delegateList)
					{
						if (CheckId(delegateitem)) delegatefolderNode.Nodes.Add(GetMemberNode(delegateitem));
					}
				}

				/// KAHATA enum 追加 Time-stamp: <2016-05-14 10:00:27 kahata>
				List<MemberModel> enumList = this.ScanMembers(CSParserUtils.GetOuterCode(this.src, 0), "enum");
				if (enumList.Count > 0)
				{
					TreeNode enumfolderNode = new TreeNode("enum", 11, 11);
					//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
					//interfacefolderNode.Tag = namespaceitem;
					rootNode.Nodes.Add(enumfolderNode);
					foreach (MemberModel enumitem in enumList)
					{
						if (CheckId(enumitem)) enumfolderNode.Nodes.Add(GetMemberNode(enumitem));
					}
				}

				List<MemberModel> structList = this.ScanMembers(CSParserUtils.GetOuterCode(this.src, 0), "struct");
				foreach (MemberModel structitem in structList) this.RecursiveBuildMemberNode(rootNode, structitem, "struct");

				List<MemberModel> classList = this.ScanMembers(this.src, "class");
				foreach (MemberModel classitem in classList) this.RecursiveBuildMemberNode(rootNode, classitem, "class");
			
			}
			if (this.src.IndexOf("namespace") > -1)
			{
				List<MemberModel> namespaceList = this.ScanMembers(this.src, "namespace");
				foreach (MemberModel namespaceitem in namespaceList)
				{
					TreeNode namespaceNode = GetMemberNode(namespaceitem);
					rootNode.Nodes.Add(namespaceNode);
					string innerCode = src.Substring(namespaceitem.innerCode.start, namespaceitem.innerCode.end - namespaceitem.innerCode.start);
					//string innerCode = namespaceitem.content;// これでもOK
					List<MemberModel> interfaceList = this.ScanMembers(innerCode, "interface");
						//CSParserRegexes.ScanMembers(this.src, CSParserRegexes.RegExpDict["interface"], this.src, this.path);
					if (interfaceList.Count > 0)
					{
						TreeNode interfacefolderNode = new TreeNode("interface", ANT ? 11 : ICON_FOLDER_CLOSED, ANT ? 11 : ICON_FOLDER_CLOSED);
						interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
						interfacefolderNode.Tag = namespaceitem;
						namespaceNode.Nodes.Add(interfacefolderNode);
						foreach (MemberModel interfaceitem in interfaceList)
						{
							if (CheckId(interfaceitem)) interfacefolderNode.Nodes.Add(GetInterfaceNode(interfaceitem));
						}
					}
					
					/// KAHATA delegate 追加 Time-stamp: <2016-05-13 20:00:27 kahata>
					List<MemberModel> delegateList = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "delegate");
					if (delegateList.Count > 0)
					{
						TreeNode delegatefolderNode = new TreeNode("delegate", 11, 11);
						//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
						//interfacefolderNode.Tag = namespaceitem;
						namespaceNode.Nodes.Add(delegatefolderNode);
						foreach (MemberModel delegateitem in delegateList)
						{
							if (CheckId(delegateitem)) delegatefolderNode.Nodes.Add(GetMemberNode(delegateitem));
						}
					}
					/// KAHATA enum 追加 Time-stamp: <2016-05-14 10:00:27 kahata>
					List<MemberModel> enumList = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "enum");
					if (enumList.Count > 0)
					{
						TreeNode enumfolderNode = new TreeNode("enum", 11, 11);
						namespaceNode.Nodes.Add(enumfolderNode);
						foreach (MemberModel enumitem in enumList)
						{
							if (CheckId(enumitem)) enumfolderNode.Nodes.Add(GetMemberNode(enumitem));
						}
					}

					List<MemberModel> structList = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "struct");
					foreach (MemberModel structitem in structList)
					{
						this.RecursiveBuildMemberNode(namespaceNode, structitem, "struct");
					}
					
					List<MemberModel> classList = this.ScanMembers(innerCode, "class");
					foreach (MemberModel classitem in classList)
					{
						this.RecursiveBuildMemberNode(namespaceNode, classitem, "class");
					}
				}
			}
      return rootNode;
		}

		public bool TreeContains(string id)
		{
			if (!MemberId.Contains(id))
			{
				MemberId.Add(id);
				return false;
			}
			else return true;
		}

		private bool CheckId(MemberModel item)
		{
			if (!MemberId.Contains(item.id))
			{
				MemberId.Add(item.id);
				return true;
			}
			else return false;
		}

		private void RecursiveBuildMemberNode(TreeNode parentNode, MemberModel memberitem, string kind)
		{
			this.RecursiveBuildMemberNode(parentNode, memberitem, kind, true, 0);
		}

		private void RecursiveBuildMemberNode(TreeNode parentNode, MemberModel memberitem, string kind,
			bool recursive= true, int level = 0)
		{
			
			TreeNode memberNode = GetMemberNode(memberitem);
			if (memberitem.implements.Count > 0)
			{
				TreeNode imprementsfolderNode = new TreeNode("implements", 11, 11);
				imprementsfolderNode.ToolTipText = memberitem.definition;
				imprementsfolderNode.Tag = memberitem;
				memberNode.Nodes.Add(imprementsfolderNode);
				foreach (string implement in memberitem.implements)
				{
					TreeNode imprementNode = new TreeNode(implement.Trim(), 13, 13);
					imprementNode.ToolTipText = memberitem.definition;
					imprementNode.Tag = memberitem;
					imprementsfolderNode.Nodes.Add(imprementNode);
				}
			}
			//BugFix 済 OK
			//string innerCode = src.Substring(memberitem.innerCode.start, memberitem.innerCode.end - memberitem.innerCode.start);
			string innerCode = memberitem.content;// これでもOK
			List<MemberModel> memberList = this.ScanMembers(innerCode, kind);

			foreach (MemberModel item in memberList)
			{
				if (recursive == true) this.RecursiveBuildMemberNode(memberNode, item, kind, true, level + 1);
			}
			if (CheckId(memberitem)) parentNode.Nodes.Add(memberNode);

			if (kind == "class")
			{
				List<MemberModel> structList = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "struct");
				//List<MemberModel> structList = this.ScanMembers(innerCode, "struct");
				foreach (MemberModel structitem in structList) this.RecursiveBuildMemberNode(memberNode, structitem, "struct");
			}	
			
			/// KAHATA delegate 追加 Time-stamp: <2016-05-13 20:00:27 kahata>
			List<MemberModel> delegateListInmember = this.ScanMembers(CSParserUtils.GetOuterCode(memberitem.content, 0), "delegate");
			if (delegateListInmember.Count > 0)
			{
				TreeNode delegatefolderNode = new TreeNode("delegate", 11, 11);
				//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
				//interfacefolderNode.Tag = namespaceitem;
				memberNode.Nodes.Add(delegatefolderNode);
				foreach (MemberModel delegateitem in delegateListInmember)
				{
					if (CheckId(delegateitem)) delegatefolderNode.Nodes.Add(GetMemberNode(delegateitem));
				}
			}

			/// KAHATA enum 追加 Time-stamp: <2016-05-14 10:00:27 kahata>
			List<MemberModel> enumList = this.ScanMembers(CSParserUtils.GetOuterCode(memberitem.content, 0), "enum");
			if (enumList.Count > 0)
			{
				TreeNode enumfolderNode = new TreeNode("enum", 11, 11);
				//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
				//interfacefolderNode.Tag = namespaceitem;
				memberNode.Nodes.Add(enumfolderNode);
				foreach (MemberModel enumitem in enumList)
				{
					if (CheckId(enumitem)) enumfolderNode.Nodes.Add(GetMemberNode(enumitem));
				}
			}

			// FieldList
			List<MemberModel> fieldList = this.ScanMembers(innerCode, "field");
			foreach (MemberModel fielditem in fieldList)
			{
				if (CheckId(fielditem)) memberNode.Nodes.Add(GetMemberNode(fielditem));
			}
			//PropertyList
			List<MemberModel> propertyList = this.ScanMembers(innerCode, "property");
			foreach (MemberModel propertyitem in propertyList)
			{
				if (CheckId(propertyitem)) memberNode.Nodes.Add(GetMemberNode(propertyitem));
			}
			//MethodList
			List<MemberModel> methodList = this.ScanMembers(innerCode, "method");;
			foreach (MemberModel methoditem in methodList)
			{
				if (!MemberId.Contains(methoditem.id))
				{
					MemberId.Add(methoditem.id);
					TreeNode methodNode = GetMemberNode(methoditem);
					memberNode.Nodes.Add(methodNode);
					if (methoditem.args != String.Empty)
					{
						TreeNode argfolderNode = new TreeNode("args", 11, 11);
						argfolderNode.ToolTipText = methoditem.args.Trim();
						argfolderNode.Tag = methoditem;
						methodNode.Nodes.Add(argfolderNode);
						string[] tmp = methoditem.args.Trim().Split(',');
						foreach (string tmp2 in tmp)
						{
							string[] tmp3 = tmp2.Trim().Split(' ');
							TreeNode argNode = new TreeNode(tmp3[1].Trim(), 31, 31);
							argNode.ToolTipText = tmp2.Trim();
							argNode.Tag = methoditem;
							argfolderNode.Nodes.Add(argNode);
						}
					}
					innerCode = src.Substring(methoditem.innerCode.start, methoditem.innerCode.end - methoditem.innerCode.start);

					/// KAHATA delegate 追加 Time-stamp: <2016-05-13 20:00:27 kahata>
					List<MemberModel> delegateListInMethod = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "delegate");
					if (delegateListInMethod.Count > 0)
					{
						TreeNode delegatefolderNode = new TreeNode("delegate", 11, 11);
						//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
						//interfacefolderNode.Tag = namespaceitem;
						methodNode.Nodes.Add(delegatefolderNode);
						foreach (MemberModel delegateitem in delegateListInMethod)
						{
							if (CheckId(delegateitem)) delegatefolderNode.Nodes.Add(GetMemberNode(delegateitem));
						}
					}

					/// KAHATA enum 追加 Time-stamp: <2016-05-14 10:00:27 kahata>
					List<MemberModel> enumListInMethod = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "enum");
					if (enumList.Count > 0)
					{
						TreeNode enumfolderNode = new TreeNode("enum", 11, 11);
						//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
						//interfacefolderNode.Tag = namespaceitem;
						methodNode.Nodes.Add(enumfolderNode);
						foreach (MemberModel enumitem in enumListInMethod)
						{
							if (CheckId(enumitem)) enumfolderNode.Nodes.Add(GetMemberNode(enumitem));
						}
					}

					List<MemberModel> variableList = this.ScanMembers(innerCode, "variable");
					foreach (MemberModel variableitem in variableList)
					{
						if (CheckId(variableitem)) methodNode.Nodes.Add(GetMemberNode(variableitem));
					}
				}
			}
		}

		private TreeNode GetMemberNode(MemberModel model)
		{
			TreeNode node = new TreeNode(model.name.Trim(), GetIcon(model), GetIcon(model));
			node.Tag = model;
			node.ToolTipText = model.definition.Trim();
			return node;
		}

		private TreeNode GetInterfaceNode(MemberModel interfaceitem)
		{
			TreeNode interfaceNode = GetMemberNode(interfaceitem);
			if (interfaceitem.implements.Count > 0)
			{
				TreeNode imprementsfolderNode = new TreeNode("implements", 11, 11);
				imprementsfolderNode.ToolTipText = interfaceitem.definition;
				imprementsfolderNode.Tag = interfaceitem;
				interfaceNode.Nodes.Add(imprementsfolderNode);
				foreach (string implement in interfaceitem.implements)
				{
					TreeNode imprementNode = new TreeNode(implement.Trim(), 13, 13);
					imprementNode.ToolTipText = interfaceitem.definition;
					imprementNode.Tag = interfaceitem;
					imprementsfolderNode.Nodes.Add(imprementNode);
				}
			}
			// BugFix済
			string innerCode = src.Substring(interfaceitem.innerCode.start, interfaceitem.innerCode.end - interfaceitem.innerCode.start);
			//string innerCode = interfaceitem.content;//これでもOK
			if (!MemberId.Contains(interfaceitem.id))
			{
				MemberId.Add(interfaceitem.id);
			}

			// PropertyList
			List<MemberModel> interfacePropertyList = this.ScanMembers(innerCode, "interfaceproperty");
			foreach (MemberModel interfacePropertyitem in interfacePropertyList)
			{
				interfacePropertyitem.kind = "property";
				interfacePropertyitem.access = "public";
				if (CheckId(interfacePropertyitem)) interfaceNode.Nodes.Add(GetMemberNode(interfacePropertyitem));
			}
			// MethodList
			List<MemberModel> interfaceMethodList = this.ScanMembers(innerCode, "interfacemethod");
			foreach (MemberModel interfaceMethoditem in interfaceMethodList)
			{
				if (CheckId(interfaceMethoditem))
				{
					interfaceMethoditem.kind = "method";
					interfaceMethoditem.access = "public";
					TreeNode methodNode = GetMemberNode(interfaceMethoditem);
					interfaceNode.Nodes.Add(methodNode);
					if (interfaceMethoditem.args != String.Empty)
					{
						TreeNode argfolderNode = new TreeNode("args", 11, 11);
						argfolderNode.ToolTipText = interfaceMethoditem.args.Trim();
						argfolderNode.Tag = interfaceMethoditem;
						methodNode.Nodes.Add(argfolderNode);
						string[] tmp = interfaceMethoditem.args.Trim().Split(',');
						foreach (string tmp2 in tmp)
						{
							string[] tmp3 = tmp2.Trim().Split(' ');
							TreeNode argNode = new TreeNode(tmp3[1].Trim(), 31, 31);
							argNode.ToolTipText = tmp2.Trim();
							argNode.Tag = interfaceMethoditem;
							argfolderNode.Nodes.Add(argNode);
						}
					}
				}
			}
			return interfaceNode;
		}

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildTree));


      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.SuspendLayout();
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "ant_buildfile.png");
      this.imageList1.Images.SetKeyName(1, "defaulttarget_obj.png");
      this.imageList1.Images.SetKeyName(2, "targetinternal_obj.png");
      this.imageList1.Images.SetKeyName(3, "targetpublic_obj.png");
      this.imageList1.Images.SetKeyName(4, "CheckAS.png");
      this.imageList1.Images.SetKeyName(5, "Class.png");
      this.imageList1.Images.SetKeyName(6, "Const.png");
      this.imageList1.Images.SetKeyName(7, "ConstPrivate.png");
      this.imageList1.Images.SetKeyName(8, "ConstProtected.png");
      this.imageList1.Images.SetKeyName(9, "Declaration.png");
      this.imageList1.Images.SetKeyName(10, "FilePlain.png");
      this.imageList1.Images.SetKeyName(11, "FolderClosed.png");
      this.imageList1.Images.SetKeyName(12, "FolderOpen.png");
      this.imageList1.Images.SetKeyName(13, "Interface.png");
      this.imageList1.Images.SetKeyName(14, "Intrinsic.png");
      this.imageList1.Images.SetKeyName(15, "Method.png");
      this.imageList1.Images.SetKeyName(16, "MethodPrivate.png");
      this.imageList1.Images.SetKeyName(17, "MethodProtected.png");
      this.imageList1.Images.SetKeyName(18, "MethodStatic.png");
      this.imageList1.Images.SetKeyName(19, "MethodStaticPrivate.png");
      this.imageList1.Images.SetKeyName(20, "MethodStaticProtected.png");
      this.imageList1.Images.SetKeyName(21, "Package.png");
      this.imageList1.Images.SetKeyName(22, "Property.png");
      this.imageList1.Images.SetKeyName(23, "PropertyPrivate.png");
      this.imageList1.Images.SetKeyName(24, "PropertyProtected.png");
      this.imageList1.Images.SetKeyName(25, "PropertyStatic.png");
      this.imageList1.Images.SetKeyName(26, "PropertyStaticPrivate.png");
      this.imageList1.Images.SetKeyName(27, "PropertyStaticProtected.png");
      this.imageList1.Images.SetKeyName(28, "QuickBuild.png");
      this.imageList1.Images.SetKeyName(29, "Template.png");
      this.imageList1.Images.SetKeyName(30, "Variable.png");
      this.imageList1.Images.SetKeyName(31, "VariablePrivate.png");
      this.imageList1.Images.SetKeyName(32, "VariableProtected.png");
      this.imageList1.Images.SetKeyName(33, "VariableStatic.png");
      this.imageList1.Images.SetKeyName(34, "VariableStaticPrivate.png");
      this.imageList1.Images.SetKeyName(35, "VariableStaticProtected.png");
      this.imageList1.Images.SetKeyName(36, "Pspad15.png");
      this.imageList1.Images.SetKeyName(37, "FlashDevelopIcon.ico");
      this.imageList1.Images.SetKeyName(38, "53.png");
      // 
      // BuildTree
      // 
      this.Name = "BuildTree";
      this.ResumeLayout(false);

    }
  }
}
