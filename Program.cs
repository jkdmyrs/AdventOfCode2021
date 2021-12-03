using System;
using System.Collections.Generic;

namespace AOC2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 'p' for practice, or any key for the real puzzle input");
            bool isPractice = Console.ReadKey().KeyChar == 'p';

            List<BasePuzzle> puzzles = new()
            {
                new Day1(isPractice),
                new Day2(isPractice),
            };

            int i = 1;
            puzzles.ForEach(puzzle =>
            {
                Console.WriteLine();
                Console.WriteLine($"Day {i} Part 1 - {puzzle.Part1Answer}");
                Console.WriteLine($"Day {i} Part 2 - {puzzle.Part2Answer}");
                Console.WriteLine();
                i++;
            });

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
