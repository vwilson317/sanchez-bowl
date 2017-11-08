using System.Collections.Generic;
using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.da
{
    public interface IDataAccess<T> where T : class
    {
        Task<T> GetAsync<T>(byte id);
        Task<IEnumerable<T>> GetAllAysnc<T>();
        Task SaveAsync<T>(T obj);
    }

    public class DataAccess<T> : IDataAccess<T> where T : class
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
