using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public interface IDomHelper 
    {
        Task<Roster> LoadPlayerDetails(string loginUrl);
    }
}