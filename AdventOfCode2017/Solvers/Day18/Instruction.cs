namespace AdventOfCode2017.Solvers.Day18
{
    internal class Instruction
    {
        private readonly string _opCode;
        private readonly string _op1;
        private readonly string _op2;

        protected Instruction(string[] instructionTokens)
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

        public void Execute(ProgramState computer)
        {
            switch (_opCode)
            {
                case "snd":
                    computer.SendMessage(GetOperandValue(_op1, computer));
                    break;
                case "set":
                    computer.SetRegisterValue(_op1, GetOperandValue(_op2, computer));
                    break;
                case "add":
                    var sum = GetOperandValue(_op1, computer) + GetOperandValue(_op2, computer);
                    computer.SetRegisterValue(_op1, sum);
                    break;
                case "sub":
                    var diff = GetOperandValue(_op1, computer) - GetOperandValue(_op2, computer);
                    computer.SetRegisterValue(_op1, diff);
                    break;
                case "mul":
                    var prod = GetOperandValue(_op1, computer) * GetOperandValue(_op2, computer);
                    computer.SetRegisterValue(_op1, prod);
                    computer.LogMultiplication();
                    break;
                case "mod":
                    var mod = GetOperandValue(_op1, computer) % GetOperandValue(_op2, computer);
                    computer.SetRegisterValue(_op1, mod);
                    break;
                case "rcv":
                    computer.ReceiveMessageToRegister(_op1);
                    break;
                case "jgz":
                    if (GetOperandValue(_op1, computer) > 0)
                        computer.RelativeJump(GetOperandValue(_op2, computer));
                    break;
                case "jnz":
                    if (GetOperandValue(_op1, computer) != 0)
                        computer.RelativeJump(GetOperandValue(_op2, computer));
                    break;
            }
        }

        private long GetOperandValue(string operand, ProgramState computer)
        {
            return char.IsLetter(operand[0])
                ? computer.GetRegisterValue(operand)
                : long.Parse(operand);
        }
    }
}