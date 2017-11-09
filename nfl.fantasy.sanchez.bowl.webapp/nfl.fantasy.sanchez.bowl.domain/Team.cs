using System;
using System.Collections.Generic;
using System.Linq;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public class Team
    {
        public Team() { }
        public Team(TeamIdentifier teamIdentifier)
        {
            TeamIdentifier = teamIdentifier;
        }

        public int Id => (int)TeamIdentifier;

        public string Name => Enum.GetName(typeof(TeamIdentifier), TeamIdentifier);
        public Roster Roster { get; set; }

        public IEnumerable<PlayerDetails> Players => Roster.Starters.Concat(Roster.Bench);

        public TeamIdentifier TeamIdentifier { get; set; }

        public double TotalScore => Roster.Starters.Sum(p => p.Score);

        //public void SetTeamId()
        //{
        //    Roster.Starters.ForEach(s => s.TeamId = (byte)TeamIdentifier);
        //    Roster.Bench.ForEach(s => s.TeamId = (byte)TeamIdentifier);
        //}
    }
}