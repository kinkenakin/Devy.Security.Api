using System.Runtime.Caching;

namespace Devy.Security.Api.Caching;

/// <summary>
/// Enterprise library cache provider
/// </summary>
public class InMemCacheProvider : ICacheProvider
{
    private static object locker = new object();
    private static MemoryCache _cache;
    private static List<CacheSettings> _cacheSettings = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemCacheProvider" /> class.
    /// </summary>
    public InMemCacheProvider()
    {
        if (_cache == null)
            _cache = GetInstance();
    }

    /// <summary>
    /// Flushes the cache
    /// </summary>
    public void Flush()
    {
        lock (locker)
        {
            foreach (var item in _cache.AsEnumerable())
                this.RemoveFromCache(item.Key);
        }
    }

    /// <summary>
    /// Gets the cache provider instance
    /// </summary>
    /// <returns></returns>
    public static MemoryCache GetInstance()
    {
        lock (locker)
        {
            if (_cache == null)
            {
                _cache = MemoryCache.Default;
            }
        }

        return _cache;
    }


    /// <summary>
    /// Loads the cache setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    private CacheSettings LoadCacheSetting(string key)
    {
        lock (locker)
        {
            if (string.IsNullOrEmpty(key) || _cacheSettings == null) return null;

            return
                (from cs in _cacheSettings?.ToList()
                 where cs.CacheKey.TrimEnd().ToUpper() == key.TrimEnd().ToUpper()
                   || (cs.CacheKey.StartsWith("%") && key.TrimEnd().ToUpper().Contains(cs.CacheKey.TrimEnd().ToUpper().Substring(2)))
                   || (cs.CacheKey.EndsWith("%") && key.TrimEnd().ToUpper().Contains(cs.CacheKey.TrimEnd().ToUpper().Substring(0, cs.CacheKey.TrimEnd().Length - 1)))
                 select cs).FirstOrDefault();
        }
    }

    public void AddCacheSetting(string key, string region, int durationInSeconds, bool isSlidingCache, int priority, List<string> dataTags)
    {
        lock (locker)
        {
            if (string.IsNullOrEmpty(key) || _cacheSettings == null) _cacheSettings = new List<CacheSettings>();

            if (!_cacheSettings.Any(t => t.CacheKey == key))
                _cacheSettings.Add(new CacheSettings()
                {
                    CacheKey = key,
                    Duration = durationInSeconds,
                    Region = region,
                    Priority = priority,
                    DataTags = dataTags,
                    IsSlidingCache = isSlidingCache
                });
            //else
            //{
            //    var cacheSetting = _cacheSettings.FirstOrDefault(t => t.CacheKey == key);
            //    cacheSetting.Duration = durationInSeconds;
            //    cacheSetting.IsSlidingCache = isSlidingCache;
            //    cacheSetting.Region = region;
            //    cacheSetting.Priority = priority;
            //    cacheSetting.DataTags = dataTags;
            //}
        }
    }

    /// <summary>
    /// Removes item from cache
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool RemoveFromCache(string key)
    {
        if (_cache == null)
            _cache = GetInstance();

        try
        {
            _cache.Remove(key);

            var cacheSetting = _cacheSettings.FirstOrDefault(t => t.CacheKey == key);
            if (cacheSetting != null) _cacheSettings.Remove(cacheSetting);
        }
        catch
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Gets from cache.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public object GetFromCache(string key)
    {
        if (key == string.Empty || key == null)
            return null;

        if (_cache == null)
            _cache = GetInstance();

        return _cache[key];
    }

    /// <summary>
    /// Adds to cache.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    public void AddToCache(object data, string key)
    {
        if (key == null || data == null)
            return;

        CacheSettings cacheSettings = LoadCacheSetting(key);
        CacheItemPolicy policy = new CacheItemPolicy()
        {
            Priority = System.Runtime.Caching.CacheItemPriority.Default,
            RemovedCallback = (CacheEntryRemovedArguments arguments) =>
            {
            },
            AbsoluteExpiration = DateTimeOffset.MaxValue
        };

        if (cacheSettings != null && cacheSettings.Duration > 0)
        {
            if (cacheSettings.IsSlidingCache)
                policy.SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Duration);
            else
                policy.AbsoluteExpiration = DateTime.Now.AddSeconds(cacheSettings.Duration);
        }

        if (_cache == null)
            _cache = GetInstance();

        _cache.Add(key, data, policy);
    }

    /// <summary>
    /// Gets the number of entries.
    /// </summary>
    /// <value>
    /// The number of entries.
    /// </value>
    public int NumberOfEntries
    {
        get
        {
            if (_cache == null)
                return 0;

            return _cache.Count();
        }
    }
}
