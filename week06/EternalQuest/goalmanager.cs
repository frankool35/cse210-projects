using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // The GoalManager handles all overall tracking:
    // - list of goals
    // - total score
    // - file save/load
    // - recording progress
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public int Score => _score;
        public IReadOnlyList<Goal> Goals => _goals.AsReadOnly();

        // Add any new goal
        public void AddGoal(Goal g)
        {
            if (g != null)
            {
                _goals.Add(g);
            }
        }

        // Display all goals with status
        public void ListGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals created yet.");
                return;
            }

            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        // Record an event based on user-selected index
        public void RecordEvent(int index)
        {
            if (index < 1 || index > _goals.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Goal g = _goals[index - 1];
            int pointsAwarded = g.RecordEvent();
            _score += pointsAwarded;

            Console.WriteLine(
                $"Event recorded for goal: {g.ShortName}. " +
                $"Points earned: {pointsAwarded}. Total score: {_score}");
        }

        // Save score + all goals to file
        public void SaveGoals(string filename)
        {
            using (StreamWriter output = new StreamWriter(filename))
            {
                output.WriteLine(_score);

                foreach (var goal in _goals)
                {
                    output.WriteLine(goal.GetStringRepresentation());
                }
            }

            Console.WriteLine($"Goals saved to: {filename}");
        }

        // Load score + goals from file
        public void LoadGoals(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0)
            {
                Console.WriteLine("File is empty.");
                return;
            }

            _goals.Clear();

            // First line is the score
            if (!int.TryParse(lines[0], out _score))
            {
                _score = 0; // fallback if score line corrupt
            }

            // Reconstruct all goals
            for (int i = 1; i < lines.Length; i++)
            {
                Goal g = CreateGoalFromString(lines[i]);
                if (g != null)
                {
                    _goals.Add(g);
                }
            }

            Console.WriteLine($"Loaded {_goals.Count} goals. Current score is now: {_score}");
        }

        // Convert a saved line back into a Goal object
        public static Goal CreateGoalFromString(string line)
        {
            int colonIndex = line.IndexOf(':');
            if (colonIndex < 0) return null;

            string type = line.Substring(0, colonIndex);
            string rest = line.Substring(colonIndex + 1);
            string[] parts = rest.Split('|');

            switch (type)
            {
                case "Simple":
                    // Format: Simple:Name|Desc|Points|IsComplete
                    string nameS = parts[0];
                    string descS = parts[1];
                    int pointsS = int.Parse(parts[2]);
                    bool complete = bool.Parse(parts[3]);

                    var sg = new SimpleGoal(nameS, descS, pointsS);
                    if (complete && !sg.IsComplete())
                    {
                        sg.RecordEvent(); // simulate completion
                    }
                    return sg;

                case "Eternal":
                    // Format: Eternal:Name|Desc|Points
                    return new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));

                case "Checklist":
                    // Format: Checklist:Name|Desc|Points|Done|Target|Bonus
                    string nameC = parts[0];
                    string descC = parts[1];
                    int pointsC = int.Parse(parts[2]);
                    int amountDone = int.Parse(parts[3]);
                    int target = int.Parse(parts[4]);
                    int bonus = int.Parse(parts[5]);

                    var cg = new ChecklistGoal(nameC, descC, pointsC, target, bonus);

                    // Simulate progress
                    for (int j = 0; j < amountDone; j++)
                    {
                        cg.RecordEvent();
                    }

                    return cg;
            }

            return null;
        }
    }
}
