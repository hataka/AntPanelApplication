using System;
using System.Collections;

namespace AntPlugin.CommonLibrary
{
	public class TwoKeysHashTable
	{
		public Hashtable ht;

		public string this[string key1, string key2]
		{
			get
			{
				string result;
				try
				{
					result = (string)((Hashtable)this.ht[key1])[key2];
				}
				catch (Exception ex)
				{
					ex.Message.ToString();
					result = "";
				}
				return result;
			}
			set
			{
				if (this.ht == null)
				{
					this.ht = new Hashtable();
				}
				if (!this.ht.Contains(key1))
				{
					this.ht[key1] = new Hashtable();
				}
				((Hashtable)this.ht[key1])[key2] = value;
			}
		}
	}
}
