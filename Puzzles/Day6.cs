namespace AOC2021.Puzzles
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day6 : BasePuzzle
    {
        private readonly List<int> _fish;

        public Day6(bool practice)
            : base(6, practice)
        {
            _fish = _puzzleInput.First().Split(",").Select(int.Parse).ToList();
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        private long RunSimulation(int numDays)
        {
            long[] counts = new long[9];
            for (int i = 0; i < counts.Length; i++)
            {
                foreach (var fish in _fish)
                {
                    if (i == fish)
                    {
                        counts[i]++;
                    }
                }
            }

            for (int i = 0; i < numDays; i++)
            {
                long numNewFish = counts[0];
                for (int j = 0; j < counts.Length - 1; j++)
                {
                    counts[j] = counts[j + 1];
                }
                counts[6] += numNewFish;
                counts[8] = numNewFish;
            }
            return counts.Sum();
        }

        public long Part1()
        {
            return RunSimulation(80);
        }

        public long Part2()
        {
            return RunSimulation(256);
        }
    }
}
