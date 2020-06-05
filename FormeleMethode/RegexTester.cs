using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FormeleMethode
{
    class RegexTester
    {
        System.Text.RegularExpressions.Regex rg;

        public RegexTester(string pattern)
        {
            rg = new System.Text.RegularExpressions.Regex(pattern);
        }

        public string Check(string s)
        {
            bool Endnode = false;
            Match match = rg.Match(s);
            Endnode = match.Value == s? true: false;
            return new string($"String {s} is {Endnode}");
        }

        private bool CheckBool(string s)
        {
            bool Endnode = false;
            Match match = rg.Match(s);
            return match.Value == s ? true : false;
        }

        public List<String> geefTaalTotN(int n, string alphabet)
        {
            List<String> acceptedWords = new List<string>();
            List<string> strings = GenerateStrings.GenerateString(n, alphabet);

            foreach (string item in strings)
            {
                if (CheckBool(item))
                {
                    acceptedWords.Add(item);
                }
            }

            return acceptedWords;
        }

        public List<String> geefFoutieveTaalTotN(int n, string alphabet)
        {
            List<String> nonAcceptedWords = new List<string>();
            List<string> strings = GenerateStrings.GenerateString(n, alphabet);

            foreach (string item in strings)
            {
                if (!CheckBool(item))
                {
                    nonAcceptedWords.Add(item);
                }
            }

            return nonAcceptedWords;
        }
    }
}
