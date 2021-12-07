namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day5 : BasePuzzle
    {
        private readonly List<((int x1, int y1) start, (int x2, int y2) end)> _allLines;
        private readonly List<((int x1, int y1) start, (int x2, int y2) end)> _straightLines;

        public Day5(bool practice)
            : base(5, practice, true)
        {
            _allLines = _puzzleInput.Select(x =>
            {
                var splitInputRow = x.Split(" ");
                var startInput = splitInputRow.First().Split(",");
                var endInput = splitInputRow.Last().Split(",");
                return ((int.Parse(startInput.First()), int.Parse(startInput.Last())), (int.Parse(endInput.First()), int.Parse(endInput.Last())));
            }).ToList();
            _straightLines = _allLines.Where(x => x.start.x1 == x.end.x2 || x.start.y1 == x.end.y2).ToList();
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        private bool IsPointOnLine(((int x1, int y1) start, (int x2, int y2) end) line, (int x, int y) point)
        {
            if (line.end.x2 - line.start.x1 == 0)
            {
                return point.x == line.end.x2 && point.y >= Math.Min(line.start.y1, line.end.y2) && point.y <= Math.Max(line.start.y1, line.end.y2);
            }
            else
            {
                int m = (line.end.y2 - line.start.y1) / (line.end.x2 - line.start.x1);
                int b = line.end.y2 - (m * line.end.x2);
                return (m * point.x) + b == point.y
                    && point.x >= Math.Min(line.start.x1, line.end.x2) && point.x <= Math.Max(line.start.x1, line.end.x2)
                    && point.y >= Math.Min(line.start.y1, line.end.y2) && point.y <= Math.Max(line.start.y1, line.end.y2);
            }
        }

        private int FindAllPointsOnAllLines(List<((int x1, int y1) start, (int x2, int y2) end)> lines)
        {
            (int w, int h) gridSize = (Math.Max(lines.Max(x => x.start.x1), lines.Max(x => x.end.x2)), Math.Max(lines.Max(x => x.start.y1), lines.Max(x => x.end.y2)));
            int[,] grid = new int[gridSize.w + 1, gridSize.h + 1];

            for (int i = 0; i <= gridSize.w; i++)
            {
                for (int j = 0; j <= gridSize.h; j++)
                {
                    foreach (var line in lines)
                    {
                        if (IsPointOnLine(line, (i, j)))
                        {
                            grid[i, j]++;
                        }
                    }
                }
            }
            return grid.Cast<int>().ToList().Count(x => x >= 2);
        }


        public int Part1()
        {
            return FindAllPointsOnAllLines(_straightLines);
        }

        public int Part2()
        {
            return FindAllPointsOnAllLines(_allLines);
        }
    }
}
