using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSParser.Model;
using System.IO;

namespace CSParser.BuildTree
{
	class BuildTree
	{
		private ImageList imageList;
		private String path;
		private string src;
		public   List<string> MemberId = new List<string>();
		/*   
		this.imageList.Images.SetKeyName(0, "ant_buildfile.png");
		this.imageList.Images.SetKeyName(1, "defaulttarget_obj.png");
		this.imageList.Images.SetKeyName(2, "targetinternal_obj.png");
		this.imageList.Images.SetKeyName(3, "targetpublic_obj.png");
		this.imageList.Images.SetKeyName(4, "CheckAS.png");
		this.imageList.Images.SetKeyName(5, "Class.png");
		this.imageList.Images.SetKeyName(6, "Const.png");
		this.imageList.Images.SetKeyName(7, "ConstPrivate.png");
		this.imageList.Images.SetKeyName(8, "ConstProtected.png");
		this.imageList.Images.SetKeyName(9, "Declaration.png");
		this.imageList.Images.SetKeyName(10, "FilePlain.png");
		this.imageList.Images.SetKeyName(11, "FolderClosed.png");
		this.imageList.Images.SetKeyName(12, "FolderOpen.png");
		this.imageList.Images.SetKeyName(13, "Interface.png");
		this.imageList.Images.SetKeyName(14, "Intrinsic.png");
		this.imageList.Images.SetKeyName(15, "Method.png");
		this.imageList.Images.SetKeyName(16, "MethodPrivate.png");
		this.imageList.Images.SetKeyName(17, "MethodProtected.png");
		this.imageList.Images.SetKeyName(18, "MethodStatic.png");
		this.imageList.Images.SetKeyName(19, "MethodStaticPrivate.png");
		this.imageList.Images.SetKeyName(20, "MethodStaticProtected.png");
		this.imageList.Images.SetKeyName(21, "Package.png");
		this.imageList.Images.SetKeyName(22, "Property.png");
		this.imageList.Images.SetKeyName(23, "PropertyPrivate.png");
		this.imageList.Images.SetKeyName(24, "PropertyProtected.png");
		this.imageList.Images.SetKeyName(25, "PropertyStatic.png");
		this.imageList.Images.SetKeyName(26, "PropertyStaticPrivate.png");
		this.imageList.Images.SetKeyName(27, "PropertyStaticProtected.png");
		this.imageList.Images.SetKeyName(28, "QuickBuild.png");
		this.imageList.Images.SetKeyName(29, "Template.png");
		this.imageList.Images.SetKeyName(30, "Variable.png");
		this.imageList.Images.SetKeyName(31, "VariablePrivate.png");
		this.imageList.Images.SetKeyName(32, "VariableProtected.png");
		this.imageList.Images.SetKeyName(33, "VariableStatic.png");
		this.imageList.Images.SetKeyName(34, "VariableStaticPrivate.png");
		this.imageList.Images.SetKeyName(35, "VariableStaticProtected.png");		
		ImageList.ImageCollection.IndexOfKey(grade); 

		 string grade = comboBox1.SelectedText;
		 pictureBox1.Image = imageList1.ImageCollection.IndexOfKey(grade);
		 pictureBox1.Image = ImageList.ImageCollection.IndexOfKey(grade); 
		*/

		public TreeNode CsOutlineTreeNode(string path, ImageList imageList)
		{
			this.path = path;
			this.imageList = imageList;
			return CsOutlineTreeNode(path);
		}
		
		private int GetIcon(CSParser.Model.MemberModel model)
		{
			switch (model.kind)
			{
				case "import": return imageList.Images.IndexOfKey("Package.png");
				case "namespace": return imageList.Images.IndexOfKey("Package.png");//21
				case "class": return imageList.Images.IndexOfKey("Class.png");//5
				case "interface": return imageList.Images.IndexOfKey("Interface.png");//13
				case "delegate": return imageList.Images.IndexOfKey("Intrinsic.png");
				case "enum": return imageList.Images.IndexOfKey("Const.png");
			}

			if ((model.kind == "field" || model.kind == "variable") && model.access == "public")
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("VariableStatic.png");//18
				else return imageList.Images.IndexOfKey("Variable.png");//15
			}

			if ((model.kind == "field" || model.kind == "variable") && model.access == "protected")
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("VariableStaticProtected.png");//20
				else return imageList.Images.IndexOfKey("VariableProtected.png");//
			}
			if ((model.kind == "field" || model.kind == "variable")
				&& (model.access == "private" || model.access == "default" || model.access == String.Empty))
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("VariableStaticPrivate.png");
				else return imageList.Images.IndexOfKey("VariablePrivate.png");
			}
			if (model.kind == "property" && model.access == "public")
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("PropertyStatic.png");//18
				else return imageList.Images.IndexOfKey("Property.png");//15
			}
			if (model.kind == "property" && model.access == "protected")
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("PropertyStaticProtected.png");//20
				else return imageList.Images.IndexOfKey("PropertyProtected.png");//
			}
			if (model.kind == "property" && (model.access == "private" || model.access == "default"))
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("PropertyStaticPrivate.png");
				else return imageList.Images.IndexOfKey("PropertyPrivate.png");
			}
			if (model.kind == "method" && model.access == "public")
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("MethodStatic.png");//18
				else return imageList.Images.IndexOfKey("Method.png");//15
			}
			if (model.kind == "method" && model.access == "protected")
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("MethodStaticProtected.png");//20
				else return imageList.Images.IndexOfKey("MethodProtected.png");//
			}
			if (model.kind == "method" && (model.access == "private" || model.access == "default"))
			{
				if (model.modifier == "static") return imageList.Images.IndexOfKey("MethodStaticPrivate.png");
				else return imageList.Images.IndexOfKey("MethodPrivate.png");
			}
			return 0;
		}

		private List<MemberModel> ScanMembers(String text, string kind)
		{
			return CSParserRegexes.ScanMembers(text, CSParserRegexes.RegExpDict[kind], this.src, this.path);
		}

		private TreeNode CsOutlineTreeNode(string path)
		{
			using (StreamReader sr = new StreamReader(
					path, Encoding.GetEncoding("UTF-8")))
			{
				this.src = sr.ReadToEnd();
			}
			TreeNode rootNode = new TreeNode(path, 10, 10);
			rootNode.Tag = this.path;
			rootNode.ToolTipText = this.path;

			if (src.IndexOf("using") > -1)
			{
				int imageindex = imageList.Images.IndexOfKey("Declaration.png");
				TreeNode packageNode = new TreeNode("using", imageindex, imageindex);
				
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
					TreeNode interfacefolderNode = new TreeNode("interface", 5, 5);
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
				//List<MemberModel> structList = this.ScanMembers(this.src, "struct");
				//MessageBox.Show(structList.Count.ToString());
				foreach (MemberModel structitem in structList) this.RecursiveBuildMemberNode(rootNode, structitem, "struct");





				
				List<MemberModel> classList = this.ScanMembers(this.src, "class");
				foreach (MemberModel classitem in classList) this.RecursiveBuildClassNode(rootNode, classitem);
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
						TreeNode interfacefolderNode = new TreeNode("interface", 5, 5);
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
						//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
						//interfacefolderNode.Tag = namespaceitem;
						namespaceNode.Nodes.Add(enumfolderNode);
						foreach (MemberModel enumitem in enumList)
						{
							if (CheckId(enumitem)) enumfolderNode.Nodes.Add(GetMemberNode(enumitem));
						}
					}





					List<MemberModel> structList = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "struct");
					//List<MemberModel> structList = this.ScanMembers(innerCode, "struct");
					//MessageBox.Show(structList.Count.ToString());
					foreach (MemberModel structitem in structList) this.RecursiveBuildMemberNode(namespaceNode, structitem, "struct");

					
					
					
					
					
					
					
					
					
					
					List<MemberModel> classList = this.ScanMembers(innerCode, "class");
					foreach (MemberModel classitem in classList)
					{
						this.RecursiveBuildClassNode(namespaceNode, classitem);
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
			List<MemberModel> memberList = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode,0), kind);

			foreach (MemberModel item in memberList) this.RecursiveBuildMemberNode(memberNode,item, kind);
			if (CheckId(memberitem)) parentNode.Nodes.Add(memberNode);

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
			List<MemberModel> methodList = this.ScanMembers(innerCode, "method");
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





		//private TreeNode RecursiveBuildMemberNode(TreeNode parentNode, MemberModel memberitem, string kind)
		//{
		//	TreeNode memberNode = GetMemberNode(memberitem);
		//	return memberNode;
		//}	
		
		
		
		
		
		
		
		
		private void RecursiveBuildClassNode(TreeNode parentNode, MemberModel classitem)
		{
			TreeNode classNode = GetMemberNode(classitem);
			if (classitem.implements.Count > 0)
			{
				TreeNode imprementsfolderNode = new TreeNode("implements", 11, 11);
				imprementsfolderNode.ToolTipText = classitem.definition;
				imprementsfolderNode.Tag = classitem;
				classNode.Nodes.Add(imprementsfolderNode);
				foreach (string implement in classitem.implements)
				{
					TreeNode imprementNode = new TreeNode(implement.Trim(), 13, 13);
					imprementNode.ToolTipText = classitem.definition;
					imprementNode.Tag = classitem;
					imprementsfolderNode.Nodes.Add(imprementNode);
				}
			}
			// BugFix 済 OK
			string innerCode = src.Substring(classitem.innerCode.start, classitem.innerCode.end - classitem.innerCode.start);
			//string innerCode = classitem.content;// これでもOK
			List<MemberModel> classList = this.ScanMembers(innerCode, "class");

			foreach (MemberModel item in classList) RecursiveBuildClassNode(classNode, item);
			if (CheckId(classitem)) parentNode.Nodes.Add(classNode);




			List<MemberModel> structList = this.ScanMembers(CSParserUtils.GetOuterCode(innerCode, 0), "struct");
			//List<MemberModel> structList = this.ScanMembers(innerCode, "struct");
			//MessageBox.Show(structList.Count.ToString());
			foreach (MemberModel structitem in structList) this.RecursiveBuildMemberNode(classNode, structitem, "struct");
			
			
			
			
			
			
			/// KAHATA delegate 追加 Time-stamp: <2016-05-13 20:00:27 kahata>
			List<MemberModel> delegateListInClass = this.ScanMembers(CSParserUtils.GetOuterCode(classitem.content, 0), "delegate");
			if (delegateListInClass.Count > 0)
			{
				TreeNode delegatefolderNode = new TreeNode("delegate", 11, 11);
				//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
				//interfacefolderNode.Tag = namespaceitem;
				classNode.Nodes.Add(delegatefolderNode);
				foreach (MemberModel delegateitem in delegateListInClass)
				{
					if (CheckId(delegateitem)) delegatefolderNode.Nodes.Add(GetMemberNode(delegateitem));
				}
			}
			
			/// KAHATA enum 追加 Time-stamp: <2016-05-14 10:00:27 kahata>
			List<MemberModel> enumList = this.ScanMembers(CSParserUtils.GetOuterCode(classitem.content, 0), "enum");
			if (enumList.Count > 0)
			{
				TreeNode enumfolderNode = new TreeNode("enum", 11, 11);
				//interfacefolderNode.ToolTipText = namespaceitem.definition.Trim();
				//interfacefolderNode.Tag = namespaceitem;
				classNode.Nodes.Add(enumfolderNode);
				foreach (MemberModel enumitem in enumList)
				{
					if (CheckId(enumitem)) enumfolderNode.Nodes.Add(GetMemberNode(enumitem));
				}
			}

			// FieldList
			List<MemberModel> fieldList = this.ScanMembers(innerCode, "field");
			foreach (MemberModel fielditem in fieldList)
			{
				if (CheckId(fielditem)) classNode.Nodes.Add(GetMemberNode(fielditem));
			}
			//PropertyList
			List<MemberModel> propertyList = this.ScanMembers(innerCode, "property");
			foreach (MemberModel propertyitem in propertyList)
			{
				if (CheckId(propertyitem)) classNode.Nodes.Add(GetMemberNode(propertyitem));
			}
			//MethodList
			List<MemberModel> methodList = this.ScanMembers(innerCode, "method");
			foreach (MemberModel methoditem in methodList)
			{
				if (!MemberId.Contains(methoditem.id))
				{
					MemberId.Add(methoditem.id);
					TreeNode methodNode = GetMemberNode(methoditem);
					classNode.Nodes.Add(methodNode);
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
	}
}
