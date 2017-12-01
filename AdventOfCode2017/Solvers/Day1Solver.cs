using System;
using System.Threading;
using AdventOfCode2016;

namespace AdventOfCode2017
{
    internal class Day1Solver
    {
        public static Day1Solver Create()
        {
            return new Day1Solver();
        }

        public int GetSolution(string fileText)
        {
            var line = fileText.Trim();

            var count = 0;
            for (int i = 0; i < line.Length; i++)
            {
                var next = (i + 1) % line.Length;
                if (line[i] == line[next])
                {
                    count += line[i] - '0';
                }
            }

            return count;
        }
        public int GetSolution2(string fileText)
        {
            var line = fileText.Trim();

            var count = 0;
            for (int i = 0; i < line.Length; i++)
            {
                var next = (i + line.Length/2) % line.Length;
                if (line[i] == line[next])
                {
                    count += line[i] - '0';
                }
            }

            return count;
        }
    }
}