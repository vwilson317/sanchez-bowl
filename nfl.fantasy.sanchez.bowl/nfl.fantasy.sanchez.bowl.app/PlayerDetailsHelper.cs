using System.Collections.Generic;
using System.Threading.Tasks;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.app
{
    public class PlayerDetailsHelper : IPlayerDetailsHelper{
        private readonly IDomHelper _domHelper;
        public const string LeagueUrl = "http://fantasy.nfl.com/league/448915/team/";
        public const string GameCenter = "/gamecenter?week=";
        public const int Week = 8;

        public PlayerDetailsHelper(IDomHelper domHelper){
            _domHelper = domHelper;
        }

        public async Task<Roster> GetPlayerDetails(TeamIdentifier teamIdentifier){
            var teamId = teamIdentifier.GetHashCode();
            var playerInfo = await _domHelper.LoadPlayerDetails($"{LeagueUrl}{teamId}{GameCenter}{Week}");
            return playerInfo;
        }
    }
}