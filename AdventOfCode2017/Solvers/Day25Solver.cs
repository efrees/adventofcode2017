using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day25Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day25Solver();

        public void Solve(string fileText)
        {
            var lines = fileText
                .SplitIntoLines()
                .Select(x => x)
                .ToList();

            var answer = DoLoop(lines);

            Output.Answer(answer);
        }

        internal int DoLoop(IEnumerable<string> lines)
        {
            var stateA = new State
            {
                Name = "A",
                ValuesToWrite = new[] { 1, 0 },
                MoveOffsets = new[] { 1, -1 },
                NextState = new State[2]
            };
            var stateB = new State
            {
                Name = "B",
                ValuesToWrite = new[] { 1, 1 },
                MoveOffsets = new[] { -1, 1 },
                NextState = new State[2]
            };
            var stateC = new State
            {
                Name = "C",
                ValuesToWrite = new[] { 1, 0 },
                MoveOffsets = new[] { 1, -1 },
                NextState = new State[2]
            };
            var stateD = new State
            {
                Name = "D",
                ValuesToWrite = new[] { 1, 1 },
                MoveOffsets = new[] { -1, -1 },
                NextState = new State[2]
            };
            var stateE = new State
            {
                Name = "E",
                ValuesToWrite = new[] { 1, 1 },
                MoveOffsets = new[] { 1, 1 },
                NextState = new State[2]
            };
            var stateF = new State
            {
                Name = "F",
                ValuesToWrite = new[] { 1, 1 },
                MoveOffsets = new[] { 1, 1 },
                NextState = new State[2]
            };
            stateA.NextState = new[] { stateB, stateC };
            stateB.NextState = new[] { stateA, stateC };
            stateC.NextState = new[] { stateA, stateD };
            stateD.NextState = new[] { stateE, stateC };
            stateE.NextState = new[] { stateF, stateA };
            stateF.NextState = new[] { stateA, stateE };

            var tape = new Dictionary<int, int>();
            var position = 0;
            var currentState = stateA;
            var iterations = 12261543;
            for (var i = 0; i < iterations; i++)
            {
                var val = GetOrDefault(tape, position);
                tape[position] = currentState.ValuesToWrite[val];
                position += currentState.MoveOffsets[val];
                currentState = currentState.NextState[val];
            }
            return tape.Values.Count(v => v == 1);
        }

        private int GetOrDefault(Dictionary<int, int> dict, int key)
        {
            return dict.ContainsKey(key) ? dict[key] : 0;
        }

        private class State
        {
            public string Name { get; set; }
            public int[] ValuesToWrite { get; set; }
            public int[] MoveOffsets { get; set; }
            public State[] NextState { get; set; }
        }
    }
}