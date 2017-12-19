namespace AdventOfCode2017.Solvers.Day18
{
    internal class Part1Instruction
    {
        private readonly string _opCode;
        private readonly string _op1;
        private readonly string _op2;

        private Part1Instruction(string[] instructionTokens)
        {
            _opCode = instructionTokens[0];
            _op1 = instructionTokens[1];

            if (instructionTokens.Length > 2)
                _op2 = instructionTokens[2];
        }

        public static Part1Instruction Parse(string arg)
        {
            var tokens = arg.Split(' ');

            return new Part1Instruction(tokens);
        }

        public void Execute(Part1ProgramState programState)
        {
            switch (_opCode)
            {
                case "snd":
                    programState.PlaySound(GetOperandValue(_op1, programState));
                    break;
                case "set":
                    programState.SetRegisterValue(_op1, GetOperandValue(_op2, programState));
                    break;
                case "add":
                    var sum = GetOperandValue(_op1, programState) + GetOperandValue(_op2, programState);
                    programState.SetRegisterValue(_op1, sum);
                    break;
                case "mul":
                    var prod = (long)GetOperandValue(_op1, programState) * GetOperandValue(_op2, programState);
                    programState.SetRegisterValue(_op1, prod);
                    break;
                case "mod":
                    var mod = GetOperandValue(_op1, programState) % GetOperandValue(_op2, programState);
                    programState.SetRegisterValue(_op1, mod);
                    break;
                case "rcv":
                    if (GetOperandValue(_op1, programState) != 0)
                        programState.RaiseValueRecovered();
                    break;
                case "jgz":
                    if (GetOperandValue(_op1, programState) > 0)
                        programState.RelativeJump(GetOperandValue(_op2, programState));
                    break;
            }
        }

        private long GetOperandValue(string operand, Part1ProgramState programState)
        {
            return char.IsLetter(operand[0])
                ? programState.GetRegisterValue(operand)
                : long.Parse(operand);
        }
    }
}