using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace SwagLab.Core
{
    public static class Configurator
    {
        private static readonly IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        public static string Browser => _config["Browser"]?.Trim().ToLowerInvariant() ?? "chrome";

        public static IReadOnlyList<string> SupportedBrowsers =>
            _config.GetSection("SupportedBrowsers").Get<List<string>>() ?? new List<string> { "chrome", "firefox", "edge" };
    }
}
