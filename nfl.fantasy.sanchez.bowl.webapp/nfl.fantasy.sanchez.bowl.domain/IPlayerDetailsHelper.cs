using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public interface IPlayerDetailsHelper
    {
        Task<Roster> GetRoster(TeamIdentifier teamIdentifier, byte week);
    }
}