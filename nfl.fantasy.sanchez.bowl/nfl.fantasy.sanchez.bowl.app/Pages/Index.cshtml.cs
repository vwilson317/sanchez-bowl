using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.app.Pages
{
    public class IndexModel : PageModel
    {
        public Team TeamOne { get; set; }
        public Team TeamTwo { get; set; }
        
        private readonly IPlayerDetailsHelper _playerDetailsHelper;
        private readonly TeamConfig _teamConfigOptions;

        public IndexModel(IPlayerDetailsHelper playerDetailsHelper, IOptions<TeamConfig> teamConfigAccessor)
        {
            _playerDetailsHelper = playerDetailsHelper;
            _teamConfigOptions = teamConfigAccessor.Value;
        }

        public async Task OnGet()
        {
            TeamOne = new Team(TeamIdentifier.Micheal);
            var rosterOne = await _playerDetailsHelper.GetPlayerDetails(TeamOne.TeamIdentifierIdentifier);
            TeamOne.Roster = rosterOne;

            TeamTwo = new Team(TeamIdentifier.Dustin);
            var rosterTwo = await _playerDetailsHelper.GetPlayerDetails(TeamTwo.TeamIdentifierIdentifier);
            TeamTwo.Roster = rosterTwo;
        }
    }
}
