using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FormeleMethode
{
    class comparer : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            int ix = int.Parse(x.name.Substring(x.name.Length - 1));
            int iy = int.Parse(y.name.Substring(x.name.Length - 1));
            if (ix == 0 || iy == 0)
            {
                return 0;
            }

            // CompareTo() method 
            return ix.CompareTo(iy);

        }
    }
    class DFAReverse
    {
        private static List<Node> nodes = new List<Node>();
        public static NDFA Reverse(DFA orignal)
        {
            List<Node> startnodes = new List<Node>();
            check(orignal.startNode);

            List<Node> newnodes = nodes.ConvertAll(node => 
            new Node(node.connections.ConvertAll(connection => new Connection(connection.letter, connection.node)), 
            node.name, node.nodeType));

            foreach (Node node in newnodes)
            {
                node.connections.Clear();
            }

            comparer compare = new comparer();

            newnodes.Sort(compare);

            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = newnodes[i];
                Node orignalnode = null;
                foreach (Node item in nodes)
                {
                    if (item.name.Equals(node.name))
                        orignalnode = item;
                }

                foreach (Connection connection in orignalnode.connections)
                {
                    foreach (Node item in newnodes)
                    {
                        if (item.name.Equals(connection.node.name))
                        {
                            item.connections.Add(new Connection(connection.letter, node));
                        }
                    }
                }

                if(orignalnode.nodeType == NodeType.StartNode)
                {
                    node.nodeType = NodeType.EndNode;
                }
                else if (orignalnode.nodeType == NodeType.EndNode) {
                    node.nodeType = NodeType.StartNode;
                    startnodes.Add(node);
                }
                newnodes[i] = node;
            }

            if (startnodes.Count > 0)
            {
                List<Node> allnodes = new List<Node>();
                for (int i = 0; i < newnodes.Count; i++)
                {
                    allnodes.Add(newnodes[i]);
                }
                return new NDFA(startnodes, allnodes);
            }
            else
                return null;
        }

        private static void check(Node node)
        {
            if (!nodes.Contains(node))
            {
                nodes.Add(node);
            }
            foreach (Connection item in node.connections)
            {
                if (!nodes.Contains(item.node))
                {
                    nodes.Add(item.node);
                    check(item.node);
                }
            }
        }
    }
}
