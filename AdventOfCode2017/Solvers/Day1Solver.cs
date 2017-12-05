using System;
using System.Diagnostics;

namespace AdventOfCode2017.Solvers
{
    internal class Day1Solver : IProblemSolver
    {
        public static Day1Solver Create()
        {
            return new Day1Solver();
        }

        public void Solve(string fileText)
        {
            var line = fileText.Trim();

            var stopwatch = new Stopwatch();
            var sumPart1 = 0;
            var sumPart2 = 0;
            stopwatch.Start();
            for (var i = 0; i < line.Length; i++)
            {
                var next = (i + 1) % line.Length;
                if (line[i] == line[next])
                {
                    sumPart1 += line[i] - '0';
                }

                var nextPart2 = (i + line.Length / 2) % line.Length;
                if (line[i] == line[nextPart2])
                {
                    sumPart2 += line[i] - '0';
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"P1: {sumPart1}");
            Console.WriteLine($"P2: {sumPart2}");
            Console.WriteLine($"Total runtime: {stopwatch.Elapsed}");
        }
    }
}