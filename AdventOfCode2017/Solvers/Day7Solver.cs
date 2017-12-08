using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Solvers
{
    internal class Day7Solver : IProblemSolver
    {
        public static IProblemSolver Create()
        {
            return new Day7Solver();
        }

        private Dictionary<string, Node> nodesByName = new Dictionary<string, Node>();
        private HashSet<string> children = new HashSet<string>();

        public void Solve(string fileText)
        {
            var lines = fileText.SplitIntoLines();
            var pattern = "(\\w+) \\((\\d+)\\)(?: -> (.*))?";

            foreach (var line in lines)
            {
                var match = Regex.Match(line, pattern);

                var node = new Node
                {
                    Name = match.Groups[1].Value,
                    Weight = int.Parse(match.Groups[2].Value)
                };

                if (match.Groups.Count > 3)
                {
                    node.ChildrenNames = match.Groups[3].Value
                        .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var childName in node.ChildrenNames)
                    {
                        children.Add(childName);
                    }

                    nodesByName.Add(node.Name, node);
                }
            }

            foreach (var node in nodesByName.Values)
            {
                if (node.ChildrenNames.Any())
                {
                    node.Children = node.ChildrenNames.Select(name => nodesByName[name]);
                }
            }

            var topNode = nodesByName.Values.Single(n => !children.Contains(n.Name));
            Console.WriteLine($"P1: {topNode.Name}");

            Node parentNode = null;
            var potentialWrongNode = topNode;
            while (potentialWrongNode.GetDifferentChild() != null)
            {
                parentNode = potentialWrongNode;
                potentialWrongNode = potentialWrongNode.GetDifferentChild();
            }

            var expected = parentNode.GetExpectedChildWeight();
            var difference = expected - potentialWrongNode.GetTotalWeight();

            Console.WriteLine($"P2: {potentialWrongNode.Weight + difference}");
        }

        private class Node
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public IEnumerable<string> ChildrenNames { get; set; }
            public IEnumerable<Node> Children { get; set; }

            public Node GetDifferentChild()
            {
                if (Children == null || !Children.Any())
                {
                    return null;
                }

                var groups = Children.GroupBy(n => n.GetTotalWeight());
                var groupWithUniqueChild = groups.FirstOrDefault(g => g.Count() == 1);

                return groupWithUniqueChild?.First();
            }

            private int _storedTotalWeight = -1;

            public int GetTotalWeight()
            {
                if (_storedTotalWeight < 0)
                    _storedTotalWeight = Weight + (Children?.Select(c => c.GetTotalWeight()).Sum() ?? 0);

                return _storedTotalWeight;
            }

            public int GetExpectedChildWeight()
            {
                var groups = Children.GroupBy(n => n.GetTotalWeight());
                var representative = groups.Single(g => g.Count() > 1).First();
                return representative.GetTotalWeight();
            }
        }
    }
}