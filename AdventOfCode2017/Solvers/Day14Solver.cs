using System;
using System.Collections;
using System.Linq;

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

        private void SolvePart1()
        {
            var knotHasher = new Day10Solver();
            var bitCount = Enumerable.Range(0, 128)
                .Select(i => knotHasher.KnotHash($"{_input}-{i}"))
                .Select(BitCountFromHex)
                .Sum();

            Output.Answer(bitCount);
        }

        private int BitCountFromHex(string hex)
        {
            return hex.Sum(c => BitsInInt(HexDigitToInt(c)));
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

        private static int HexDigitToInt(char c)
        {
            if (char.IsDigit(c))
            {
                return c - '0';
            }
            return char.ToLower(c) - 'a' + 10;
        }

        internal void SolvePart2()
        {
            var knotHasher = new Day10Solver();
            var bitField = Enumerable.Range(0, 128)
                .Select(i => knotHasher.KnotHash($"{_input}-{i}"))
                .Select(BitArrayFromHex)
                .ToArray();

            var groupCount = 0;
            for (int i = 0; i < bitField.Length; i++)
            {
                for (var j = 0; j < bitField[0].Length; j++)
                {
                    if (bitField[i][j])
                    {
                        groupCount++;
                        ZeroOutGroup(bitField, i, j);
                    }
                }
            }

            Output.Answer(groupCount);
        }

        private BitArray BitArrayFromHex(string hex)
        {
            var bytes = Enumerable.Range(0, hex.Length / 2)
                .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
                .ToArray();
            return new BitArray(bytes);
        }

        private void ZeroOutGroup(BitArray[] bitField, int row, int col)
        {
            if (row < 0 || row >= bitField.Length || col < 0 || col >= bitField[row].Length)
                return;

            if (!bitField[row][col]) return;

            bitField[row][col] = false;

            ZeroOutGroup(bitField, row + 1, col);
            ZeroOutGroup(bitField, row - 1, col);
            ZeroOutGroup(bitField, row, col + 1);
            ZeroOutGroup(bitField, row, col - 1);
        }
    }
}