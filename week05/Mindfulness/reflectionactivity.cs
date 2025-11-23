// ReflectionActivity.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MindfulnessProgram
{
    class ReflectionActivity : Activity
    {
        // Prompts and questions (private member variables)
        private readonly List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        // Queues to ensure non-repeating usage until all items are used
        private Queue<string> _promptQueue;
        private Queue<string> _questionQueue;
        private readonly Random _random = new Random();

        public ReflectionActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
            _promptQueue = new Queue<string>(Shuffle(_prompts));
            _questionQueue = new Queue<string>(Shuffle(_questions));
        }

        public override void Run()
        {
            Start();

            if (_promptQueue.Count == 0) _promptQueue = new Queue<string>(Shuffle(_prompts));
            string prompt = _promptQueue.Dequeue();
            Console.WriteLine("\nPrompt:\n" + prompt + "\n");
            Console.WriteLine("Reflect on the following questions:");

            var sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < DurationInSeconds)
            {
                if (_questionQueue.Count == 0) _questionQueue = new Queue<string>(Shuffle(_questions));
                string question = _questionQueue.Dequeue();
                Console.WriteLine("\n- " + question);

                int remaining = DurationInSeconds - (int)sw.Elapsed.TotalSeconds;
                int pause = Math.Min(5, Math.Max(0, remaining)); // spinner pause per spec
                if (pause > 0)
                {
                    ShowSpinner(pause);
                }
            }
            sw.Stop();

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
    }
}
