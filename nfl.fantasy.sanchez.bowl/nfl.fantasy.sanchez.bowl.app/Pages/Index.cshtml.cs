using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
            var queryStr = Request.QueryString.Value;
            var parsedQueryStr = HttpUtility.ParseQueryString(queryStr);
            var queryObj = NameValueCollection(parsedQueryStr);
            TeamOne = new Team(TeamIdentifier.Micheal);
            var rosterOne = await _playerDetailsHelper.GetPlayerDetails(TeamOne.TeamIdentifierIdentifier, queryObj.Week, queryObj.Completed);
            TeamOne.Roster = rosterOne;

            TeamTwo = new Team(TeamIdentifier.Dustin);
            var rosterTwo = await _playerDetailsHelper.GetPlayerDetails(TeamTwo.TeamIdentifierIdentifier, queryObj.Week, queryObj.Completed);
            TeamTwo.Roster = rosterTwo;
        }

        public static QueryValuesObj NameValueCollection(NameValueCollection collection)
        {
            var result = new QueryValuesObj();
            for (int i = 0; i < collection.Keys.Count; i++)
            {
                var key = collection.Keys[i].ToLower();
                var val = collection.GetValues(i)[0];
                result.SetProp(key, val);
            }
            return result;
        }

        public enum QueryParams
        {
            week,
            completed,
            team1,
            team2
        }

        public class QueryValuesObj
        {
            public byte Week { get; set; }
            public bool Completed { get; set; }
            public string Team1 { get; set; }
            public string Team2 { get; set; }

            private readonly string _week = EnumNames.GetName(QueryParams.week);
            private readonly string _completed = EnumNames.GetName(QueryParams.completed);
            private readonly string _team1 = EnumNames.GetName(QueryParams.team1);
            private readonly string _team2 = EnumNames.GetName(QueryParams.team2);

            public void SetProp(string propName, dynamic value)
            {
                if (propName == _week)
                {
                    Week = byte.Parse(value);
                }
                else if (propName == _completed)
                {
                    Completed = bool.Parse(value);
                }
                else if (propName == _team1)
                {
                    Team1 = value;
                }
                else if (propName == _team2)
                {
                    Team2 = value;
                }
            }

            public static class EnumNames
            {
                public static string GetName<T>(T enumType)
                {
                    return Enum.GetName(typeof(T), enumType);
                }
            }
        }
    }
}
