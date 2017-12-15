using System;

namespace AdventOfCode2017
{
    public static class IntExtensions
    {
        public static void RepetitionsOf(this int repetitions, Action action)
        {
            for (var i = 0; i < repetitions; i++)
            {
                action();
            }
        }
    }
}
