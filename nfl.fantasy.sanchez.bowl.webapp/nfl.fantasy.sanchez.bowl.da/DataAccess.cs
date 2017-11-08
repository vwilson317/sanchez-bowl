using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.da
{
    public interface IQueryable
    {
        byte Id { get; set; }
    }

    public interface IDataAccess<T> where T : class, new()
    {
        Task<T> GetAsync<T>(int id) where T : new();
        Task<IEnumerable<T>> GetAllAysnc<T>();
        Task SaveAsync<T>(T obj);
    }

    public class DataAccess<T> : IDataAccess<T> where T : class, new()
    {
        //TODO: make config value
        private const string db = @"SanchezBowl.db";
        private string thisName = typeof(T).Name.ToLower();
        public Task<IEnumerable<T>> GetAllAysnc<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetAsync<T>(int id) where T : new()
        {
            using (var db = new LiteDatabase(@"SanchezBowl.db"))
            {
                var dbObjs = db.GetCollection<T>(thisName);
                var result = dbObjs.Find(Query.EQ("Id", id))
                    .Select(x => new T()).ToList();
                return Task.FromResult(default(T));
            }
        }

        public async Task SaveAsync<T>(T obj)
        {
            using (var db = new LiteDatabase(@"SanchezBowl.db"))
            {
                var dbObjs = db.GetCollection<T>(thisName);
                dbObjs.EnsureIndex("Id");
                var result = await Task.FromResult(dbObjs.Insert(obj));
            }
        }
    }
}
