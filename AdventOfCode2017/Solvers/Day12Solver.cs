using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Solvers
{
    internal class Day12Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day12Solver();
        }

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var lines = fileText.SplitIntoLines();
            //max in my input was 1999
            var graph = new bool[2000, 2000];
            ParseGraphFromLines(lines, graph);

            var count = CountChildren(0, graph);

            Console.WriteLine($"P1: {count}");
        }

        private void SolvePart2(string fileText)
        {
            var lines = fileText.SplitIntoLines();
            //max in my input was 1999
            var graph = new bool[2000, 2000];
            var groups = new int[2000];
            ParseGraphFromLines(lines, graph);

            var groupCount = 1;
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                var notConnected = !Enumerable.Range(0, graph.GetLength(1)).Any(j => graph[i, j]);
                var alreadyVisited = groups[i] > 0;
                if (notConnected || alreadyVisited)
                    continue;

                CountChildrenRecursively(i, graph, groups, groupCount);
                groupCount++;
            }
            Console.WriteLine($"P2: {groupCount - 1}");
        }

        private void ParseGraphFromLines(IEnumerable<string> lines, bool[,] graph)
        {
            var pattern = @"(?<parent>\d+) <-> (?<remainder>.*)";
            var parsed = lines.Select(l => Regex.Match(l, pattern))
                .Select(m => new
                {
                    Parent = int.Parse(m.Groups["parent"].Value),
                    Children = Array.ConvertAll(m.Groups["remainder"].Value.SplitRemovingEmpty(',', ' ').ToArray(),
                        int.Parse)
                });
            foreach (var row in parsed)
            {
                foreach (var child in row.Children)
                {
                    graph[row.Parent, child] = graph[child, row.Parent] = true;
                }
            }
        }

        private int CountChildren(int root, bool[,] graph)
        {
            var visited = new int[2000];
            return CountChildrenRecursively(root, graph, visited);
        }

        private int CountChildrenRecursively(int root, bool[,] graph, int[] visited, int groupLabel = 1)
        {
            visited[root] = groupLabel;
            var sum = 0;
            for (var i = 0; i < graph.GetLength(1); i++)
            {
                if (graph[root, i] && visited[i] <= 0)
                {
                    sum += CountChildrenRecursively(i, graph, visited);
                }
            }
            return sum + 1;
        }
    }
}