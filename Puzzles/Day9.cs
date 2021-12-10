namespace AOC2021.Puzzles
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day9 : BasePuzzle
    {
        private int[][] _heightMap;
        private int _numRows;
        private int _numCols;

        public Day9(bool practice)
            : base(9, practice)
        {
            _numCols = _puzzleInput.First().Length;
            _numRows = _puzzleInput.Count;
            _heightMap = new int[_numRows][];
            for (int i = 0; i < _numRows; i++)
            {
                _heightMap[i] = _puzzleInput[i].ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            }
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        private List<(int height, (int row, int col) position)> AdjacentLocations(int row, int col)
        {
            List<(int height, (int row, int col) position)> adjacents = new();
            // left col
            if (col == 0)
            {
                // right (col + 1)
                adjacents.Add((_heightMap[row][col + 1], (row, col + 1)));
                // above (row - 1) if row != 0
                if (row != 0) adjacents.Add((_heightMap[row - 1][col], (row - 1, col)));
                // below (row + 1) if row != numRows - 1
                if (row != _numRows -1) adjacents.Add((_heightMap[row + 1][col], (row + 1, col)));
            }
            // right col
            else if (col == _numCols - 1)
            {
                // left  (col - 1)
                adjacents.Add((_heightMap[row][col - 1], (row, col - 1)));
                // above (row - 1) if row != 0
                if (row != 0) adjacents.Add((_heightMap[row - 1][col], (row - 1, col)));
                // below (row + 1) if row != numRows - 1
                if (row != _numRows - 1) adjacents.Add((_heightMap[row + 1][col], (row + 1, col)));
            }
            // anywhere else
            else
            {
                // left  (col - 1)
                adjacents.Add((_heightMap[row][col - 1], (row, col - 1)));
                // right (col + 1)
                adjacents.Add((_heightMap[row][col + 1], (row, col + 1)));
                // above (row - 1) if row != 0
                if (row != 0) adjacents.Add((_heightMap[row - 1][col], (row - 1, col)));
                // below (row + 1) if row != numRows - 1
                if (row != _numRows - 1) adjacents.Add((_heightMap[row + 1][col], (row + 1, col)));
            }
            return adjacents;
        }

        public int Part1()
        {
            List<(int row, int col)> _lowPoints = new();
            for(int i = 0; i < _numRows; i++)
            {
                for (int j = 0; j < _numCols; j++)
                {
                    if (AdjacentLocations(i, j).TrueForAll(x => x.height > _heightMap[i][j]))
                    {
                        _lowPoints.Add((i, j));
                    }
                }
            }
            int riskSum = 0;
            _lowPoints.ForEach(x =>
            {
                riskSum += (1 + _heightMap[x.row][x.col]);
            });
            return riskSum;
        }

        private HashSet<(int row, int col)> FindBasin((int row, int col) lowpoint, HashSet<(int row, int col)> basinLocations)
        {
            var adjacents = AdjacentLocations(lowpoint.row, lowpoint.col);
            adjacents.ForEach(x =>
            {
                int currentHeight = _heightMap[lowpoint.row][lowpoint.col];
                if (x.height < 9 && x.height > currentHeight)
                {
                    basinLocations.Add((x.position));
                    basinLocations = FindBasin(x.position, basinLocations);
                }
            });

            return basinLocations;
        }

        public int Part2()
        {
            List<(int row, int col)> _lowPoints = new();
            for (int i = 0; i < _numRows; i++)
            {
                for (int j = 0; j < _numCols; j++)
                {
                    if (AdjacentLocations(i, j).TrueForAll(x => x.height > _heightMap[i][j]))
                    {
                        _lowPoints.Add((i, j));
                    }
                }
            }

            List<int> basinSizes = new();
            _lowPoints.ForEach(x =>
            {
                var basinSize = FindBasin((x.row, x.col), new HashSet<(int row, int col)> { (x.row, x.col) }).Count;
                basinSizes.Add(basinSize);
            });

            var biggest3 = basinSizes.OrderByDescending(x => x).Take(3).ToList();
            return biggest3[0] * biggest3[1] * biggest3[2];
        }
    }
}
