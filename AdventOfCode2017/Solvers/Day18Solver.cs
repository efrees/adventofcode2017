using System.Linq;
using AdventOfCode2017.Solvers.Day18;

namespace AdventOfCode2017.Solvers
{
    internal class Day18Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day18Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private static void SolvePart1(string fileText)
        {
            var instructions = fileText.SplitIntoLines()
                .Select(Part1Instruction.Parse);
            var computer = new Part1ProgramState();
            computer.LoadProgram(instructions);
            var answer = computer.ExecuteUntilRecover();
            Output.Answer(answer);
        }

        private static void SolvePart2(string fileText)
        {
            var instructions = fileText.SplitIntoLines()
                .Select(Instruction.Parse);

            var messageStreams = new[] { new MessageStream(), new MessageStream() };
            var programStates = new[] {
                new ProgramState(messageStreams[1], messageStreams[0]),
                new ProgramState(messageStreams[0], messageStreams[1])
            };

            programStates[0].LoadProgram(instructions);
            programStates[0].SetRegisterValue("p", 0);

            programStates[1].LoadProgram(instructions);
            programStates[1].SetRegisterValue("p", 1);

            var currentExecutingProgram = 0;
            while (true)
            {
                programStates[currentExecutingProgram].ExecuteUntilBlocked();

                if (CannotProgress(programStates))
                    break;

                currentExecutingProgram = (currentExecutingProgram + 1) % 2;
            }

            var answer = messageStreams[1].TotalMessageCount;
            Output.Answer(answer);
        }

        private static bool CannotProgress(ProgramState[] programStates)
        {
            return programStates.All(ps => ps.IsBlockedOrHalted());
        }
    }
}