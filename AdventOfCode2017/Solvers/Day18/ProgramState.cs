using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solvers.Day18
{
    internal class ProgramState
    {
        private readonly MessageStream _inputStream;
        private readonly MessageStream _outputStream;
        private Instruction[] _instructions;
        private readonly Dictionary<string, long> _registers = new Dictionary<string, long>();
        private long _currentInstruction;
        private bool _justJumped;
        public int MultiplicationCount;

        public ExecutionStatus ExecutionStatus { get; private set; } = ExecutionStatus.Running;

        public ProgramState()
        {
        }
        public ProgramState(MessageStream inputStream, MessageStream outputStream)
        {
            _inputStream = inputStream;
            _outputStream = outputStream;
        }

        public void LoadProgram(IEnumerable<Instruction> instructions)
        {
            _instructions = instructions.ToArray();
        }

        public void ExecuteUntilBlocked()
        {
            if (ExecutionStatus == ExecutionStatus.Blocked)
                ExecutionStatus = ExecutionStatus.Running;

            while (ExecutionStatus == ExecutionStatus.Running)
            {
                _justJumped = false;
                var instruction = _instructions[_currentInstruction];
                instruction.Execute(this);

                if (ExecutionStatus != ExecutionStatus.Blocked && !_justJumped)
                {
                    _currentInstruction++;
                }

                if (_currentInstruction < 0 || _currentInstruction >= _instructions.Length)
                {
                    ExecutionStatus = ExecutionStatus.Stopped;
                }
            }
        }

        public bool IsBlockedOrHalted()
        {
            return ExecutionStatus == ExecutionStatus.Stopped
                || (ExecutionStatus == ExecutionStatus.Blocked && !_inputStream.HasMessage());
        }

        public void RelativeJump(long jumpAmount)
        {
            _currentInstruction += jumpAmount;
            _justJumped = true;
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

        public void SendMessage(long value)
        {
            _outputStream.Send(value);
        }

        public void ReceiveMessageToRegister(string register)
        {
            if (_inputStream.HasMessage())
            {
                SetRegisterValue(register, _inputStream.Receive());
            }
            else
            {
                ExecutionStatus = ExecutionStatus.Blocked;
            }
        }

        public void LogMultiplication()
        {
            MultiplicationCount++;
        }
    }
}