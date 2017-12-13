using System;
using System.Windows.Forms;

namespace AdventOfCode2017.Solvers
{
    internal class Output
    {
        public static void Line(string info)
        {
            Console.WriteLine(info);
        }

        public static void Answer(object answer)
        {
            Console.WriteLine($"P1: {answer}");
            Clipboard.SetText(answer.ToString());
        }
    }
}