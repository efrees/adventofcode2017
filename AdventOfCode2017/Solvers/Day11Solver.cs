using System;

namespace AdventOfCode2017.Solvers
{
    internal class Day11Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day11Solver();
        }

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
        }

        internal int SolvePart1(string fileText)
        {
            var moves = fileText.Split(',');

            var x = 0;
            var y = 0;
            var max = 0;

            foreach (var move in moves)
            {
                if (move == "n")
                    y += 2;

                if (move == "s")
                    y -= 2;

                if (move.Length > 1)
                {
                    if (move[0] == 'n')
                        y++;

                    if (move[0] == 's')
                        y--;

                    if (move[1] == 'e')
                        x++;
                    if (move[1] == 'w')
                        x--;
                }
                var dist = GetDistance(x, y);

                if (dist > max)
                    max = dist;
            }

            var answer = GetDistance(x, y);
            Console.WriteLine($"P1: {answer}");
            Console.WriteLine($"P2: {max}");
            return answer;
        }

        private static int GetDistance(int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            var diagonalMoves = Math.Min(x, y);
            y -= diagonalMoves;
            x -= diagonalMoves;
            y /= 2;
            var answer = diagonalMoves + y + x;
            return answer;
        }
    }
}