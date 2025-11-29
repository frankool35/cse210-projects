using System;

namespace EternalQuest
{
    public abstract class Goal
    {
        private string _shortName;
        private string _description;
        private int _points;

        protected Goal(string name, string description, int points)
        {
            _shortName = name;
            _description = description;
            _points = points;
        }

        // Make these public so GoalManager and Program can access them
        public string ShortName => _shortName;
        public string Description => _description;
        public int Points => _points;

        public abstract int RecordEvent();
        public abstract bool IsComplete();

        public virtual string GetDetailsString()
        {
            string status = IsComplete() ? "[X]" : "[ ]";
            return $"{status} {ShortName} ({Description})";
        }

        public abstract string GetStringRepresentation();

        public override string ToString()
        {
            return GetDetailsString();
        }
    }
}
