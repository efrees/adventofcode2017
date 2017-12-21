using NUnit.Framework;

namespace AdventOfCode2017.Solvers
{
    internal class Day21SolverTests
    {
        [Test]
        public void EnhancementRule_should_match_flip()
        {
            var targetPattern = Day21Solver.EnhancementRule.ParsePart(".#./..#/###");
            var matchingRule = Day21Solver.EnhancementRule.Parse("###/..#/.#. => .");

            Assert.IsTrue(matchingRule.AnyPatternMatches(targetPattern));
        }

        [Test]
        public void EnhancementRule_should_match_rotation()
        {
            var targetPattern = Day21Solver.EnhancementRule.ParsePart(".#./..#/###");
            var matchingRule = Day21Solver.EnhancementRule.Parse("#../#.#/##. => .");

            Assert.IsTrue(matchingRule.AnyPatternMatches(targetPattern));
        }

        [Test]
        public void EnhancementRule_should_match_rotated_flip()
        {
            var targetPattern = Day21Solver.EnhancementRule.ParsePart(".#./..#/###");
            var matchingRule = Day21Solver.EnhancementRule.Parse("..#/#.#/.## => .");

            Assert.IsTrue(matchingRule.AnyPatternMatches(targetPattern));
        }
    }
}
