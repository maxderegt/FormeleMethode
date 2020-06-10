using System;
using System.Collections.Generic;
using System.Linq;

namespace FormeleMethode
{
    /// <summary>
    /// This class creates an NDFA given a Regex that is created with the RegExp class
    /// See FOR lesson 2 powerpoint for what each letter means (sheets 14 to 19)
    /// </summary>
    public class Thompson
    {
        /// <summary>
        /// create a NDFA given a RegExp
        /// </summary>
        /// <param name="reg">the given RegExp</param>
        /// <returns> a list of nodes from which to create a NDFA</returns>
        public static List<Node> CreateAutomaat(RegExp reg)
        {
            List<Node> automaton = new List<Node>() { //our list of nodes, initialized with a start and end node, they are a given for any automaton
                new Node(new List<Connection>(), "q0", NodeType.StartNode), //node a
                new Node(new List<Connection>(), "q1", NodeType.EndNode)    //node b
            };

            //leftstate is initially the startnode, rightstate the endnode, statecounter keeps track of the last node in automaton
            int stateCounter = 1, leftState = 0, rightState = 1;

            ModifyAutomaat(reg, automaton, stateCounter, leftState, rightState);

            return automaton;
        }

        /// <summary>
        /// Handles the different operators (+*|. en ONE) and sends everything to the right 'rule' to be handled.
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaton">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        private static void ModifyAutomaat(RegExp reg, List<Node> automaton, int stateCounter, int leftState, int rightState)
        {
            switch (reg._operator) //checking which operator has to be handled and sends it to the right rule
            {
                case RegExp.Operator.PLUS:
                    Regel5(reg, automaton, stateCounter, leftState, rightState);
                    break;
            case RegExp.Operator.STAR:
                Regel6(reg, automaton, stateCounter, leftState, rightState);
                break;
            case RegExp.Operator.OR:
                Regel4(reg, automaton, stateCounter, leftState, rightState);
                break;
                case RegExp.Operator.DOT:
                    Regel3(reg, automaton, stateCounter, leftState, rightState);
                    break;
                case RegExp.Operator.ONE:
                    Regel1En2(reg, automaton, stateCounter, leftState, rightState);
                    break;
                default: 
                    throw new ArgumentOutOfRangeException(); //if this gets triggered it would mean that there is a new operator in RegExp
            }
        }
        /// <summary>
        /// The translation of a single terminal symbol and an empty production(one) (rule 1 and 2)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaton">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel1En2(RegExp reg, List<Node> automaton, int stateCounter, int leftState, int rightState)
        {
            //only the first letter is used
            char symbol = reg.terminals.First(); //terminals contains the letter for the connection to go from node[leftState] to node[rightState]
            automaton[leftState].AddConnection(new Connection(symbol, automaton[rightState])); //from a to b
        }

        /// <summary>
        /// Translation from concatenation (dot) (rule 3)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaton">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel3(RegExp reg, List<Node> automaton, int stateCounter, int leftState, int rightState)
        {
            var newState = stateCounter + 1;
            stateCounter = newState;
            //node c, a dot means that there is atleast 1 new node
            automaton.Add(new Node(new List<Connection>(), "q" + newState, NodeType.NormalNode));

            ModifyAutomaat(reg.left, automaton, stateCounter, leftState, newState); //handle left side of the dot
            ModifyAutomaat(reg.right, automaton, stateCounter, newState, rightState); //handle right side of the dot
        }

        /// <summary>
        /// The translation of the choice operator (or) (rule 4)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaton">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel4(RegExp reg, List<Node> automaton, int stateCounter, int leftState, int rightState)
        {
            //the first path to handle (left side)
            var newLeftState = stateCounter + 1;
            var newRightState = newLeftState + 1;
            stateCounter = newRightState;

            //node c, add a new node for the or left side(the first epsilon) 
            automaton.Add(new Node(new List<Connection>(), "q" + newLeftState, NodeType.NormalNode));

            //node d, the new node has a connection to automaton[rightState] with epsilon (the last epsilon)
            automaton.Add(new Node(new List<Connection>() { new Connection('ϵ', automaton[rightState]) }, "q" + newRightState, NodeType.NormalNode)); //from d to b
            automaton[leftState].AddConnection(new Connection('ϵ', automaton[newLeftState])); //from a to c

            ModifyAutomaat(reg.left, automaton, stateCounter, newLeftState, newRightState); //handle left side of the or

            //there are 2 paths to handle, this handles the second one (right side)
            newLeftState = stateCounter + 1;
            newRightState = newLeftState + 1;
            stateCounter = newRightState;

            //node e, add a new node for the or right side (the first epsilon)
            automaton.Add(new Node(new List<Connection>(), "q" + newLeftState, NodeType.NormalNode));

            //node f, the new node has a connection to automaton[rightState] with epsilon (the last epsilon)
            automaton.Add(new Node(new List<Connection>() { new Connection('ϵ', automaton[rightState]) }, "q" + newRightState, NodeType.NormalNode)); //from f to b
            automaton[leftState].AddConnection(new Connection('ϵ', automaton[newLeftState])); //from a to e

            ModifyAutomaat(reg.right, automaton, stateCounter, newLeftState, newRightState); //handle right side of the or
        }

        /// <summary>
        /// The translation of the plus operator (+) (rule 5)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaton">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel5(RegExp reg, List<Node> automaton, int stateCounter, int leftState, int rightState)
        {
            var newLeftState = stateCounter + 1;
            var newRightState = newLeftState + 1;
            stateCounter = newRightState;

            //add 2 new nodes for the + operator
            automaton.Add(new Node(new List<Connection>(), "q" + newLeftState, NodeType.NormalNode)); //node c
            automaton.Add(new Node(new List<Connection>(), "q" + newRightState, NodeType.NormalNode)); //node d

            automaton[leftState].AddConnection(new Connection('ϵ', automaton[newLeftState])); //from a to c
            automaton[newRightState].AddConnection(new Connection('ϵ', automaton[rightState])); //from d to b
            automaton[newRightState].AddConnection(new Connection('ϵ', automaton[newLeftState])); //from d to c

            ModifyAutomaat(reg.left, automaton, stateCounter, newLeftState, newRightState); //a plus operator has no right side 
        }

        /// <summary>
        /// The translation of the star operator (*) (rule 6)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaton">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel6(RegExp reg, List<Node> automaton, int stateCounter, int leftState, int rightState)
        {
            var newLeftState = stateCounter + 1;
            var newRightState = newLeftState + 1;
            stateCounter = newRightState;

            automaton.Add(new Node(new List<Connection>(), "q" + newLeftState, NodeType.NormalNode)); //node c
            automaton.Add(new Node(new List<Connection>(), "q" + newRightState, NodeType.NormalNode)); //node d

            automaton[leftState].AddConnection(new Connection('ϵ', automaton[rightState])); //from a to b
            automaton[leftState].AddConnection(new Connection('ϵ', automaton[newLeftState])); //from a to c
            automaton[newRightState].AddConnection(new Connection('ϵ', automaton[rightState])); //from d to b
            automaton[newRightState].AddConnection(new Connection('ϵ', automaton[newLeftState])); //from d to c

            //rule 6 has no right state (*) so we only need to check the left state
            ModifyAutomaat(reg.left, automaton, stateCounter, newLeftState, newRightState);
        }
    }
}