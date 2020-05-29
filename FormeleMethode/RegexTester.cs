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
    }
}
