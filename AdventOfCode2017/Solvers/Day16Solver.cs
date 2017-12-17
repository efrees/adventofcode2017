using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day16Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day16Solver();
        private string[] _moves;
        private string _movesText;

        public void Solve(string fileText)
        {
            _moves = _movesText == fileText && _moves != null
                ? _moves
                : fileText.Split(',');
            _movesText = fileText;
            SolvePart1(_moves);
            SolvePart2(_moves);
        }

        private void SolvePart1(string[] moves)
        {
            var newline = "abcdefghijklmnop".ToCharArray();
            var startIndex = DanceMoves(moves, newline, 0);
            var answer = string.Join("", newline.Skip(startIndex).Concat(newline.Take(startIndex)));
            Output.Answer(answer);
        }

        private void SolvePart2(string[] moves)
        {
            var newline = "abcdefghijklmnop".ToCharArray();
            var seenStates = new Dictionary<string, int>();
            var step = 0;
            var remainingSteps = 1000000000;
            var currentStart = 0;
            while (remainingSteps > 0)
            {
                var orderKey = new string(newline) + currentStart;
                if (!seenStates.ContainsKey(orderKey))
                {
                    seenStates[orderKey] = step;
                }
                else
                {
                    //loop found
                    var loopSize = step - seenStates[orderKey];
                    remainingSteps = remainingSteps % loopSize;
                }
                currentStart = DanceMoves(moves, newline, currentStart);
                step++;
                remainingSteps--;
            }
            var answer = string.Join("", newline.Skip(currentStart).Concat(newline.Take(currentStart)));
            Output.Answer(answer);
        }

        private int DanceMoves(string[] moves, char[] line, int currentStart)
        {
            foreach (var move in moves)
            {
                switch (move[0])
                {
                    case 's':
                        var spin = int.Parse(move.Substring(1));
                        currentStart = InRange(currentStart - spin);
                        break;
                    case 'p':
                        var ai = Array.IndexOf(line, move[1]);
                        var bi = Array.IndexOf(line, move[3]);
                        line[ai] = move[3];
                        line[bi] = move[1];
                        break;
                    case 'x':
                        var indexes = Array.ConvertAll(move.Substring(1).Split('/'), int.Parse);
                        indexes[0] = InRange(currentStart+indexes[0]);
                        indexes[1] = InRange(currentStart+indexes[1]);
                        var t = line[indexes[0]];
                        line[indexes[0]] = line[indexes[1]];
                        line[indexes[1]] = t;
                        break;
                }
            }
            return currentStart;
        }

        private int InRange(int x)
        {
            return (x + 16) % 16;
        }
    }
}