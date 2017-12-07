using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day6Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day6Solver();
        }

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
        }

        private Dictionary<int, int> _statesSeen = new Dictionary<int, int>();
        private void SolvePart1(string fileText)
        {
            var strings = fileText.Split('\t')
                .ToArray();
            var currentState = Array.ConvertAll(strings, int.Parse); ;
            var count = 0;

            while (StateNotSeenBefore(currentState))
            {
                LogAsSeen(currentState, count);
                Redistribute(currentState);

                count++;
            }

            var targetState = currentState.CreateHash();
            var loopLength = count - _statesSeen[targetState];

            Console.WriteLine($"P1: {count}");
            Console.WriteLine($"P2: {loopLength}");
        }

        private void LogAsSeen(int[] previous, int index)
        {
            var newArray = previous.CreateHash();
            _statesSeen.Add(newArray, index);
        }

        private bool StateNotSeenBefore(int[] nextState)
        {
            var nextStateString = nextState.CreateHash();
            return !_statesSeen.ContainsKey(nextStateString);
        }

        private static void Redistribute(int[] nextState)
        {
            var max = nextState.Max();
            var index = nextState.ToList().FindIndex(x => x == max);

            nextState[index] = 0;
            for (var i = 1; max > 0; i++, max--)
            {
                nextState[(index + i) % nextState.Length]++;
            }
        }
    }
}