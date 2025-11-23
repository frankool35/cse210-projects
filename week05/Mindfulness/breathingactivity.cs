// BreathingActivity.cs
using System;
using System.Diagnostics;

namespace MindfulnessProgram
{
    class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        public override void Run()
        {
            Start();

            var sw = Stopwatch.StartNew();
            const int breathStepSeconds = 4; // seconds for inhale/exhale step

            bool inhale = true;
            while (sw.Elapsed.TotalSeconds < DurationInSeconds)
            {
                Console.WriteLine();
                Console.Write(inhale ? "Breathe in... " : "Breathe out... ");

                int remaining = DurationInSeconds - (int)sw.Elapsed.TotalSeconds;
                int pause = Math.Min(breathStepSeconds, Math.Max(0, remaining));
                if (pause > 0)
                {
                    // Show countdown for the step (will not exceed total duration)
                    ShowCountdown(pause);
                }

                inhale = !inhale;
            }

            sw.Stop();
            Finish();
        }
    }
}
