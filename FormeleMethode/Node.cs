using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    public class Node
    {
        public List<Connection> connections;
        public string name;
        public NodeType nodeType;

        public Node(string name, NodeType nodeType)
        {
            this.name = name;
            this.nodeType = nodeType;
        }

        public Node(List<Connection> connections, string name, NodeType nodeType)
        {
            this.connections = connections;
            this.name = name;
            this.nodeType = nodeType;
        }

        public void AddConnections(List<Connection> connections)
        {
            this.connections = connections;
        }
    }
}
