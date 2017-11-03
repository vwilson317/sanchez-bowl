using System;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public class PlayerDetails
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public double Score { get; set; }

        public Positions PositionType => (Positions) Enum.Parse(typeof(Positions), Position?.Split('-')[0].Trim());

        public bool IsStarter { get; set; }
    }
}