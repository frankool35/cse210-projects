using System;

namespace EternalQuest
{
    class Program
    {
        // Creativity note (for rubric): 
        // I added a simple "leveling" concept where every 1000 points the player "levels up" and receives a console celebration.
        // Also added a small "achievement" display when specific thresholds are reached (e.g., 500, 1000).
        // These are explained here as required by the rubric and are implemented inline in Main's menu actions.

        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            const string saveFile = "goals.txt";

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Eternal Quest Menu ===");
                Console.WriteLine($"Score: {manager.Score}");
                Console.WriteLine("1. Create a new goal");
                Console.WriteLine("2. List goals");
                Console.WriteLine("3. Record an event for a goal");
                Console.WriteLine("4. Save goals");
                Console.WriteLine("5. Load goals");
                Console.WriteLine("6. Show achievements");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewGoal(manager);
                        break;
                    case "2":
                        manager.ListGoals();
                        break;
                    case "3":
                        manager.ListGoals();
                        Console.Write("Enter goal number to record event for: ");
                        if (int.TryParse(Console.ReadLine(), out int sel))
                        {
                            manager.RecordEvent(sel);
                            CheckForLevelUps(manager); // creativity feature
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;
                    case "4":
                        manager.SaveGoals(saveFile);
                        break;
                    case "5":
                        manager.LoadGoals(saveFile);
                        break;
                    case "6":
                        ShowAchievements(manager);
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            Console.WriteLine("Goodbye!");
        }

        static void CreateNewGoal(GoalManager manager)
        {
            Console.WriteLine("Choose goal type: 1) Simple 2) Eternal 3) Checklist");
            string type = Console.ReadLine();
            Console.Write("Short name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string desc = Console.ReadLine();
            Console.Write("Points for each event (integer): ");
            if (!int.TryParse(Console.ReadLine(), out int points)) points = 0;

            if (type == "1")
            {
                var g = new SimpleGoal(name, desc, points);
                manager.AddGoal(g);
                Console.WriteLine("Simple goal added.");
            }
            else if (type == "2")
            {
                var g = new EternalGoal(name, desc, points);
                manager.AddGoal(g);
                Console.WriteLine("Eternal goal added.");
            }
            else if (type == "3")
            {
                Console.Write("Target number (how many times to complete): ");
                if (!int.TryParse(Console.ReadLine(), out int target)) target = 1;
                Console.Write("Bonus points when completed: ");
                if (!int.TryParse(Console.ReadLine(), out int bonus)) bonus = 0;
                var g = new ChecklistGoal(name, desc, points, target, bonus);
                manager.AddGoal(g);
                Console.WriteLine("Checklist goal added.");
            }
            else
            {
                Console.WriteLine("Unknown type. Aborting.");
            }
        }

        // Creativity: simple leveling/achievement system
        static void CheckForLevelUps(GoalManager manager)
        {
            int score = manager.Score;
            int level = score / 1000;
            if (score > 0 && score % 1000 == 0)
            {
                Console.WriteLine($"*** Congratulations! You reached {score} points — Level {level} achieved! ***");
            }
            // example achievement thresholds
            if (score >= 500 && score < 600)
            {
                Console.WriteLine("*** Achievement: Rookie (500 points) ***");
            }
            if (score >= 1000 && score < 1100)
            {
                Console.WriteLine("*** Achievement: Seasoned (1000 points) ***");
            }
        }

        static void ShowAchievements(GoalManager manager)
        {
            Console.WriteLine("Achievements (examples):");
            if (manager.Score >= 1000) Console.WriteLine("- Seasoned (1000+ points)");
            if (manager.Score >= 500) Console.WriteLine("- Rookie (500+ points)");
            if (manager.Score < 500) Console.WriteLine("- Keep going to earn achievements!");
        }
    }
}


