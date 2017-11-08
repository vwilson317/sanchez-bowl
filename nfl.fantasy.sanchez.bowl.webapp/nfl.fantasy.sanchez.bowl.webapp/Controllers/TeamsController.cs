using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using nfl.fantasy.sanchez.bowl.da;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.webapp.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly IPlayerDetailsHelper _playerDetailsHelper;
        private readonly IDataAccess<Team> _dataAccess;
        private readonly TeamConfig _teamConfigOptions;

        public TeamsController(IPlayerDetailsHelper playerDetailsHelper, IOptions<TeamConfig> teamConfigAccessor,
            IDataAccess<Team> dataAccess)
        {
            _playerDetailsHelper = playerDetailsHelper;
            _dataAccess = dataAccess;
            _teamConfigOptions = teamConfigAccessor.Value;
        }

        [HttpGet("{teamIdentifier}/[action]/{weekNum}")]
        public async Task<Team> Week(TeamIdentifier teamIdentifier, byte weekNum)
        {
            var team = new Team(teamIdentifier);
            var roster = await _playerDetailsHelper.GetPlayerDetails(team.TeamIdentifier, weekNum);
            team.Roster = roster;

            await _dataAccess.GetAsync<Team>(team.Id);
            return team;
        }

        [HttpPost]
        public async Task<bool> Save([FromBody]Team team)
        {
            await _dataAccess.SaveAsync(team);
            return await Task.FromResult(true);
        }
    }
}
