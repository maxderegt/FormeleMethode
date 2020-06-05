using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FormeleMethode
{
    class NDFAtoDFA
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
        class Row
        {
            public List<List<Node>> row = new List<List<Node>>();
            public string name;

            public Row(string name)
            {
                this.name = name;
            }
        }
        private static List<Char> letters = new List<char>();
        private static Node Fuik = null;
        public static DFA ToDFA(NDFA ndfa)
        {
            List<Row> myList = GenerateTable(ndfa);
            Filltable(myList, ndfa);
            int size = ndfa.Nodes.Count;
            bool done = false;

            while (!done)
            {
                Filltable(myList, ndfa);
                if (size == ndfa.Nodes.Count)
                    done = true;
            }


            return ConvertTableToDFA(myList, ndfa);
        }
        
        private static DFA ConvertTableToDFA(List<Row> myList, NDFA ndfa)
        {
            Node startnode = null;
            foreach (Node item in ndfa.Nodes)
            {
                item.connections.Clear();
                if (item.nodeType == NodeType.StartNode)
                    startnode = item;
            }
            for (int row = 0; row < myList.Count; row++)
            {
                for (int column = 0; column < myList[row].row.Count; column++)
                {
                    if (myList[row].row[column].Count == 0)
                    {
                        if(Fuik == null)
                        {
                            Fuik = new Node("fuik", NodeType.NormalNode);
                            Fuik.connections = new List<Connection> { new Connection('a', Fuik), new Connection('b', Fuik) };
                            ndfa.Nodes.Add(Fuik);
                        }
                        myList[row].row[column].Add(ndfa.Nodes[ndfa.Nodes.Count - 1]);
                    }

                    string name = myList[row].name;

                    foreach (Node item in ndfa.Nodes)
                    {
                        if (name.Equals(item.name))
                        {
                            foreach (Node node in myList[row].row[column])
                            {
                                item.connections.Add(new Connection(letters[column], node));
                            }
                        }
                    }
                }
            }

            List<Node> Found = new List<Node>();
            foreach (Node item in ndfa.Nodes)
            {
                if (item.nodeType == NodeType.StartNode)
                    Found.Add(item);
                foreach (Connection connection in item.connections)
                {
                    if (!Found.Contains(connection.node))
                    {
                        Found.Add(connection.node);
                    }
                }
            }
            for (int i = ndfa.Nodes.Count-1; i > 0; i--)
            {
                if (!Found.Contains(ndfa.Nodes[i]))
                {
                    ndfa.Nodes.Remove(ndfa.Nodes[i]);
                    
                }
            }
            
            return new DFA(startnode, ndfa.Nodes);
        }

        private static void Filltable(List<Row> myList, NDFA ndfa)
        {
            for (int row = 0; row < myList.Count; row++)
            {
                for (int column = 0; column < myList[row].row.Count; column++)
                {
                    if (myList[row].row[column].Count > 1)
                    {
                        NodeType nodeType = NodeType.NormalNode;

                        for (int connections = 0; connections < myList[row].row[column].Count; connections++)
                        {
                            if (myList[row].row[column][connections].nodeType == NodeType.EndNode)
                                nodeType = NodeType.EndNode;
                        }
                        string name = "";
                        List<Node> nodes = new List<Node>();
                        foreach (Node item in myList[row].row[column])
                        {
                            nodes.Add(item);
                        }
                        comparer compare = new comparer();
                        nodes.Sort(compare);

                        foreach (Node item in nodes)
                        {
                            name = name + item.name;
                        }

                        bool exists = false;
                        foreach (Node item in ndfa.Nodes)
                        {
                            if (item.name.Equals(name))
                                exists = true;
                        }

                        if (!exists)
                        {
                            Node newnode = new Node(name, nodeType);
                            ndfa.Nodes.Add(newnode);
                            Row newrow = new Row(name);
                            foreach (char letter in letters)
                            {
                                newrow.row.Add(new List<Node>());
                            }
                            foreach (Row item in myList)
                            {
                                foreach (Node node in nodes)
                                {
                                    if (item.name.Equals(node.name))
                                    {
                                        for (int i = 0; i < letters.Count; i++)
                                        {
                                            foreach (Node columnNode in item.row[i])
                                            {
                                                if (!newrow.row[i].Contains(columnNode))
                                                    newrow.row[i].Add(columnNode);
                                            }
                                        }


                                    }
                                }
                            }
                            myList.Add(newrow);
                            
                        }

                    }
                }
            }

            for (int row = 0; row < myList.Count; row++)
            {
                for (int column = 0; column < myList[row].row.Count; column++)
                {
                    if (myList[row].row[column].Count > 1)
                    {
                        string name = "";
                        List<Node> nodes = new List<Node>();
                        foreach (Node item in myList[row].row[column])
                        {
                            nodes.Add(item);
                        }
                        comparer compare = new comparer();
                        nodes.Sort(compare);

                        foreach (Node item in nodes)
                        {
                            name = name + item.name;
                        }

                        foreach (Node item in ndfa.Nodes)
                        {
                            if (item.name.Equals(name))
                            {
                                myList[row].row[column].Clear();
                                myList[row].row[column].Add(item);
                            }
                        }
                    }
                }
            }

        }


        private static List<Row> GenerateTable(NDFA ndfa)
        {
            List<Row> myList = new List<Row>();

            foreach (Node item in ndfa.Nodes)
            {
                foreach (Connection connection in item.connections)
                {
                    if (!letters.Contains(connection.letter) && !connection.letter.Equals('ϵ'))
                    {
                        letters.Add(connection.letter);
                    }
                }
            }


            foreach (Node item in ndfa.Nodes)
            {
                Row row = new Row(item.name);
                foreach (char letter in letters)
                {
                    row.row.Add(new List<Node>());
                }

                foreach (Connection connection in item.connections)
                {
                    for (int i = 0; i < letters.Count; i++)
                    {
                        if (connection.letter.Equals(letters[i]))
                        {
                            row.row[i].Add(connection.node);
                            foreach (Connection connection1 in connection.node.connections)
                            {
                                if (connection1.letter.Equals('ϵ'))
                                {
                                    List<Node> secondlist = EndOnNodeWithEpsilon(connection1.node);
                                    foreach (Node epsilonnode in secondlist)
                                    {
                                        if (!row.row[i].Contains(epsilonnode))
                                            row.row[i].Add(epsilonnode);
                                    }

                                }
                            }
                        }
                        else if (connection.letter.Equals('ϵ'))
                        {
                            List<List<Node>> results = EpsilonConnection(connection.node);
                            for (int i2 = 0; i2 < letters.Count; i2++)
                            {
                                foreach (Node node in results[i2])
                                {
                                    if (!row.row[i2].Contains(node))
                                        row.row[i2].Add(node);
                                }
                            }
                        }

                    }
                }
                myList.Add(row);
            }
            return myList;
        }

        private static List<List<Node>> EpsilonConnection(Node node)
        {
            List<List<Node>> row = new List<List<Node>>();
            foreach (char letter in letters)
            {
                row.Add(new List<Node>());
            }

            foreach (Connection connection in node.connections)
            {
                for (int i = 0; i < letters.Count; i++)
                {
                    if (connection.letter.Equals(letters[i]))
                    {
                        row[i].Add(connection.node);
                        foreach (Connection connection1 in connection.node.connections)
                        {
                            if (connection1.letter.Equals('ϵ'))
                            {
                                List<Node> secondlist = EndOnNodeWithEpsilon(connection1.node);
                                foreach (Node epsilonnode in secondlist)
                                {
                                    row[i].Add(epsilonnode);
                                }

                            }
                        }
                    }
                    else if (connection.letter.Equals('ϵ'))
                    {
                        List<List<Node>> results = EpsilonConnection(connection.node);
                        for (int i2 = 0; i2 < letters.Count; i2++)
                        {
                            foreach (Node item in results[i2])
                            {
                                row[i2].Add(item);
                            }
                        }
                    }

                }
            }

            return row;
        }

        private static List<Node> EndOnNodeWithEpsilon(Node node)
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(node);
            foreach (Connection connection in node.connections)
            {
                if (connection.letter.Equals('ϵ'))
                {
                    List<Node> morenodes = EndOnNodeWithEpsilon(node);
                    foreach (Node item in morenodes)
                    {
                        nodes.Add(item);
                    }
                }
            }

            return nodes;
        }
    }
}
