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
            List<string> strings = GenerateStrings.GenerateString(5, "ab");
            

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
            
            List<Node> BeginsWithBABAANodes = GenerateDFA.GenerateDFABeginsWith("babaa", "ab");
            BeginsWithBABAA = new DFA(BeginsWithBABAANodes[2]);
            foreach (string item in strings)
            {
                Console.WriteLine(BeginsWithBABAA.Check(item));
            }

            List<String> correctWords = BeginsWithBABAA.geefTaalTotN(5, "ab");
            Console.WriteLine("-----------------");
            Console.WriteLine("Correct words BeginsWithBABAA");
            foreach (String s in correctWords)
            {
                Console.WriteLine(s);
            }
            
            List<String> inCorrectWords = BeginsWithBABAA.geefFoutieveTaalTotN(5, "ab");
            Console.WriteLine("-----------------");
            Console.WriteLine("incorrect words BeginsWithBABAA");
            foreach (String s in inCorrectWords)
            {
                Console.WriteLine(s);
            }

            CreateGraph(BeginsWithBABAANodes, "DFABWBABAA");
            List<Node> EndsWithBABAANodes = GenerateDFA.GenerateDFAEndsWith("babaa", "ab");
            CreateGraph(EndsWithBABAANodes, "DFAEWBABAA");

            List<Node> ContainsBABAANodes = GenerateDFA.GenerateDFAContains("babaa", "ab");
            CreateGraph(ContainsBABAANodes, "DFACBABAA");


            Console.WriteLine("---- (NDFA) reverse begins with babaa -----");

            NDFA reversedBABAA = DFAReverse.Reverse(BeginsWithBABAA);
            foreach (string item in strings)
            {
                Console.WriteLine(reversedBABAA.Check(item));
            }

            CreateGraph(reversedBABAA.Nodes, "ReversedBABAA");


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

            NDFA ContainsAAorBB = new NDFA(new List<Node>() { NDFA_C_AAoBB_Nodes[0] }, NDFA_C_AAoBB_Nodes);
            foreach (string item in strings)
            {
                Console.WriteLine(ContainsAAorBB.Check(item));
            }

            correctWords = ContainsAAorBB.geefTaalTotN(5, "ab");
            Console.WriteLine("-----------------");
            Console.WriteLine("Correct words ContainsAAorBB");
            foreach (String s in correctWords)
            {
                Console.WriteLine(s);
            }

            inCorrectWords = ContainsAAorBB.geefFoutieveTaalTotN(5, "ab");
            Console.WriteLine("-----------------");
            Console.WriteLine("incorrect words ContainsAAorBB");
            foreach (String s in inCorrectWords)
            {
                Console.WriteLine(s);
            }


            CreateGraph(NDFA_C_AAoBB_Nodes, "NDFACAAoBB");


            Console.WriteLine("---- test NDFA to DFA -----");
            Console.WriteLine("---------- NDFA -----------");
            List<Node> NDFATODFA = new List<Node>()
            {
                new Node("q0", NodeType.StartNode),
                new Node("q1", NodeType.EndNode),
                new Node("q2", NodeType.EndNode),
                new Node("q3", NodeType.NormalNode),
                new Node("q4", NodeType.NormalNode)
            };
            NDFATODFA[0].AddConnections(new List<Connection>()
            {
                new Connection('b', NDFATODFA[1]),
                new Connection('a', NDFATODFA[1]),
                new Connection('b', NDFATODFA[2]),
            });
            NDFATODFA[1].AddConnections(new List<Connection>()
            {
                new Connection('a', NDFATODFA[2]),
                new Connection('ϵ', NDFATODFA[3]),
                new Connection('b', NDFATODFA[3])
            });
            NDFATODFA[2].AddConnections(new List<Connection>()
            {
                new Connection('a', NDFATODFA[1])
            });
            NDFATODFA[3].AddConnections(new List<Connection>()
            {
                new Connection('a', NDFATODFA[4]),
                new Connection('a', NDFATODFA[1])
            });
            NDFATODFA[4].AddConnections(new List<Connection>()
            {
                new Connection('b', NDFATODFA[4]),
                new Connection('ϵ', NDFATODFA[2])
            });

            NDFA TESTNDFATODFA = new NDFA(new List<Node>(){NDFATODFA[0]}, NDFATODFA);

            CreateGraph(TESTNDFATODFA.Nodes, "test_NDFA_NDFAtoDFA");

            Console.WriteLine("----------- DFA -----------");
            DFA TESTDFA = NDFAtoDFA.ToDFA(TESTNDFATODFA);
            CreateGraph(TESTDFA.Nodes, "test_DFA_NDFAtoDFA");


            Console.WriteLine("---- regularexpressionexample -----");


            strings.Clear();
            strings = GenerateStrings.GenerateString(5, "abc");

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

            NDFA NDFAregularexpression = new NDFA(new List<Node>() { regularexpressionexample[0] }, regularexpressionexample);
            foreach (string item in strings)
            {
                Console.WriteLine(NDFAregularexpression.Check(item));
            }
            CreateGraph(regularexpressionexample, "regularexpressionexample");
            
            Console.WriteLine("---------- Regular expression tester with Regex and Thompson ------------");
            Console.WriteLine("----------                        (a|bc)*                    -----------");
            RegexTester regexTester = new RegexTester(@"(a|bc)*");
            
            correctWords = regexTester.geefTaalTotN(5, "abc");
            Console.WriteLine("-----------------");
            Console.WriteLine("Correct words (a|bc)*");
            foreach (String s in correctWords)
            {
                Console.WriteLine(s);
            }

            inCorrectWords = regexTester.geefFoutieveTaalTotN(5, "abc");
            Console.WriteLine("-----------------");
            Console.WriteLine("incorrect words (a|bc)*");
            foreach (String s in inCorrectWords)
            {
                Console.WriteLine(s);
            }


            RegExp reg = new RegExp("a").or(new RegExp("b").dot(new RegExp("c"))).star();
            Console.WriteLine("REGEX: " + reg.ToString());

            List<Node> ndfa = Thompson.CreateAutomaat(reg);
            NDFA NDFAregularexpression2 = new NDFA(new List<Node>() { ndfa[0] }, ndfa);

            foreach (string item in strings)
            {
                Console.WriteLine(NDFAregularexpression2.Check(item));
                //Console.WriteLine("Regex test: " + regexTester.Check(item));
            }

            Console.WriteLine("---------- Regular expression parser ------------");
            Console.WriteLine("----------  input String: (a|bc)*                    -----------");

            RegExp reg2 = RegexParser.parse("(a|bc)*");
            Console.WriteLine("----------  result String: " + reg2.ToString() + "                    -----------");

            List<Node> ndfa2 = Thompson.CreateAutomaat(reg2);
            CreateGraph(ndfa2, "RegExParser");

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
                        FillColor = Color.Green,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Filled,
                        Width = 0.5f,
                        Height = 0.5f
                    };
                }
                else if(node.nodeType == NodeType.StartNode)
                {
                    graphnode = new DotNode(node.name)
                    {
                        Shape = DotNodeShape.Circle,
                        Label = node.name,
                        FillColor = Color.LightBlue,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Filled,
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
                    int number = 0;
                    for (int i2 = 0; i2 < dotNodes.Count; i2++)
                    {
                        if (dotNodes[i2].Label.Text.Equals(nodename))
                        {
                            number = i2;
                        }
                    }

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
