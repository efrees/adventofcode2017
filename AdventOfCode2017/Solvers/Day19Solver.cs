using System;
using System.Linq;
using System.Text;

namespace AdventOfCode2017.Solvers
{
    internal class Day19Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day19Solver();

        public void Solve(string fileText)
        {
//            fileText = @"     |          
//     |  +--+    
//     A  |  C    
// F---|--|-E---+ 
//     |  |  |  D 
//     +B-+  +--+ 
//";
            var lines = fileText.TrimEnd().Split('\n')
                .Select(s => s.ToCharArray())
                .ToArray();

            var y = 0;
            var x = Array.IndexOf(lines[0], '|');
            var direction = 0; //down
            var sb = new StringBuilder();
            var stopped = false;
            var stepCount = 0;

            while (!stopped)
            {
                switch (direction)
                {
                    case 0:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveVertically(lines, x, y, 1))
                        {
                            y++;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveHorizontally(lines, y, x, 1))
                            {
                                x++;
                                direction = 3;
                            }
                            else if (CanMoveHorizontally(lines, y, x, -1))
                            {
                                x--;
                                direction = 1;
                            }
                        }
                        else
                        {
                            stopped = true;
                        }
                        break;

                    case 1:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveHorizontally(lines, y, x, -1))
                        {
                            x--;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveVertically(lines, x, y, 1))
                            {
                                y++;
                                direction = 0;
                            }
                            else if (CanMoveVertically(lines, x, y, -1))
                            {
                                y--;
                                direction = 2;
                            }
                        }
                        else
                        {
                            stopped = true;
                        }
                        break;

                    case 2:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveVertically(lines, x, y, -1))
                        {
                            y--;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveHorizontally(lines, y, x, +1))
                            {
                                x++;
                                direction = 3;
                            }
                            else if (CanMoveHorizontally(lines, y, x, -1))
                            {
                                x--;
                                direction = 1;
                            }
                        }
                        else
                        {
                            stopped = true;
                        }
                        break;

                    case 3:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveHorizontally(lines, y, x, +1))
                        {
                            x++;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveVertically(lines, x, y, 1))
                            {
                                y++;
                                direction = 0;
                            }
                            else if (CanMoveVertically(lines, x, y, -1))
                            {
                                y--;
                                direction = 2;
                            }
                        }
                        else
                        {
                            stopped = true;
                        }
                        break;
                }

                stepCount++;
            }

            Output.Answer(sb.ToString());
            Output.Answer(stepCount, "StepCount");
        }

        private bool CanMoveVertically(char[][] lines, int x, int y, int increment)
        {
            var nextY = y + increment;
            return nextY >= 0 && nextY < lines.Length
                   && (lines[nextY][x] == '|' || lines[nextY][x] == '+' || char.IsLetter(lines[nextY][x])
                       || (lines[nextY][x] == '-' && CanMoveVertically(lines, x, nextY, increment)));
        }

        //private static bool CanMoveVertically(string[] lines, int x, int nextY)
        //{
        //    return nextY >= 0 && nextY < lines.Length
        //        && (lines[nextY][x] == '|' || lines[nextY][x] == '+' || char.IsLetter(lines[nextY][x]));
        //}

        private static bool CanMoveHorizontally(char[][] lines, int y, int x, int increment)
        {
            var nextX = x + increment;
            return nextX >= 0 && nextX < lines[y].Length
                   && (lines[y][nextX] == '-' || lines[y][nextX] == '+' || char.IsLetter(lines[y][nextX])
                       || lines[y][nextX] == '|' && CanMoveHorizontally(lines, y, nextX, increment));
        }
    }
}