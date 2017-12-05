using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Solvers
{
    internal class Day4Solver : IProblemSolver
    {
        public static Day4Solver Create()
        {
            return new Day4Solver();
        }

        public void Solve(string fileText)
        {
            var count = 0;
            var countPart2 = 0;

            foreach (var line in fileText.SplitIntoLines())
            {
                if (IsValidPassphrase(line))
                {
                    count++;
                }
                if (IsValidPassphrasePart2(line))
                    countPart2++;
            }

            Console.WriteLine($"P1: {count}");
            Console.WriteLine($"P2: {countPart2}");
        }

        private bool IsValidPassphrase(string line)
        {
            var foundWords = new HashSet<string>();
            var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (foundWords.Contains(word))
                    return false;

                foundWords.Add(word);
            }
            return true;
        }

        private bool IsValidPassphrasePart2(string line)
        {
            var foundWords = new HashSet<string>();
            var words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                var charArray = word.ToCharArray();
                Array.Sort(charArray);
                var orderedWord = new string(charArray);
                if (foundWords.Contains(orderedWord))
                    return false;

                foundWords.Add(orderedWord);
            }
            return true;
        }
    }
}