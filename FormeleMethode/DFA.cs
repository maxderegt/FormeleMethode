using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    class DFA
    {
        public Node startNode;

        public DFA(Node startNode)
        {
            this.startNode = startNode;
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
    
    }


}
