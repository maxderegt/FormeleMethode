using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    //this class handles DFA's
    class DFA
    {
        //The node the DFA starts from
        public Node startNode;
        //List of all nodes needed for graphs/conversion
        public List<Node> Nodes;

        //standard constructor
        public DFA(Node startNode, List<Node> nodes)
        {
            Nodes = nodes;
            this.startNode = startNode;
        }

        //this function checks if the string with the DFA and outputs it to Console
        public string Check(string s)
        {
            char[] chararr = s.ToCharArray();
            Node CurrentNode = startNode;
            //goes through each character in the string
            foreach (char character in chararr)
            {
                //goes through all the connections in the currentnode
                foreach (Connection connection in CurrentNode.connections)
                {
                    //if the connection allowed letter equals the currenct character of the string
                    if (connection.letter.Equals(character))
                    {   
                        //if so, change the current node to the next
                        CurrentNode = connection.node;
                        break;
                    }
                }
            }

           return new string($"DFA {s} is {CurrentNode.nodeType == NodeType.EndNode}");
        }

        //this functions returns true/false if the string is in the DFA
        private bool CheckBool(string s)
        {
            char[] chararr = s.ToCharArray();
            Node CurrentNode = startNode;
            //goes through each character in the string
            foreach (char character in chararr)
            {
                //goes through all the connections in the currentnode
                foreach (Connection connection in CurrentNode.connections)
                {
                    //if the connection allowed letter equals the currenct character of the string
                    if (connection.letter.Equals(character))
                    {
                        //if so, change the current node to the next
                        CurrentNode = connection.node;
                        break;
                    }
                }
            }
            //return true/false if the string is in the DFA
            return CurrentNode.nodeType == NodeType.EndNode;
        }

        //this functions generates all words in the automaton of length n
        public List<String> geefTaalTotN(int n, string alphabet)
        {
            List<String> acceptedWords = new List<string>();
            List<string> strings = GenerateStrings.GenerateString(n, alphabet);

            foreach (string item in strings)
            {
                if(CheckBool(item))
                {
                    acceptedWords.Add(item);
                }
            }

            return acceptedWords;
        }

        //this functions generates all words not in the automaton of length n
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
