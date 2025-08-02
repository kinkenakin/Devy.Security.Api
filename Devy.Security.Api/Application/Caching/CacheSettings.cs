namespace Devy.Security.Api.Caching;

/// <summary>
/// This class defines properties for Cache settings
/// </summary>
internal class CacheSettings
{
    /// <summary>
    /// Gets or sets the cache key.
    /// </summary>
    /// <value>
    /// The cache key.
    /// </value>
    public string CacheKey { get; set; }
    /// <summary>
    /// Gets or sets the region.
    /// </summary>
    /// <value>
    /// The region.
    /// </value>
    public string Region { get; set; }
    /// <summary>
    /// Gets or sets the duration for the cache object.
    /// </summary>
    /// <value>
    /// The duration.
    /// </value>
    public int Duration { get; set; }
    /// <summary>
    /// Gets or sets the priority.
    /// </summary>
    /// <value>
    /// The priority.
    /// </value>
    public int Priority { get; set; }
    /// <summary>
    /// Gets or sets the list of data tags.
    /// </summary>
    /// <value>
    /// The data tags.
    /// </value>
    public List<string> DataTags { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this instance is sliding cache.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is sliding cache; otherwise, <c>false</c>.
    /// </value>
    public bool IsSlidingCache { get; set; }
}
