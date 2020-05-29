using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormeleMethode
{
    public class Thompson
    {
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

            ModifyAutomaat(reg, ref automaat, ref stateCounter, leftState, rightState);

            return automaat;
        }

        private static void ModifyAutomaat(RegExp reg, ref List<Node> automaat, ref int stateCounter, int leftState, int rightState)
        {
            switch (reg._operator)
            {
                case RegExp.Operator.PLUS:
                    Regel5(reg, ref automaat, ref stateCounter, leftState, rightState);
                    break;
            case RegExp.Operator.STAR:
                Regel6(reg, ref automaat, ref stateCounter, leftState, rightState);
                break;
            case RegExp.Operator.OR:
                Regel4(reg, ref automaat, ref stateCounter, leftState, rightState);
                break;
                case RegExp.Operator.DOT:
                    Regel3(reg, ref automaat, ref stateCounter, leftState, rightState);
                    break;
                case RegExp.Operator.ONE:
                    Regel1En2(reg, ref automaat, ref stateCounter, leftState, rightState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void Regel1En2(RegExp reg, ref List<Node> automaat, ref int stateCounter, int leftState, int rightState)
        {
            char symbol = reg.terminals.First();

            automaat[leftState].AddConnections(new List<Connection>()
            {
                new Connection(symbol, automaat[rightState])
            });
        }
        public static void Regel3(RegExp reg, ref List<Node> automaat, ref int stateCounter, int leftState, int rightState)
        {
            var newState = stateCounter + 1;
            stateCounter = newState;

            automaat.Add(new Node("q" + newState, NodeType.NormalNode));
            automaat[newState].AddConnections(new List<Connection>());

            ModifyAutomaat(reg.left, ref automaat, ref stateCounter, leftState, newState);
            ModifyAutomaat(reg.right, ref automaat, ref stateCounter, newState, rightState);
        }

        public static void Regel4(RegExp reg, ref List<Node> automaat, ref int stateCounter, int leftState, int rightState)
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

            ModifyAutomaat(reg.left, ref automaat, ref stateCounter, newLeftState, newRightState);

            newLeftState = stateCounter + 1;
            newRightState = newLeftState + 1;
            stateCounter = newRightState;
            automaat.Add(new Node("q" + newLeftState, NodeType.NormalNode));
            automaat.Add(new Node("q" + newRightState, NodeType.NormalNode));
            automaat[newLeftState].AddConnections(new List<Connection>());
            automaat[newRightState].AddConnections(new List<Connection>());
            automaat[leftState].AddConnection(new Connection('ϵ', automaat[newLeftState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[rightState]));
            ModifyAutomaat(reg.right, ref automaat, ref stateCounter, newLeftState, newRightState);
        }
        public static void Regel5(RegExp reg, ref List<Node> automaat, ref int stateCounter, int leftState, int rightState)
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

            ModifyAutomaat(reg.left, ref automaat, ref stateCounter, newLeftState, newRightState);
        }
        public static void Regel6(RegExp reg, ref List<Node> automaat, ref int c, int leftState, int rightState)
        {
            var newLeftState = c + 1;
            var newRightState = newLeftState + 1;
            c = newRightState;

            automaat.Add(new Node("q" + newLeftState, NodeType.NormalNode));
            automaat.Add(new Node("q" + newRightState, NodeType.NormalNode));
            automaat[newLeftState].AddConnections(new List<Connection>());
            automaat[newRightState].AddConnections(new List<Connection>());

            automaat[leftState].AddConnection(new Connection('ϵ', automaat[rightState]));
            automaat[leftState].AddConnection(new Connection('ϵ', automaat[newLeftState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[rightState]));
            automaat[newRightState].AddConnection(new Connection('ϵ', automaat[newLeftState]));

            ModifyAutomaat(reg.left, ref automaat, ref c, newLeftState, newRightState);
        }
    }
}
