using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttinConcerns.Caching
{
   public interface ICacheManager
   {
       void AddCache(string key, object value, int duration);
       T GetCache<T>(string key);
       object Get(string key); 
       bool IsCache(string key);
       void Remove(string key);
       void RemoveByPattern(string pattern);

    }
}
