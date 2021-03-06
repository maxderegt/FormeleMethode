﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FormeleMethode
{
    /// <summary>
    /// Uses the RegularExpressions namespace to check if string matches a regular expression.
    /// </summary>
    class RegexTester
    {
        Regex rg;

        /// <summary>
        /// creates an regular expression based on the input string
        /// </summary>
        /// <param name="pattern">The regular expression</param>
        public RegexTester(string pattern)
        {
            rg = new Regex(pattern);
        }

        /// <summary>
        /// Checks if a word is valid
        /// </summary>
        /// <param name="s">the word to check</param>
        /// <returns>A string with the answer</returns>
        public string Check(string s)
        {
            bool Endnode = false;
            Match match = rg.Match(s); //returns the found matches
            Endnode = match.Value == s? true: false; //checks if it is a full word match
            return new string($"Regex {s} is {Endnode}");
        }

        /// <summary>
        /// Checks if a word is valid
        /// </summary>
        /// <param name="s">the word to check</param>
        /// <returns>A boolean with the answer</returns>
        private bool CheckBool(string s)
        {
            //bool Endnode = false;
            Match match = rg.Match(s); //returns the found matches
            return match.Value == s ? true : false; //checks if it is a full word match
        }

        /// <summary>
        /// returns all valid words given length and an alphabet that are valid in our language
        /// </summary>
        /// <param name="n">how many letters to generate words with</param>
        /// <param name="alphabet">what letters are available in this language</param>
        /// <returns>A list of all valid words</returns>
        public List<String> geefTaalTotN(int n, string alphabet)
        {
            List<String> acceptedWords = new List<string>(); //list of accepted words
            List<string> strings = GenerateStrings.GenerateString(n, alphabet); //generates a list of all strings that will be checked

            foreach (string item in strings) //check each word in strings to find all valid words
            {
                if (CheckBool(item))
                {
                    acceptedWords.Add(item); //word was valid and added to the list
                }
            }

            return acceptedWords; 
        }

        /// <summary>
        /// returns all invalid words given length and an alphabet that are valid in our language
        /// </summary>
        /// <param name="n">how many letters to generate words with</param>
        /// <param name="alphabet">what letters are available in this language</param>
        /// <returns>A list of all invalid words</returns>
        public List<String> geefFoutieveTaalTotN(int n, string alphabet)
        {
            List<String> nonAcceptedWords = new List<string>(); //list of non accepted words
            List<string> strings = GenerateStrings.GenerateString(n, alphabet); //generates a list of all strings that will be checked

            foreach (string item in strings) //check each word in strings to find all valid words
            {
                if (!CheckBool(item))
                {
                    nonAcceptedWords.Add(item); //word was invalid and added to the list
                }
            }

            return nonAcceptedWords;
        }
    }
}
