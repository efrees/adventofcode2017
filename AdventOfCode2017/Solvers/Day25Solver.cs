using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Solvers
{
    internal class Day25Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day25Solver();

        public void Solve(string fileText)
        {
            var lines = new Queue<string>(fileText.SplitIntoLines());

            var startStateName = CaptureSubstringWithPattern(lines.Dequeue(), "Begin in state (\\w+).");
            var numberOfIterations = CaptureIntWithPattern(lines.Dequeue(), "after (\\d+) steps");

            var transitions = ParseTransitions(lines);

            var turingMachine = new TuringMachine(startStateName, transitions);
            turingMachine.RunForIterations(numberOfIterations);
            var answer = turingMachine.GetChecksum();

            Output.Answer(answer);
        }

        private IDictionary<string, IDictionary<int, Transition>> ParseTransitions(Queue<string> lines)
        {
            var transitions = new Dictionary<string, IDictionary<int, Transition>>();
            while (lines.Count > 0)
            {
                var stateHeader = Regex.Match(lines.Dequeue(), "In state (\\w+):");
                if (!stateHeader.Success)
                {
                    continue;
                }

                var stateName = stateHeader.Groups[1].Value;
                transitions[stateName] = ParseTransitionsForState(lines);
            }
            return transitions;
        }

        private static Dictionary<int, Transition> ParseTransitionsForState(Queue<string> lines)
        {
            var stateTransitions = new Dictionary<int, Transition>();
            while (lines.Any() && lines.Peek().Contains("If the current value is"))
            {
                var transitionValue = CaptureIntWithPattern(lines.Dequeue(), "If the current value is (\\d+):");
                var outputValue = CaptureIntWithPattern(lines.Dequeue(), "Write the value (\\d+)");
                var moveValue = CaptureSubstringWithPattern(lines.Dequeue(), "Move one slot to the (\\w+)") == "right" ? 1 : -1;
                var nextState = CaptureSubstringWithPattern(lines.Dequeue(), "Continue with state (\\w+)");
                stateTransitions.Add(transitionValue, new Transition
                {
                    Output = outputValue,
                    Move = moveValue,
                    ToState = nextState
                });
            }
            return stateTransitions;
        }

        private static string CaptureSubstringWithPattern(string input, string pattern)
        {
            return Regex.Match(input, pattern).Groups[1].Value;
        }

        private static int CaptureIntWithPattern(string input, string pattern)
        {
            return int.Parse(CaptureSubstringWithPattern(input, pattern));
        }

        private class TuringMachine
        {
            public TuringMachine(string startStateName, IDictionary<string, IDictionary<int, Transition>> transitions)
            {
                _currentState = startStateName;
                _transitions = transitions;
            }

            private int _currentPosition;
            private string _currentState;
            private readonly IDictionary<string, IDictionary<int, Transition>> _transitions;
            private readonly IDictionary<int, int> _tape = new Dictionary<int, int>();

            public void RunForIterations(int iterations)
            {
                for (var i = 0; i < iterations; i++)
                {
                    var val = _tape.ContainsKey(_currentPosition) ? _tape[_currentPosition] : 0;
                    var transition = _transitions[_currentState][val];
                    _tape[_currentPosition] = transition.Output;
                    _currentPosition += transition.Move;
                    _currentState = transition.ToState;
                }
            }

            public int GetChecksum()
            {
                return _tape.Count(x => x.Value == 1);
            }
        }

        private class Transition
        {
            public int Output { get; set; }
            public int Move { get; set; }
            public string ToState { get; set; }
        }
    }
}