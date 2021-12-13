namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day13 : BasePuzzle
    {
        private List<(string foldDir, int foldLoc)> _folds;

        private HashSet<(int x, int y)> _dots;

        public Day13(bool practice)
            : base(13, practice)
        {
            _dots = _puzzleInput.Where(x => x.Contains(",")).Select(x =>
            {
                var splitLine = x.Split(',');
                return (int.Parse(splitLine[0]), int.Parse(splitLine[1]));
            }).ToHashSet();
            _folds = _puzzleInput.Where(x => x.Contains("x") || x.Contains("y")).Select(x =>
            {
                var splitLine = x.Split('=');
                return (splitLine.First().Split(' ').Last(), int.Parse(splitLine.Last()));
            }).ToList();
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2();

        public int Part1()
        {
            var tempDots = _dots.ToArray();
            int xFold = _folds.First(x => x.foldDir == "x").foldLoc;
            for (int i = 0; i < tempDots.Length; i++)
            {
                if (tempDots[i].x > xFold)
                {
                    tempDots[i].x = xFold - Math.Abs(tempDots[i].x - xFold);
                }
            }
            _dots = tempDots.ToHashSet();
            return _dots.Count();
        }

        public string Part2()
        {
            _folds.ForEach(fold =>
            {
                if (fold.foldDir == "x")
                {
                    var tempDots = _dots.ToArray();
                    for (int i = 0; i < tempDots.Length; i++)
                    {
                        if (tempDots[i].x > fold.foldLoc)
                        {
                            tempDots[i].x = fold.foldLoc - Math.Abs(tempDots[i].x - fold.foldLoc);
                        }
                    }
                    _dots = tempDots.ToHashSet();
                }
                else
                {
                    var tempDots = _dots.ToArray();
                    for (int i = 0; i < tempDots.Length; i++)
                    {
                        if (tempDots[i].y > fold.foldLoc)
                        {
                            tempDots[i].y = fold.foldLoc - Math.Abs(tempDots[i].y - fold.foldLoc);
                        }
                    }
                    _dots = tempDots.ToHashSet();
                }
            });

            // print the result to get the code
            int maxX = _dots.Max(x => x.x) + 1;
            int maxY = _dots.Max(x => x.y) + 1;

            var grid = new char[maxX][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = Enumerable.Repeat('.', maxY).ToArray();
            }


            for (int j = 0; j < grid[0].Length; j++)
            {
                var lineText = string.Empty;
                for (int i = 0; i < grid.Length; i++)
                {
                    if (_dots.Contains((i, j)))
                    {
                        grid[i][j] = '#';
                    }
                    lineText += grid[i][j];
                }
                Console.WriteLine(lineText);
            }
            return "answer printed in console";
        }
    }
}
