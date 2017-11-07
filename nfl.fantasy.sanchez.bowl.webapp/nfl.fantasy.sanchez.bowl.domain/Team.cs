using System;
using System.Linq;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public class Team
    {
        public Team(TeamIdentifier teamIdentifierIdentifier)
        {
            TeamIdentifierIdentifier = teamIdentifierIdentifier;
        }

        public string Name => Enum.GetName(typeof(TeamIdentifier), TeamIdentifierIdentifier);
        private Roster _roster;
        public Roster Roster {
            get { return _roster; }
            set
            {
                value.Starters.ForEach(s => s.TeamId = (byte)TeamIdentifierIdentifier);
                value.Bench.ForEach(s => s.TeamId = (byte)TeamIdentifierIdentifier);
                _roster = value;
            }
        }
        public TeamIdentifier TeamIdentifierIdentifier { get; set; }

        public double TotalScore => Roster.Starters.Sum(p => p.Score);
    }
}