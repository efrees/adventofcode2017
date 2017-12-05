using System;
using System.IO;
using AdventOfCode2017.Solvers;

namespace AdventOfCode2017
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileText = GetInputFromFile("day5input.txt");
            Day5Solver.Create().Solve(fileText);

            Console.ReadKey();
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("../../InputFiles/" + filename);
        }
    }
}
