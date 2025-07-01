using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Runtime.InteropServices;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SwagLab.Core
{
    public static class WebDriverFactory
    {
        private static readonly IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        private static string ConfiguredBrowser =>
            _config["Browser"]?.Trim().ToLowerInvariant() ?? "chrome";

        public static IWebDriver CreateDriver() =>
            CreateDriverInternal(ConfiguredBrowser);

        public static IWebDriver CreateDriver(string browserName) =>
            CreateDriverInternal(string.IsNullOrWhiteSpace(browserName)
                ? ConfiguredBrowser
                : browserName.Trim().ToLowerInvariant());

        private static IWebDriver CreateDriverInternal(string browser)
        {
            switch (browser)
            {
                case "safari":
                    if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        throw new PlatformNotSupportedException("Safari is only supported on macOS.");
                    return new SafariDriver();

                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();

                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();

                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();

                case "ie":
                case "internet explorer":
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    return new InternetExplorerDriver();

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }
        }
    }
}
