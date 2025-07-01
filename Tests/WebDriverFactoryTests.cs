using log4net;
using OpenQA.Selenium;
using PageObject.Test;
using SwagLab.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace Tests
{
    public class WebDriverFactoryTests : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WebDriverFactoryTests));
        private IWebDriver? _driver;

        static WebDriverFactoryTests()
        {
            LogInitializer.Initialize();
        }

        public void Dispose()
        {
            Logger.Info("Disposing WebDriver...");
            _driver?.Quit();
            _driver?.Dispose();
            _driver = null;
        }

        public static IEnumerable<object[]> GetSupportedBrowsers()
        {
            var supported = Configurator.SupportedBrowsers;
            return supported.Select(b => new object[] { b });
        }

        [Fact]
        public void CreateDriver_WithUnsupportedBrowser_ThrowsArgumentException()
        {
            Logger.Info("Testing unsupported browser: opera");

            Assert.Throws<ArgumentException>(() =>
            {
                _driver = WebDriverFactory.CreateDriver("opera");
            });
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

            Assert.Throws<PlatformNotSupportedException>(() =>
            {
                _driver = WebDriverFactory.CreateDriver("safari");
            });
        }
    }
}
