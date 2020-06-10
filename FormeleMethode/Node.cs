using System.Collections.Generic;

namespace FormeleMethode
{
    //this class handles Nodes, contains all their information
    public class Node
    {
        //list of connections from this node to another node
        public List<Connection> connections = new List<Connection>();
        //name of the node
        public string name;
        //type of the node
        public NodeType nodeType;

        //standard constructor
        public Node(string name, NodeType nodeType)
        {
            this.name = name;
            this.nodeType = nodeType;
        }

        //constructor with list of connections
        public Node(List<Connection> connections, string name, NodeType nodeType)
        {
            this.connections = connections;
            this.name = name;
            this.nodeType = nodeType;
        }

        //adds list of connections
        public void AddConnections(List<Connection> connections)
        {
            this.connections = connections;
        }
        
        //adds one connection
        public void AddConnection(Connection connection)
        {
            this.connections.Add(connection);
        }
    }
}