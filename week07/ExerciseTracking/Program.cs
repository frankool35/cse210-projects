using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running("30 Nov 2025", 30, 4.8));    // 4.8 km
        activities.Add(new Cycling("30 Nov 2025", 40, 20));     // 20 kph
        activities.Add(new Swimming("30 Nov 2025", 25, 30));    // 30 laps

        foreach (Activity a in activities)
        {
            Console.WriteLine(a.GetSummary());
        }
    }
}
