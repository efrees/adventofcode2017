using System;
using System.Linq;
using System.Text;

namespace AdventOfCode2017.Solvers
{
    internal class Day14Solver : IProblemSolver
    {
        private readonly string _input;

        private Day14Solver(string input)
        {
            _input = input;
        }

        public static IProblemSolver Create() => new Day14Solver("ffayrhll");

        public void Solve(string fileText)
        {
            SolvePart1();
            SolvePart2();
        }

        internal int SolvePart1()
        {
            var knotHasher = new Day10Solver();
            var bitCount = Enumerable.Range(0, 128)
                .Select(i => $"{_input}-{i}")
                .Select(s => knotHasher.KnotHash(s))
                .Select(hex => BitCountFromHex(hex))
                .Sum();

            Output.Answer(bitCount);
            return bitCount;
        }

        private int BitCountFromHex(string hex)
        {
            var count = 0;
            foreach (var c in hex)
            {
                if (char.IsDigit(c))
                {
                    count += BitsInInt(c - '0');
                }
                else
                {
                    count += BitsInInt(char.ToLower(c) - 'a' + 10);
                }
            }
            return count;
        }

        private int BitsInInt(int i)
        {
            var count = 0;
            while (i > 0)
            {
                i &= i - 1;
                count++;
            }
            return count;
        }

        internal void SolvePart2()
        {
            var knotHasher = new Day10Solver();
            var bitField = Enumerable.Range(0, 128)
                .Select(i => $"{_input}-{i}")
                .Select(s => knotHasher.KnotHash(s))
                .Select(hex => BitStringFromHex(hex).ToCharArray())
                .ToArray();

            var groupCount = 0;
            for (int i = 0; i < bitField.Length; i++)
            {
                for (var j = 0; j < bitField[0].Length; j++)
                {
                    if (bitField[i][j] == '1')
                    {
                        groupCount++;
                        ZeroOutGroup(bitField, i, j);
                    }
                }
            }

            Output.Answer(groupCount);
        }

        private string BitStringFromHex(string hex)
        {
            var sb = new StringBuilder();
            foreach (var c in hex)
            {
                var i = 0;
                if (char.IsDigit(c))
                {
                    i = c - '0';
                }
                else
                {
                    i = char.ToLower(c) - 'a' + 10;
                }
                var padLeft = Convert.ToString(i, 2).PadLeft(4, '0');
                sb.Append(padLeft.Substring(padLeft.Length - 4));
            }
            return sb.ToString();
        }

        private void ZeroOutGroup(char[][] bitField, int i, int i1)
        {
            if (i < 0 || i >= bitField.Length || i1 < 0 || i1 >= bitField[i].Length)
                return;

            if (bitField[i][i1] == '0') return;

            bitField[i][i1] = '0';

            ZeroOutGroup(bitField, i+1, i1);
            ZeroOutGroup(bitField, i-1, i1);
            ZeroOutGroup(bitField, i, i1+1);
            ZeroOutGroup(bitField, i, i1-1);
        }
    }
}