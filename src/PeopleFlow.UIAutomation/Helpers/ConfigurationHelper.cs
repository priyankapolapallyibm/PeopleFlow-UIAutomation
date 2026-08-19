using Microsoft.Extensions.Configuration;
using PeopleFlow.UIAutomation.Config;

namespace PeopleFlow.UIAutomation.Helpers;

public static class ConfigurationHelper
{
    private static IConfigurationRoot? _config;

    public static IConfigurationRoot Configuration => _config ??= BuildConfiguration();

    private static IConfigurationRoot BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }

    public static AppSettings GetAppSettings()
    {
        var settings = new AppSettings();
        Configuration.GetSection("AppSettings").Bind(settings);
        return settings;
    }

    public static Credentials GetCredentials()
    {
        var creds = new Credentials();
        Configuration.GetSection("Credentials").Bind(creds);
        return creds;
    }
}
