namespace AOC2021
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BasePuzzle
    {
        internal static List<string> _puzzleInput;

        public BasePuzzle(int day, bool practice)
        {
            _puzzleInput = System.IO.File.ReadAllLines(@$"\\Mac\Home\projects\aoc2021\Input\{day.ToString() + (practice ? "_practice" : string.Empty)}.txt").ToList();
        }

        public abstract int Part1Answer { get; }
        public abstract int Part2Answer { get; }
    }
}
