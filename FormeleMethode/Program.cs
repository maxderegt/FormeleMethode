using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace FormeleMethode
{
    class Program
    {
        StringBuilder batfile = new StringBuilder();

        static void Main(string[] args)
        {
            Program program = new Program();
        }

        public Program()
        {
            List<string> strings = GenerateStrings.GenerateString(5, "abc");
            /*
            Console.WriteLine("---------- DFA -------------");
            Console.WriteLine("---- begins with babaa -----");
            List<Node> DFA_BWB_ABAA_Nodes = new List<Node>()
            {
                new Node("q0", NodeType.StartNode),
                new Node("q1", NodeType.NormalNode),
                new Node("q2", NodeType.NormalNode),
                new Node("q3", NodeType.NormalNode),
                new Node("q4", NodeType.NormalNode),
                new Node("q5", NodeType.EndNode),
                new Node("q6", NodeType.NormalNode)
            };
            DFA_BWB_ABAA_Nodes[0].AddConnections(new List<Connection>()
            {
                new Connection('b', DFA_BWB_ABAA_Nodes[1]),
                new Connection('a', DFA_BWB_ABAA_Nodes[6])
            });
            DFA_BWB_ABAA_Nodes[1].AddConnections(new List<Connection>()
            {
                new Connection('b', DFA_BWB_ABAA_Nodes[6]),
                new Connection('a', DFA_BWB_ABAA_Nodes[2])
            });
            DFA_BWB_ABAA_Nodes[2].AddConnections(new List<Connection>()
            {
                new Connection('b', DFA_BWB_ABAA_Nodes[3]),
                new Connection('a', DFA_BWB_ABAA_Nodes[6])
            });
            DFA_BWB_ABAA_Nodes[3].AddConnections(new List<Connection>()
            {
                new Connection('b', DFA_BWB_ABAA_Nodes[6]),
                new Connection('a', DFA_BWB_ABAA_Nodes[4])
            });
            DFA_BWB_ABAA_Nodes[4].AddConnections(new List<Connection>()
            {
                new Connection('b', DFA_BWB_ABAA_Nodes[6]),
                new Connection('a', DFA_BWB_ABAA_Nodes[5])
            });
            DFA_BWB_ABAA_Nodes[5].AddConnections(new List<Connection>()
            {
                new Connection('b', DFA_BWB_ABAA_Nodes[5]),
                new Connection('a', DFA_BWB_ABAA_Nodes[5])
            });
            DFA_BWB_ABAA_Nodes[6].AddConnections(new List<Connection>()
            {
                new Connection('b', DFA_BWB_ABAA_Nodes[6]),
                new Connection('a', DFA_BWB_ABAA_Nodes[6])
            });

            DFA BeginsWithBABAA = new DFA(DFA_BWB_ABAA_Nodes[0]);
            foreach (string item in strings)
            {
                Console.WriteLine(BeginsWithBABAA.Check(item));
            }

            CreateGraph(DFA_BWB_ABAA_Nodes, "DFABWBABAA");

            Console.WriteLine("---- starts with abb or ends with baab -----");
            List<Node> DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes = new List<Node>()
            {
                new Node("q0", NodeType.StartNode),
                new Node("q1", NodeType.NormalNode),
                new Node("q2", NodeType.NormalNode),
                new Node("q3", NodeType.EndNode),
                new Node("q4", NodeType.NormalNode),
                new Node("q5", NodeType.NormalNode),
                new Node("q6", NodeType.NormalNode),
                new Node("q7", NodeType.NormalNode),
                new Node("q8", NodeType.EndNode)
            };

            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[0].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[1]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[4])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[1].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[6]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[2])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[2].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[6]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[3])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[3].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[3]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[3])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[4].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[4]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[5])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[5].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[6]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[5])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[6].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[7]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[5])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[7].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[4]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[8])
            });
            DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[8].AddConnections(new List<Connection>()
            {
                new Connection('a', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[4]),
                new Connection('b', DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[5])
            });

            DFA StartsWithABBorEndsWithBAAB = new DFA(DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[0]);
            foreach (string item in strings)
            {
                Console.WriteLine(StartsWithABBorEndsWithBAAB.Check(item));
            }

            CreateGraph(DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes, "DFASTARTWABBORENDSWBAAB");

            Console.WriteLine("---------- NDFA ------------");
            Console.WriteLine("---- Contains aa or bb -----");
            List<Node> NDFA_C_AAoBB_Nodes = new List<Node>()
            {
                new Node("q0", NodeType.StartNode),
                new Node("q1", NodeType.NormalNode),
                new Node("q2", NodeType.NormalNode),
                new Node("q3", NodeType.EndNode),
            };

            NDFA_C_AAoBB_Nodes[0].AddConnections(new List<Connection>()
            {
                new Connection('b', NDFA_C_AAoBB_Nodes[0]),
                new Connection('a', NDFA_C_AAoBB_Nodes[0]),
                new Connection('b', NDFA_C_AAoBB_Nodes[2]),
                new Connection('a', NDFA_C_AAoBB_Nodes[1])
            });
            NDFA_C_AAoBB_Nodes[1].AddConnections(new List<Connection>()
            {
                new Connection('a', NDFA_C_AAoBB_Nodes[3]),
                new Connection('b', NDFA_C_AAoBB_Nodes[0])
            });
            NDFA_C_AAoBB_Nodes[2].AddConnections(new List<Connection>()
            {
                new Connection('b', NDFA_C_AAoBB_Nodes[3]),
                new Connection('a', NDFA_C_AAoBB_Nodes[0])
            });
            NDFA_C_AAoBB_Nodes[3].AddConnections(new List<Connection>()
            {
                new Connection('b', NDFA_C_AAoBB_Nodes[3]),
                new Connection('a', NDFA_C_AAoBB_Nodes[3])
            });

            NDFA ContainsAAorBB = new NDFA(new List<Node>() { NDFA_C_AAoBB_Nodes[0] });
            foreach (string item in strings)
            {
                Console.WriteLine(ContainsAAorBB.Check(item));
            }


            CreateGraph(NDFA_C_AAoBB_Nodes, "NDFACAAoBB");


            Console.WriteLine("---- regularexpressionexample -----");
            List<Node> regularexpressionexample = new List<Node>()
            {
                new Node("q0", NodeType.StartNode),
                new Node("q1", NodeType.NormalNode),
                new Node("q2", NodeType.NormalNode),
                new Node("q3", NodeType.NormalNode),
                new Node("q4", NodeType.EndNode),
            };
            regularexpressionexample[0].AddConnections(new List<Connection>()
            {
                new Connection('ϵ', regularexpressionexample[1]),
                new Connection('ϵ', regularexpressionexample[4])
            });
            regularexpressionexample[1].AddConnections(new List<Connection>()
            {
                new Connection('a', regularexpressionexample[3]),
                new Connection('b', regularexpressionexample[2])
            });
            regularexpressionexample[2].AddConnections(new List<Connection>()
            {
                new Connection('c', regularexpressionexample[3])
            });
            regularexpressionexample[3].AddConnections(new List<Connection>()
            {
                new Connection('ϵ', regularexpressionexample[4]),
                new Connection('ϵ', regularexpressionexample[1])
            });
            regularexpressionexample[4].AddConnections(new List<Connection>());

            NDFA NDFAregularexpression = new NDFA(new List<Node>() { regularexpressionexample[0] });
            foreach (string item in strings)
            {
                Console.WriteLine(NDFAregularexpression.Check(item));
            }
            CreateGraph(regularexpressionexample, "regularexpressionexample");
            */

            Console.WriteLine("");

            Console.WriteLine("---------- Regular expression tester with Regex and Thompson ------------");
            Console.WriteLine("----------                        (a|bc)*                    -----------");
            RegexTester regexTester = new RegexTester(@"(a|bc)*");
            RegExp reg = new RegExp("a").or(new RegExp("b").dot(new RegExp("c"))).star();
            Console.WriteLine("REGEX: " + reg.ToString());

            List<Node> ndfa = Thompson.CreateAutomaat(reg);
            NDFA NDFAregularexpression2 = new NDFA(new List<Node>() { ndfa[0] });

            foreach (string item in strings)
            {
                Console.WriteLine(NDFAregularexpression2.Check(item));
            }


            CreateGraph(ndfa, "RegExb");

            File.WriteAllText("pdf.bat", batfile.ToString());
            System.Diagnostics.Process.Start("pdf.bat");

            Console.ReadLine();
        }

        public void CreateGraph(List<Node> nodes, string name)
        {
            var graph = new DotGraph(name, true);
            List<DotNode> dotNodes = new List<DotNode>();
            foreach (var node in nodes)
            {
                DotNode graphnode;
                if (node.nodeType == NodeType.EndNode)
                {
                    graphnode = new DotNode(node.name)
                    {
                        Shape = DotNodeShape.DoubleCircle,
                        Label = node.name,
                        FillColor = Color.Coral,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Solid,
                        Width = 0.5f,
                        Height = 0.5f
                    };
                }
                else
                {
                    graphnode = new DotNode(node.name)
                    {
                        Shape = DotNodeShape.Circle,
                        Label = node.name,
                        FillColor = Color.Coral,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Solid,
                        Width = 0.5f,
                        Height = 0.5f
                    };
                }

                dotNodes.Add(graphnode);
                graph.Elements.Add(graphnode);                
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i];
                foreach (var item in node.connections)
                {
                    string nodename = item.node.name;
                    int number = int.Parse(nodename.Substring(nodename.Length - 1));

                    var myEdge = new DotEdge(dotNodes[i], dotNodes[number])
                    {
                        ArrowHead = DotEdgeArrowType.Vee,
                        ArrowTail = DotEdgeArrowType.Diamond,
                        Color = Color.Black,
                        FontColor = Color.Black,
                        Label = item.letter.ToString()
                    };

                    graph.Elements.Add(myEdge);
                }
            }

            var dot = graph.Compile();
            dot = dot.Insert(10 + name.Length, "rankdir=\"LR\";");
            File.WriteAllText(name + ".dot", dot);
            batfile.AppendLine($"dot -T pdf {name}.dot -O");
        }
    }
}
