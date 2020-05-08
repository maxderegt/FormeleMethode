using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    class NDFA
    {
        public List<Node> startNodes;

        public NDFA(List<Node> startNodes)
        {
            this.startNodes = startNodes;
        }

        private bool check(Node node, int i, string s)
        {
            bool Endnode = false;
            char[] chararr = s.ToCharArray();
            if (i < chararr.Length)
            {
                char character = chararr[i];
                foreach (Connection connection in node.connections)
                {
                    if (connection.letter.Equals(character))
                    {
                        Endnode = check(connection.node, i + 1, s);
                        if (Endnode == true)
                            return true;
                    }
                }
            }
            return (node.nodeType == NodeType.EndNode || Endnode);
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
