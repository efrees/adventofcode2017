using System;
using System.Text;

namespace AdventOfCode2017.Solvers
{
    internal class Day3Solver : IProblemSolver
    {
        private const int PuzzleInput = 265149;

        public static Day3Solver Create()
        {
            return new Day3Solver();
        }

        public void Solve(string fileText)
        {
            SolvePart1();
            SolvePart2();
        }

        public void SolvePart1()
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

        public void SolvePart2()
        {
            var grid = new int[518, 518];
            var startCoord = 518 / 2;

            grid[startCoord, startCoord] = 1;

            var currentX = startCoord + 1;
            var currentY = startCoord;

            var direction = 3;
            var movesInThisDirection = 0;
            var sideLength = 1;

            var lastNumber = 1;
            while (lastNumber <= PuzzleInput)
            {
                var nextNumber = GetSumAround(grid, currentX, currentY);
                grid[currentX, currentY] = nextNumber;

                movesInThisDirection++;
                if (direction == 3 && movesInThisDirection == sideLength
                    || direction != 3 && movesInThisDirection == sideLength - 1)
                {
                    //turn
                    direction = (direction + 1) % 4;
                    movesInThisDirection = 0;

                    if (direction == 0)
                    {
                        sideLength = sideLength + 2;
                        movesInThisDirection = 1;
                    }
                }

                switch (direction)
                {
                    case 0: //up
                        currentY++;
                        break;
                    case 1: //left
                        currentX--;
                        break;
                    case 2://down
                        currentY--;
                        break;
                    case 3: //right
                        currentX++;
                        break;
                }

                lastNumber = nextNumber;
            }

            Console.WriteLine($"P2: {lastNumber}");
        }

        private int GetSumAround(int[,] grid, int currentX, int currentY)
        {
            var sum = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    sum += grid[currentX + i, currentY + j];
                }
            }
            return sum;
        }

        private string GridToString(int[,] grid)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    sb.Append(grid[i, j]);
                    sb.Append(',');
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}