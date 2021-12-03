using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    public class Day1 : BasePuzzle
    {
        private readonly List<int> PuzzleInput;

        public Day1(bool practice = false)
            : base(1, practice)
        {
            PuzzleInput = _puzzleInput.Select(int.Parse).ToList();
        }

        public override int Part1Answer => Part1();

        public override int Part2Answer => Part2();

        private int Part1()
        {
            int count = 0;
            int i = 0;

            PuzzleInput.ForEach(item =>
            {
                if (i > 0)
                {
                    if (item > PuzzleInput.ElementAt(i - 1)) count++;
                }
                i++;
            });

            return count;
        }

        private int Part2()
        {
            int count = 0;
            int i = 0;

            PuzzleInput.ForEach(item =>
            {
                if (i > 0 && i < PuzzleInput.Count - 2)
                {
                    int windowSum = item + PuzzleInput.ElementAt(i - 1) + PuzzleInput.ElementAt(i + 1);
                    int nextWindowSum = item + PuzzleInput.ElementAt(i + 1) + PuzzleInput.ElementAt(i + 2);
                    if (nextWindowSum > windowSum) count++;
                }
                i++;
            });

            return count;
        }
    }
}
