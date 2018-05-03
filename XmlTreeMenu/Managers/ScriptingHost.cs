//using CSScriptLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AntPlugin.XmlTreeMenu.Managers
{
  class ScriptingHost : MarshalByRefObject
  {
    /*
    /// http://www.csscript.net/help/script_hosting_guideline_.html
    /// <summary>
    /// Executes the script in a seperate appdomain and then unloads it
    /// NOTE: This is more suitable for one pass processes
    /// </summary>
    public void ExecuteScriptExternal(String script)
    {
      if (!File.Exists(script)) throw new FileNotFoundException();
      using (AsmHelper helper = new AsmHelper(CSScript.Compile(script, null, true), null, true))
      {
        helper.Invoke("*.Execute");
        //helper.Invoke("*.Main");
      }
    }

    /// <summary>
    /// Executes the script and adds it to the current app domain
    /// NOTE: This locks the assembly script file
    /// </summary>
    public void ExecuteScriptInternal(String script, Boolean random)
    {
      if (!File.Exists(script)) throw new FileNotFoundException();
      String file = random ? Path.GetTempFileName() : null;
      AsmHelper helper = new AsmHelper(CSScript.Load(script, file, false, null));
      helper.Invoke("*.Execute");
      //helper.Invoke("*.Main");
    }

    public void ExecuteScriptInternal(String script, Boolean random, object[] obj)
    {
      if (!File.Exists(script)) throw new FileNotFoundException();
      String file = random ? Path.GetTempFileName() : null;
      AsmHelper helper = new AsmHelper(CSScript.Load(script, file, false, null));
      helper.Invoke("*.Execute", obj);
      //helper.Invoke("*.Main");
    }

    public object ExecuteScriptInternal(String script, Boolean random, string target, object[] obj)
    {
      if (!File.Exists(script)) throw new FileNotFoundException();
      String file = random ? Path.GetTempFileName() : null;
      AsmHelper helper = new AsmHelper(CSScript.Load(script, file, false, null));
      return helper.Invoke(target, obj);
      //helper.Invoke("*.Main");
    }
    */
  }
}
