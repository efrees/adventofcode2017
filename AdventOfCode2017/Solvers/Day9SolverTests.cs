using NUnit.Framework;

namespace AdventOfCode2017.Solvers
{
    internal class Day9SolverTests
    {
        [TestCase("{}", 1)]
        [TestCase("{{{}}}", 6)]
        [TestCase("{{},{}}", 5)]
        [TestCase("{{{},{},{{}}}}", 16)]
        [TestCase("{<a>,<a>,<a>,<a>}", 1)]
        [TestCase("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
        [TestCase("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
        [TestCase("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
        public void solve_examples_correctly(string input, int expectedScore)
        {
            var solver = new Day9Solver();

            var answer = solver.ScoreGroups(input);

            Assert.AreEqual(expectedScore, answer);
        }

        [TestCase("<>", 0)]
        public void count_garbage_correctly(string input, int expectedCount)
        {
            var solver = new Day9Solver();

            var actualCount = solver.CountUncancelledGarbage(input);

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
