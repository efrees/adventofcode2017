using System;
using System.IO;
using System.Threading;
using AdventOfCode2017.Solvers;

namespace AdventOfCode2017
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var fileText = GetInputFromFile("day13input.txt");
            Day13Solver.Create().Solve(fileText);

            Console.ReadKey();
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("../../InputFiles/" + filename);
        }
    }
}
