using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using CommonInterface.Helpers;
using System.Xml.Serialization;
using System.Windows.Forms;
using CommonInterface.CommonLibrary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization;

namespace CommonInterface.Utilities
{
  public class ObjectSerializer
  {
    //private static BinaryFormatter formatter = new BinaryFormatter();
    //create a SoapFormatter to serialize the object
    private static SoapFormatter formatter = new SoapFormatter();
    //オブジェクトの型を指定する
    static ObjectSerializer()
    {
      formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
      AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomainAssemblyResolve);
    }

    /// <summary>
    /// The BinaryFormatter may need some help finding Assemblies from various directories
    /// </summary>
    static Assembly CurrentDomainAssemblyResolve(Object sender, ResolveEventArgs args)
    {
      AssemblyName assemblyName = new AssemblyName(args.Name);
      String ffile = Path.Combine(PathHelper.AppDir, assemblyName.Name + ".exe");
      String afile = Path.Combine(PathHelper.AppDir, assemblyName.Name + ".dll");
      String dfile = Path.Combine(PathHelper.PluginDir, assemblyName.Name + ".dll");
      String ufile = Path.Combine(PathHelper.UserPluginDir, assemblyName.Name + ".dll");
      if (File.Exists(ffile)) return Assembly.LoadFrom(ffile);
      if (File.Exists(afile)) return Assembly.LoadFrom(afile);
      if (File.Exists(dfile)) return Assembly.LoadFrom(dfile);
      if (File.Exists(ufile)) return Assembly.LoadFrom(ufile);
      return null;
    }

    /// <summary>
    /// Serializes the specified object to a binary file
    /// </summary>
    public static void Serialize(String file, Object obj)
    {
      Int32 count = 0;
      while (true)
      {
        try
        {
          using (FileStream stream = File.Create(file))
          {
            formatter.Serialize(stream, obj);
          }
          return;
        }
        catch (Exception ex)
        {
          count++;
          if (count > 10)
          {
            MessageBox.Show(Lib.OutputError(ex.Message.ToString()), MethodBase.GetCurrentMethod().Name);
            //ErrorManager.ShowError(ex);
            return;
          }
          Thread.Sleep(100);
        }
      }
    }

    /// <summary>
    /// Deserializes the specified object from a binary file
    /// </summary>
    public static Object Deserialize(String file, Object obj, Boolean checkValidity)
    {
      try
      {
        FileHelper.EnsureUpdatedFile(file);
        Object settings = InternalDeserialize(file, obj.GetType());
        if (checkValidity)
        {
          Object defaults = Activator.CreateInstance(obj.GetType());
          PropertyInfo[] properties = settings.GetType().GetProperties();
          foreach (PropertyInfo property in properties)
          {
            Object current = GetValue(settings, property.Name);
            if (current == null || (current is Color && (Color)current == Color.Empty))
            {
              Object value = GetValue(defaults, property.Name);
              SetValue(settings, property.Name, value);
            }
          }
        }
        return settings;
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()), MethodBase.GetCurrentMethod().Name);
        //ErrorManager.ShowError(ex);
        return obj;
      }
    }

    public static Object Deserialize(String file, Object obj)
    {
      return Deserialize(file, obj, true);
    }

    /// <summary>
    /// Fixes some common issues when serializing
    /// </summary>
    private static Object InternalDeserialize(String file, Type type)
    {
      FileInfo info = new FileInfo(file);
      if (!info.Exists)
      {
        return Activator.CreateInstance(type);
      }
      else if (info.Exists && info.Length == 0)
      {
        info.Delete();
        return Activator.CreateInstance(type);
      }
      else
      {
        using (FileStream stream = info.Open(FileMode.Open, FileAccess.Read))
        {
          //xs = new XmlSerializer(type);
          return formatter.Deserialize(stream);
          //return serializer.Deserialize(stream);
        }
      }
    }

    /// <summary>
    /// Sets a value of a setting
    /// </summary>
    public static void SetValue(Object obj, String name, Object value)
    {
      try
      {
        Type type = obj.GetType();
        PropertyInfo info = type.GetProperty(name);
        if (info == null) return;
        info.SetValue(obj, value, null);
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()), MethodBase.GetCurrentMethod().Name);
        //ErrorManager.ShowError(ex);
      }
    }

    /// <summary>
    /// Gets a value of a setting as an object
    /// </summary>
    public static Object GetValue(Object obj, String name)
    {
      try
      {
        Type type = obj.GetType();
        PropertyInfo info = type.GetProperty(name);
        if (info == null) return null;
        return info.GetValue(obj, null);
      }
      catch (Exception ex)
      {
        //ErrorManager.ShowError(ex);
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()), MethodBase.GetCurrentMethod().Name);
        return null;
      }
    }

    //SampleClassオブジェクトをXMLファイルに保存する
    public static void XmlSerialize(String file, Object obj)
    {
      //保存先のファイル名
      string fileName = file;// @"C:\test\sample.xml";

      //保存するクラス(SampleClass)のインスタンスを作成
      //SampleClass obj = new SampleClass();
      //obj.Message = "テストです。";
      //obj.Number = 123;

      //XmlSerializerオブジェクトを作成
      //オブジェクトの型を指定する
      System.Xml.Serialization.XmlSerializer serializer =
          new System.Xml.Serialization.XmlSerializer(obj.GetType());
      //書き込むファイルを開く（UTF-8 BOM無し）
      System.IO.StreamWriter sw = new System.IO.StreamWriter(
          fileName, false, new System.Text.UTF8Encoding(false));
      //シリアル化し、XMLファイルに保存する
      serializer.Serialize(sw, obj);
      //ファイルを閉じる
      sw.Close();
    }

    //XMLファイルをSampleClassオブジェクトに復元する
    public static Object XmlDeserialize(String file, Object obj)
    {
      //保存元のファイル名
      string fileName = file;// @"C:\test\sample.xml";

      Assembly asm = obj.GetType().Assembly;
      Type myType = asm.GetType();
      Object obj2 = Activator.CreateInstance(myType);
      //XmlSerializerオブジェクトを作成
      System.Xml.Serialization.XmlSerializer serializer =
          new System.Xml.Serialization.XmlSerializer(myType);
      //読み込むファイルを開く
      System.IO.StreamReader sr = new System.IO.StreamReader(
          fileName, new System.Text.UTF8Encoding(false));
      //XMLファイルから読み込み、逆シリアル化する
      Object obj3 = serializer.Deserialize(sr);
      //ファイルを閉じる
      sr.Close();
      return obj3;
    }

    public static void SoapSerialize(String file, Object obj)
    {
      SoapFormatter soapformatter = new SoapFormatter();
      Int32 count = 0;
      while (true)
      {
        try
        {
          using (FileStream stream = File.Create(file))
          {
            soapformatter.Serialize(stream, obj);
          }
          return;
        }
        catch (Exception ex)
        {
          count++;
          if (count > 10)
          {
            MessageBox.Show(Lib.OutputError(ex.Message.ToString()), MethodBase.GetCurrentMethod().Name);
            return;
          }
          Thread.Sleep(100);
        }
      }
    }

    public static Object SoapDeserialize(String file, Object obj)
    {
      FileInfo info = new FileInfo(file);
      SoapFormatter soapformatter = new SoapFormatter();
      try
      {
        if (!info.Exists)
        {
          return Activator.CreateInstance(obj.GetType());
        }
        else if (info.Exists && info.Length == 0)
        {
          info.Delete();
          return Activator.CreateInstance(obj.GetType());
        }
        else
        {
          using (FileStream stream = info.Open(FileMode.Open, FileAccess.Read))
          {
            return soapformatter.Deserialize(stream);
          }
        }
      }
      catch (SerializationException ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()), MethodBase.GetCurrentMethod().Name);
        throw;
      }
      //finally { //stream.Close();}
    }

  }
}
