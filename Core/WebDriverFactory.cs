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
        public static IWebDriver CreateDriver(string browserName = null)
        {
            string browser = (browserName ?? Configurator.Browser).Trim().ToLowerInvariant();

            if (!Configurator.SupportedBrowsers.Contains(browser))
                throw new ArgumentException($"Browser '{browser}' is not supported. Supported browsers: {string.Join(", ", Configurator.SupportedBrowsers)}");

            return browser switch
            {
                "chrome" => CreateChromeDriver(),
                "firefox" => CreateFirefoxDriver(),
                "edge" => CreateEdgeDriver(),
                "safari" when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => CreateSafariDriver(),
                "safari" => throw new PlatformNotSupportedException("Safari is only supported on macOS."),
                "ie" or "internet explorer" => CreateInternetExplorerDriver(),
                _ => throw new ArgumentException($"Browser '{browser}' is not supported. Supported browsers:\n{string.Join(", ", Configurator.SupportedBrowsers)}")
            };
        }

        private static IWebDriver CreateChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver();
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new FirefoxDriver();
        }

        private static IWebDriver CreateEdgeDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            return new EdgeDriver();
        }

        private static IWebDriver CreateSafariDriver()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                throw new PlatformNotSupportedException("Safari is only supported on macOS.");
            return new SafariDriver();
        }

        private static IWebDriver CreateInternetExplorerDriver()
        {
            new DriverManager().SetUpDriver(new InternetExplorerConfig());
            return new InternetExplorerDriver();
        }
    }
}
