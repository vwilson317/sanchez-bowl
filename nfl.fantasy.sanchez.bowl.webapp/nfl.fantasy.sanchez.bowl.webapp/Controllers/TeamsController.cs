using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.webapp.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly IPlayerDetailsHelper _playerDetailsHelper;
        private readonly TeamConfig _teamConfigOptions;

        public TeamsController(IPlayerDetailsHelper playerDetailsHelper, IOptions<TeamConfig> teamConfigAccessor)
        {
            _playerDetailsHelper = playerDetailsHelper;
            _teamConfigOptions = teamConfigAccessor.Value;
        }

        [HttpGet("{teamIdentifier}/[action]/{weekNum}")]
        public async Task<Team> Week(TeamIdentifier teamIdentifier, byte weekNum)
        {
            var team = new Team(teamIdentifier);
            var roster = await _playerDetailsHelper.GetPlayerDetails(team.TeamIdentifierIdentifier, weekNum);
            team.Roster = roster;
            return team;
        }
    }
}
