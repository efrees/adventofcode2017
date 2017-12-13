using NUnit.Framework;

namespace AdventOfCode2017.Solvers
{
    internal class Day11SolverTests
    {
        [TestCase("ne,ne,ne", 3)]
        [TestCase("ne,ne,sw,sw", 0)]
        [TestCase("ne,ne,s,s", 2)]
        [TestCase("se,sw,se,sw,sw", 3)]
        [TestCase("nw,sw,sw,nw", 4)]
        [TestCase("nw,ne,nw,ne,s,s", 0)]
        public void solve_examples_correctly(string input, int expected)
        {
            var actual = new Day11Solver().SolvePart1(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
