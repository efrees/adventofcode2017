using System;
using System.Collections;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day15Solver : IProblemSolver
    {
        private int _genASeed = 16807;
        private int _genBSeed = 48271;
        private int _mod = 2147483647;

        public static IProblemSolver Create() => new Day15Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var currentNumbers = fileText.SplitIntoLines()
                .Select(s => s.Split(' ').Last())
                .Select(int.Parse)
                .ToArray();
            var answer = 0;
            foreach (var iter in Enumerable.Range(1, 40000000))
            {
                currentNumbers[0] = GenNext(currentNumbers[0], _genASeed);
                currentNumbers[1] = GenNext(currentNumbers[1], _genBSeed);

                if (NumbersMatch(currentNumbers[0], currentNumbers[1]))
                    answer++;
            }

            Output.Answer(answer);
        }

        private void SolvePart2(string fileText)
        {
            var currentNumbers = fileText.SplitIntoLines()
                .Select(s => s.Split(' ').Last())
                .Select(int.Parse)
                .ToArray();
            var answer = 0;
            foreach (var iter in Enumerable.Range(1, 5000000))
            {
                currentNumbers[0] = GenNextDivisible(currentNumbers[0], _genASeed, 4);
                currentNumbers[1] = GenNextDivisible(currentNumbers[1], _genBSeed, 8);

                if (NumbersMatch(currentNumbers[0], currentNumbers[1]))
                    answer++;
            }

            Output.Answer(answer);
        }

        private int GenNextDivisible(int currentNumber, int seed, int divisor)
        {
            var next = GenNext(currentNumber, seed);
            while (next % divisor != 0)
                next = GenNext(next, seed);

            return next;
        }

        private bool NumbersMatch(int currentA, int curB)
        {
            return (currentA & 0xFFFF) == (curB & 0xFFFF);
        }

        private int GenNext(int currentNumber, int seed)
        {
            return (int)((((long)currentNumber) * seed) % _mod);
        }
    }
}