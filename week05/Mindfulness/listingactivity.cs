// ListingActivity.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MindfulnessProgram
{
    class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private Queue<string> _promptQueue;
        private readonly Random _random = new Random();

        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
            _promptQueue = new Queue<string>(Shuffle(_prompts));
        }

        public override void Run()
        {
            Start();

            if (_promptQueue.Count == 0) _promptQueue = new Queue<string>(Shuffle(_prompts));
            string prompt = _promptQueue.Dequeue();

            Console.WriteLine("\nPrompt:\n" + prompt + "\n");
            Console.WriteLine("You will have a few seconds to think, then start listing items. Press Enter after each item.");

            // Short countdown before starting
            ShowCountdown(5);
            Console.WriteLine("\nBegin listing (press Enter after each).");

            var items = new List<string>();
            var sw = Stopwatch.StartNew();

            // Keep reading lines until the duration expires. Use ReadLineWithTimeout to avoid blocking past duration.
            while (sw.Elapsed.TotalSeconds < DurationInSeconds)
            {
                int remainingMs = (int)((DurationInSeconds - sw.Elapsed.TotalSeconds) * 1000);
                if (remainingMs <= 0) break;

                string entry = ReadLineWithTimeout(remainingMs).Result; // blocking wait until either user input or timeout
                if (entry == null)
                {
                    // time expired while waiting for a read
                    break;
                }
                if (!string.IsNullOrWhiteSpace(entry))
                {
                    items.Add(entry.Trim());
                }
            }

            sw.Stop();

            Console.WriteLine($"\nYou listed {items.Count} items:");
            foreach (var it in items)
            {
                Console.WriteLine($" - {it}");
            }

            Finish();
        }

        private List<T> Shuffle<T>(IEnumerable<T> items)
        {
            var list = new List<T>(items);
            for (int i = list.Count - 1; i >= 1; i--)
            {
                int j = _random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
            return list;
        }

        // Async read with timeout; returns null on timeout
        private async Task<string> ReadLineWithTimeout(int timeoutMs)
        {
            Task<string> readTask = Task.Run(() => Console.ReadLine());
            Task completed = await Task.WhenAny(readTask, Task.Delay(timeoutMs));
            if (completed == readTask)
            {
                return readTask.Result;
            }
            else
            {
                return null;
            }
        }
    }
}
