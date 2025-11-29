using System;

namespace EternalQuest
{
    // A Simple Goal is completed once and awarded points only one time.
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _isComplete = false;
        }

        // When the user records the event:
        // - If not complete, award full points and mark complete.
        // - If already complete, award 0.
        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return Points;
            }
            return 0;
        }

        public override bool IsComplete()
        {
            return _isComplete;
        }

        // Saving format used by GoalManager:
        // Simple:Name|Description|Points|IsComplete
        public override string GetStringRepresentation()
        {
            return $"Simple:{ShortName}|{Description}|{Points}|{_isComplete}";
        }
    }
}
