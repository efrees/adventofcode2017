using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day8Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day8Solver();
        }

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var lines = fileText.SplitIntoLines();
            var registers = new Dictionary<string, int>();
            var maxEver = 0;
            foreach (var line in lines)
            {
                ExecuteInstruction(line, registers);
                var max = registers.Values.Any() ? registers.Values.Max() : 0;
                if (max > maxEver)
                    maxEver = max;
            }

            Console.WriteLine($"P1: {registers.Values.Max()}");
            Console.WriteLine($"P2: {maxEver}");
        }

        private void ExecuteInstruction(string line, Dictionary<string, int> registers)
        {
            var tokens = line.Split(' ');
            var register = tokens[0];
            var possibleNegation = tokens[1] == "inc" ? 1 : -1;
            var amount = int.Parse(tokens[2]);
            var conditionReg = tokens[4];
            var op = tokens[5];
            var conditionArg = tokens[6];

            if (ConditionMet(conditionReg, op, conditionArg, registers))
            {
                ChangeRegister(register, possibleNegation * amount, registers);
            }
        }

        private bool ConditionMet(string conditionReg, string op, string conditionArg, Dictionary<string, int> registers)
        {
            var left = registers.ContainsKey(conditionReg) ? registers[conditionReg] : 0;
            var right = int.Parse(conditionArg);

            switch (op)
            {
                case "<": return left < right;
                case "<=": return left <= right;
                case ">": return left > right;
                case ">=": return left >= right;
                case "!=": return left != right;
                case "==": return left == right;
                default: throw new ArgumentException();
            }
        }

        private void ChangeRegister(string register, int amount, Dictionary<string, int> registers)
        {
            var current = registers.ContainsKey(register) ? registers[register] : 0;
            registers[register] = current + amount;
        }
    }
}