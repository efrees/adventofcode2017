using NUnit.Framework;

namespace AdventOfCode2017.Solvers
{
    internal class Day25SolverTests
    {
        [TestCase(@"input", 1)]
        public void solve_examples(string input, int expected)
        {
            var actual = new Day25Solver().DoLoop(input.SplitIntoLines());

            Assert.AreEqual(expected, actual);
        }
    }
}
