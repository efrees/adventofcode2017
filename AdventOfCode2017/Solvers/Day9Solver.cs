using System;
using System.Text;

namespace AdventOfCode2017.Solvers
{
    internal class Day9Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day9Solver();
        }

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var score = ScoreGroups(fileText);

            Console.WriteLine($"P1: {score}");

            var garbageCount = CountUncancelledGarbage(fileText);
            Console.WriteLine($"P2: {garbageCount}");
        }

        internal int ScoreGroups(string fileText)
        {
            int unused;
            var cleanedText = RemoveGarbage(fileText, out unused);

            return SumGroupScores(cleanedText);
        }

        internal int CountUncancelledGarbage(string fileText)
        {
            int count;
            RemoveGarbage(fileText, out count);
            return count;
        }

        private string RemoveGarbage(string fileText, out int uncancelledCount)
        {
            var sb = new StringBuilder();
            var lastConsumed = -1;
            var garbageStart = -1;
            var nextCharCancelled = false;
            uncancelledCount = 0;
            for (var i = 0; i < fileText.Length; i++)
            {
                if (garbageStart < 0)
                {
                    if (fileText[i] == '<')
                    {
                        garbageStart = i;
                    }
                }
                else
                {
                    if (!nextCharCancelled && fileText[i] == '!')
                    {
                        nextCharCancelled = true;
                        continue;
                    }

                    if (nextCharCancelled)
                    {
                        nextCharCancelled = false;
                        uncancelledCount -= 2;
                        continue;
                    }

                    if (fileText[i] == '>')
                    {
                        sb.Append(fileText.Substring(lastConsumed + 1, garbageStart - lastConsumed - 1));
                        lastConsumed = i;
                        uncancelledCount += i +1 - garbageStart - 2;
                        garbageStart = -1;
                    }
                }
            }

            sb.Append(fileText.Substring(lastConsumed + 1));
            return sb.ToString();
        }

        private int SumGroupScores(string cleanedText)
        {
            var depth = 0;
            var sum = 0;
            foreach (var c in cleanedText)
            {
                if (c == '{')
                {
                    depth++;
                }
                else if (c == '}')
                {
                    sum += depth;
                    depth--;
                }
            }
            return sum;
        }
    }
}