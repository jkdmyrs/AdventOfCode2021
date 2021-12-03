using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2021
{
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

        public override int Part1Answer => Part1();

        public override int Part2Answer => Part2();

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
