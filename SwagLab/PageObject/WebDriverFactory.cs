using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

public static class WebDriverFactory
{
    private static IWebDriver? _driver;

    // Mapowanie przeglądarek do odpowiednich konfiguracji
    private static readonly Dictionary<string, Action> DriverSetup = new()
    {
        { "chrome", () => new DriverManager().SetUpDriver(new ChromeConfig()) },
        { "firefox", () => new DriverManager().SetUpDriver(new FirefoxConfig()) },
        { "edge", () => new DriverManager().SetUpDriver(new EdgeConfig()) },
        { "internet explorer", () => new DriverManager().SetUpDriver(new InternetExplorerConfig()) }
    };

    public static IWebDriver GetDriver(string browser)
    {
        if (_driver != null)
            return _driver;

        browser = browser.ToLower();

        // Safari specjalny przypadek, tylko dla macOS
        if (browser.Equals("safari"))
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _driver = new SafariDriver();
            }
            else
            {
                throw new PlatformNotSupportedException("Safari is only supported on macOS.");
            }
        }
        else if (DriverSetup.ContainsKey(browser))
        {
            // Ustawienie drivera dla przeglądarki, której wersja jest w słowniku
            DriverSetup[browser]();
            _driver = CreateDriverForBrowser(browser);
        }
        else
        {
            throw new ArgumentException($"Unsupported browser: {browser}");
        }

        _driver.Manage().Window.Maximize();
        return _driver;
    }

    // Tworzenie odpowiedniego drivera w oparciu o nazwę przeglądarki
    private static IWebDriver CreateDriverForBrowser(string browser)
    {
        switch (browser)
        {
            case "chrome":
                return new ChromeDriver();
            case "firefox":
                return new FirefoxDriver();
            case "edge":
                return new EdgeDriver();
            case "internet explorer":
                return new InternetExplorerDriver();
            default:
                throw new ArgumentException($"Unsupported browser: {browser}");
        }
    }
}
