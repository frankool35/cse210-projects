// Activity.cs
using System;
using System.Diagnostics;
using System.Threading;

namespace MindfulnessProgram
{
    /// <summary>
    /// Base class for all mindfulness activities.
    /// Contains shared attributes and behaviors (start/finish animations, duration handling).
    /// </summary>
    abstract class Activity
    {
        // Encapsulated private fields (underscoreCamelCase) and public read-only properties
        private readonly string _name;
        private readonly string _description;
        private int _durationInSeconds;

        public string Name => _name;
        public string Description => _description;
        public int DurationInSeconds => _durationInSeconds;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _durationInSeconds = 0;
        }

        // Standard start routine used by all activities
        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"--- {Name} ---\n");
            Console.WriteLine(Description + "\n");
            _durationInSeconds = PromptForDuration();
            Console.WriteLine("\nGet ready...");
            ShowSpinner(3);
        }

        // Standard finish routine used by all activities
        public void Finish()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(3);
            Console.WriteLine($"You have completed the {Name} for {DurationInSeconds} seconds.\n");
            Console.WriteLine("Press Enter to return to the main menu...");
            Console.ReadLine();
        }

        // Request duration from user (validated)
        private int PromptForDuration()
        {
            while (true)
            {
                Console.Write("Enter duration in seconds: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                {
                    return seconds;
                }
                Console.WriteLine("Please enter a positive integer.");
            }
        }

        // Spinner animation (uses backspace to overwrite)
        protected void ShowSpinner(int seconds)
        {
            var sw = Stopwatch.StartNew();
            string[] spinner = { "|", "/", "-", "\\" };
            int i = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[i % spinner.Length]);
                Thread.Sleep(250);
                Console.Write("\b");
                i++;
            }
            sw.Stop();
        }

        // Countdown (shows numbers)
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        // Abstract run method - each activity provides its own behavior
        public abstract void Run();
    }
}
