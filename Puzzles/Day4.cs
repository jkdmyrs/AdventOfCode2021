namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day4 : BasePuzzle
    {
        private readonly List<int> _draws;

        private List<List<(int number, bool marked)>> _boards;

        public Day4(bool practice)
            : base(4, practice)
        {
            _draws = _puzzleInput.First().Split(',').Select(int.Parse).ToList();
        }

        private void InitBoards()
        {
            var temp = new List<string>(_puzzleInput.Where(x => x.Contains(" ")));
            _boards = new();
            while (temp.Any())
            {
                List<(int, bool)> board = new();
                for (int i = 0; i < 5; i++)
                {
                    var inputRow = temp[i];
                    var boardRow = inputRow.Split(" ").Where(x => x != string.Empty).Select(x => (int.Parse(x), false));
                    board.AddRange(boardRow);
                }
                temp = temp.GetRange(5, temp.Count - 5);
                _boards.Add(board);
            }
        }

        private bool IsWinner(List<(int number, bool marked)> board)
        {
            for (int i = 0; i < 25; i+=5)
            {
                List<(int number, bool marked)> row = board.GetRange(i, 5);
                if (row.All(x => x.marked) == true)
                {
                    return true;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                List<(int number, bool marked)> column = new();
                for (int j = i; j < 25; j+=5)
                {
                    column.Add(board[j]);
                }
                if (column.All(x => x.marked))
                {
                    return true;
                }
            }
            return false;
        }

        public override int Part1Answer => Part1();

        public override int Part2Answer => Part2();

        public int Part1()
        {
            InitBoards();
            foreach (int draw in _draws)
            {
                foreach (List<(int number, bool marked)> board in _boards)
                {
                    for (int i = 0; i < board.Count; i++)
                    {
                        if (board[i].number == draw)
                        {
                            var spot = board[i];
                            spot.marked = true;
                            board[i] = spot;
                        }
                    }
                    if (IsWinner(board))
                    {
                        IEnumerable<int> unmarked = board.Where(x => x.marked == false).Select(x => x.number);
                        return unmarked.Sum() * draw;
                    }
                }
            }
            throw new Exception("no winner");
        }

        public int Part2()
        {
            InitBoards();
            List<(int number, bool marked)> lastWinner = new();
            int lastDraw = 0;
            foreach (int draw in _draws)
            {
                for (int j = _boards.Count - 1; j >= 0; j--)
                {
                    for (int i = 0; i < _boards[j].Count; i++)
                    {
                        if (_boards[j][i].number == draw)
                        {
                            var spot = _boards[j][i];
                            spot.marked = true;
                            _boards[j][i] = spot;
                        }
                    }
                    if (IsWinner(_boards[j]))
                    {
                        lastWinner = _boards[j];
                        lastDraw = draw;
                        _boards.RemoveAt(j);
                    }
                    if (_boards.Count == 0)
                    {
                        IEnumerable<int> unmarked = lastWinner.Where(x => x.marked == false).Select(x => x.number);
                        return unmarked.Sum() * lastDraw;
                    }
                }
            }
            throw new Exception("no answer");
        }
    }
}
