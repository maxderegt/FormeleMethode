using System;
using System.Collections.Generic;
using System.Text;

namespace FormeleMethode
{
    class GenerateDFA
    {
        public static List<Node> GenerateDFABeginsWith(string beginsWithText, string alphabet)
        {
            int count = 3;
            List<Node> DFA_BW = new List<Node>() //slightly less code when saying q2 is the startnode
            {
                new Node("q0", NodeType.EndNode),
                new Node("q1", NodeType.NormalNode),
                new Node("q2", NodeType.StartNode)
            };
            DFA_BW[0].AddConnections(new List<Connection>());
            DFA_BW[1].AddConnections(new List<Connection>());
            DFA_BW[2].AddConnections(new List<Connection>());

            foreach (char c in beginsWithText.ToCharArray())
            {
                if (count != beginsWithText.Length + 2)
                {
                    DFA_BW.Add(new Node(new List<Connection>(), "q" + count, NodeType.NormalNode));
                    DFA_BW[count - 1].AddConnection(new Connection(c, DFA_BW[count]));
                    foreach (char ch in alphabet)
                    {
                        if (ch != c)
                        {
                            DFA_BW[count - 1].AddConnection(new Connection(ch, DFA_BW[1]));
                        }
                    }
                }
                else //final node
                {
                    DFA_BW[count - 1].AddConnection(new Connection(c, DFA_BW[0]));
                    foreach (char ch in alphabet)
                    {
                        if (ch != c)
                        {
                            DFA_BW[count - 1].AddConnection(new Connection(ch, DFA_BW[1]));
                        }
                    }
                }

                
                count++;
            }

            //makes sure it stays true if we started with the proper text
            foreach (char c in alphabet.ToCharArray())
            {
                DFA_BW[0].AddConnection(new Connection(c, DFA_BW[0]));
            }

            //makes sure it stays false if we started with the wrong text
            foreach (char c in alphabet.ToCharArray())
            {
                DFA_BW[1].AddConnection(new Connection(c, DFA_BW[1]));
            }

            return DFA_BW;
        }

        public static List<Node> GenerateDFAEndsWith(string endsWithText, string alphabet)
        {
            int count = 2;
            List<Node> DFA_EW = new List<Node>() //slightly less code when saying q2 is the startnode
            {
                new Node("q0", NodeType.EndNode),
                new Node("q1", NodeType.StartNode)
            };
            DFA_EW[0].AddConnections(new List<Connection>());
            DFA_EW[1].AddConnections(new List<Connection>());

            //all nodes added
            foreach (char c in endsWithText.ToCharArray())
            {
                if (count != endsWithText.Length + 1)
                {
                    DFA_EW.Add(new Node(new List<Connection>(), "q" + count, NodeType.NormalNode));
                    DFA_EW[count - 1].AddConnection(new Connection(c, DFA_EW[count]));
                }
                else
                {
                    DFA_EW[count - 1].AddConnection(new Connection(c, DFA_EW[0]));
                }


                count++;
            }

            count = 2;
            String text = "";
            //let's see how far each node moves back depending on the letter
            foreach (char c in endsWithText.ToCharArray())
            {
                foreach(char ch in alphabet.ToCharArray())
                {
                    if (c != ch)
                    {
                        if (count == 2) //first letter
                        {
                            DFA_EW[count - 1].AddConnection(new Connection(ch, DFA_EW[1]));
                        }
                        else 
                        {
                            bool found = false;

                            for (int i = count - 2; i > 0; i--) //look up one node back at a time
                            {
                                String txt = text + ch;
                                if (txt.EndsWith(endsWithText.Substring(0, i)))
                                {
                                    found = true;
                                    DFA_EW[count - 1].AddConnection(new Connection(ch, DFA_EW[i+1]));
                                    break;
                                }
                            }
                            if(!found)
                            {
                                DFA_EW[count - 1].AddConnection(new Connection(ch, DFA_EW[1]));
                            }
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
                    String txt = text + ch;
                    if (txt.EndsWith(endsWithText.Substring(0, i)))
                    {
                        found = true;
                        DFA_EW[0].AddConnection(new Connection(ch, DFA_EW[i + 1]));
                        break;
                    }
                }
                if (!found)
                {
                    DFA_EW[0].AddConnection(new Connection(ch, DFA_EW[1]));
                }
            }
            

            return DFA_EW;
        }

        public static List<Node> GenerateDFAContains(string endsWithText, string alphabet)
        {
            int count = 2;
            List<Node> DFA_C = new List<Node>() //slightly less code when saying q2 is the startnode
            {
                new Node("q0", NodeType.EndNode),
                new Node("q1", NodeType.StartNode)
            };
            DFA_C[0].AddConnections(new List<Connection>());
            DFA_C[1].AddConnections(new List<Connection>());

            //all nodes added
            foreach (char c in endsWithText.ToCharArray())
            {
                if (count != endsWithText.Length + 1)
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

            count = 2;
            String text = "";
            //let's see how far each node moves back depending on the letter
            foreach (char c in endsWithText.ToCharArray())
            {
                foreach (char ch in alphabet.ToCharArray())
                {
                    if (c != ch)
                    {
                        if (count == 2) //first letter
                        {
                            DFA_C[count - 1].AddConnection(new Connection(ch, DFA_C[1]));
                        }
                        else
                        {
                            bool found = false;

                            for (int i = count - 2; i > 0; i--) //look up one node back at a time
                            {
                                String txt = text + ch;
                                if (txt.EndsWith(endsWithText.Substring(0, i)))
                                {
                                    found = true;
                                    DFA_C[count - 1].AddConnection(new Connection(ch, DFA_C[i + 1]));
                                    break;
                                }
                            }
                            if (!found)
                            {
                                DFA_C[count - 1].AddConnection(new Connection(ch, DFA_C[1]));
                            }
                        }
                    }
                }
                text += c;
                count++;
            }
            foreach (char ch in alphabet.ToCharArray()) //for the final node
            {
                DFA_C[0].AddConnection(new Connection(ch, DFA_C[0]));
            }


            return DFA_C;
        }
    }
}
