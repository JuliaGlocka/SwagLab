using Microsoft.Extensions.Configuration;
using System.IO;

public static class Configurator
{
    private static IConfigurationRoot _configuration;

    static Configurator()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // or path to Core
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string Browser => _configuration["Browser"];
}