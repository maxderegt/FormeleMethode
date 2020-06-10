namespace FormeleMethode
{
    //This class handles the Connection from node to node.
    // Letter is the character that allowed for the connection
    // Node is the node which the connection is to
    public class Connection
    {
        public char letter;
        public Node node;

        public Connection(char letter, Node node)
        {
            this.letter = letter;
            this.node = node;
        }
    }
}
