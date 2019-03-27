using System;
using System.Collections;
using System.Collections.Generic;

namespace AntPanelApplication.CommonLibrary
{
	internal class TwoKeysDictionary<T, U> : IEnumerable<KeyValuePair<T, Dictionary<U, object>>>, IEnumerable
	{
		private Dictionary<T, Dictionary<U, object>> dictionary = new Dictionary<T, Dictionary<U, object>>();

		public object this[T key1, U key2]
		{
			get
			{
				return this.dictionary[key1][key2];
			}
			set
			{
				if (!this.dictionary.ContainsKey(key1))
				{
					this.dictionary[key1] = new Dictionary<U, object>();
				}
				this.dictionary[key1][key2] = value;
			}
		}

		public IEnumerator<KeyValuePair<T, Dictionary<U, object>>> GetEnumerator()
		{
			foreach (KeyValuePair<T, Dictionary<U, object>> current in this.dictionary)
			{
				yield return current;
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Dictionary<U, object>>)this).GetEnumerator();
		}
	}
}
