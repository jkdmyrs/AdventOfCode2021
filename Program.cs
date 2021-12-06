namespace AOC2021
{
    using AOC2021.Puzzles;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 'p' for practice, or any key for the real puzzle input");
            bool isPractice = Console.ReadKey().KeyChar == 'p';
            GetPuzzles(isPractice).ForEach(RunPuzzle);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static List<BasePuzzle> GetPuzzles(bool isPractice)
        {
            List<BasePuzzle> puzzles = new();
            List<Type> puzzleClasses = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.IsSubclassOf(typeof(BasePuzzle))).ToList();
            puzzleClasses.ForEach(puzzle => puzzles.Add((BasePuzzle)puzzle.GetConstructor(new[] { typeof(bool) }).Invoke(new object[] { isPractice })));
            return puzzles;
        }

        private static void RunPuzzle(BasePuzzle puzzle)
        {
            int puzzleNumber = int.Parse(puzzle.GetType().Name.Substring(3));
            if (puzzle.Skip)
            {
                Console.WriteLine();
                Console.WriteLine($"Skipping Day {puzzleNumber} because it runs long");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Day {puzzleNumber} Part 1 - {puzzle.Part1Answer}");
                Console.WriteLine($"Day {puzzleNumber} Part 2 - {puzzle.Part2Answer}");
                Console.WriteLine();
            }
        }
    }
}
