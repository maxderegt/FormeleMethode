using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    public class CompareByLength : IComparer<string>
    {
        public int Compare(string y, string x)
        {
            var s1 = y;
            var s2 = x;
            if (s1 == null || s2 == null) return -1;

            if (s1.Length == s2.Length)
            {
                return s1.CompareTo(s2);
            }

            return s1.Length - s2.Length;
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

        public RegExp left;
        public RegExp right;

        public Operator _operator;
        public String terminals;

        /// <summary>
        /// Single letters
        /// </summary>
        public RegExp()
        {
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
            left = null;
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
            result.left = this;
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
            result.left = this;
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
            result.left = this;
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
            result.left = this;
            result.right = e2;
            return result;
        }

        public SortedSet<String> getLanguage(int maxSteps)
        {
            //compare by lenght?
            SortedSet<String> emptyLanguage = new SortedSet<string>(new CompareByLength());
            SortedSet<String> languageResult = new SortedSet<string>(new CompareByLength());

            SortedSet<String> languageLeft, languageRight;

            if (maxSteps < 1) return emptyLanguage;

            switch (this._operator)
            {
                case Operator.ONE:
                    languageResult.Add(terminals);
                    break;

                case Operator.OR:
                    languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                    languageRight = right == null ? emptyLanguage : right.getLanguage(maxSteps - 1);
                    foreach (var l in languageLeft)
                        languageResult.Add(l);

                    foreach (var r in languageRight)
                        languageResult.Add(r);
                    break;


                case Operator.DOT:
                    languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                    languageRight = right == null ? emptyLanguage : right.getLanguage(maxSteps - 1);
                    foreach (var s1 in languageLeft)
                        foreach (var s2 in languageRight)
                        { languageResult.Add(s1 + s2); }
                    break;

                // STAR(*) en PLUS(+) kunnen we bijna op dezelfde manier uitwerken:
                case Operator.STAR:
                case Operator.PLUS:
                    languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                    foreach (var l in languageLeft)
                        languageResult.Add(l);
                    for (int i = 1; i < maxSteps; i++)
                    {
                        HashSet<String> languageTemp = new HashSet<String>(languageResult);
                        foreach (var s1 in languageLeft)
                        {
                            foreach (var s2 in languageTemp)
                            {
                                languageResult.Add(s1 + s2);
                            }
                        }
                    }
                    if (this._operator == Operator.STAR)
                    { languageResult.Add(""); }
                    break;

                default:
                    Console.WriteLine("getLanguage is nog niet gedefinieerd voor de operator: " + this._operator);
                    break;
            }


            return languageResult;
        }

        public override string ToString()
        {
            string leftS = "", rightS = "", regS = "";
            if (left != null) leftS = left.ToString();
            if (right != null) rightS = right.ToString();

            switch (_operator)
            {
                case Operator.PLUS:
                    regS = $"{leftS}+";
                    break;
                case Operator.STAR:
                    regS = $"{leftS}*";
                    break;
                case Operator.OR:
                    regS = $"({leftS}|{rightS})";
                    break;
                case Operator.DOT:
                    regS = $"({leftS}.{rightS})";
                    break;
                case Operator.ONE:
                    regS = terminals;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return regS;
        }
    }
}
