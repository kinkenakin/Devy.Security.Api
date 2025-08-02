namespace Devy.Security.Api.Caching;

/// <summary>
/// Interface for cache providers - replace the specific
/// </summary>
public interface ICacheProvider
{
    /// <summary>
    /// Gets the number of entries.
    /// </summary>
    /// <value>
    /// The number of entries.
    /// </value>
    int NumberOfEntries { get; }
    /// <summary>
    /// Remove any object based on key
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    bool RemoveFromCache(string key);
    /// <summary>
    /// Gets from cache.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    object GetFromCache(string key);
    /// <summary>
    /// Adds to cache.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    void AddToCache(object data, string key);
}
