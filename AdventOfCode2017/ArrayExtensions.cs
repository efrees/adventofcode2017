namespace AdventOfCode2017
{
    public static class ArrayExtensions
    {
        public static int CreateHash(this int[] ints)
        {
            var hash = ints.Length;
            foreach (var next in ints)
                hash = unchecked(hash * 314159 + next);
            return hash;
        }
    }
}
