using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.dataaccess
{
    public interface ITeamDataAccess<Team>
    {

    }
    public class TeamDataAccess : DataAccessBase<Team>, ITeamDataAccess<Team>
    {

    }

    public interface IDataAccess<T> where T : class
    {
        Task<T> GetAsync<T>(byte id);
        Task<IEnumerable<T>> GetAllAysnc<T>();
        Task SaveAsync<T>(T obj);
    }

    public abstract class DataAccessBase<T> : IDataAccess<T> where T : class
    {
        public Task<IEnumerable<T1>> GetAllAysnc<T1>()
        {
            throw new System.NotImplementedException();
        }

        public Task<T1> GetAsync<T1>(byte id)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync<T1>(T1 obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
