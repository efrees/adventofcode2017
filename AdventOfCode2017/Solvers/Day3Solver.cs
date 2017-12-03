using System;
using AdventOfCode2016;

namespace AdventOfCode2017.Solvers
{
    internal class Day3Solver
    {
        private const int PuzzleInput = 265149;

        public static Day3Solver Create()
        {
            return new Day3Solver();
        }

        public void Solve(string fileText)
        {
            var sideLength = 1;
            var innerTotal = 1;

            while (innerTotal < PuzzleInput)
            {
                sideLength += 2;
                innerTotal += 4 * (sideLength - 1);
                //Console.WriteLine(innerTotal);
            }

            //back up a step
            innerTotal -= 4 * (sideLength - 1);

            //next is above bottom right
            var relativeX = sideLength / 2;
            var relativeY = -sideLength / 2 + 1;
            innerTotal++;

            if (innerTotal < PuzzleInput)
            {
                var change = Math.Min(PuzzleInput - innerTotal, sideLength - 2);
                innerTotal += change;
                relativeY += change;
            }

            if (innerTotal < PuzzleInput)
            {
                var change = Math.Min(PuzzleInput - innerTotal, sideLength - 1);
                innerTotal += change;
                relativeX -= change;
            }

            if (innerTotal < PuzzleInput)
            {
                var change = Math.Min(PuzzleInput - innerTotal, sideLength - 1);
                innerTotal += change;
                relativeY -= change;
            }

            if (innerTotal < PuzzleInput)
            {
                var change = Math.Min(PuzzleInput - innerTotal, sideLength - 1);
                innerTotal += change;
                relativeX += change;
            }

            var answer = Math.Abs(relativeX) + Math.Abs(relativeY);
            Console.WriteLine($"P1: {answer}");
        }
    }
}