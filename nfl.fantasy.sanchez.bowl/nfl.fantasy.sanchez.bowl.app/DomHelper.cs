using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.app
{
    public class DomHelper : IDomHelper
    {
        private readonly IHtmlWebWrapper _htmlWebWrapper;
        private readonly TeamConfig _teamConfigAccessor;

        public DomHelper(IHtmlWebWrapper htmlWebWrapper, IOptions<TeamConfig> teamConfigAccessor)
        {
            _htmlWebWrapper = htmlWebWrapper;
            _teamConfigAccessor = teamConfigAccessor.Value;
        }

        //Important notes: loads the first teams roster size. 
        //as of 10-24-2017 once nfl.com determines the week is over the url changes 
        public async Task<Roster> LoadPlayerDetails(string loginUrl)
        {
            var roster = new Roster();
            var doc = await _htmlWebWrapper.LoadFromWebAsync(loginUrl);

            var xpathQuery = "//tbody//tr";

            var rosterNodes = doc.DocumentNode.SelectNodes(xpathQuery);

            var playerInfoCells = rosterNodes.ToList();
            for (var i = 0; i < _teamConfigAccessor.RosterSize; i++)
            {
                var currentPlayInfoCell = playerInfoCells[i];
                if (!currentPlayInfoCell.InnerText.ToLower().Equals("bench"))
                {
                    var playerInfo = new PlayerDetails
                    {
                        Name = GetNodeInnerText(currentPlayInfoCell, (n) => n.Descendants().FirstOrDefault(d => d.Name == "a")),
                        Position = GetNodeInnerText(currentPlayInfoCell, (n) => n.Descendants().FirstOrDefault(d => d.Name == "em")).ToUpper(),
                        Score = double.Parse(GetNodeInnerText(rosterNodes[i], (n) => n.Descendants().LastOrDefault()))
                    };

                    if (i < _teamConfigAccessor.StartersCount)
                    {
                        roster.Starters.Add(playerInfo);
                    }
                    else
                    {
                        roster.Bench.Add(playerInfo);
                    }
                }
            }

            return roster;
        }

        private string GetNodeInnerText(HtmlNode node, Func<HtmlNode, HtmlNode> func)
        {
            var thisNode = func(node);
            var txt = thisNode.InnerText;
            return txt;
        }
    }
}
