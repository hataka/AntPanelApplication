using System;
using System.Drawing;

namespace AntPlugin.XMLTreeMenu.Controls
{
	internal class FontLabels
	{
		public static string GetLongLabel(Font font)
		{
			return string.Concat(new object[]
			{
				font.Name,
				", ",
				font.Size,
				", ",
				font.Style.ToString()
			});
		}

		public static string GetShortLabel(Font font)
		{
			return font.Name;
		}
	}
}
