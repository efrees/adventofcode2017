using System.Linq;
using AdventOfCode2017.Solvers.Day18;

namespace AdventOfCode2017.Solvers
{
    internal class Day23Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day23Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        internal int SolvePart1(string fileText)
        {
            var lines = fileText.SplitIntoLines()
                .Select(Instruction.Parse);
            var computer = new ProgramState();
            computer.LoadProgram(lines);
            computer.ExecuteUntilBlocked();
            var answer = computer.MulCount;
            Output.Answer(answer);
            return answer;
        }

        internal void SolvePart2(string fileText)
        {
            var b = 109300;
            var target = b + 17000;
            var count = 0;
            while (true)
            {
                var f = false;
                var d = 2;
                while (d != b)
                {
                    if (b % d == 0)
                        f = true;
                    d++;
                }
                if (f) count++;
                if (b == target) break;
                b += 17;
            }

            Output.Answer(count);
        }
    }
}