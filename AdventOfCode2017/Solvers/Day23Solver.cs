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
            SolvePart2FromOptimizedProgram();
            SolvePart2ByReimplementation();
        }

        private void SolvePart1(string fileText)
        {
            var lines = fileText.SplitIntoLines()
                .Select(Instruction.Parse);
            var computer = new ProgramState();
            computer.LoadProgram(lines);
            computer.ExecuteUntilBlocked();
            var answer = computer.MultiplicationCount;
            Output.Answer(answer);
        }

        private void SolvePart2FromOptimizedProgram()
        {
            var fileText = Input.GetInputFromFile("day23optimized.txt");
            var lines = fileText.SplitIntoLines()
                .Select(Instruction.Parse);
            var computer = new ProgramState();
            computer.LoadProgram(lines);
            computer.ExecuteUntilBlocked();
            var answer = computer.GetRegisterValue("h");
            Output.Answer(answer);
        }

        private void SolvePart2ByReimplementation()
        {
            var b = 109300;
            var target = b + 17000;
            var count = 0;
            while (b <= target)
            {
                var isComposite = false;
                var d = 2;
                while (d * d <= b)
                {
                    if (b % d == 0)
                        isComposite = true;
                    d++;
                }
                if (isComposite) count++;
                b += 17;
            }

            Output.Answer(count);
        }
    }
}