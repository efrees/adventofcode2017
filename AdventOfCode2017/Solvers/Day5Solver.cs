using System;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day5Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day5Solver();
        }

        public void Solve(string fileText)
        {
            Console.WriteLine($"P1: {SolvePart1(fileText)}");
            Console.WriteLine($"P2: {SolvePart2(fileText)}");
        }

        private static int SolvePart1(string fileText)
        {
            var lines = fileText.SplitIntoLines()
                .Select(int.Parse)
                .ToArray();

            var steps = 0;
            var current = 0;

            while (current >= 0 && current < lines.Length)
            {
                var temp = lines[current];
                lines[current]++;
                current += temp;
                steps++;
            }
            return steps;
        }

        private static int SolvePart2(string fileText)
        {
            var lines = fileText.SplitIntoLines()
                .Select(int.Parse)
                .ToArray();

            var steps = 0;
            var current = 0;

            while (current >= 0 && current < lines.Length)
            {
                var temp = lines[current];
                if (temp >= 3)
                    lines[current]--;
                else
                    lines[current]++;
                current += temp;
                steps++;
            }
            return steps;
        }
    }
}