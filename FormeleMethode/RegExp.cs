using System;
using System.Collections.Generic;

namespace FormeleMethode
{
    public class CompareByLength : IComparer<string>
    {
        public int Compare(string y, string x)
        {
            var s1 = y; //string node 1
            var s2 = x; //string node 2
            if (s1 == null || s2 == null) return -1; //nothing to compare

            if (s1.Length == s2.Length) //if of equal length, do a string compare
            {
                return s1.CompareTo(s2); 
            }

            return s1.Length - s2.Length; //return the longest
        }
    }

    /// <summary>
    /// Class made by Paul de Mast in Java, rewritten to C# to work with Regex.
    /// </summary>
    public class RegExp
    {
        // De mogelijke operatoren voor een reguliere expressie (+, *, |, .) 
        // Daarnaast ook een operator definitie voor 1 keer repeteren (default)
        public enum Operator { PLUS, STAR, OR, DOT, ONE }

        public RegExp left; //left side of RegExp object 
        public RegExp right; //right side of RegExp object 

        public Operator _operator; //operator of RegExp object (ONE, STAR, PLUS, DOT, OR)
        public String terminals;

        /// <summary>
        /// Single letters
        /// </summary>
        public RegExp()
        { //nothing detected, all can be set to default/null
            _operator = Operator.ONE; 
            terminals = "";
            left = null; 
            right = null;
        }

        /// <summary>
        /// Single letters
        /// </summary>
        /// <param name="p"></param>
        public RegExp(String p)
        { 
            _operator = Operator.ONE;
            terminals = p;
            left = null; //single character, which means there is no left or right side
            right = null;
        }

        /// <summary>
        /// + symbol
        /// </summary>
        /// <returns></returns>
        public RegExp plus()
        {
            RegExp result = new RegExp();
            result._operator = Operator.PLUS;
            result.left = this; //a plus always contains the text to the left in Regular expressions
            return result;
        }

        /// <summary>
        /// * symbol
        /// </summary>
        /// <returns></returns>
        public RegExp star()
        {
            RegExp result = new RegExp();
            result._operator = Operator.STAR;
            result.left = this; //a star always contains the RegExp to the left in Regular expressions
            return result;
        }

        /// <summary>
        /// | symbol
        /// </summary>
        /// <param name="e2"></param>
        /// <returns></returns>
        public RegExp or(RegExp e2)
        {
            RegExp result = new RegExp();
            result._operator = Operator.OR;
            result.left = this; //a or has RegExp both on it's left side as well as to the right of it
            result.right = e2;
            return result;
        }

        /// <summary>
        /// . symbol
        /// </summary>
        /// <param name="e2"></param>
        /// <returns></returns>
        public RegExp dot(RegExp e2)
        {
            RegExp result = new RegExp();
            result._operator = Operator.DOT;
            result.left = this; //a dot contains it's old RegExp to the left
            result.right = e2; //the new RegExp to the right of a dot
            return result;
        }

        /// <summary>
        /// Writes the RegExp to a string format, this works recursively 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string leftS = "", rightS = "", regS = "";
            if (left != null) leftS = left.ToString(); //there is content in the RegExp left side, check and store it's content with tostring
            if (right != null) rightS = right.ToString(); //there is content in the Regexp right side, check and store it's content with tostring

            switch (_operator) //the operator is important to check to know which symbols to place and which side of an operator there is a RegExp 
            {
                case Operator.PLUS: //a plus only contains a RegExp on the left side
                    regS = $"{leftS}+";
                    break;
                case Operator.STAR:
                    regS = $"{leftS}*"; //a star only contains a RegExp on the left side
                    break;
                case Operator.OR:
                    regS = $"({leftS}|{rightS})"; //a or contains RegExp on both sides
                    break;
                case Operator.DOT:
                    regS = $"({leftS}.{rightS})"; //a dot contains RegExp on both sides
                    break;
                case Operator.ONE:
                    regS = terminals; //single symbol is in the terminal
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return regS;
        }
    }
}