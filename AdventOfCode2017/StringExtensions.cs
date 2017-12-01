using System.Collections.Generic;

namespace AdventOfCode2016
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitIntoLines(this string inputText)
        {
            if (inputText == null)
                return new string[] {};

            return inputText
                .Replace("\r\n", "\n")
                .Replace("\r", "")
                .Trim()
                .Split('\n');
        }
    }
}
