using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.webapp.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        public Team TeamOne { get; set; }
        public Team TeamTwo { get; set; }

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
            var queryStr = Request.QueryString.Value;
            //var queryParamObj = RequestQueryStrParser<QueryParamObj>.Parse(queryStr);
            //var week = queryParamObj.Week;
            TeamOne = new Team(teamIdentifier);
            var rosterOne = await _playerDetailsHelper.GetPlayerDetails(TeamOne.TeamIdentifierIdentifier, weekNum);
            TeamOne.Roster = rosterOne;
            return TeamOne;
            //TeamTwo = new Team(TeamIdentifier.Dustin);
            //var rosterTwo = await _playerDetailsHelper.GetPlayerDetails(TeamTwo.TeamIdentifierIdentifier, week);
            //TeamTwo.Roster = rosterTwo;
        }
    }
}
