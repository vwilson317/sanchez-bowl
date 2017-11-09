using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public class PlayerDetailsHelper : IPlayerDetailsHelper{
        private readonly IDomHelper _domHelper;
        public const string LeagueUrl = "http://fantasy.nfl.com/league/448915/team/";
        public const string GameCenter = "/gamecenter?gameCenterTab=track&trackType=sbs&week=";

        public PlayerDetailsHelper(IDomHelper domHelper){
            _domHelper = domHelper;
        }

        public async Task<Roster> GetRoster(TeamIdentifier teamIdentifier, byte week){
            var teamId = (byte)teamIdentifier.GetHashCode();
            string url = $"{LeagueUrl}{teamId}{GameCenter}{week}";
            var roster = await _domHelper.LoadPlayerDetails(url);
            SetTeamIds(teamId,roster);
            return roster;
        }

        private void SetTeamIds(byte teamId, Roster roster)
        {
            roster.Starters.ForEach(s => s.TeamId = teamId);
            roster.Bench.ForEach(s => s.TeamId = teamId);
        }
    }
}