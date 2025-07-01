using Microsoft.Extensions.Configuration;

namespace SwagLab.Core
{
    public static class Configurator
    {
        private static IConfiguration? _config;

        public static IConfiguration Config =>
            _config ??= new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

        // Używamy Config do bezpiecznego odczytu, żeby wymusić inicjalizację
        public static string Browser => Config["Browser"] ?? throw new InvalidOperationException("Browser setting missing.");

        public static List<string> SupportedBrowsers =>
            Config.GetSection("SupportedBrowsers").Get<List<string>>() ?? new List<string>();
    }
}