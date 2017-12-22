using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day22Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day22Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private static void SolvePart1(string fileText)
        {
            var gridInit = fileText.SplitIntoLines()
                .ToArray();
            var centerY = gridInit.Length / 2;
            var centerX = gridInit[centerY].Length / 2;
            var grid = new Dictionary<Tuple<int, int>, bool>();
            for (int i = 0; i < gridInit.Length; i++)
            {
                for (int j = 0; j < gridInit[0].Length; j++)
                {
                    if (gridInit[i][j] == '#') grid[Tuple.Create(i - centerY, j - centerX)] = true;
                }
            }

            var currentX = 0;
            var currentY = 0;
            var direction = new Vector { X = 0, Y = -1 }; //up

            var countInfections = 0;
            for (var iter = 0; iter < 10000; iter++)
            {
                var point = Tuple.Create(currentY, currentX);
                if (grid.ContainsKey(point) && grid[point])
                {
                    grid[point] = false;
                    direction.TurnRight();
                    currentY += direction.Y;
                    currentX += direction.X;
                }
                else
                {
                    grid[point] = true;
                    direction.TurnLeft();
                    currentY += direction.Y;
                    currentX += direction.X;
                    countInfections++;
                }
            }
            Output.Answer(countInfections);
        }

        private static void SolvePart2(string fileText)
        {
            var gridInit = fileText.SplitIntoLines()
                .ToArray();
            var centerY = gridInit.Length / 2;
            var centerX = gridInit[centerY].Length / 2;
            var grid = new Dictionary<Tuple<int, int>, NodeState>();
            for (int i = 0; i < gridInit.Length; i++)
            {
                for (int j = 0; j < gridInit[0].Length; j++)
                {
                    if (gridInit[i][j] == '#') grid[Tuple.Create(i - centerY, j - centerX)] = NodeState.Infected;
                    if (gridInit[i][j] == '.') grid[Tuple.Create(i - centerY, j - centerX)] = NodeState.Clean;
                }
            }

            var currentX = 0;
            var currentY = 0;
            var direction = new Vector { X = 0, Y = -1 }; //up

            var countInfections = 0;
            for (var iter = 0; iter < 10000000; iter++)
            {
                var point = Tuple.Create(currentY, currentX);
                if (!grid.ContainsKey(point))
                    grid[point] = NodeState.Clean;

                switch (grid[point])
                {
                    case NodeState.Clean:
                        grid[point] = NodeState.Weakened;
                        direction.TurnLeft();
                        break;
                    case NodeState.Weakened:
                        grid[point] = NodeState.Infected;
                        countInfections++;
                        break;
                    case NodeState.Infected:
                        grid[point] = NodeState.Flagged;
                        direction.TurnRight();
                        break;
                    case NodeState.Flagged:
                        grid[point] = NodeState.Clean;
                        direction.TurnLeft();
                        direction.TurnLeft();
                        break;
                }
                currentY += direction.Y;
                currentX += direction.X;
            }
            Output.Answer(countInfections);
        }

        private enum NodeState
        {
            Clean,
            Weakened,
            Infected,
            Flagged
        }

        private class Vector
        {
            public int X { get; set; }
            public int Y { get; set; }

            public void TurnRight()
            {
                var newX = -Y;
                var newY = X;
                X = newX;
                Y = newY;
            }

            public void TurnLeft()
            {
                var newX = Y;
                var newY = -X;
                X = newX;
                Y = newY;
            }
        }
    }
}