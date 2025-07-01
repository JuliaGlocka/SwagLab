using PageObject.Test;
﻿using OpenQA.Selenium;
using System;
using System.Runtime.InteropServices;
using Xunit;
using log4net;
using log4net.Config;
using SwagLab.Core;

namespace Tests
{
    public class WebDriverFactoryTests : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WebDriverFactoryTests));

        // Static constructor to ensure log4net is initialized only once
        static WebDriverFactoryTests()
        {
            LogInitializer.Initialize(); // Ennusre log4net is initialized once for all tests
        }

        private IWebDriver? _driver;

        public void Dispose()
        {
            Logger.Info("Disposing WebDriver...");
            _driver?.Quit();
            _driver?.Dispose();
            _driver = null;
        }

        [Theory]
        [InlineData("chrome")]
        [InlineData("firefox")]
        [InlineData("edge")]
        public void CreateDriver_WithSupportedBrowser_ReturnsDriver(string browser)
        {
            Logger.Info($"Test started for browser: {browser}");

            _driver = WebDriverFactory.CreateDriver();

            Logger.Info($"Driver created: {_driver?.GetType().Name}");

            Assert.NotNull(_driver);
            Assert.Contains(browser, _driver.GetType().Name, StringComparison.OrdinalIgnoreCase);

            Logger.Info("Test passed.");
        }

        [Fact]
        public void CreateDriver_WithUnsupportedBrowser_ThrowsArgumentException()
        {
            Logger.Info("Testing unsupported browser: opera");
            Assert.Throws<ArgumentException>(() => WebDriverFactory.CreateDriver());
        }

        [Fact]
        public void CreateDriver_Safari_OnNonMac_ThrowsPlatformNotSupportedException()
        {
            Logger.Info("Testing Safari driver on non-macOS platform");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Logger.Warn("Skipping test: running on macOS.");
                return;
            }

            Assert.Throws<PlatformNotSupportedException>(() => WebDriverFactory.CreateDriver());
        }
    }
}
