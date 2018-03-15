using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;

namespace Business.Common
{
    public static class GlobalCache
    {
        private static ObjectCache cache = MemoryCache.Default;

        public static void Insert(string key, object value)
        {
            if (value != null && Settings.Enabled)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                if (Settings.SlidingExpiration != TimeSpan.Zero)
                {
                    policy.SlidingExpiration = Settings.SlidingExpiration;
                }
                if (Settings.CacheExpiration != TimeSpan.Zero)
                {
                    policy.AbsoluteExpiration = DateTime.Now.Add(Settings.CacheExpiration);
                }
                cache.Add(key, value, policy);
            }
        }

        /// <summary>
        /// Get Cached Object
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return (Settings.Enabled ? cache[key] : null);
        }
        //public static object GetCopy(string key)
        //{
        //    object retValue = (Settings.Enabled ? cache[key] : null);
        //    return retValue.CloneJson();
        //}

        /// <summary>
        /// Remove Cached object
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Remove(string key)
        {
            return cache.Remove(key);
        }

        /// <summary>
        /// Remove all cached objects
        /// </summary>
        public static void RemoveAll()
        {
            IDictionaryEnumerator cacheEnumerator = (IDictionaryEnumerator)((IEnumerable)cache).GetEnumerator();
            while (cacheEnumerator.MoveNext())
            {
                Remove(cacheEnumerator.Key.ToString());
            }
        }
        public static void SetCacheItem(string key, object value)
        {
            cache.Set(key, value, DateTime.Now.Add(Settings.CacheExpiration));
        }

        #region Cached Dynamic Method Call

        public static T1 ExecuteCache<T1>(Type source, string methodName, params object[] args)
        {
            if (methodName.Contains('.'))
            {
                methodName = methodName.Substring(methodName.LastIndexOf(".") + 1);
            }
            if (Settings.Enabled)
            {
                string cacheKey = source.Name + "_" + methodName + (args != null && args.Length > 0 ? "_" + String.Join("_", args.Select(t => t.ToString()).ToArray<string>()) : string.Empty);
                if (GlobalCache.Get(cacheKey) == null)
                {
                    MethodInfo obMethodInfo = source.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, null, ExtensionHelper.GetTypes(args), null);
                    GlobalCache.Insert(cacheKey, obMethodInfo.Invoke(null, args));
                }
                return (T1)GlobalCache.Get(cacheKey);
            }
            else
            {

                return (T1)source.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, null, ExtensionHelper.GetTypes(args), null).Invoke(null, args);
            }
        }

        /// <summary>
        /// Cache Results of Instance Methods
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="source"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns>retuns T1</returns>
        public static T1 ExecuteInstanceCache<T1>(Type source, string methodName, params object[] args)
        {
            if (methodName.Contains('.'))
            {
                methodName = methodName.Substring(methodName.LastIndexOf(".") + 1);
            }
            ConstructorInfo obConstructorInfo = source.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
            if (Settings.Enabled)
            {
                string cacheKey = source.Name + "_" + methodName + (args != null && args.Length > 0 ? "_" + String.Join("_", args.Select(t => t.ToString()).ToArray<string>()) : string.Empty);
                if (GlobalCache.Get(cacheKey) == null)
                {
                    object obClass = obConstructorInfo.Invoke(new object[] { });
                    MethodInfo obMethodInfo = source.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, ExtensionHelper.GetTypes(args), null);
                    GlobalCache.Insert(cacheKey, obMethodInfo.Invoke(obClass, args));
                }
                return (T1)GlobalCache.Get(cacheKey);
            }
            else
            {
                object obClass = obConstructorInfo.Invoke(new object[] { });
                return (T1)source.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, ExtensionHelper.GetTypes(args), null).Invoke(obClass, args);
            }
        }

        #endregion
    }
}
