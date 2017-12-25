using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day25Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day25Solver();

        public void Solve(string fileText)
        {
            var lines = fileText
                .SplitIntoLines()
                .Select(x => x)
                .ToList();

            var answer = DoLoop(lines);

            Output.Answer(answer);
        }

        internal int DoLoop(IEnumerable<string> lines)
        {
            var count = 0;
            var iterations = lines.Count();
            for (var i = 0; i < iterations; i++)
            {
                //for(var j = 0; j < iterations; j++){    }
                if (true)
                {
                    count++;
                }
            }
            return count;
        }
    }
}