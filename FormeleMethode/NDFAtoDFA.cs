using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FormeleMethode
{
    class NDFAtoDFA
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
        class stringcomparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                int ix = int.Parse(x);
                int iy = int.Parse(y);
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
            Filltable(myList, ndfa.Nodes);
            int size = ndfa.Nodes.Count;
            bool done = false;

            while (!done)
            {
                Filltable(myList, ndfa.Nodes);
                if (size == ndfa.Nodes.Count)
                    done = true;
            }


            return ConvertTableToDFA(myList, ndfa);
        }

        private static List<Node> Nodes = new List<Node>(); 
        public static DFA ToDFA2(NDFA ndfa)
        {
            Nodes.Clear();
            if(Fuik == null)
            {
                Fuik = new Node("q999999999", NodeType.NormalNode);
                Fuik.connections = new List<Connection> { new Connection('a', Fuik), new Connection('b', Fuik) };
            }
            List<Row> myList = GenerateTable(ndfa);
            CopyNodes(ndfa);
            Filltable(myList, Nodes);
            return ConvertTableToDFA2(myList);
        }

        private static void CopyNodes(NDFA ndfa)
        {
            foreach (Node item in ndfa.Nodes)
            {
                Nodes.Add(new Node(item.name, item.nodeType));
            }

            foreach (Node node in Nodes)
            {
                foreach (Node item in ndfa.Nodes)
                {
                    if (node.name.Equals(item.name))
                    {
                        foreach (Connection connection in item.connections)
                        {
                            foreach (Node connectednode in Nodes)
                            {
                                if (connection.node.name.Equals(connectednode.name))
                                {
                                    node.AddConnection(new Connection(connection.letter, connectednode));
                                }
                            }
                        }
                    }
                }
            }
        }

        private static DFA ConvertTableToDFA2(List<Row> myList)
        {
            List<Node> nodes = new List<Node>();
            List<Node> startnodes = new List<Node>();
            for (int row = 0; row < myList.Count; row++)
            {
                for (int column = 0; column < myList[row].row.Count; column++)
                {
                    foreach (Node node in myList[row].row[column])
                    {
                        if (!nodes.Contains(node))
                        {
                            node.connections.Clear();
                            nodes.Add(node);
                        }
                    }
                }
            }

            foreach (var node in Nodes)
            {
                if (!nodes.Contains(node) && node.nodeType == NodeType.StartNode)
                {
                    bool needstartnode = true;
                    foreach (var node2 in nodes)
                    {
                        if (node2.name.Equals(node.name))
                            needstartnode = false;
                    }
                    if (needstartnode)
                    {
                        node.connections.Clear();
                        nodes.Add(node);
                    }
                }
            }

            foreach (Node node2 in Nodes)
            {
                if(node2.nodeType == NodeType.StartNode)
                {
                    bool exists = false;
                    foreach (Node node in Nodes)
                    {
                        if (node.name.Equals(node2.name))
                        {
                            exists = true;
                            bool startnodeexist = false;
                            foreach (Node startnode in startnodes)
                            {
                                if (startnode.name.Equals(node2.name))
                                {
                                    startnodeexist = true;
                                }
                            }
                            if (!startnodeexist)
                            {
                                startnodes.Add(node2);
                            }
                        }
                    }
                    if (!exists)
                    {
                        node2.connections.Clear();
                        nodes.Add(node2);
                        startnodes.Add(node2);
                    }
                }
            }

            for (int row = 0; row < myList.Count; row++)
            {
                for (int column = 0; column < myList[row].row.Count; column++)
                {
                    foreach (Node node in nodes)
                    {
                        if (myList[row].name.Equals(node.name))
                        {
                            foreach (Node connectednode in myList[row].row[column])
                            {
                                node.AddConnection(new Connection(letters[column], connectednode));
                            }
                        }
                    }
                }
            }



            List<Node> Dontremove = new List<Node>();
            foreach (Node node in nodes)
            {
                List<Node> Found2 = new List<Node>();
                Found2.Add(node);
                getallnodes(node);

                void getallnodes(Node node)
                {
                    foreach (Connection connection in node.connections)
                    {
                        if (!Found2.Contains(connection.node))
                        {
                            Found2.Add(connection.node);
                            getallnodes(connection.node);
                        }
                    }

                };
                bool starttype = false;
                foreach (Node foundnode in Found2)
                {
                    if (foundnode.nodeType == NodeType.StartNode)
                    {
                        starttype = true;
                    }
                }
                if (starttype)
                {
                    foreach (Node node1 in Found2)
                    {
                        if (!Dontremove.Contains(node1))
                            Dontremove.Add(node1);
                    }
                }
            }


            for (int i = nodes.Count - 1; i > -1; i--)
            {
                if (!Dontremove.Contains(nodes[i]))
                    nodes.Remove(nodes[i]);
            }
            bool needfuik = false;
            foreach (Node node in nodes)
            {
                foreach (char letter in letters)
                {
                    bool found = false;
                    foreach (Connection connection in node.connections)
                    {
                        if (letter.Equals(connection.letter))
                            found = true;
                    }
                    if (!found)
                    {
                        node.AddConnection(new Connection(letter, Fuik));
                        needfuik = true;
                    }
                }
            }

            if (needfuik)
            {
                nodes.Add(Fuik);
            }

            DFA dfa = new DFA(startnodes[0], nodes);
            return dfa;
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
                        if (Fuik == null)
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
                                bool exists = false;
                                Connection newconnection = new Connection(letters[column], node);
                                foreach (Connection connection in item.connections)
                                {
                                    if (connection.letter.Equals(newconnection.letter) && connection.node.Equals(newconnection.node))
                                    {
                                        exists = true;
                                    }
                                }
                                if (!exists)
                                    item.connections.Add(newconnection);
                            }
                        }
                    }
                }
            }
            bool foundall = false;

            while (!foundall)
            {
                List<Node> Found = new List<Node>();
                foreach (Node item in ndfa.Nodes)
                {
                    if (item.nodeType == NodeType.StartNode)
                        Found.Add(item);
                    foreach (Connection connection in item.connections)
                    {
                        if (!Found.Contains(connection.node) && item != connection.node)
                        {
                            Found.Add(connection.node);
                        }
                    }
                }

                if (Found.Count >= ndfa.Nodes.Count)
                    foundall = !foundall;

                for (int i = ndfa.Nodes.Count - 1; i > -1; i--)
                {
                    if (!Found.Contains(ndfa.Nodes[i]))
                    {
                        ndfa.Nodes.Remove(ndfa.Nodes[i]);

                    }
                }

            }

            List<Node> Dontremove = new List<Node>();
            foreach (Node node in ndfa.Nodes)
            {
                List<Node> Found2 = new List<Node>();
                Found2.Add(node);
                getallnodes(node);

                void getallnodes(Node node)
                {
                    foreach (Connection connection in node.connections)
                    {
                        if (!Found2.Contains(connection.node))
                        {
                            Found2.Add(connection.node);
                            getallnodes(connection.node);
                        }
                    }

                };
                bool starttype = false;
                foreach (Node foundnode in Found2)
                {
                    if (foundnode.nodeType == NodeType.StartNode)
                    {
                        starttype = true;
                    }
                }
                if (starttype)
                {
                    foreach (Node node1 in Found2)
                    {
                        if (!Dontremove.Contains(node1))
                            Dontremove.Add(node1);
                    }
                }
            }


            for (int i = ndfa.Nodes.Count - 1; i > -1; i--)
            {
                if (!Dontremove.Contains(ndfa.Nodes[i]))
                    ndfa.Nodes.Remove(ndfa.Nodes[i]);
            }

            int greatest = -1;

            foreach (Node item in ndfa.Nodes)
            {
                int i = 0;
                foreach (char letter in item.name)
                {
                    if (letter.Equals('q'))
                        i++;
                }
                if (i > 1)
                {
                    greatest = -1;

                    foreach (Node node in ndfa.Nodes)
                    {
                        if (!node.name.Equals("fuik"))
                        {
                            bool done = true;
                            string number = "";
                            foreach (char letter in node.name)
                            {
                                if (letter.Equals('q'))
                                {
                                    done = !done;
                                }
                                else
                                {
                                    number += letter;
                                }
                                if (done)
                                {
                                    break;
                                }

                            }
                            int i2 = int.Parse(number);
                            if (i2 > greatest)
                                greatest = i2;
                        }

                    }
                    greatest++;
                    item.name = "q" + int.Parse(greatest.ToString());
                }
            }
            greatest++;
            foreach (Node item in ndfa.Nodes)
            {
                if (item.name.Equals("fuik"))
                {
                    item.name = "q" + int.Parse(greatest.ToString());
                    break;
                }
            }

            comparer compare = new comparer();
            ndfa.Nodes.Sort(compare);

            for (int i = 0; i < ndfa.Nodes.Count; i++)
            {
                ndfa.Nodes[i].name = "q" + i;
            }

            return new DFA(startnode, ndfa.Nodes);
        }

        private static void Filltable(List<Row> myList, List<Node> ndfa)
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
                        //replace with new sort after name
                        //comparer compare = new comparer();
                        //nodes.Sort(compare);

                        foreach (Node item in nodes)
                        {
                            name = name + item.name;
                        }

                        string[] words = name.Split('q');
                        List<int> splits = new List<int>();
                        foreach (string item in words)
                        {
                            if (!item.Equals(""))
                                splits.Add(int.Parse(item));
                        }
                        for (int i = 0; i < splits.Count; i++)
                        {
                            splits.Sort();
                        }
                        name = "";
                        foreach (int item in splits)
                        {

                            name = name + "q" + item;
                        }


                        bool exists = false;
                        foreach (Node item in ndfa)
                        {
                            if (item.name.Equals(name))
                                exists = true;
                        }

                        if (!exists)
                        {
                            Node newnode = new Node(name, nodeType);
                            ndfa.Add(newnode);
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
                       
                        foreach (Node item in nodes)
                        {
                            name = name + item.name;
                        }

                        string[] words = name.Split('q');
                        List<int> splits = new List<int>();
                        foreach (string item in words)
                        {
                            if (!item.Equals(""))
                                splits.Add(int.Parse(item));
                        }
                        for (int i = 0; i < splits.Count; i++)
                        {
                            splits.Sort();
                        }
                        name = "";
                        foreach (int item in splits)
                        {

                            name = name + "q" + item;
                        }

                        foreach (Node item in ndfa)
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
