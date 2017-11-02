using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.webapp.Controllers
{
    public class HomeController : Controller
    {
        public Team TeamOne { get; set; }
        public Team TeamTwo { get; set; }

        private readonly IPlayerDetailsHelper _playerDetailsHelper;
        private readonly TeamConfig _teamConfigOptions;

        public HomeController(IPlayerDetailsHelper playerDetailsHelper, IOptions<TeamConfig> teamConfigAccessor)
        {
            _playerDetailsHelper = playerDetailsHelper;
            _teamConfigOptions = teamConfigAccessor.Value;
        }

        //public async Task OnGet()
        //{
        //    var queryStr = Request.QueryString.Value;
        //    var queryParamObj = RequestQueryStrParser<QueryParamObj>.Parse(queryStr);
        //    var week = queryParamObj.Week;
        //    TeamOne = new Team(TeamIdentifier.Micheal);
        //    var rosterOne = await _playerDetailsHelper.GetPlayerDetails(TeamOne.TeamIdentifierIdentifier, week);
        //    TeamOne.Roster = rosterOne;

        //    TeamTwo = new Team(TeamIdentifier.Dustin);
        //    var rosterTwo = await _playerDetailsHelper.GetPlayerDetails(TeamTwo.TeamIdentifierIdentifier, week);
        //    TeamTwo.Roster = rosterTwo;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
