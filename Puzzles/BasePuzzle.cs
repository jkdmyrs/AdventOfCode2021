namespace AOC2021.Puzzles
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BasePuzzle
    {
        internal readonly List<string> _puzzleInput;

        public BasePuzzle(int day, bool practice, bool skip = false)
        {
            _puzzleInput = System.IO.File.ReadAllLines(@$"\\Mac\Home\projects\aoc2021\Input\{day.ToString() + (practice ? "_practice" : string.Empty)}.txt").ToList();
            Skip = skip;
        }

        public bool Skip { get; private set; }

        public abstract string Part1Answer { get; }
        public abstract string Part2Answer { get; }
    }
}
