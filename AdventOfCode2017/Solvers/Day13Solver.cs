using System;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day13Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day13Solver();
        }

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var lines = fileText.SplitIntoLines()
                .Select(l => Array.ConvertAll(l.SplitRemovingEmpty(':', ' ').ToArray(), int.Parse));
            var complexity = 0;
            foreach (var line in lines)
            {
                if (line[0] % (line[1] * 2 - 2) == 0)
                    complexity += line[1] * line[0];
            }

            Output.Answer(complexity);
        }

        private void SolvePart2(string fileText)
        {
            var lines = fileText.SplitIntoLines()
                .Select(l => Array.ConvertAll(l.SplitRemovingEmpty(':', ' ').ToArray(), int.Parse));
            var delay = 0;
            while (true)
            {
                var found = false;
                foreach (var line in lines)
                {
                    if ((delay + line[0]) % (line[1] * 2 - 2) == 0)
                    {
                        delay++;
                        found = true;
                        break;
                    }
                }
                if (!found) break;
            }

            Output.Answer(delay);
        }
    }
}