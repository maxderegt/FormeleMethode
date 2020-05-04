using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    class Connection
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
