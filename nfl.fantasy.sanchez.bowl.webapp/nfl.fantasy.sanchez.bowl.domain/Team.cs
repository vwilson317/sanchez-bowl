using System;
using System.Linq;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public class Team
    {
        public Team(TeamIdentifier teamIdentifier)
        {
            TeamIdentifier = teamIdentifier;
        }

        public byte Id => (byte)TeamIdentifier;

        public string Name => Enum.GetName(typeof(TeamIdentifier), TeamIdentifier);
        private Roster _roster;
        public Roster Roster {
            get => _roster;
            set
            {
                value.Starters.ForEach(s => s.TeamId = (byte)TeamIdentifier);
                value.Bench.ForEach(s => s.TeamId = (byte)TeamIdentifier);
                _roster = value;
            }
        }
        public TeamIdentifier TeamIdentifier { get; set; }

        public double TotalScore => Roster.Starters.Sum(p => p.Score);
    }
}