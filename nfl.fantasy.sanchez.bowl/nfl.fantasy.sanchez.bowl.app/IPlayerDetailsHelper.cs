using System.Collections.Generic;
using System.Threading.Tasks;
using nfl.fantasy.sanchez.bowl.domain;

namespace nfl.fantasy.sanchez.bowl.app
{
    public interface IPlayerDetailsHelper
    {
        Task<Roster> GetPlayerDetails(TeamIdentifier teamIdentifier, byte week);
    }
}