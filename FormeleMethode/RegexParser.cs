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

    /// <summary>
    /// A basic parser that rewrites strings into a RegExp object.
    /// </summary>
    class RegexParser
    {
        /// <summary>
        /// A basic parser that rewrites strings into a RegExp object.
        /// </summary>
        /// <param name="text">The text to create a RegExp from</param>
        /// <returns>a RegExp based on the text</returns>
        public static RegExp parse(string text)
        {
            RegExp regex = new RegExp();

            Mode currentMode = 0;
            List<Char> chars = new List<char>();

            //parse each letter
            foreach (char c in text.ToCharArray())
            {
                //What does the letter mean for the conversion?
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
                        switch (currentMode) //the mode is important for handling the dot, or and one
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
            if (chars.Count != 0) //makes sure a Regexp object is properly closed off.
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
