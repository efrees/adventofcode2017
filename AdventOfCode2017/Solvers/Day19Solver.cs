using System;
using System.Linq;
using System.Text;

namespace AdventOfCode2017.Solvers
{
    internal class Day19Solver : IProblemSolver
    {
        private enum Direction
        {
            Down,
            Left,
            Up,
            Right
        }

        public static IProblemSolver Create() => new Day19Solver();

        public void Solve(string fileText)
        {
            var lines = fileText.TrimEnd().Split('\n')
                .Select(s => s.ToCharArray())
                .ToArray();

            var y = 0;
            var x = Array.IndexOf(lines[0], '|');
            var direction = Direction.Down;
            var sb = new StringBuilder();
            var stepCount = 1;

            var stopped = false;
            while (!stopped)
            {
                switch (direction)
                {
                    case Direction.Down:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveTo(lines, y + 1, x))
                        {
                            y++;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveTo(lines, y, x + 1))
                            {
                                x++;
                                direction = Direction.Right;
                            }
                            else if (CanMoveTo(lines, y, x - 1))
                            {
                                x--;
                                direction = Direction.Left;
                            }
                        }
                        else
                        {
                            stopped = true;
                        }
                        break;

                    case Direction.Left:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveTo(lines, y, x - 1))
                        {
                            x--;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveTo(lines, y + 1, x))
                            {
                                y++;
                                direction = Direction.Down;
                            }
                            else if (CanMoveTo(lines, y - 1, x))
                            {
                                y--;
                                direction = Direction.Up;
                            }
                        }
                        else
                        {
                            stopped = true;
                        }
                        break;

                    case Direction.Up:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveTo(lines, y - 1, x))
                        {
                            y--;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveTo(lines, y, x + 1))
                            {
                                x++;
                                direction = Direction.Right;
                            }
                            else if (CanMoveTo(lines, y, x - 1))
                            {
                                x--;
                                direction = Direction.Left;
                            }
                        }
                        else
                        {
                            stopped = true;
                        }
                        break;

                    case Direction.Right:
                        if (char.IsLetter(lines[y][x]))
                            sb.Append(lines[y][x]);

                        if (CanMoveTo(lines, y, x + 1))
                        {
                            x++;
                        }
                        else if (lines[y][x] == '+')
                        {
                            if (CanMoveTo(lines, y + 1, x))
                            {
                                y++;
                                direction = Direction.Down;
                            }
                            else if (CanMoveTo(lines, y - 1, x))
                            {
                                y--;
                                direction = Direction.Up;
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
            Output.Answer(stepCount, "StepCount (P2)");
        }

        private static bool CanMoveTo(char[][] lines, int y, int x)
        {
            return x >= 0 && x < lines[y].Length && lines[y][x] != ' ';
        }
    }
}