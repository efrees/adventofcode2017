using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day17Solver : IProblemSolver
    {
        private int _step = 324;

        public static IProblemSolver Create() => new Day17Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var answer = 0;
            var list = new List<int> { 0 };
            var index = 0;
            for (int i = 1; i < 2018; i++)
            {
                index = (index + _step + 1) % list.Count;
                list.Insert(index, i);
            }

            answer = list[(list.IndexOf(2017) + 1) % list.Count];
            Output.Answer(answer);
        }

        private void SolvePart2(string fileText)
        {
            var length = 1;
            var secondElement = -1;
            var index = 0;
            for (var i = 1; i <= 50000000; i++)
            {
                var countedTo = (index + _step) % length;
                if (countedTo == 0)
                    secondElement = i;

                length++;
                index = countedTo + 1;
            }

            Output.Answer(secondElement);
        }
    }
}