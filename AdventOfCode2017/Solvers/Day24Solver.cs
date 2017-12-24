using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Solvers
{
    internal class Day24Solver : IProblemSolver
    {
        public static IProblemSolver Create() => new Day24Solver();

        public void Solve(string fileText)
        {
            SolvePart1(fileText);
            SolvePart2(fileText);
        }

        private void SolvePart1(string fileText)
        {
            var components = fileText.SplitIntoLines()
                .Select(ParseComponent)
                .SelectMany(x => new[] { new { End = x.Ends[0], Component = x }, new { End = x.Ends[1], Component = x } })
                .ToLookup(x => x.End)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Component).ToList());


            var answer = FindHighestScore(components);
            Output.Answer(answer);
        }

        private int FindHighestScore(Dictionary<int, List<BridgeComponent>> components)
        {
            var usedSet = new HashSet<BridgeComponent>();
            return FindHighestScoreRecursive(0, components, usedSet);
        }

        private int FindHighestScoreRecursive(int currentSize, Dictionary<int, List<BridgeComponent>> components, HashSet<BridgeComponent> usedSet)
        {
            if (!components.ContainsKey(currentSize))
                return 0;

            var options = components[currentSize];
            var maxScore = 0;
            foreach (var comp in options)
            {
                if (usedSet.Contains(comp))
                    continue;

                usedSet.Add(comp);
                var otherEnd = comp.Ends[0] == currentSize ? comp.Ends[1] : comp.Ends[0];
                var thisComponentScore = comp.Ends.Sum();
                var score = thisComponentScore + FindHighestScoreRecursive(otherEnd, components, usedSet);

                if (score > maxScore)
                {
                    maxScore = score;
                }

                usedSet.Remove(comp);
            }

            return maxScore;
        }

        private void SolvePart2(string fileText)
        {
            var components = fileText.SplitIntoLines()
                .Select(ParseComponent)
                .SelectMany(x => new[] { new { End = x.Ends[0], Component = x }, new { End = x.Ends[1], Component = x } })
                .ToLookup(x => x.End)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Component).ToList());

            var answer = FindLongestBridge(components);
            Output.Answer(answer);
        }

        private int FindLongestBridge(Dictionary<int, List<BridgeComponent>> components)
        {
            var usedSet = new HashSet<BridgeComponent>();
            return FindLongestBridgeRecursive(0, components, usedSet).Strength;
        }

        private LongestBridgeResult FindLongestBridgeRecursive(int currentSize, Dictionary<int, List<BridgeComponent>> components, HashSet<BridgeComponent> usedSet)
        {
            if (!components.ContainsKey(currentSize))
                return new LongestBridgeResult();

            var options = components[currentSize];
            var maxScore = new LongestBridgeResult();
            foreach (var comp in options)
            {
                if (usedSet.Contains(comp))
                    continue;

                usedSet.Add(comp);
                var otherEnd = comp.Ends[0] == currentSize ? comp.Ends[1] : comp.Ends[0];
                var thisComponentScore = comp.Ends.Sum();
                var nestedBest = FindLongestBridgeRecursive(otherEnd, components, usedSet);
                nestedBest.Strength += thisComponentScore;
                nestedBest.Length += 1;

                if (nestedBest.Length > maxScore.Length
                    || (nestedBest.Length == maxScore.Length && nestedBest.Strength > maxScore.Strength))
                {
                    maxScore = nestedBest;
                }

                usedSet.Remove(comp);
            }

            return maxScore;
        }

        private class LongestBridgeResult
        {
            public int Length { get; set; }
            public int Strength { get; set; }
        }

        private BridgeComponent ParseComponent(string arg)
        {

            return new BridgeComponent
            {
                Ends = Array.ConvertAll(arg.Split('/'), int.Parse)
            };
        }

        private class BridgeComponent
        {
            public int[] Ends { get; set; }
        }
    }
}