using System.Collections.Generic;

namespace nfl.fantasy.sanchez.bowl.domain
{
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