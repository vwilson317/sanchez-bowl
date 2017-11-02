using System;
using System.Collections.Generic;
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

    public class Roster
    {
        public Roster()
        {
            Starters = new List<PlayerDetails>();
            Bench = new List<PlayerDetails>();
        }
        public IList<PlayerDetails> Starters { get; }
        public IList<PlayerDetails> Bench { get; }

        public int Count => Starters.Count + Bench.Count;
    }
}