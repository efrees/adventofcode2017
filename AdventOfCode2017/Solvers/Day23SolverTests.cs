using NUnit.Framework;

namespace AdventOfCode2017.Solvers
{
    internal class Day23SolverTests
    {
        [Test]
        public void solve_examples()
        {
            var input = @"";
            var expected = 0;

            var actual = new Day23Solver().SolvePart1(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
