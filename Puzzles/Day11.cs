namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day11 : BasePuzzle
    {
        private (int energy, bool flashed)[][] _octopi;

        public Day11(bool practice)
            : base(11, practice)
        {
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        private void InitOctopi()
        {
            _octopi = new (int energy, bool flashed)[_puzzleInput.First().Length][];
            for (int i = 0; i < _octopi.Length; i++)
            {
                _octopi[i] = _puzzleInput[i].ToCharArray().Select(x => (int.Parse(x.ToString()), false)).ToArray();
            }
        }

        private void Add1ToAll()
        {
            for (int r = 0; r < _octopi.Length; r++)
            {
                for (int c = 0; c < _octopi[r].Length; c++)
                {
                    _octopi[r][c].energy++;
                }
            }
        }

        private void ResetFlashed()
        {
            for (int r = 0; r < _octopi.Length; r++)
            {
                for (int c = 0; c < _octopi[r].Length; c++)
                {
                    if (_octopi[r][c].flashed)
                        _octopi[r][c].energy = 0;
                    _octopi[r][c].flashed = false;
                }
            }
        }

        public List<(int energy, bool flashed)> Flatten()
        {
            List<(int energy, bool flashed)> list = new();
            for (int r = 0; r < _octopi.Length; r++)
            {
                list.AddRange(_octopi[r].ToList());
            }
            return list;
        }

        public void Add1ToAdjacent(int row, int col)
        {
            for (int i = row -1; i < row + 2; i++)
            {
                for (int j = col - 1; j < col + 2; j++)
                {
                    if (i == row && j == col)
                    {
                        continue;
                    }
                    if (i < 0 || i > _octopi.Length - 1 || j < 0 || j > _octopi.First().Length - 1)
                    {
                        continue;
                    }
                    _octopi[i][j].energy++;
                }
            }
        }

        public int Part1()
        {
            InitOctopi();
            int totalFlashes = 0;
            for (int i = 0; i < 100; i++)
            {
                int stepFlashes = 0;
                Add1ToAll();
                while (Flatten().Any(x => x.energy > 9 && !x.flashed))
                {
                    for (int r = 0; r < _octopi.Length; r++)
                    {
                        for (int c = 0; c < _octopi[r].Length; c++)
                        {
                            if (_octopi[r][c].energy > 9 && !_octopi[r][c].flashed)
                            {
                                _octopi[r][c].flashed = true;
                                Add1ToAdjacent(r, c);
                            }
                        }
                    }
                }
                stepFlashes = Flatten().Count(x => x.flashed);
                totalFlashes += stepFlashes;
                ResetFlashed();
            }
            return totalFlashes;
        }

        public int Part2()
        {
            InitOctopi();
            int i = 1;
            while (true)
            {
                Add1ToAll();
                while (Flatten().Any(x => x.energy > 9 && !x.flashed))
                {
                    for (int r = 0; r < _octopi.Length; r++)
                    {
                        for (int c = 0; c < _octopi[r].Length; c++)
                        {
                            if (_octopi[r][c].energy > 9 && !_octopi[r][c].flashed)
                            {
                                _octopi[r][c].flashed = true;
                                Add1ToAdjacent(r, c);
                            }
                        }
                    }
                }
                if (Flatten().All(x => x.flashed))
                {
                    return i;
                }
                ResetFlashed();
                i++;
            }
            throw new Exception("it never happened");
        }
    }
}
