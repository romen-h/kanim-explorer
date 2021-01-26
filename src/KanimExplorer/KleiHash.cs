using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KanimalExplorer
{
	public static class KleiUtil
	{
		public static int HashString(string str)
		{
			if (str == null) return 0;

			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				num = ((int)str.ToLower()[i]) + (num << 6) + (num << 16) - num;
			}
			return num;
		}

		public static int KHash(this string str) => HashString(str);
	}
}
