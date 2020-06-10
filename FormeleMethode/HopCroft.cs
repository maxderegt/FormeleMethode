using System.Collections.Generic;

namespace FormeleMethode
{
    class HopCroft
    {
        public static DFA MinimizeHopCroft(DFA nonMinimizedDFA)
        {
            //step 1, remove unreachable states
            nonMinimizedDFA = RemoveStates(nonMinimizedDFA); //removes all unused states before optimizing

            //step 2, equivalent states are merged together
            nonMinimizedDFA = Minimize(nonMinimizedDFA);

            //step 3, partitioning
            DFA minimizedDFA = Partitioning(nonMinimizedDFA);

            return minimizedDFA;
        }

        /// <summary>
        /// Used for filtering unused nodes (minimizing starts somewhere)
        /// </summary>
        /// <param name="nonMinimizedDFA"></param>
        /// <returns></returns>
        private static DFA RemoveStates(DFA nonMinimizedDFA)
        {
            List<Node> Dontremove = new List<Node>();
            foreach (Node node in nonMinimizedDFA.Nodes)
            {
                List<Node> Found = new List<Node>();
                Found.Add(node);
                getallnodes(node);

                void getallnodes(Node node)
                {
                    foreach (Connection connection in node.connections)
                    {
                        if (!Found.Contains(connection.node))
                        {
                            Found.Add(connection.node);
                            getallnodes(connection.node);
                        }
                    }

                };
                bool starttype = false;
                foreach (Node foundnode in Found)
                {
                    if (foundnode.nodeType == NodeType.StartNode)
                    {
                        starttype = true;
                    }
                }
                if (starttype)
                {
                    foreach (Node node1 in Found)
                    {
                        if (!Dontremove.Contains(node1))
                            Dontremove.Add(node1);
                    }
                }
            }

            for (int i = nonMinimizedDFA.Nodes.Count - 1; i > -1; i--)
            {
                if (!Dontremove.Contains(nonMinimizedDFA.Nodes[i]))
                    nonMinimizedDFA.Nodes.Remove(nonMinimizedDFA.Nodes[i]);
            }

            return nonMinimizedDFA;
        }

        private static DFA Minimize(DFA nonMinimizedDFA)
        {
            bool optimized = false;
            DFA minimizedDFA = nonMinimizedDFA;

            while (!optimized) //this ensures we keep looping though all nodes untill we are 100% certain there can't be any more optimizations
            {
                optimized = true;
                Node node1ToOptimize = null;
                int node1Index = 0;
                Node node2ToOptimize = null;
                int node2Index = 0;
                nonMinimizedDFA = minimizedDFA;

                //if the language is accepted from multiple states, they are equivelent
                foreach (Node node1 in nonMinimizedDFA.Nodes)
                {
                    if (!optimized) break;
                    foreach (Node node2 in nonMinimizedDFA.Nodes)
                    {
                        if (!optimized) break;
                        if (node1 == node2) continue; //no use checking a connection with the same node
                        if (node1.connections.Count == node2.connections.Count && node1.connections.Count != 0) //equivalent in connection count and more then 0 connections
                        {
                            bool match = true;
                            int index = 0;
                            while (index < node1.connections.Count)
                            {
                                if (node1.connections[index].letter != node2.connections[index].letter || node1.connections[index].node.name != node2.connections[index].node.name || node1.nodeType != node2.nodeType)
                                {
                                    match = false;
                                    break;
                                }
                                index++;
                            }
                            if (!match) continue; //no match was found

                            node1ToOptimize = node1;
                            node2ToOptimize = node2;
                            optimized = false; //since we found a connection to optimize, we should check it all again
                        }
                        node2Index++;
                    }
                    node1Index++;
                }

                if (!optimized) //2 nodes were found that could be combined
                {
                    //create new node
                    Node newnode = new Node(node1ToOptimize.connections, node1ToOptimize.name + node2ToOptimize.name, node1ToOptimize.nodeType);
                    minimizedDFA.Nodes.Add(newnode);

                    //find all connections
                    for (int i = 0; i < minimizedDFA.Nodes.Count; i++)
                    {
                        for (int j = 0; j < minimizedDFA.Nodes[i].connections.Count; j++)
                        {
                            if (minimizedDFA.Nodes[i].connections[j].node.name == node1ToOptimize.name || minimizedDFA.Nodes[i].connections[j].node.name == node2ToOptimize.name)
                                minimizedDFA.Nodes[i].connections[j].node = minimizedDFA.Nodes[minimizedDFA.Nodes.Count - 1]; //make all old connections point to the new node
                        }
                    }

                    //remove old ones
                    minimizedDFA.Nodes.Remove(node2ToOptimize);
                    minimizedDFA.Nodes.Remove(node1ToOptimize);
                }
            }

            return minimizedDFA;
        }

        /// <summary>
        /// Used for step 3 of HopCroft's algorithm
        /// </summary>
        /// <param name="nonMinimizedDFA"></param>
        /// <returns></returns>
        private static DFA Partitioning(DFA nonMinimizedDFA)
        {
            DFA minimizedDFA = nonMinimizedDFA;
            return minimizedDFA;
        }
    }
}