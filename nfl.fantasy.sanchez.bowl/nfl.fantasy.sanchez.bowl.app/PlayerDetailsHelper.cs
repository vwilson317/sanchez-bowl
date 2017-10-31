﻿using System.Collections.Generic;
using System.Threading.Tasks;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.app
{
    public class PlayerDetailsHelper : IPlayerDetailsHelper{
        private readonly IDomHelper _domHelper;
        public const string LeagueUrl = "http://fantasy.nfl.com/league/448915/team/";
        public const string GameCenter = "/gamecenter?week=";

        public PlayerDetailsHelper(IDomHelper domHelper){
            _domHelper = domHelper;
        }

        public async Task<Roster> GetPlayerDetails(TeamIdentifier teamIdentifier, byte week, bool previousWeek = false){
            var teamId = teamIdentifier.GetHashCode();
            string url = $"{LeagueUrl}{teamId}";
            if (previousWeek)
            {
                url += $"/gamecenter?gameCenterTab=track&trackType=sbs&week={week}";
            }
            else
            {
                url += $"{GameCenter}{week}";
            }
            var playerInfo = await _domHelper.LoadPlayerDetails(url);
            return playerInfo;
        }
    }
}