using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bzway.Common.Share
{
    public interface ICacheManager
    {

        T Get<T>(string key, Func<T> call, int timeOut = 0);
        IList<string> GetAllKey();

        bool Remove(string key = "");
        bool IsSet(string key);
        void Set(string key, object value, int timeOut = 0);
    }

    public class DefaultCache : ICacheManager
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

        public bool IsSet(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key = "")
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, int timeOut = 0)
        {
            throw new NotImplementedException();
        }
    }

    public class RedisCache : ICacheManager
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

        public bool IsSet(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key = "")
        {
            throw new NotImplementedException();
        }
        public void Set(string key, object value, int timeOut = 0)
        {
            throw new NotImplementedException();
        }
    }
}
