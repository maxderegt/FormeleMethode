using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    class DFA
    {
        public Node startNode;
        public List<Node> Nodes;

        public DFA(Node startNode)
        {
            this.startNode = startNode;
        }

        public DFA(Node startNode, List<Node> nodes) : this(startNode)
        {
            Nodes = nodes;
        }
        public bool Check(string s, bool tf)
        {
            char[] chararr = s.ToCharArray();
            Node CurrentNode = startNode;
            foreach (char character in chararr)
            {
                foreach (Connection connection in CurrentNode.connections)
                {
                    if (connection.letter.Equals(character))
                    {
                        CurrentNode = connection.node;
                        break;
                    }
                }
            }

            return CurrentNode.nodeType == NodeType.EndNode;
        }

        public string Check(string s)
        {
            char[] chararr = s.ToCharArray();
            Node CurrentNode = startNode;
            foreach (char character in chararr)
            {
                foreach (Connection connection in CurrentNode.connections)
                {
                    if (connection.letter.Equals(character))
                    {   
                        CurrentNode = connection.node;
                        break;
                    }
                }
            }

           return new string($"String {s} ends at node {CurrentNode.name} and is {CurrentNode.nodeType == NodeType.EndNode}");
        }

        private bool CheckBool(string s)
        {
            char[] chararr = s.ToCharArray();
            Node CurrentNode = startNode;
            foreach (char character in chararr)
            {
                foreach (Connection connection in CurrentNode.connections)
                {
                    if (connection.letter.Equals(character))
                    {
                        CurrentNode = connection.node;
                        break;
                    }
                }
            }

            return CurrentNode.nodeType == NodeType.EndNode;
        }

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
