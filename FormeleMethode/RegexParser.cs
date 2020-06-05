using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    enum Mode
    {
        nothing,
        or,
        dot
    }
    class RegexParser
    {
        public static RegExp parse(string text)
        {
            RegExp regex = new RegExp();

            Mode currentMode = 0;
            List<Char> chars = new List<char>();

            foreach (char c in text.ToCharArray())
            {
                switch(c)
                {
                    case '(':
                        break;
                    case ')':
                        RegExp reg = new RegExp();
                        foreach (Char ch in chars)
                        {
                            if (reg.left == null && reg.right == null && reg.terminals == "") reg = new RegExp(ch.ToString());
                            else reg = reg.dot(new RegExp(ch.ToString()));
                        }
                        chars.Clear();
                        regex = regex.or(reg);
                        currentMode = 0;
                        break;
                    case '|':
                        currentMode = Mode.or;
                        break;
                    case '+':
                        regex = regex.plus();
                        break;
                    case '*':
                        regex = regex.star();
                        break;
                    case '.':
                        currentMode = Mode.dot;
                        break;
                    default:
                        switch (currentMode)
                        {
                            case Mode.nothing:
                                if (regex.left == null && regex.right == null && regex.terminals == "") regex = new RegExp(c.ToString());
                                else regex = regex.dot(new RegExp(c.ToString()));
                                break;
                            case Mode.or:
                                chars.Add(c);
                                break;
                            case Mode.dot:
                                regex = regex.dot(new RegExp(c.ToString()));
                                break;
                        }
                        break;
                }

            }
            if (chars.Count != 0)
            {
                RegExp reg = new RegExp();
                foreach (Char ch in chars)
                {
                    if (reg.left == null && reg.right == null && reg.terminals == "") reg = new RegExp(ch.ToString());
                    else reg = reg.dot(new RegExp(ch.ToString()));
                }
                regex = regex.or(reg);
                currentMode = 0;
            }

            return regex;
        }
    }
}
