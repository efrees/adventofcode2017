using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using AdventOfCode2017.Solvers;

namespace AdventOfCode2017
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var fileText = GetInputFromFile("day16input.txt");
            Day16Solver.Create().Solve(fileText);

            Console.ReadKey();
        }

        private static string GetInputFromFile(string filename)
        {
            return File.ReadAllText("../../InputFiles/" + filename);
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

    internal class Day16Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day16Solver();
        }

        public void Solve(string fileText)
        {
            var moves = fileText.Split(',');
            var line = Enumerable.Range(0, 16).Select(i => (char)(i + 'a')).ToArray();
            var seenStates = new Dictionary<string, int>();
            var step = 0;
            var remainingSteps = 1000000000;//billion
            var currentStart = 0;
            while (remainingSteps > 0)
            {
                if (!seenStates.ContainsKey(new string(line)))
                {
                    seenStates[new string(line)] = step;
                }
                else
                {
                    //loop found
                    var loopSize = step - seenStates[new string(line)];
                    remainingSteps = remainingSteps % loopSize;
                }
                foreach (var move in moves)
                {
                    switch (move[0])
                    {
                        case 's':
                            var spin = int.Parse(move.Substring(1));
                            currentStart -= spin;
                            currentStart = InRange(currentStart);
                            break;
                        case 'p':
                            var ai = Array.IndexOf(line, move[1]);
                            var bi = Array.IndexOf(line, move[3]);
                            line[ai] = move[3];
                            line[bi] = move[1];
                            break;
                        case 'x':
                            var indexes = move.Substring(1).Split('/').Select(int.Parse).ToArray();
                            var t = line[InRange(currentStart + indexes[0])];
                            line[InRange(currentStart + indexes[0])] = line[InRange(currentStart + indexes[1])];
                            line[InRange(currentStart + indexes[1])] = t;
                            break;
                    }
                }
                step++;
                remainingSteps--;
            }
            var answer = string.Join("", line.Concat(line).Skip(InRange(currentStart)).Take(16));
            Output.Answer(answer);
        }

        private int InRange(int x)
        {
            while (x < 0) x += 16;
            return x % 16;
        }
    }
}
