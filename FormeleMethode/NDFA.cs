using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    class NDFA
    {
        public List<Node> startNodes;
        public List<Node> Nodes;

        public NDFA(List<Node> startNodes, List<Node> nodes) { 
            Nodes = nodes;
            this.startNodes = startNodes;
        }

        private bool check(Node node, int i, string s)
        {
            bool Endnode = false;
            char[] chararr = s.ToCharArray();
            if (i < chararr.Length)
            {
                char character = chararr[i];
                if (node.connections.Count == 0) //let's assume since there are no connections that this is the end of the check
                {
                    if (node.nodeType == NodeType.EndNode)
                        return true;
                    else 
                        return false;
                }

                foreach (Connection connection in node.connections)
                {
                    if (connection.letter.Equals(character))
                    {
                        Endnode = check(connection.node, i + 1, s);
                        if (Endnode == true)
                            return true;
                    }
                    else if (connection.letter.Equals('ϵ'))
                    {
                        Connection connection2 = CheckForEpsilon(connection.node, character);
                        if(connection2 != null)
                        {
                            Endnode = check(connection2.node, i + 1, s);
                            if (Endnode == true)
                                return true;
                        }
                    }
                }
            }
            if (i < s.Length)
                return false;

            bool EpsilonAsEndNode = false;
            EpsilonAsEndNode = Episilonasendnode(node);
            bool returnvalue = (node.nodeType == NodeType.EndNode || Endnode || EpsilonAsEndNode);
            return returnvalue;
        }

        private bool Episilonasendnode(Node node)
        {
            bool EpsilonAsEndNode = false;
            foreach (Connection connection1 in node.connections)
            {
                if (connection1.letter.Equals('ϵ'))
                {
                    if (connection1.node.nodeType == NodeType.EndNode)
                    {
                        EpsilonAsEndNode = true;
                        break;
                    }
                    EpsilonAsEndNode = Episilonasendnode(connection1.node);
                }
            }

            return EpsilonAsEndNode;
        }

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
                    Connection connection2 = CheckForEpsilon(connection.node, character);
                    if (connection2 != null)
                        return connection2;
                }
            }
            return null;
        }


        public string Check(string s)
        {
            bool Endnode = false;
            foreach (var node in startNodes)
            {
                Endnode = Endnode || check(node, 0, s);
                if (Endnode)
                    break;
            }

            return new string($"String {s} is {Endnode}");
        }
    }
}
