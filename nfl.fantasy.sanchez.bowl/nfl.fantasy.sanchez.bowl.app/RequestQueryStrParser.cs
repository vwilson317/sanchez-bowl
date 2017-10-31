using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace nfl.fantasy.sanchez.bowl.app
{
    public static class RequestQueryStrParser<T> where T  : class, new()
    {
        public static T Parse(string queryString)
        {
            var collection = HttpUtility.ParseQueryString(queryString);

            var result = new T();
            var properties = typeof(T).GetProperties();

            for (int i = 0; i < collection.Keys.Count; i++)
            {
                var key = collection.Keys[i].ToLower();
                var val = collection.GetValues(i)[0];

                var prop = properties.FirstOrDefault(x => x.Name.ToLower().Equals(key));
                var converter = TypeDescriptor.GetConverter(prop?.PropertyType);
                var convertedVal = converter.ConvertFrom(val);
                prop?.SetValue(result, convertedVal);
            }
            return result;
        }
    }
}