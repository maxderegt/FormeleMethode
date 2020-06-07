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
            int ix = int.Parse(x.name.Substring(1));
            int iy = int.Parse(y.name.Substring(1));
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
        public static NDFA Reverse2(DFA orignal)
        {
            List<Node> nodes = new List<Node>();
            List<Node> startnodes = new List<Node>();
            foreach (Node item in orignal.Nodes)
            {
                NodeType type = NodeType.NormalNode;
                if (item.nodeType == NodeType.StartNode)
                    type = NodeType.EndNode;
                else if (item.nodeType == NodeType.EndNode)
                    type = NodeType.StartNode;
                Node newnode = new Node(item.name, type);
                nodes.Add(newnode);

                if (newnode.nodeType == NodeType.StartNode)
                    startnodes.Add(newnode);
            }

            foreach (Node node in orignal.Nodes)
            {
                foreach (Connection connection in node.connections)
                {
                    foreach (Node newnode in nodes)
                    {
                        if (newnode.name.Equals(connection.node.name))
                        {
                            foreach (Node fromnode in nodes)
                            {
                                if (fromnode.name.Equals(node.name))
                                {
                                    newnode.AddConnection(new Connection(connection.letter, fromnode));
                                }
                            }
                        }
                    }
                }
            }
            NDFA ndfa = new NDFA(startnodes, nodes);
            return ndfa;
        }


        public static NDFA Reverse(DFA orignal)
        {
            List<Node> nodes;
            List<Node> startnodes = new List<Node>();
            nodes = orignal.Nodes;
            List<Node> newnodes = new List<Node>();

            foreach (Node item in nodes)
            {
                Node newnode = new Node(item.name, item.nodeType);
                newnodes.Add(newnode);
            }

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
    }
}
