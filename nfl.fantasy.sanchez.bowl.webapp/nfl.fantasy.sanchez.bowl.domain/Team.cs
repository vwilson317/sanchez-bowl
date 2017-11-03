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
        public Roster Roster { get; set; }
        public TeamIdentifier TeamIdentifierIdentifier { get; set; }

        public double TotalScore => Roster.Starters.Sum(p => p.Score);
    }
}