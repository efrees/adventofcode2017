using System;
using System.IO;

namespace AdventOfCode2017
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileText = GetInputFromFile("day1input.txt");
            var result = Day1Solver.Create().GetSolution(fileText);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("../../InputFiles/" + filename);
        }
    }
}
