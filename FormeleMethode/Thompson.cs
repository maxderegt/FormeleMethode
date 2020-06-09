using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormeleMethode
{
    /// <summary>
    /// This class creates an NDFA given a Regex that is created with the RegExp class
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
            List<Node> automaat = new List<Node>()
            {
                new Node("q0", NodeType.StartNode),
                new Node("q1", NodeType.EndNode)
            };
            automaat[0].AddConnections(new List<Connection>());
            automaat[1].AddConnections(new List<Connection>());

            int stateCounter = 1, leftState = 0, rightState = 1;

            ModifyAutomaat(reg, automaat, stateCounter, leftState, rightState);

            return automaat;
        }

        /// <summary>
        /// Handles the different operators (+*|. en ONE) and sends everything to the right 'rule' to be handled.
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaat">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        private static void ModifyAutomaat(RegExp reg, List<Node> automaat, int stateCounter, int leftState, int rightState)
        {
            switch (reg._operator)
            {
                case RegExp.Operator.PLUS:
                    Regel5(reg, automaat, stateCounter, leftState, rightState);
                    break;
            case RegExp.Operator.STAR:
                Regel6(reg, automaat, stateCounter, leftState, rightState);
                break;
            case RegExp.Operator.OR:
                Regel4(reg, automaat, stateCounter, leftState, rightState);
                break;
                case RegExp.Operator.DOT:
                    Regel3(reg, automaat, stateCounter, leftState, rightState);
                    break;
                case RegExp.Operator.ONE:
                    Regel1En2(reg, automaat, stateCounter, leftState, rightState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// The translation of a single terminal symbol and an empty production(one) (rule 1 and 2)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaat">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel1En2(RegExp reg, List<Node> automaat, int stateCounter, int leftState, int rightState)
        {
            char symbol = reg.terminals.First();

            automaat[leftState].AddConnections(new List<Connection>()
            {
                new Connection(symbol, automaat[rightState])
            });
        }

        /// <summary>
        /// Translation from concatenation (dot) (rule 3)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaat">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel3(RegExp reg, List<Node> automaat, int stateCounter, int leftState, int rightState)
        {
            var newState = stateCounter + 1;
            stateCounter = newState;

            automaat.Add(new Node("q" + newState, NodeType.NormalNode));
            automaat[newState].AddConnections(new List<Connection>());

            ModifyAutomaat(reg.left, automaat, stateCounter, leftState, newState);
            ModifyAutomaat(reg.right, automaat, stateCounter, newState, rightState);
        }

        /// <summary>
        /// The translation of the choice operator (or) (rule 4)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaat">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel4(RegExp reg, List<Node> automaat, int stateCounter, int leftState, int rightState)
        {
            //the first path to handle
            var newLeftState = stateCounter + 1;
            var newRightState = newLeftState + 1;
            stateCounter = newRightState;

            automaat.Add(new Node("q" + newLeftState, NodeType.NormalNode));
            automaat.Add(new Node("q" + newRightState, NodeType.NormalNode));
            automaat[newLeftState].AddConnections(new List<Connection>());
            automaat[newRightState].AddConnections(new List<Connection>());

            automaat[leftState].AddConnection(new Connection('ϵ', automaat[newLeftState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[rightState]));

            ModifyAutomaat(reg.left, automaat, stateCounter, newLeftState, newRightState);

            //there are 2 paths to handle, this handles the second one
            newLeftState = stateCounter + 1;
            newRightState = newLeftState + 1;
            stateCounter = newRightState;
            automaat.Add(new Node("q" + newLeftState, NodeType.NormalNode));
            automaat.Add(new Node("q" + newRightState, NodeType.NormalNode));
            automaat[newLeftState].AddConnections(new List<Connection>());
            automaat[newRightState].AddConnections(new List<Connection>());
            automaat[leftState].AddConnection(new Connection('ϵ', automaat[newLeftState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[rightState]));
            ModifyAutomaat(reg.right, automaat, stateCounter, newLeftState, newRightState);
        }

        /// <summary>
        /// The translation of the plus operator (+) (rule 5)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaat">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel5(RegExp reg, List<Node> automaat, int stateCounter, int leftState, int rightState)
        {
            var newLeftState = stateCounter + 1;
            var newRightState = newLeftState + 1;
            stateCounter = newRightState;

            automaat.Add(new Node("q" + newLeftState, NodeType.NormalNode));
            automaat.Add(new Node("q" + newRightState, NodeType.NormalNode));
            automaat[newLeftState].AddConnections(new List<Connection>());
            automaat[newRightState].AddConnections(new List<Connection>());

            automaat[leftState].AddConnection(new Connection('ϵ', automaat[newLeftState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[rightState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[newLeftState]));

            ModifyAutomaat(reg.left, automaat, stateCounter, newLeftState, newRightState);
        }

        /// <summary>
        /// The translation of the star operator (*) (rule 6)
        /// </summary>
        /// <param name="reg">the regular expression created with the RegExp class</param>
        /// <param name="automaat">the NDFA</param>
        /// <param name="stateCounter">keeps track of the next state to add</param>
        /// <param name="leftState">From state</param>
        /// <param name="rightState">To state</param>
        public static void Regel6(RegExp reg, List<Node> automaat, int stateCounter, int leftState, int rightState)
        {
            var newLeftState = stateCounter + 1;
            var newRightState = newLeftState + 1;
            stateCounter = newRightState;

            automaat.Add(new Node("q" + newLeftState, NodeType.NormalNode));
            automaat.Add(new Node("q" + newRightState, NodeType.NormalNode));
            automaat[newLeftState].AddConnections(new List<Connection>());
            automaat[newRightState].AddConnections(new List<Connection>());

            automaat[leftState].AddConnection(new Connection('ϵ', automaat[rightState]));
            automaat[leftState].AddConnection(new Connection('ϵ', automaat[newLeftState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[rightState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[newLeftState]));

            ModifyAutomaat(reg.left, automaat, stateCounter, newLeftState, newRightState);
        }
    }
}
