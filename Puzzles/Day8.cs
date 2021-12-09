namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day8 : BasePuzzle
    {
        private readonly List<(List<string> patterns, List<string> output)> _notes;

        private readonly List<(int number, int segments)> _segmentCounts = new List<(int, int)>
        {
            ( 0, 6 ),
            ( 1, 2 ),
            ( 2, 5 ),
            ( 3, 5 ),
            ( 4, 4 ),
            ( 5, 5 ),
            ( 6, 6 ),
            ( 7, 3 ),
            ( 8, 7 ),
            ( 9, 6 )
        };

        public Day8(bool practice)
            : base(8, practice)
        {
            _notes = new();
            _puzzleInput.ForEach(line =>
            {
                var splitNotes = line.Split("|");
                _notes.Add((splitNotes[0].Trim().Split(" ").ToList(), splitNotes[1].Trim().Split(" ").ToList()));
            });
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        public int Part1()
        {
            List<int> countsToLookFor = _segmentCounts.GroupBy(x => x.segments).Where(x => x.Count() == 1).Select(x => x.Key).ToList();
            int count = 0;
            countsToLookFor.ForEach(countToLookFor =>
            {
                _notes.ForEach(note => count += note.output.Where(x => x.Length == countToLookFor).Count());
            });
            return count;
        }

        public int Part2()
        {
            bool PatternContainsLetters(string pattern, char[] letters)
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    if (!pattern.Contains(letters[i]))
                    {
                        return false;
                    }
                }
                return true;
            }

            int outputSum = 0;

            _notes.ForEach(note =>
            {
                char[] lettersIn1 = note.output.Concat(note.patterns).First(x => x.Length == _segmentCounts.First(y => y.number == 1).segments).ToCharArray();
                char[] lettersIn4 = note.output.Concat(note.patterns).First(x => x.Length == _segmentCounts.First(y => y.number == 4).segments).ToCharArray();
                char[] lettersIn7 = note.output.Concat(note.patterns).First(x => x.Length == _segmentCounts.First(y => y.number == 7).segments).ToCharArray();
                char[] lettersIn8 = note.output.Concat(note.patterns).First(x => x.Length == _segmentCounts.First(y => y.number == 8).segments).ToCharArray();

                char[] lettersIn9 = null;
                char[] lettersIn0 = null;
                char[] lettersIn6 = null;
                char[] lettersIn3 = null;
                char[] lettersIn5 = null;
                char[] lettersIn2 = null;

                var ninesZerosOrSixes = note.output.Concat(note.patterns).Where(x => x.Length == 6).ToList();
                ninesZerosOrSixes.ForEach(pattern =>
                {
                    if (PatternContainsLetters(pattern, lettersIn4))
                    {
                        if (lettersIn9 == null)
                        {
                            lettersIn9 = pattern.ToCharArray();
                        }
                    }
                    else if (PatternContainsLetters(pattern, lettersIn7))
                    {
                        if (lettersIn0 == null)
                        {
                            lettersIn0 = pattern.ToCharArray();
                        }
                    }
                    else
                    {
                        if (lettersIn6 == null)
                        {
                            lettersIn6 = pattern.ToCharArray();
                        }
                    }
                });


                var threesFivesOrTwos = note
                    .output
                    .Concat(note.patterns)
                    .Where(x
                        => x.Length != 6
                            && x.Length != _segmentCounts.First(x => x.number == 1).segments
                            && x.Length != _segmentCounts.First(x => x.number == 4).segments
                            && x.Length != _segmentCounts.First(x => x.number == 7).segments
                            && x.Length != _segmentCounts.First(x => x.number == 8).segments)
                    .ToList();
                char oneSixIntersect = lettersIn6.Intersect(lettersIn1).First();
                threesFivesOrTwos.ForEach(pattern =>
                {

                    if (PatternContainsLetters(pattern, lettersIn1))
                    {
                        if (lettersIn3 == null)
                        {
                            lettersIn3 = pattern.ToCharArray();
                        }
                    }
                    else if (PatternContainsLetters(pattern, new char[] { oneSixIntersect }))
                    {
                        if (lettersIn5 == null)
                        {
                            lettersIn5 = pattern.ToCharArray();
                        }
                    }
                    else
                    {
                        if (lettersIn2 == null)
                        {
                            lettersIn2 = pattern.ToCharArray();
                        }
                    }
                });

                string outputDisplay = string.Empty;
                note.output.ForEach(output =>
                {
                    var orderedOutput = output.ToCharArray().OrderByDescending(x => x);

                    if (orderedOutput.SequenceEqual(lettersIn0.OrderByDescending(x => x)))
                    {
                        outputDisplay += "0";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn1.OrderByDescending(x => x)))
                    {
                        outputDisplay += "1";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn2.OrderByDescending(x => x)))
                    {
                        outputDisplay += "2";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn3.OrderByDescending(x => x)))
                    {
                        outputDisplay += "3";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn4.OrderByDescending(x => x)))
                    {
                        outputDisplay += "4";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn5.OrderByDescending(x => x)))
                    {
                        outputDisplay += "5";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn6.OrderByDescending(x => x)))
                    {
                        outputDisplay += "6";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn7.OrderByDescending(x => x)))
                    {
                        outputDisplay += "7";
                    }
                    else if (orderedOutput.SequenceEqual(lettersIn8.OrderByDescending(x => x)))
                    {
                        outputDisplay += "8";
                    }
                    else
                    {
                        outputDisplay += "9";
                    }
                });
                int outputDisplayNumber = int.Parse(outputDisplay);
                outputSum += outputDisplayNumber;
            });

            return outputSum;
        }
    }
}
