namespace AOC2021.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day3 : BasePuzzle
    {
        public Day3(bool practice)
            : base(3, practice)
        {
        }

        public override string Part1Answer => Part1().ToString();

        public override string Part2Answer => Part2().ToString();

        private int BinaryToInt(List<char> binary)
        {
            int exponent = binary.Count() - 1;
            int decimalValue = 0;
            binary.ForEach(val =>
            {
                if (val == '1')
                {
                    decimalValue += (int)Math.Pow(2, exponent);
                }
                exponent--;
            });
            return decimalValue;
        }

        public int Part1()
        {
            List<List<char>> groupedBits = new();
            for (int i = 0; i < _puzzleInput.First().Length; i++)
            {
                groupedBits.Add(_puzzleInput.Select(x => x[i]).ToList());
            }

            List<char> gammaRateBinary = new();
            groupedBits.ForEach(group =>
            {
                gammaRateBinary.Add(group.Count(x => x == '0') > group.Count(x => x == '1') ? '0' : '1');
            });

            List<char> epsilonRateBinary = gammaRateBinary.Select(x => x == '0' ? '1' : '0').ToList();

            int gamma = BinaryToInt(gammaRateBinary);
            int epsilon = BinaryToInt(epsilonRateBinary);

            return gamma * epsilon;
        }

        public int Part2()
        {
            List<string> remainingNumbers = new(_puzzleInput);
            List<char> groupedBits = new();
            int position = 0;

            while (remainingNumbers.Count > 1)
            {
                groupedBits = remainingNumbers.Select(x => x[position]).ToList();
                if (groupedBits.Count(x => x == '0') > groupedBits.Count(x => x == '1'))
                {
                    remainingNumbers.RemoveAll(x => x[position] == '1');
                }
                else
                {
                    remainingNumbers.RemoveAll(x => x[position] == '0');
                }
                position++;
            }

            int oxygenRating = BinaryToInt(remainingNumbers.First().ToCharArray().ToList());

            remainingNumbers = new(_puzzleInput);
            groupedBits = new();
            position = 0;

            while (remainingNumbers.Count > 1)
            {
                groupedBits = remainingNumbers.Select(x => x[position]).ToList();
                if (groupedBits.Count(x => x == '0') <= groupedBits.Count(x => x == '1'))
                {
                    remainingNumbers.RemoveAll(x => x[position] == '1');
                }
                else
                {
                    remainingNumbers.RemoveAll(x => x[position] == '0');
                }
                position++;
            }

            int c02Rating = BinaryToInt(remainingNumbers.First().ToCharArray().ToList());

            return oxygenRating * c02Rating;
        }
    }
}
