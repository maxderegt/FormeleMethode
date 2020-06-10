using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public void HardcodedExamples()
        {
            List<string> strings = GenerateStrings.GenerateString(5, "ab");

            #region DFA
            //------------------------------- DFA -------------------------------
            Console.WriteLine("---------- DFA -------------");
            Console.WriteLine("------- hardcoded ----------");
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
            DFA BeginsWithBABAA = new DFA(DFA_BWB_ABAA_Nodes[0], DFA_BWB_ABAA_Nodes);
            foreach (string item in strings)
            {
                Console.WriteLine(BeginsWithBABAA.Check(item));
            }
            CreateGraph(DFA_BWB_ABAA_Nodes, "DFA_BWBABAA");

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
            DFA StartsWithABBorEndsWithBAAB = new DFA(DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes[0], DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes);
            foreach (string item in strings)
            {
                Console.WriteLine(StartsWithABBorEndsWithBAAB.Check(item));
            }
            CreateGraph(DFA_STARTW_ABB_OR_ENDSW_BAAB_Nodes, "DFA_STARTWABBORENDSWBAAB");

            #endregion

            #region NDFA
            //------------------------------- NDFA -------------------------------
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
            CreateGraph(NDFA_C_AAoBB_Nodes, "NDFA_CAAoBB");
            #endregion           

            #region regex
            //---------------------------- regex -------------------------------
            List<string> regexstrings = GenerateStrings.GenerateString(5, "abc");
            Console.WriteLine("---------- regex ------------");
            Console.WriteLine("--------- (a|bc)*  ----------");
            RegexTester regexTester = new RegexTester(@"(a|bc)*");
            
            foreach (string item in regexstrings)
            {
                Console.WriteLine(regexTester.Check(item));
            }

            Console.WriteLine("--------- wrong  ----------");
            List<string> foutievetaal = regexTester.geefFoutieveTaalTotN(5, "abc");
            foreach (string item in foutievetaal)
            {
                Console.WriteLine(regexTester.Check(item));
            }
            Console.WriteLine("--------- correct  ----------");
            List<string> correctetaal = regexTester.geefTaalTotN(5, "abc");
            foreach (string item in correctetaal)
            {
                Console.WriteLine(regexTester.Check(item));
            }
            #endregion

            #region GenerateBeginsWith

            Console.WriteLine("-------- Generated --------");
            List<Node> BeginsWithBABAANodes = GenerateDFA.GenerateDFABeginsWith("babaa", "ab");
            Node beginsstartnode = null;
            foreach (Node item in BeginsWithBABAANodes)
            {
                if (item.nodeType == NodeType.StartNode)
                    beginsstartnode = item;
            }
            DFA GEN_BeginsWithBABAA = new DFA(beginsstartnode, BeginsWithBABAANodes);
            foreach (string item in strings)
            {
                Console.WriteLine(GEN_BeginsWithBABAA.Check(item));
            }

            List<String> correctWordsbw = GEN_BeginsWithBABAA.geefTaalTotN(5, "ab");
            Console.WriteLine("-------- Correct words BeginsWithBABAA --------");
            foreach (String s in correctWordsbw)
            {
                Console.WriteLine(s);
            }

            List<String> inCorrectWordsbw = GEN_BeginsWithBABAA.geefFoutieveTaalTotN(5, "ab");
            Console.WriteLine("-------- incorrect words BeginsWithBABAA --------");
            foreach (String s in inCorrectWordsbw)
            {
                Console.WriteLine(s);
            }
            CreateGraph(GEN_BeginsWithBABAA.Nodes, "GEN_DFABWBABAA");
            #endregion

            #region GenerateEndsWith
            List<Node> EndsWithBABAANodes = GenerateDFA.GenerateDFAEndsWith("aabab", "ab");
            Node EndsWithstartnode = null;
            foreach (Node item in EndsWithBABAANodes)
            {
                if (item.nodeType == NodeType.StartNode)
                    EndsWithstartnode = item;
            }
            DFA GEN_EndsWithBABAA = new DFA(EndsWithstartnode, EndsWithBABAANodes);
            foreach (string item in strings)
            {
                Console.WriteLine(GEN_EndsWithBABAA.Check(item));
            }

            List<String> correctWordsew = GEN_EndsWithBABAA.geefTaalTotN(5, "ab");
            Console.WriteLine("-------- Correct words EndsWithAABAB --------");
            foreach (String s in correctWordsew)
            {
                Console.WriteLine(s);
            }

            List<String> inCorrectWordsew = GEN_EndsWithBABAA.geefFoutieveTaalTotN(5, "ab");
            Console.WriteLine("-------- incorrect words EndsWithAABAB --------");
            foreach (String s in inCorrectWordsew)
            {
                Console.WriteLine(s);
            }
            CreateGraph(GEN_EndsWithBABAA.Nodes, "GEN_DFAEWAABAB");
            #endregion

            #region Contains
            List<Node> ContainsBABAANodes = GenerateDFA.GenerateDFAContains("bab", "ab");
            Node Containsstartnode = null;
            foreach (Node item in ContainsBABAANodes)
            {
                if (item.nodeType == NodeType.StartNode)
                    Containsstartnode = item;
            }
            DFA GEN_ContainsBABAA = new DFA(Containsstartnode, ContainsBABAANodes);
            foreach (string item in strings)
            {
                Console.WriteLine(GEN_ContainsBABAA.Check(item));
            }

            List<String> correctWordsc = GEN_ContainsBABAA.geefTaalTotN(5, "ab");
            Console.WriteLine("-------- Correct words ContainsBAB --------");
            foreach (String s in correctWordsc)
            {
                Console.WriteLine(s);
            }

            List<String> inCorrectWordsc = GEN_ContainsBABAA.geefFoutieveTaalTotN(5, "ab");
            Console.WriteLine("-------- incorrect words ContainsBAB --------");
            foreach (String s in inCorrectWordsc)
            {
                Console.WriteLine(s);
            }
            CreateGraph(GEN_ContainsBABAA.Nodes, "GEN_DFAEWBAB");
            #endregion

            #region Regex -> NDFA
            Console.WriteLine("---------- Regular expression tester with Regex and Thompson ------------");
            Console.WriteLine("----------                        (a|bc)*                    -----------");
            regexTester = new RegexTester(@"(a|bc)*");
            RegExp reg = new RegExp("a").or(new RegExp("b").dot(new RegExp("c"))).star();
            List<Node> ndfa = Thompson.CreateAutomaat(reg);
            NDFA NDFAregularexpression2 = new NDFA(new List<Node>() { ndfa[0] }, ndfa);

            foreach (string item in strings)
            {
                Console.WriteLine(NDFAregularexpression2.Check(item));
                Console.WriteLine("Regex test: " + regexTester.Check(item));
            }

            CreateGraph(NDFAregularexpression2.Nodes, "NDFA_RegexThompsonToNDFA");
            #endregion

            #region NDFAtoDFA
            //--------------------------- NDFA -> DFA ---------------------------
            Console.WriteLine("---- test NDFA to DFA -----");
            Console.WriteLine("---------- NDFA -----------");
            List<Node> NDFATODFA = new List<Node>()
            {
                new Node("q1", NodeType.StartNode),
                new Node("q2", NodeType.EndNode),
                new Node("q3", NodeType.EndNode),
                new Node("q4", NodeType.NormalNode),
                new Node("q5", NodeType.NormalNode)
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
            NDFA TESTNDFATODFA = new NDFA(new List<Node>() { NDFATODFA[0] }, NDFATODFA);
            CreateGraph(TESTNDFATODFA.Nodes, "NDFA_NDFAtoDFA");
            foreach (string item in strings)
            {
                //Console.WriteLine(TESTNDFATODFA.Check(item));
            }
            Console.WriteLine("----------- DFA -----------");
            DFA TESTDFA = NDFAtoDFA.ToDFA2(TESTNDFATODFA);
            CreateGraph(TESTDFA.Nodes, "DFA_NDFAtoDFA");
            foreach (string item in strings)
            {
                Console.WriteLine(TESTNDFATODFA.Check(item));
                Console.WriteLine(TESTDFA.Check(item));
            }
            #endregion

            #region Reverse
            Console.WriteLine("---- (NDFA) reverse begins with babaa -----");

            NDFA reversedBABAA = DFAReverse.Reverse2(BeginsWithBABAA);
            foreach (string item in strings)
            {
                Console.WriteLine(reversedBABAA.Check(item));
            }

            CreateGraph(reversedBABAA.Nodes, "ReversedBABAA");
            #endregion
        }

        public void Regexparse(string s)
        {
            Console.WriteLine("entered string: " + s);
            RegexTester tester;
            try
            {
                tester = new RegexTester(s);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            string answer = new String(s.Distinct().ToArray());
            answer = new String(answer.Where(Char.IsLetter).ToArray());
            List<string> strings = GenerateStrings.GenerateString(5, answer);

            RegExp exp = RegexParser.parse(s);
            List<Node> ndfa = Thompson.CreateAutomaat(exp);
            NDFA NDFAregularexpression = new NDFA(new List<Node>() { ndfa[0] }, ndfa);
            List<string> regexcorrect = tester.geefTaalTotN(5, answer);
            List<string> regexincorrect = tester.geefFoutieveTaalTotN(5, answer);
            CreateGraph(NDFAregularexpression.Nodes, "REGEX_NDFA");

            DFA DFAregularexpression = NDFAtoDFA.ToDFA2(NDFAregularexpression);
            CreateGraph(DFAregularexpression.Nodes, "REGEX_DFA");

            NDFA Reverse = DFAReverse.Reverse2(DFAregularexpression);
            CreateGraph(Reverse.Nodes, "REGEX_DFA_Reverse");


            Console.WriteLine("-------- Correct words Contains --------");
            foreach (String item in regexcorrect)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------- incorrect words --------");
            foreach (String item in regexincorrect)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("-------- regex/ndfa/dfa/ndfa Reversed --------");
            foreach (string item in strings)
            {
                Console.WriteLine(tester.Check(item));
                Console.WriteLine(NDFAregularexpression.Check(item));
                Console.WriteLine(DFAregularexpression.Check(item));
                Console.WriteLine("reverse "+Reverse.Check(item));
            }
        }

        public void Contains(string s, int n)
        {
            string alphabet = new String(s.Distinct().ToArray());
            alphabet = new String(alphabet.Where(Char.IsLetter).ToArray());
            List<string> strings = GenerateStrings.GenerateString(n, alphabet);

            Console.WriteLine("-------- Generated --------");
            List<Node> ContainsNodes = GenerateDFA.GenerateDFAContains(s, alphabet); 
            Node Containsstartnode = null;
            foreach (Node item in ContainsNodes)
            {
                if (item.nodeType == NodeType.StartNode)
                    Containsstartnode = item;
            }
            DFA GEN_Contains = new DFA(Containsstartnode, ContainsNodes);
            foreach (string item in strings)
            {
                Console.WriteLine(GEN_Contains.Check(item));
            }

            List<String> correctWordsbw = GEN_Contains.geefTaalTotN(n, alphabet);
            Console.WriteLine("-------- Correct words Contains " + s + " --------");
            foreach (String item in correctWordsbw)
            {
                Console.WriteLine(item);
            }

            List<String> inCorrectWordsbw = GEN_Contains.geefFoutieveTaalTotN(n, alphabet);
            Console.WriteLine("-------- incorrect words Contains " + s + " --------");
            foreach (String item in inCorrectWordsbw)
            {
                Console.WriteLine(item);
            }
            CreateGraph(GEN_Contains.Nodes, "GEN_DFAC" + s);

            Console.WriteLine("-------- Contains (reversed) --------");
            NDFA reverse = DFAReverse.Reverse2(GEN_Contains);
            foreach (string item in strings)
            {
                Console.WriteLine(reverse.Check(item));
            }
            CreateGraph(reverse.Nodes, "GEN_reversedDFAC" + s);
            Console.WriteLine("-------- Contains (reversed -> DFA) --------");
            DFA dfa = NDFAtoDFA.ToDFA2(reverse);
            foreach (string item in strings)
            {
                Console.WriteLine(dfa.Check(item));
            }
            CreateGraph(dfa.Nodes, "GEN_reverseddfaDFAC" + s);

        }

        public void EndsWith(string s, int n)
        {
            string alphabet = new String(s.Distinct().ToArray());
            alphabet = new String(alphabet.Where(Char.IsLetter).ToArray());
            List<string> strings = GenerateStrings.GenerateString(n, alphabet);

            Console.WriteLine("-------- Generated --------");
            List<Node> EndsWithBABAANodes = GenerateDFA.GenerateDFAEndsWith(s, alphabet); 
            Node EndsWithstartnode = null;
            foreach (Node item in EndsWithBABAANodes)
            {
                if (item.nodeType == NodeType.StartNode)
                    EndsWithstartnode = item;
            }
            DFA GEN_EndsWithBABAA = new DFA(EndsWithstartnode, EndsWithBABAANodes);
            foreach (string item in strings)
            {
                Console.WriteLine(GEN_EndsWithBABAA.Check(item));
            }

            List<String> correctWordsbw = GEN_EndsWithBABAA.geefTaalTotN(n, alphabet);
            Console.WriteLine("-------- Correct words Ends With " + s + " --------");
            foreach (String item in correctWordsbw)
            {
                Console.WriteLine(item);
            }

            List<String> inCorrectWordsbw = GEN_EndsWithBABAA.geefFoutieveTaalTotN(n, alphabet);
            Console.WriteLine("-------- incorrect words Ends With " + s + " --------");
            foreach (String item in inCorrectWordsbw)
            {
                Console.WriteLine(item);
            }
            CreateGraph(GEN_EndsWithBABAA.Nodes, "GEN_DFAEW" + s);

            Console.WriteLine("-------- Ends With (reversed) --------");
            NDFA reverse = DFAReverse.Reverse2(GEN_EndsWithBABAA);
            foreach (string item in strings)
            {
                Console.WriteLine(reverse.Check(item));
            }
            CreateGraph(reverse.Nodes, "GEN_reversedDFAEW" + s);
            Console.WriteLine("-------- Ends With (reversed -> DFA) --------");
            DFA dfa = NDFAtoDFA.ToDFA2(reverse);
            foreach (string item in strings)
            {
                Console.WriteLine(dfa.Check(item));
            }
            CreateGraph(dfa.Nodes, "GEN_reverseddfaDFAEW" + s);
        }

        public void BeginsWith(string s, int n)
        {
            string alphabet = new String(s.Distinct().ToArray());
            alphabet = new String(alphabet.Where(Char.IsLetter).ToArray());
            List<string> strings = GenerateStrings.GenerateString(n, alphabet);

            Console.WriteLine("-------- Generated --------");
            List<Node> BeginsWithBABAANodes = GenerateDFA.GenerateDFABeginsWith(s, alphabet);
            Node beginsstartnode = null;
            foreach (Node item in BeginsWithBABAANodes)
            {
                if (item.nodeType == NodeType.StartNode)
                    beginsstartnode = item;
            }
            DFA GEN_BeginsWithBABAA = new DFA(beginsstartnode, BeginsWithBABAANodes);
            foreach (string item in strings)
            {
                Console.WriteLine(GEN_BeginsWithBABAA.Check(item));
            }

            List<String> correctWordsbw = GEN_BeginsWithBABAA.geefTaalTotN(n, alphabet);
            Console.WriteLine("-------- Correct words Begins With "+ s +" --------");
            foreach (String item in correctWordsbw)
            {
                Console.WriteLine(item);
            }

            List<String> inCorrectWordsbw = GEN_BeginsWithBABAA.geefFoutieveTaalTotN(n, alphabet);
            Console.WriteLine("-------- incorrect words Begins With " + s + " --------");
            foreach (String item in inCorrectWordsbw)
            {
                Console.WriteLine(item);
            }
            CreateGraph(GEN_BeginsWithBABAA.Nodes, "GEN_DFABW"+s);


            Console.WriteLine("-------- Begins With (reversed) --------");
            NDFA reverse = DFAReverse.Reverse2(GEN_BeginsWithBABAA);
            foreach (string item in strings)
            {
                Console.WriteLine(reverse.Check(item));
            }
            CreateGraph(reverse.Nodes, "GEN_reversedDFABW" + s);
            Console.WriteLine("-------- Begins With (reversed -> DFA) --------");
            DFA dfa = NDFAtoDFA.ToDFA2(reverse);
            foreach (string item in strings)
            {
                Console.WriteLine(dfa.Check(item));
            }
            CreateGraph(dfa.Nodes, "GEN_reverseddfaDFABW" + s);
        }

        public Program()
        {
            while (true)
            {
                Console.WriteLine("Welcome to Max de Regt en Gerdtinus Netten their assignment");
                Console.WriteLine("Choose one of options below (type in the number)");
                Console.WriteLine("[0] Hardcoded examples");
                Console.WriteLine("[1] Enter Begins with DFA");
                Console.WriteLine("[2] Enter Ends with DFA");
                Console.WriteLine("[3] Enter Contains DFA");
                Console.WriteLine("[4] Enter Regex");
                string input = Console.ReadLine();
                int option;
                try
                {
                    option = int.Parse(input);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Not an number");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                bool busy = true;

                bool correctstring = false;
                bool correctnumber = false;
                string beginswith = "";
                string amounts = "";
                int amounti = 0;

                while (busy)
                {
                    switch (option)
                    {
                        case 0:
                            HardcodedExamples();
                            SaveToPDF();
                            busy = false;
                            break;
                        case 1:
                            while (!correctstring)
                            {
                                Console.WriteLine("What should it begin with? (only letters)");
                                beginswith = Console.ReadLine();
                                bool wrongstring = false;
                                foreach (char item in beginswith)
                                {
                                    if (!char.IsLetter(item))
                                    {
                                        wrongstring = true;
                                        Console.WriteLine("Contains characters that are not letters");
                                        break;
                                    }
                                }
                                if (wrongstring)
                                    continue;
                                correctstring = true;
                            }
                            while (!correctnumber) { 
                                Console.WriteLine("How long should the words be? (in numbers)");
                                amounts = Console.ReadLine();
                                try
                                {
                                    amounti = int.Parse(amounts);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Not an number");
                                    continue;
                                }
                                correctnumber = true;
                            }
                            BeginsWith(beginswith, amounti);
                            SaveToPDF();
                            busy = false;
                            break;
                        case 2:
                            while (!correctstring)
                            {
                                Console.WriteLine("What should it end with? (only letters)");
                                beginswith = Console.ReadLine();
                                bool wrongstring = false;
                                foreach (char item in beginswith)
                                {
                                    if (!char.IsLetter(item))
                                    {
                                        wrongstring = true;
                                        Console.WriteLine("Contains characters that are not letters");
                                        break;
                                    }
                                }
                                if (wrongstring)
                                    continue;
                                correctstring = true;
                            }
                            while (!correctnumber)
                            {
                                Console.WriteLine("How long should the words be? (in numbers)");
                                amounts = Console.ReadLine();
                                try
                                {
                                    amounti = int.Parse(amounts);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Not an number");
                                    continue;
                                }
                                correctnumber = true;
                            }
                            EndsWith(beginswith, amounti);
                            SaveToPDF();
                            busy = false;
                            break;
                        case 3:
                            while (!correctstring)
                            {
                                Console.WriteLine("What should it contain? (only letters)");
                                beginswith = Console.ReadLine();
                                bool wrongstring = false;
                                foreach (char item in beginswith)
                                {
                                    if (!char.IsLetter(item))
                                    {
                                        wrongstring = true;
                                        Console.WriteLine("Contains characters that are not letters");
                                        break;
                                    }
                                }
                                if (wrongstring)
                                    continue;
                                correctstring = true;
                            }
                            while (!correctnumber)
                            {
                                Console.WriteLine("How long should the words be? (in numbers)");
                                amounts = Console.ReadLine();
                                try
                                {
                                    amounti = int.Parse(amounts);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Not an number");
                                    continue;
                                }
                                correctnumber = true;
                            }
                            Contains(beginswith, amounti);
                            SaveToPDF();
                            busy = false;
                            break;
                        case 4:
                            Console.WriteLine("Enter your regex");
                            Regexparse(Console.ReadLine());
                            SaveToPDF();
                            busy = false;
                            break;
                        default:
                            Console.WriteLine("Not an option");
                            busy = false;
                            break;
                    }
                }
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void SaveToPDF()
        {
            File.WriteAllText("pdf.bat", batfile.ToString());
            System.Diagnostics.Process.Start("pdf.bat");
            batfile.Clear();
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
            batfile.AppendLine($"start {name}.dot.pdf -O");
        }
    }
}