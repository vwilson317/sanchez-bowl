using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.app
{
    public interface IDomHelper 
    {
        Task<Roster> LoadPlayerDetails(string loginUrl);
    }
}