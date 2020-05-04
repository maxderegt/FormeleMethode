using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormeleMethode
{
    class GenerateStrings
    {
		static string[] arrA;
		static List<string> strings = new List<string>();
		static private void nBits(int n)
		{
			if (n <= 0)
			{
				strings.Add(string.Join("",arrA));
			}
			else
			{
				arrA[n - 1] = "a";
				nBits(n - 1);
				arrA[n - 1] = "b";
				nBits(n - 1);
			}
		}

		public static List<String> GenerateString(int i)
		{
			strings.Clear();
			arrA = new string[i];
			nBits(i);
			return strings;
		}
	}
}
