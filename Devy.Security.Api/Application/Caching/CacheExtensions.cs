namespace Devy.Security.Api.Caching;

public static class CacheExtensions
{
    private static ICacheProvider cacheProvider;
    /// <summary>
    /// The cache set
    /// </summary>
    public static bool CacheSet = false;

    /// <summary>
    /// Set which cache provider we are using - must be called be client
    /// </summary>
    /// <param name="provider">The provider.</param>
    public static void SetCacheProvider(ICacheProvider provider)
    {
        cacheProvider = provider;
        CacheSet = true;
    }

    /// <summary>
    /// Removes from cache.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public static bool RemoveFromCache(string key)
    {
        if (cacheProvider != null)
            return cacheProvider.RemoveFromCache(key);

        return false;
    }

    /// <summary>
    /// Gets from cache.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">Please set cache provider (call SetCacheProvider) before using caching</exception>
	public static object GetFromCache(string key)
    {
        if (cacheProvider == null)
            throw new InvalidOperationException("Please set cache provider (call SetCacheProvider) before using caching");

        return cacheProvider.GetFromCache(key);
    }

    /// <summary>
    /// Adds to cache.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    /// <exception cref="System.InvalidOperationException">Please set cache provider (call SetCacheProvider) before using caching</exception>
	public static void AddToCache(object data, string key)
    {
        if (cacheProvider == null)
            throw new InvalidOperationException("Please set cache provider (call SetCacheProvider) before using caching");

        cacheProvider.AddToCache(data, key);
    }

}
