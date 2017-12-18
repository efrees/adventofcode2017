using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day18Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day18Solver();

        public void Solve(string fileText)
        {
            var instructions = fileText.SplitIntoLines()
                .Select(Instruction.Parse);
            var computer = new Computer();
            computer.LoadProgram(instructions);
            var answer = computer.ExecuteUntilRecover();
            Output.Answer(answer);
        }
    }

    internal class Computer
    {
        private Instruction[] _instructions;
        private Dictionary<string, long> _registers = new Dictionary<string, long>();
        private long _currentInstruction = 0;
        private bool _justJumped;
        private long _lastFrequencyPlayed;
        private bool _recoveredValue;

        public void LoadProgram(IEnumerable<Instruction> instructions)
        {
            _instructions = instructions.ToArray();
        }

        public long ExecuteUntilRecover()
        {
            while (_currentInstruction >= 0 && _currentInstruction < _instructions.Length)
            {
                _justJumped = false;
                var instruction = _instructions[_currentInstruction];
                instruction.Execute(this);

                if (_recoveredValue)
                {
                    return _lastFrequencyPlayed;
                }

                if (!_justJumped)
                {
                    _currentInstruction++;
                }
            }
            return -1;
        }

        public void RelativeJump(long jumpAmount)
        {
            _currentInstruction += jumpAmount;
            _justJumped = true;
        }

        public void PlaySound(long frequency)
        {
            _lastFrequencyPlayed = frequency;
        }

        public void RaiseValueRecovered()
        {
            _recoveredValue = true;
        }

        public long GetRegisterValue(string register)
        {
            return _registers.ContainsKey(register)
                ? _registers[register]
                : 0;
        }

        public void SetRegisterValue(string register, long value)
        {
            _registers[register] = value;
        }
    }

    internal class Instruction
    {
        private readonly string _opCode;
        private string _op1;
        private string _op2;

        private Instruction(string[] instructionTokens)
        {
            _opCode = instructionTokens[0];
            _op1 = instructionTokens[1];

            if (instructionTokens.Length > 2)
                _op2 = instructionTokens[2];
        }

        public static Instruction Parse(string arg)
        {
            var tokens = arg.Split(' ');

            return new Instruction(tokens);
        }

        public void Execute(Computer computer)
        {
            switch (_opCode)
            {
                case "snd":
                    computer.PlaySound(GetOperandValue(_op1, computer));
                    break;
                case "set":
                    computer.SetRegisterValue(_op1, GetOperandValue(_op2, computer));
                    break;
                case "add":
                    var sum = GetOperandValue(_op1, computer) + GetOperandValue(_op2, computer);
                    computer.SetRegisterValue(_op1, sum);
                    break;
                case "mul":
                    var prod = (long)GetOperandValue(_op1, computer) * GetOperandValue(_op2, computer);
                    computer.SetRegisterValue(_op1, prod);
                    break;
                case "mod":
                    var mod = GetOperandValue(_op1, computer) % GetOperandValue(_op2, computer);
                    computer.SetRegisterValue(_op1, mod);
                    break;
                case "rcv":
                    if (GetOperandValue(_op1, computer) != 0)
                        computer.RaiseValueRecovered();
                    break;
                case "jgz":
                    if (GetOperandValue(_op1, computer) > 0)
                        computer.RelativeJump(GetOperandValue(_op2, computer));
                    break;
            }
        }

        private long GetOperandValue(string operand, Computer computer)
        {
            return char.IsLetter(operand[0])
                ? computer.GetRegisterValue(operand)
                : long.Parse(operand);
        }
    }
}