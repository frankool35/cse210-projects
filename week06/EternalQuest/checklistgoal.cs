using System;

namespace EternalQuest
{
    // Checklist goals require completing an action a certain number of times.
    // Each event gives points, and upon reaching the target a one-time bonus is awarded.
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus; // Awarded only when the checklist is completed

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            // Ensure the goal is never created with invalid values
            _target = Math.Max(1, target);
            _bonus = Math.Max(0, bonus);
            _amountCompleted = 0;
        }

        // Record one completion. Award points, and if target is reached, add bonus.
        public override int RecordEvent()
        {
            if (_amountCompleted < _target)
            {
                _amountCompleted++;

                int total = Points;

                // Award bonus exactly when reaching the target
                if (_amountCompleted == _target)
                {
                    total += _bonus;
                }

                return total;
            }

            // No additional points after completion
            return 0;
        }

        // Considered complete once the number of completions reaches the target
        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        // Display progress such as: [ ] Temple Attendance (Attend 10 times) -- Completed 2/10
        public override string GetDetailsString()
        {
            string status = IsComplete() ? "[X]" : "[ ]";

            return $"{status} {ShortName} ({Description}) -- Completed {_amountCompleted}/{_target}";
        }

        // Save format: Checklist:Name|Description|Points|AmountCompleted|Target|Bonus
        public override string GetStringRepresentation()
        {
            return $"Checklist:{ShortName}|{Description}|{Points}|{_amountCompleted}|{_target}|{_bonus}";
        }
    }
}
