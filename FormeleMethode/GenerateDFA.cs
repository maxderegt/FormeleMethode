using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    /// <summary>
    /// Class used for generating DFA's that need specific requirements
    /// Can generate the following DFA's :begin with, contains, ends with
    /// </summary>
    class GenerateDFA
    {
        /// <summary>
        /// Generates a DFA that starts with the text in var: beginsWithText taking into account the alphabet 
        /// </summary>
        /// <param name="beginsWithText">What text does it need to start with?</param>
        /// <param name="alphabet">What letters are in the language?</param>
        /// <returns></returns>
        public static List<Node> GenerateDFABeginsWith(string beginsWithText, string alphabet)
        {
            int count = 3; //keeps track of the current node
            List<Node> DFA_BW = new List<Node>() //slightly less code when using q2 as the startnode
            {
                new Node(new List<Connection>(), "q0", NodeType.EndNode), 
                new Node(new List<Connection>(), "q1", NodeType.NormalNode),
                new Node(new List<Connection>(), "q2", NodeType.StartNode)
            }; //absolute minimum amount of required nodes

            //the string to get the text from
            foreach (char c in beginsWithText.ToCharArray())
            {
                //checks if the letter is the final letter
                if (count != beginsWithText.Length + 2)
                {
                    DFA_BW.Add(new Node(new List<Connection>(), "q" + count, NodeType.NormalNode));
                    DFA_BW[count - 1].AddConnection(new Connection(c, DFA_BW[count])); //user entered the right character connection
                }
                else //final node
                    DFA_BW[count - 1].AddConnection(new Connection(c, DFA_BW[0])); //user entered the right character connections

                foreach (char ch in alphabet)
                {
                    if (ch != c) //user entered the wrong character connections
                    {
                        DFA_BW[count - 1].AddConnection(new Connection(ch, DFA_BW[1]));
                    }
                }

                count++;
            }

            //add connections for the endnode(q0) and invalid input node(q1)
            foreach (char c in alphabet.ToCharArray())
            {
                DFA_BW[0].AddConnection(new Connection(c, DFA_BW[0])); //makes sure it stays true if we started with the proper text
                DFA_BW[1].AddConnection(new Connection(c, DFA_BW[1])); //makes sure it stays false if user entered the wrong text
            }

            return DFA_BW;
        }

        /// <summary>
        /// Generates a DFA that ends with the text in var: endsWithText taking into account the alphabet 
        /// </summary>
        /// <param name="endsWithText">What text does it need to end with?</param>
        /// <param name="alphabet">What letters are in the language?</param>
        /// <returns></returns>
        public static List<Node> GenerateDFAEndsWith(string endsWithText, string alphabet)
        {
            int count = 2; //keeps track of the current node
            List<Node> DFA_EW = new List<Node>() //slightly less code when saying q2 is the startnode
            {
                new Node(new List<Connection>(), "q0", NodeType.EndNode),
                new Node(new List<Connection>(), "q1", NodeType.StartNode)
            }; //absolute minimum amount of required nodes

            //adds the nodes required for the string endsWithText
            foreach (char c in endsWithText.ToCharArray())
            {
                if (count != endsWithText.Length + 1) //if the current letter is not the final one
                {
                    DFA_EW.Add(new Node(new List<Connection>(), "q" + count, NodeType.NormalNode));
                    DFA_EW[count - 1].AddConnection(new Connection(c, DFA_EW[count]));
                }
                else //final letter
                    DFA_EW[count - 1].AddConnection(new Connection(c, DFA_EW[0]));

                count++;
            }

            count = 2; //keeps track of the current node
            String text = "";
            //loop though all letters that should be entered (endsWithText)
            foreach (char c in endsWithText.ToCharArray())
            {
                foreach(char ch in alphabet.ToCharArray()) //loop though all letters that could be entered according to our alphabet
                {
                    if (c != ch) //wrong input detected
                    {
                        bool found = false;

                        for (int i = count - 2; i > 0; i--) //look up one node back at a time
                        {
                            String txt = text + ch; //keeps track of what could be entered
                            if (txt.EndsWith(endsWithText.Substring(0, i))) //is the input the same as the node expects
                            {
                                found = true;
                                DFA_EW[count - 1].AddConnection(new Connection(ch, DFA_EW[i + 1])); //a connection was found
                                break;
                            }
                        }
                        if (!found)
                        {
                            DFA_EW[count - 1].AddConnection(new Connection(ch, DFA_EW[1])); //no connection was found, user returns to start node
                        }

                    }
                }
                text += c;
                count++;
            }
            foreach (char ch in alphabet.ToCharArray()) //for the final node
            {
                bool found = false;

                for (int i = count - 2; i > 0; i--) //look up one node back at a time
                {
                    String txt = text + ch; //keeps track of what could be entered
                    if (txt.EndsWith(endsWithText.Substring(0, i))) //is the input the same as the node expects
                    {
                        found = true;
                        DFA_EW[0].AddConnection(new Connection(ch, DFA_EW[i + 1])); //a connection was found 
                        break;
                    }
                }
                if (!found)
                {
                    DFA_EW[0].AddConnection(new Connection(ch, DFA_EW[1])); //no connection was found, user returns to start node
                }
            }
            
            return DFA_EW;
        }

        /// <summary>
        /// Generates a DFA that contains the text in var: containsText taking into account the alphabet 
        /// </summary>
        /// <param name="containsText">What text does it need to contain?</param>
        /// <param name="alphabet">What letters are in the language?</param>
        /// <returns></returns>
        public static List<Node> GenerateDFAContains(string containsText, string alphabet)
        {
            int count = 2; //keeps track of the current node
            List<Node> DFA_C = new List<Node>() //slightly less code when saying q2 is the startnode
            {
                new Node(new List<Connection>(), "q0", NodeType.EndNode),
                new Node(new List<Connection>(), "q1", NodeType.StartNode)
            }; //absolute minimum amount of required nodes

            //adds the nodes required for the string containsText
            foreach (char c in containsText.ToCharArray())
            {
                if (count != containsText.Length + 1)
                {
                    DFA_C.Add(new Node(new List<Connection>(), "q" + count, NodeType.NormalNode));
                    DFA_C[count - 1].AddConnection(new Connection(c, DFA_C[count]));
                }
                else
                {
                    DFA_C[count - 1].AddConnection(new Connection(c, DFA_C[0]));
                }

                count++;
            }

            count = 2; //keeps track of the current node
            String text = "";
            //loop though all letters that should be entered (containsText)
            foreach (char c in containsText.ToCharArray())
            {
                foreach (char ch in alphabet.ToCharArray())  //loop though all letters that could be entered according to our alphabet
                {
                    if (c != ch) //wrong input detected
                    {
                        bool found = false;

                        for (int i = count - 2; i > 0; i--) //look up one node back at a time
                        {
                            String txt = text + ch; //keeps track of what could be entered
                            if (txt.EndsWith(containsText.Substring(0, i)))
                            {
                                found = true; //node found 
                                DFA_C[count - 1].AddConnection(new Connection(ch, DFA_C[i + 1]));
                                break;
                            }
                        }
                        if (!found) //node not found
                        {
                            DFA_C[count - 1].AddConnection(new Connection(ch, DFA_C[1]));
                        }
                    }
                }
                text += c;
                count++;
            }

            foreach (char ch in alphabet.ToCharArray()) //for the final node, ensures all input is valid after the right text has been entered
            {
                DFA_C[0].AddConnection(new Connection(ch, DFA_C[0]));
            }

            return DFA_C;
        }
    }
}