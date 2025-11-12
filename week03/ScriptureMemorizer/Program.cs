using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    /*
     * W03 Scripture Memorizer Project
     * 
     * EXCEEDS REQUIREMENTS:
     * - Added a library of multiple scriptures and random selection at runtime.
     * - Clean structure: each class in its own file (Reference, Word, Scripture).
     * - Demonstrates encapsulation, constructors, and modular design.
     * 
     * Student: [Your Name]
     * Date: [Submission Date]
     */

    class Program
    {
        static void Main(string[] args)
        {
            // Library of multiple scriptures
            List<(Reference, string)> scriptureLibrary = new List<(Reference, string)>
            {
                (new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart and lean not unto thine own understanding."),
                (new Reference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
                (new Reference("Philippians", 4, 13), "I can do all things through Christ which strengtheneth me."),
                (new Reference("Psalm", 23, 1, 2), "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures.")
            };

            // Select one scripture randomly
            Random random = new Random();
            var chosen = scriptureLibrary[random.Next(scriptureLibrary.Count)];
            Reference reference = chosen.Item1;
            Scripture scripture = new Scripture(reference, chosen.Item2);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress ENTER to hide words or type 'quit' to end.");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWords(3);

                if (scripture.IsCompletelyHidden())
                {
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    Console.WriteLine("\nAll words are hidden. Good job memorizing!");
                    break;
                }
            }
        }
    }
}
