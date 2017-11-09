using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.da
{
    public interface IQueryable
    {
        byte Id { get; set; }
    }

    public interface IDataAccess<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAysnc();
        Task SaveAsync(T obj);
    }

    public class DataAccess<T> : IDataAccess<T> where T : class
    {
        //TODO: make config value
        private string _file = $"{Directory.GetCurrentDirectory()}\\SanchezBowl-{System.DateTime.Now.Year}-{typeof(T).Name}.txt";

        private string thisName = typeof(T).Name.ToLower();
        public async Task<IEnumerable<T>> GetAllAysnc()
        {
            return await GetRecords(_file);
        }

        public async Task<T> GetAsync(int id)
        {
            var recs = await GetRecords(_file);
            //lastest rec will be append to the file

            return recs.LastOrDefault(x => (int)x.GetType().GetProperty("Id").GetValue(x) == id);
        }

        public async Task SaveAsync(T obj)
        {
            var recs = await GetRecords(_file);
            recs = recs.Append(obj);
            var json = JsonConvert.SerializeObject(recs);
            await File.WriteAllTextAsync(_file, json);
        }

        private async Task<IEnumerable<T>> GetRecords(string file)
        {
            var recs = Enumerable.Empty<T>();
            FileStream fileStream;
            if (!File.Exists(_file))
            {
                fileStream = File.Create(_file);
            }
            else
            {
                using (var sr = new StreamReader(File.OpenRead(_file)))
                {
                    var text = await File.ReadAllTextAsync(_file);
                    if (!string.IsNullOrEmpty(text))
                    {
                        recs = JsonConvert.DeserializeObject<IEnumerable<T>>(text);
                    }
                }
            }
            return recs;
        }
    }
}
