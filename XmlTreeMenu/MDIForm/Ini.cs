using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntPlugin
{
	public class Ini
	{
		public class Section : Dictionary<string, string>
		{
		}

		private static class Utl
		{
			public static int IndexNotOfAny(string str, string anyOf)
			{
				return Ini.Utl.IndexNotOfAny(str, anyOf, 0);
			}

			public static int IndexNotOfAny(string str, string anyOf, int startIndex)
			{
				bool flag = false;
				for (int i = startIndex; i < str.Length; i++)
				{
					for (int j = 0; j < anyOf.Length; j++)
					{
						if (str[i] == anyOf[j])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return i;
					}
					flag = false;
				}
				return -1;
			}

			public static int LastIndexNotOfAny(string str, string anyOf, int startIndex)
			{
				bool flag = false;
				for (int i = startIndex; i >= 0; i--)
				{
					for (int j = 0; j < anyOf.Length; j++)
					{
						if (str[i] == anyOf[j])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return i;
					}
					flag = false;
				}
				return -1;
			}
		}

		private SortedList<string, Ini.Section> _Sections = new SortedList<string, Ini.Section>(8);

		public virtual SortedList<string, Ini.Section> Sections
		{
			get
			{
				return this._Sections;
			}
		}

		public virtual void Load(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._Sections.Clear();
			Ini.Section section = new Ini.Section();
			this._Sections.Add("", section);
			for (string text = reader.ReadLine(); text != null; text = reader.ReadLine())
			{
				if (!text.StartsWith(";"))
				{
					string text2;
					string text3;
					if (Ini.IsSectionLine(text))
					{
						Ini.ParseLineAsSection(text, out text2);
						if (text2 != null)
						{
							Ini.Section section2 = this.GetSection(text2);
							if (section2 == null)
							{
								section2 = new Ini.Section();
								this._Sections.Add(text2, section2);
							}
							section = section2;
						}
					}
					else if (Ini.ParseLineAsEntry(text, out text2, out text3) && text2 != null && text3 != null)
					{
						section[text2] = text3;
					}
				}
			}
		}

		public virtual void Load(string filePath)
		{
			this.Load(filePath, Encoding.Default);
		}

		public virtual void Load(string filePath, Encoding encoding)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				this.Load(new StreamReader(stream, encoding));
			}
		}

		public virtual void Save(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			foreach (KeyValuePair<string, Ini.Section> current in this._Sections)
			{
				if (current.Key != string.Empty)
				{
					writer.WriteLine("[{0}]", current.Key);
				}
				foreach (KeyValuePair<string, string> current2 in current.Value)
				{
					writer.WriteLine("{0}={1}", current2.Key, current2.Value);
				}
			}
		}

		public virtual void Save(string filePath, Encoding encoding)
		{
			this.Save(filePath, encoding, "\r\n");
		}

		public virtual void Save(string filePath, Encoding encoding, string newLineCode)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (newLineCode == null)
			{
				throw new ArgumentNullException("newLineCode");
			}
			if (newLineCode == "")
			{
				throw new ArgumentException("parameter newLineCode must not be an empty string.");
			}
			using (FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, encoding);
				streamWriter.NewLine = newLineCode;
				this.Save(streamWriter);
				streamWriter.Close();
			}
		}

		public virtual T Get<T>(string sectionName, string entryName, T defaultValue) where T : struct
		{
			if (sectionName == null)
			{
				throw new ArgumentNullException("sectionName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (entryName == "")
			{
				throw new ArgumentException("parameter entryName cannot be an empty string.");
			}
			string value = this.Get(sectionName, entryName, null);
			try
			{
				T result;
				if (defaultValue is Enum)
				{
					result = (T)((object)Enum.Parse(typeof(T), value, false));
					return result;
				}
				result = (T)((object)Convert.ChangeType(value, typeof(T), null));
				return result;
			}
			catch (FormatException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (InvalidCastException)
			{
			}
			return defaultValue;
		}

		public virtual string Get(string sectionName, string entryName, string defaultValue)
		{
			if (sectionName == null)
			{
				throw new ArgumentNullException("sectionName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (entryName == "")
			{
				throw new ArgumentException("parameter entryName cannot be an empty string.");
			}
			Ini.Section section = this.GetSection(sectionName);
			if (section == null)
			{
				return defaultValue;
			}
			string result;
			if (!section.TryGetValue(entryName, out result))
			{
				return defaultValue;
			}
			return result;
		}

		public virtual int GetInt(string sectionName, string entryName, int minValue, int maxValue, int defaultValue)
		{
			if (sectionName == null)
			{
				throw new ArgumentNullException("sectionName");
			}
			if (entryName == "")
			{
				throw new ArgumentException("parameter entryName cannot be an empty string.");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (maxValue < minValue)
			{
				throw new ArgumentException("minValue must be equal or less than maxValue.");
			}
			string text = this.Get(sectionName, entryName, null);
			if (text == null)
			{
				return defaultValue;
			}
			int num;
			try
			{
				num = int.Parse(text);
			}
			catch (Exception)
			{
				return defaultValue;
			}
			if (num < minValue || maxValue < num)
			{
				return defaultValue;
			}
			return num;
		}

		public virtual void Set<T>(string sectionName, string entryName, T value)
		{
			if (sectionName == null)
			{
				throw new ArgumentNullException("sectionName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (entryName == "")
			{
				throw new ArgumentException("parameter entryName cannot be an empty string.");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Ini.Section section = this.GetSection(sectionName);
			if (section == null)
			{
				section = new Ini.Section();
				this._Sections.Add(sectionName, section);
			}
			section[entryName] = value.ToString();
		}

		public virtual void Remove(string sectionName, string entryName)
		{
			if (sectionName == null)
			{
				throw new ArgumentNullException("sectionName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			Ini.Section section;
			if (!this._Sections.TryGetValue(sectionName, out section))
			{
				return;
			}
			section.Remove(entryName);
			if (section.Count == 0)
			{
				this._Sections.Remove(sectionName);
			}
		}

		public virtual void Clear()
		{
			this._Sections.Clear();
		}

		private static void ParseLineAsSection(string line, out string sectionName)
		{
			int num = line.IndexOf("[");
			if (num == -1)
			{
				sectionName = null;
				return;
			}
			int num2 = line.LastIndexOf("]");
			if (num2 == -1)
			{
				sectionName = null;
				return;
			}
			sectionName = line.Substring(num + 1, num2 - num - 1);
		}

		private static bool ParseLineAsEntry(string line, out string entryName, out string entryValue)
		{
			int num = Ini.Utl.IndexNotOfAny(line, " \t=", 0);
			if (num != -1)
			{
				int num2 = Ini.Utl.LastIndexNotOfAny(line, " \t=", line.IndexOf("="));
				if (num2 != -1)
				{
					num2++;
					int num3 = Ini.Utl.IndexNotOfAny(line, " \t=", num2);
					if (num3 != -1)
					{
						int num4 = line.Length - 1;
						if (num4 != -1)
						{
							num4++;
							entryName = line.Substring(num, num2 - num);
							entryValue = line.Substring(num3, num4 - num3);
							return true;
						}
					}
				}
			}
			entryName = null;
			entryValue = null;
			return false;
		}

		private static bool IsEntryLine(string line)
		{
			return Ini.Utl.IndexNotOfAny(line, " \t=") < line.IndexOf("=");
		}

		private static bool IsSectionLine(string line)
		{
			return line.StartsWith("[") && line.LastIndexOf("]") != -1;
		}

		private Ini.Section GetSection(string sectionName)
		{
			Ini.Section result;
			if (!this._Sections.TryGetValue(sectionName, out result))
			{
				return null;
			}
			return result;
		}
	}
}
