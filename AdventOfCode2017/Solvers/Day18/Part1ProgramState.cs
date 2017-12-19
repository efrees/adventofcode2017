using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers.Day18
{
    internal class Part1ProgramState
    {
        private Part1Instruction[] _instructions;
        private readonly Dictionary<string, long> _registers = new Dictionary<string, long>();
        private long _currentInstruction;
        private bool _justJumped;
        private long _lastFrequencyPlayed;
        private bool _recoveredValue;

        public void LoadProgram(IEnumerable<Part1Instruction> instructions)
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
}