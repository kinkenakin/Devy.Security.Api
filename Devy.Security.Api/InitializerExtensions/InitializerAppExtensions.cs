namespace Devy.Security.Api.InitializerExtensions;

public static class InitializerAppExtensions
{
    /// <summary>
    /// Initializes the application settings.
    /// </summary>
    /// <param name="appSettings">The application settings.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static AppSettings InitializeApplicationSettings(this AppSettings appSettings, IConfiguration configuration)
    {
        if (configuration == null)
            return appSettings;

        appSettings = appSettings ?? new AppSettings();
        configuration.Bind(appSettings);

        return appSettings;
    }
}
