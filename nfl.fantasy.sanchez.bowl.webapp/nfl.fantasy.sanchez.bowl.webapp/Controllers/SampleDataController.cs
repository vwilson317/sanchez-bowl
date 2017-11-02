using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.webapp.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Team TeamOne { get; set; }
        public Team TeamTwo { get; set; }

        private readonly IPlayerDetailsHelper _playerDetailsHelper;
        private readonly TeamConfig _teamConfigOptions;

        public SampleDataController(IPlayerDetailsHelper playerDetailsHelper, IOptions<TeamConfig> teamConfigAccessor)
        {
            _playerDetailsHelper = playerDetailsHelper;
            _teamConfigOptions = teamConfigAccessor.Value;
        }

        [HttpGet("[action]")]
        public async Task<Team> Teams()
        {
            var queryStr = Request.QueryString.Value;
            var queryParamObj = RequestQueryStrParser<QueryParamObj>.Parse(queryStr);
            var week = queryParamObj.Week;
            TeamOne = new Team(TeamIdentifier.Micheal);
            var rosterOne = await _playerDetailsHelper.GetPlayerDetails(TeamOne.TeamIdentifierIdentifier, week);
            TeamOne.Roster = rosterOne;
            return TeamOne;
            //TeamTwo = new Team(TeamIdentifier.Dustin);
            //var rosterTwo = await _playerDetailsHelper.GetPlayerDetails(TeamTwo.TeamIdentifierIdentifier, week);
            //TeamTwo.Roster = rosterTwo;
        }
    }
}
