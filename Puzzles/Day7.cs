namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day7 : BasePuzzle
    {
        private readonly List<int> _positions;

        public Day7(bool practice)
            : base(7, practice)
        {
            _positions = _puzzleInput.First().Split(",").Select(int.Parse).ToList();
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        public int Part1()
        {
            int minPos = _positions.Min();
            int maxPos = _positions.Max();

            List<(int position, int fuelCost)> fuelCosts = new();
            for (int i = minPos; i <= maxPos; i++)
            {
                int cost = 0;
                _positions.ForEach(pos =>
                {
                    cost += Math.Abs(pos - i);
                });
                fuelCosts.Add((i, cost));
            }
            return fuelCosts.Min(x => x.fuelCost);
        }

        public int Part2()
        {
            int minPos = _positions.Min();
            int maxPos = _positions.Max();

            List<(int position, int fuelCost)> fuelCosts = new();
            for (int i = minPos; i <= maxPos; i++)
            {
                int cost = 0;
                _positions.ForEach(pos =>
                {
                    int moves = Math.Abs(pos - i);
                    cost += (moves * (moves + 1)) / 2;
                });
                fuelCosts.Add((i, cost));
            }
            return fuelCosts.Min(x => x.fuelCost);
        }
    }
}
