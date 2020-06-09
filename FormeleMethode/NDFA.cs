using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    //this class handles NDFA's
    class NDFA
    {
        //The nodes the NDFA starts from
        public List<Node> startNodes;
        //List of all nodes needed for graphs/conversion
        public List<Node> Nodes;

        //standard constructor
        public NDFA(List<Node> startNodes, List<Node> nodes) { 
            Nodes = nodes;
            this.startNodes = startNodes;
        }

        //this function checks if the string with the NDFA and outputs it to Console
        private bool check(Node node, int i, string s)
        {
            bool Endnode = false;
            char[] chararr = s.ToCharArray();
            if (i < chararr.Length)
            {
                char character = chararr[i];
                //let's assume since there are no connections that this is the end of the check
                if (node.connections.Count == 0) 
                {
                    if (node.nodeType == NodeType.EndNode)
                        return true;
                    else 
                        return false;
                }

                //goes through all the connections in the node
                foreach (Connection connection in node.connections)
                {
                    //if the connection allowed letter equals the currenct character of the string
                    if (connection.letter.Equals(character))
                    {
                        //since NDFA, recursively check all connections
                        Endnode = check(connection.node, i + 1, s);
                        //if one check returns true, the NDFA is true;
                        if (Endnode == true)
                            return true;
                    }
                    //if allowed letter is epsilon
                    else if (connection.letter.Equals('ϵ'))
                    {
                        //check if allowed connection is one of the epsilons
                        Connection connection2 = CheckForEpsilon(connection.node, character);
                        if(connection2 != null)
                        {
                            //since NDFA, recursively check all connections
                            Endnode = check(connection2.node, i + 1, s);
                            //if one check returns true, the NDFA is true;
                            if (Endnode == true)
                                return true;
                        }
                    }
                }
            }
            if (i < s.Length)
                return false;

            //check if the last node has a epsilon connection that leads to an endnode
            bool EpsilonAsEndNode = false;
            EpsilonAsEndNode = Episilonasendnode(node);

            //if lastnode is endnode or endnode has been found or epsilon connection to endnode
            bool returnvalue = (node.nodeType == NodeType.EndNode || Endnode || EpsilonAsEndNode);
            return returnvalue;
        }

        //check if the last node has a epsilon connection that leads to an endnode
        private bool Episilonasendnode(Node node)
        {
            bool EpsilonAsEndNode = false;
            //goes through all connections and see if it has an epsilon connection to a endnode
            foreach (Connection connection1 in node.connections)
            {
                if (connection1.letter.Equals('ϵ'))
                {
                    if (connection1.node.nodeType == NodeType.EndNode)
                    {
                        EpsilonAsEndNode = true;
                        break;
                    }
                    //if epsilon connected node has epsilon connections check them as well
                    EpsilonAsEndNode = Episilonasendnode(connection1.node);
                }
            }

            return EpsilonAsEndNode;
        }


        //checks if allowed connections is one of the epsilons
        private Connection CheckForEpsilon(Node node, char character)
        {
            foreach (Connection connection in node.connections)
            {
                if (connection.letter.Equals(character))
                {
                    return connection;
                }
                else if (connection.letter.Equals('ϵ'))
                {
                    //if epsilon connected node has epsilon connections check them as well
                    Connection connection2 = CheckForEpsilon(connection.node, character);
                    if (connection2 != null)
                        return connection2;
                }
            }
            return null;
        }


        //this function checks if the string with the DFA and outputs it to Console
        public string Check(string s)
        {
            bool Endnode = false;
            foreach (var node in startNodes)
            {
                Endnode = Endnode || check(node, 0, s);
                if (Endnode)
                    break;
            }

            return new string($"NDFA {s} is {Endnode}");
        }

        //this functions returns true/false if the string is in the DFA
        private bool CheckBool(string s)
        {
            bool Endnode = false;
            foreach (var node in startNodes)
            {
                Endnode = Endnode || check(node, 0, s);
                if (Endnode)
                    break;
            }

            return Endnode;
        }

        //this functions generates all words in the automaton of length n
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
