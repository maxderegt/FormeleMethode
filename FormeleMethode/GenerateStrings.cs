using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace FormeleMethode
{
    class GenerateStrings
    {
		static string[] arrA;
		static List<string> strings = new List<string>();
		static string letters;
		static private void nBits(int n)
		{
			if (n <= 0)
			{
				strings.Add(string.Join("",arrA));
			}
			else
			{
				foreach (char letter in letters)
				{
					arrA[n - 1] = letter.ToString();
					nBits(n - 1);
				}
			}
		}

		public static List<String> GenerateString(int i, string alphabet)
		{
			letters = alphabet;
			strings.Clear();
			arrA = new string[i];
			nBits(i);
			return strings;
		}
	}
}
