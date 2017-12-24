using System;
using System.Collections.Generic;
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
            var components = ParseComponentsFromInput(fileText);
            var answer = FindHighestScore(components);
            Output.Answer(answer);
        }

        private void SolvePart2(string fileText)
        {
            var components = ParseComponentsFromInput(fileText);
            var answer = FindLongestBridge(components);
            Output.Answer(answer);
        }

        private Dictionary<int, List<BridgeComponent>> ParseComponentsFromInput(string fileText)
        {
            return fileText.SplitIntoLines()
                .Select(ParseComponent)
                .SelectMany(x => new[]
                {
                    new { Port = x.Ports[0], Component = x },
                    new { Port = x.Ports[1], Component = x }
                })
                .ToLookup(x => x.Port)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Component).ToList());
        }

        private int FindHighestScore(Dictionary<int, List<BridgeComponent>> components)
        {
            var usedSet = new HashSet<BridgeComponent>();
            return FindHighestScoreRecursive(0, components, usedSet);
        }

        private int FindHighestScoreRecursive(int portTypeToConnect, Dictionary<int, List<BridgeComponent>> components, HashSet<BridgeComponent> usedSet)
        {
            var maxScore = 0;
            foreach (var comp in components[portTypeToConnect].Except(usedSet))
            {
                if (usedSet.Contains(comp))
                    continue;

                usedSet.Add(comp);
                var otherEnd = comp.Ports[0] == portTypeToConnect ? comp.Ports[1] : comp.Ports[0];
                var thisComponentScore = comp.Ports.Sum();
                var score = thisComponentScore + FindHighestScoreRecursive(otherEnd, components, usedSet);

                if (score > maxScore)
                {
                    maxScore = score;
                }

                usedSet.Remove(comp);
            }

            return maxScore;
        }

        private int FindLongestBridge(Dictionary<int, List<BridgeComponent>> components)
        {
            var usedSet = new HashSet<BridgeComponent>();
            return FindLongestBridgeRecursive(0, components, usedSet).Strength;
        }

        private LongestBridgeResult FindLongestBridgeRecursive(int portTypeToConnect, Dictionary<int, List<BridgeComponent>> components, HashSet<BridgeComponent> usedSet)
        {
            var maxScore = new LongestBridgeResult();
            foreach (var comp in components[portTypeToConnect].Except(usedSet))
            {
                usedSet.Add(comp);
                var otherEnd = comp.Ports[0] == portTypeToConnect ? comp.Ports[1] : comp.Ports[0];
                var thisComponentScore = comp.Ports.Sum();
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

        private BridgeComponent ParseComponent(string arg)
        {
            return new BridgeComponent
            {
                Ports = Array.ConvertAll(arg.Split('/'), int.Parse)
            };
        }

        private class LongestBridgeResult
        {
            public int Length { get; set; }
            public int Strength { get; set; }
        }

        private class BridgeComponent
        {
            public int[] Ports { get; set; }
        }
    }
}