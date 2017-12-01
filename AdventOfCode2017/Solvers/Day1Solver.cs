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
        
        public void Solve(string fileText)
        {
            var line = fileText.Trim();

            var sumPart1 = 0;
            var sumPart2 = 0;
            for (var i = 0; i < line.Length; i++)
            {
                var next = (i + 1) % line.Length;
                if (line[i] == line[next])
                {
                    sumPart1 += line[i] - '0';
                }

                var nextPart2 = (i + line.Length/2) % line.Length;
                if (line[i] == line[nextPart2])
                {
                    sumPart2 += line[i] - '0';
                }
            }
            
            Console.WriteLine($"P1: {sumPart1}");
            Console.WriteLine($"P2: {sumPart2}");
        }
    }
}