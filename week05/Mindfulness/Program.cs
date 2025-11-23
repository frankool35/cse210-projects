       // Program.cs
using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create one instance of each activity (keeps prompt queues between runs in same session)
            var activities = new Dictionary<int, Activity>
            {
                { 1, new BreathingActivity() },
                { 2, new ReflectionActivity() },
                { 3, new ListingActivity() }
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1) Breathing Activity");
                Console.WriteLine("2) Reflection Activity");
                Console.WriteLine("3) Listing Activity");
                Console.WriteLine("4) Exit");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();
                if (!int.TryParse(choice, out int opt))
                {
                    Console.WriteLine("Invalid choice. Press Enter to continue.");
                    Console.ReadLine();
                    continue;
                }

                if (opt == 4) break;

                if (activities.ContainsKey(opt))
                {
                    activities[opt].Run();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Press Enter to continue.");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("\nGoodbye! Take care.");
        }
    }
}
   
