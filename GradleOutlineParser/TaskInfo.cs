//using PluginCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AntPlugin.CommonLibrary
{
  public class TaskInfo
  {
    public string name=String.Empty;
    public string buildFile=String.Empty;
    public String definition = String.Empty;
    public String id = String.Empty;
    public string parent = String.Empty;
    public Int32 outerStart = 0;
    public Int32 outerEnd = 0;
    public Boolean defaultTask=false;
    public Boolean publicTask = true;
    public string iconPath = Path.Combine(System.Windows.Forms.Application.StartupPath, @"SettingData\targetpublic_obj.png"); 
    public string description = String.Empty;
    public string args = String.Empty;
    public string innerCode = String.Empty;
    public string outerCode = String.Empty;

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }
    public string BuildFile
    {
      get
      {
        return this.buildFile;
      }
      set
      {
        this.buildFile = value;
      }
    }
    public Boolean DefaultTask
    {
      get
      {
        return this.defaultTask;
      }
      set
      {
        this.defaultTask = value;
      }
    }
    public Boolean PublicTask
    {
      get
      {
        return this.publicTask;
      }
      set
      {
        this.publicTask = value;
      }
    }
    public String Description
    {
      get
      {
        return this.description;
      }
      set
      {
        this.description = value;
      }
    }
    public String IconPath
    {
      get
      {
        return this.iconPath;
      }
      set
      {
        this.iconPath = value;
      }
    }
    public String Args
    {
      get
      {
        return this.args;
      }
      set
      {
        this.args = value;
      }
    }
    public String InnerCode
    {
      get
      {
        return this.innerCode;
      }
      set
      {
        this.innerCode = value;
      }
    }
    public Int32 OuterStart
    {
      get
      {
        return this.outerStart;
      }
      set
      {
        this.outerStart = value;
      }
    }
    public Int32 OuterEnd

    {
      get
      {
        return this.outerEnd;
      }
      set
      {
        this.outerEnd = value;
      }
    }
    public String OuterCode
    {
      get
      {
        return this.outerCode;
      }
      set
      {
        this.outerCode = value;
      }
    }
    public String Definition
    {
      get
      {
        return this.definition;
      }
      set
      {
        this.definition = value;
      }
    }
    public String Id
    {
      get
      {
        return this.id;
      }
      set
      {
        this.id = value;
      }
    }
    public string Parent
    {
      get
      {
        return this.parent;
      }
      set
      {
        this.parent = value;
      }
    }

  }
}

