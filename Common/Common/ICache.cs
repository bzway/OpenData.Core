using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bzway.Common.Share
{
    public interface ICache
    {

        T Get<T>(string key, Func<T> call, int timeOut = 0);
        IList<string> GetAllKey();

        bool Remove(string key = "");

    }

    public class DefaultCache : ICache
    {

        public DefaultCache()
        {


        }
        public T Get<T>(string key, Func<T> call, int timeOut = 0)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetAllKey()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key = "")
        {
            throw new NotImplementedException();
        }
    }

    public class RedisCache : ICache
    {
        public RedisCache()
        {
        }
        public T Get<T>(string key, Func<T> call, int timeOut = 0)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetAllKey()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key = "")
        {
            throw new NotImplementedException();
        }
    }
}
