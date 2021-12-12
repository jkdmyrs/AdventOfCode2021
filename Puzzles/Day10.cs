namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day10 : BasePuzzle
    {
        private List<char[]> _lines;

        public Day10(bool practice)
            : base(10, practice)
        {
            _lines = new();
            _puzzleInput.ForEach(x => _lines.Add(x.ToCharArray()));
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        private bool IsOpen(char c)
        {
            if (c == '(' || c == '{' || c == '<' || c == '[') return true;
            return false;
        }

        public long Part1()
        {
            long corruptedScore = 0;
            _lines.ForEach(line =>
            {
                List<char> openChars = new();
                foreach (char chr in line)
                {
                    if (IsOpen(chr))
                    {
                        openChars.Add(chr);
                    }
                    else
                    {
                        // we are at a closing char
                        // if we don't have any open chars
                        // OR the most recent open char doesnt match (biggest diff between open/close is 2)
                        // then the line is corrupt
                        if (openChars.Count == 0 || Math.Abs(chr - openChars.Last()) > 2)
                        {
                            switch (chr)
                            {
                                case ')':
                                    corruptedScore += 3;
                                    break;
                                case ']':
                                    corruptedScore += 57;
                                    break;
                                case '}':
                                    corruptedScore += 1197;
                                    break;
                                case '>':
                                    corruptedScore += 25137;
                                    break;
                            }
                            break;
                        }
                        else
                        {
                            // the closing char matches the open char
                            // remove the open char since it has been closed
                            openChars.RemoveAt(openChars.Count - 1);
                        }
                    }
                }
            });
            return corruptedScore;
        }

        public long Part2()
        {
            List<long> scores = new();
            _lines.ForEach(line =>
            {
                bool isCorrupt = false;
                long lineScore = 0;
                List<char> openChars = new();
                foreach (char chr in line)
                {
                    if (IsOpen(chr))
                    {
                        openChars.Add(chr);
                    }
                    else
                    {
                        // we are at a closing char
                        // if we don't have any open chars
                        // OR the most recent open char doesnt match (biggest diff between open/close is 2)
                        // then the line is corrupt
                        if (openChars.Count == 0 || Math.Abs(chr - openChars.Last()) > 2)
                        {
                            isCorrupt = true;
                            break;
                        }
                        else
                        {
                            // the closing char matches the open char
                            // remove the open char since it has been closed
                            openChars.RemoveAt(openChars.Count - 1);
                        }
                    }
                }
                if (!isCorrupt)
                {
                    openChars.Reverse();
                    openChars.ForEach(x =>
                    {
                        lineScore = lineScore * 5;
                        int characterScore;
                        switch (x)
                        {
                            case '(':
                                characterScore = 1;
                                break;
                            case '[':
                                characterScore = 2;
                                break;
                            case '{':
                                characterScore = 3;
                                break;
                            case '<':
                                characterScore = 4;
                                break;
                            default:
                                throw new Exception("invalid char");
                        }
                        lineScore += characterScore;
                    });
                    scores.Add(lineScore);
                }
            });
            scores.Sort();
            int take = (int)Math.Round((decimal)scores.Count / 2, MidpointRounding.AwayFromZero);
            return scores.Take(take).Last();
        }
    }
}
