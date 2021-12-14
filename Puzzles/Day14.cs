namespace AOC2021.Puzzles
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day14 : BasePuzzle
    {
        private string _template;
        private List<(string pair, string insert)> _rules;
        private Dictionary<(string pair, string insert), long> _rulesCounted;

        public Day14(bool practice)
            : base(14, practice)
        {
            _template = _puzzleInput.First();
            _rules = _puzzleInput.Skip(2).Select(line =>
            {
                var splitLine = line.Split(' ');
                return (splitLine.First(), splitLine.Last());
            }).ToList();
            _rulesCounted = new();
            _puzzleInput.Skip(2).ToList().ForEach(line =>
            {
                var splitLine = line.Split(' ');

                long count = 0;
                string pair = $"{splitLine.First().First()}{splitLine.First().Last()}";
                for (int i = 0; i < _template.Length - 1; i++)
                {
                    var templatePair = $"{_template[i]}{_template[i + 1]}";
                    if (pair == templatePair)
                    {
                        count++;
                    }
                }

                _rulesCounted.Add((splitLine.First(), splitLine.Last()), count);
            });
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        public int Part1()
        {
            // doesn't work for part 2
            for (int step = 0; step < 10; step++)
            {
                string temp = string.Empty;
                for (int i = 0; i < _template.Length - 1; i++)
                {
                    string pair = $"{_template[i]}{_template[i + 1]}";
                    string insert = _rules.First(x => x.pair == pair).insert;
                    temp += $"{_template[i]}{insert}";
                }
                _template = temp + _template.Last();
            }

            int max = _template.ToCharArray().GroupBy(x => x).Max(x => x.Count());
            int min = _template.ToCharArray().GroupBy(x => x).Min(x => x.Count());

            return max - min;
        }

        public long Part2()
        {
            Dictionary<char, long> charCounts = new();
            for (int step = 0; step < 40; step++)
            {
                charCounts = new();
                Dictionary<string, long> stepCounts = new();
                foreach (var rule in _rulesCounted)
                {
                    stepCounts.Add(rule.Key.pair, 0);
                    charCounts.TryAdd(rule.Key.pair[0], 0);
                    charCounts.TryAdd(rule.Key.pair[1], 0);
                }
                foreach (var rule in _rulesCounted)
                {
                    if (rule.Value > 0)
                    {
                        string pairCreated1 = $"{rule.Key.pair.First()}{rule.Key.insert}";
                        string pairCreated2 = $"{rule.Key.insert}{rule.Key.pair.Last()}";
                        stepCounts[pairCreated1] = stepCounts[pairCreated1] + rule.Value;
                        charCounts[pairCreated1[0]] = charCounts[pairCreated1[0]] + rule.Value;
                        charCounts[pairCreated1[1]] = charCounts[pairCreated1[1]] + rule.Value;
                        stepCounts[pairCreated2] = stepCounts[pairCreated2] + rule.Value;
                    }
                }
                foreach (var rule in _rulesCounted.ToList())
                {
                    _rulesCounted[rule.Key] = stepCounts[rule.Key.pair];
                }
            }

            char lastChar = _template.Last();
            charCounts[lastChar] = charCounts[lastChar] + 1;

            long max = charCounts.Max(x => x.Value);
            long min = charCounts.Min(x => x.Value);
            return max - min;
        }
    }
}
