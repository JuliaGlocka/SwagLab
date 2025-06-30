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

public static class WebDriverFactory
{
    private static readonly IConfigurationRoot configuration;

    static WebDriverFactory()
    {
        configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static IWebDriver CreateDriver()
    {
        string browser = configuration["Browser"]?.ToLower() ?? "chrome";

        if (browser == "safari")
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return new SafariDriver();
            else
                throw new PlatformNotSupportedException("Safari is only supported on macOS.");
        }

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
                new DriverManager().SetUpDriver(new InternetExplorerConfig());
                return new InternetExplorerDriver();

            default:
                throw new ArgumentException($"Unsupported browser: {browser}");
        }
    }
}
