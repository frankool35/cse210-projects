using System;

namespace EternalQuest
{
    // Eternal Goals represent actions that are repeated forever (e.g., daily scripture study).
    // They are never marked complete and always award points each time the action is recorded.
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        // Eternal goals never complete, so recording an event always awards points.
        public override int RecordEvent()
        {
            return Points;
        }

        // Eternal goals never reach a "completed" state.
        public override bool IsComplete()
        {
            return false;
        }

        // Saving format expected by GoalManager:
        // Eternal:Name|Description|Points
        public override string GetStringRepresentation()
        {
            return $"Eternal:{ShortName}|{Description}|{Points}";
        }
    }
}
