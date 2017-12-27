using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AdventOfCode2017.Solvers;

namespace AdventOfCode2017
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var fileText = Input.GetInputFromFile("day25input.txt");
            TimeAction(() => Day25Solver.Create().Solve(fileText));

            Console.ReadKey();
        }

        private static void TimeAction(Action action)
        {
            var stopwatch = new Stopwatch();
            var times = new List<double>();
            for (var i = 0; i < 10; i++)
            {
                stopwatch.Restart();
                action();
                stopwatch.Stop();
                times.Add(stopwatch.Elapsed.TotalMilliseconds);
            }
            Console.WriteLine($@"
Hi: {times.Max():N3}ms
Lo: {times.Min():N3}ms
Av: {times.Average():N3}ms");
        }
    }
}
