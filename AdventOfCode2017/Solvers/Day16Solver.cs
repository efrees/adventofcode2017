using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day16Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day16Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var moves = fileText.Split(',');
            var newline = Enumerable.Range(0, 16).Select(i => (char)(i + 'a')).ToArray();
            var startIndex = DanceMoves(moves, newline, 0);
            var answer = string.Join("", newline.Concat(newline).Skip(InRange(startIndex)).Take(16));
            Output.Answer(answer);
        }

        private void SolvePart2(string fileText)
        {
            var moves = fileText.Split(',');
            var newline = Enumerable.Range(0, 16).Select(i => (char)(i + 'a')).ToArray();
            var seenStates = new Dictionary<string, int>();
            var step = 0;
            var remainingSteps = 1000000000;
            var currentStart = 0;
            while (remainingSteps > 0)
            {
                var currentOrder = new string(newline);
                if (!seenStates.ContainsKey(currentOrder))
                {
                    seenStates[currentOrder] = step;
                }
                else
                {
                    //loop found
                    var loopSize = step - seenStates[currentOrder];
                    remainingSteps = remainingSteps % loopSize;
                }
                currentStart = DanceMoves(moves, newline, currentStart);
                step++;
                remainingSteps--;
            }
            var answer = string.Join("", newline.Concat(newline).Skip(InRange(currentStart)).Take(16));
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
                        currentStart -= spin;
                        currentStart = InRange(currentStart);
                        break;
                    case 'p':
                        var ai = Array.IndexOf(line, move[1]);
                        var bi = Array.IndexOf(line, move[3]);
                        line[ai] = move[3];
                        line[bi] = move[1];
                        break;
                    case 'x':
                        var indexes = move.Substring(1).Split('/').Select(int.Parse).ToArray();
                        var t = line[InRange(currentStart + indexes[0])];
                        line[InRange(currentStart + indexes[0])] = line[InRange(currentStart + indexes[1])];
                        line[InRange(currentStart + indexes[1])] = t;
                        break;
                }
            }
            return currentStart;
        }

        private int InRange(int x)
        {
            while (x < 0) x += 16;
            return x % 16;
        }
    }
}