using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    enum Mode //some operators need to be kept in mind while parsing, those are the or and dot
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
            RegExp regex = new RegExp(); //the output RegExp object

            Mode currentMode = 0; //no dot or or found yet
            List<Char> chars = new List<char>(); //memory of all found characters, needed for ) and or

            //parse each letter
            foreach (char c in text.ToCharArray())
            {
                //What does the letter mean for the conversion?
                switch(c)
                {
                    case '(': //can be ignored
                        break;
                    case ')': //handle all leftover characters since we are going up in depth
                        RegExp reg = new RegExp();
                        foreach (Char ch in chars)
                        {
                            if (reg.left == null && reg.right == null && reg.terminals == "") reg = new RegExp(ch.ToString()); //if it's the first charachter
                            else reg = reg.dot(new RegExp(ch.ToString())); //there was atleast 1 character before this one
                        }
                        chars.Clear(); //chars are used, array can be cleard
                        regex = regex.or(reg);
                        currentMode = 0; //or used to set mode back to nothing
                        break;
                    case '|': //or detected
                        currentMode = Mode.or;
                        break;
                    case '+': //+ detected
                        regex = regex.plus();
                        break;
                    case '*': //star detected
                        regex = regex.star();
                        break;
                    case '.': //dot detected
                        currentMode = Mode.dot;
                        break;
                    default: //no special character detected
                        switch (currentMode) //the mode is important for handling the dot, or and one
                        {
                            case Mode.nothing:
                                if (regex.left == null && regex.right == null && regex.terminals == "") regex = new RegExp(c.ToString()); //if it's the first charachter
                                else regex = regex.dot(new RegExp(c.ToString())); //there was atleast 1 character before this one
                                break;
                            case Mode.or:
                                chars.Add(c); //an or expects all characters in one RegExp object, add them to an array first
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
                    if (reg.left == null && reg.right == null && reg.terminals == "") reg = new RegExp(ch.ToString()); //if it's the first charachter
                    else reg = reg.dot(new RegExp(ch.ToString())); //there was atleast 1 character before this one
                }
                regex = regex.or(reg);
            }

            return regex;
        }
    }
}