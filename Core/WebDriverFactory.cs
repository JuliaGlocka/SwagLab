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
using Microsoft.Extensions.Configuration;

namespace Core
{
    public static class WebDriverFactory
    {
        private static readonly IConfigurationRoot configuration;

        static WebDriverFactory()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static IWebDriver CreateDriver()
        {
            string browser = configuration["Browser"]?.ToLower() ?? "chrome";
            return CreateDriverInternal(browser);
        }

        // Overload to allow specifying browser name
        public static IWebDriver CreateDriver(string browserName)
        {
            string browser = browserName?.ToLower() ?? configuration["Browser"]?.ToLower() ?? "chrome";
            return CreateDriverInternal(browser);
        }

        private static IWebDriver CreateDriverInternal(string browser)
        {
            // Handle empty/null browser names
            if (string.IsNullOrWhiteSpace(browser))
            {
                browser = configuration["Browser"]?.ToLower() ?? "chrome";
            }

            // Handle Safari first (special case for platform check)
            if (browser == "safari")
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return new SafariDriver();
                }
                else
                {
                    throw new PlatformNotSupportedException("Safari is only supported on macOS.");
                }
            }

            // Handle other browsers
            switch (browser)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();

                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();

                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();

                case "internet explorer":
                case "ie":
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    return new InternetExplorerDriver();

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }
        }
    }
}