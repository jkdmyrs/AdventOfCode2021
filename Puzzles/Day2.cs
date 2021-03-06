namespace AOC2021.Puzzles
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day2 : BasePuzzle
    {
        private readonly List<(string direction, int amount)> PuzzleInput;
        public Day2(bool practice)
            : base(2, practice)
        {
            PuzzleInput = _puzzleInput.Select(x =>
            {
                string[] parts = x.Split();
                return (parts[0], int.Parse(parts[1]));
            }).ToList();
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        private int Part1()
        {
            (int x, int y) position = (0, 0);
            PuzzleInput.ForEach(x =>
            {
                switch (x.direction)
                {
                    case "forward":
                        position.x += x.amount;
                        break;
                    case "down":
                        position.y += x.amount;
                        break;
                    case "up":
                        position.y -= x.amount;
                        break;
                }
            });
            return position.x * position.y;
        }

        private int Part2()
        {
            (int x, int y) position = (0, 0);
            int aim = 0;
            PuzzleInput.ForEach(x =>
            {
                switch (x.direction)
                {
                    case "forward":
                        position.x += x.amount;
                        position.y += (aim * x.amount);
                        break;
                    case "down":
                        aim += x.amount;
                        break;
                    case "up":
                        aim -= x.amount;
                        break;
                }
            });
            return position.x * position.y;
        }
    }
}
