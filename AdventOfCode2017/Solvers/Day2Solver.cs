using System;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day2Solver : IProblemSolver
    {
        public static Day2Solver Create()
        {
            return new Day2Solver();
        }

        public void Solve(string fileText)
        {
            var sum = 0;
            var sum2 = 0;

            foreach (var line in fileText.SplitIntoLines())
            {
                sum += ComputeChecksumForLine(line);
                sum2 += ComputeEvenDivisionForLine(line);
            }
            Console.WriteLine($"P1: {sum}");
            Console.WriteLine($"P2: {sum2}");
        }

        private int ComputeChecksumForLine(string line)
        {
            var list = Array.ConvertAll(line.Split('\t'), int.Parse);
            return list.Max() - list.Min();
        }

        private int ComputeEvenDivisionForLine(string line)
        {
            var list = Array.ConvertAll(line.Split('\t'), int.Parse);

            for (int i = 0; i < list.Length; i++)
            {
                for (var j = 0; j < list.Length; j++)
                {
                    if (i == j) continue;

                    if (list[i] < list[j] && list[j] % list[i] == 0)
                    {
                        return list[j] / list[i];
                    }
                }
            }

            throw new ArgumentException();
        }
    }
}