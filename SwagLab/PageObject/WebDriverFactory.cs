using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.IE;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace PageObject
{
    public static class WebDriverFactory
    {
        private static IWebDriver? _driver;

        public static IWebDriver GetDriver(string browser)
        {
            if (_driver != null)
                return _driver;

            switch (browser.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _driver = new ChromeDriver();
                    break;

                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    _driver = new FirefoxDriver();
                    break;

                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    _driver = new EdgeDriver();
                    break;

                case "safari":
                    _driver = new SafariDriver();
                    break;

                case "ie":
                case "internet explorer":
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    _driver = new InternetExplorerDriver();
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }

            _driver.Manage().Window.Maximize();
            return _driver;
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}
