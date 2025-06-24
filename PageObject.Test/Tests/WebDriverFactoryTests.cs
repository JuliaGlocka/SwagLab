using OpenQA.Selenium;
using PageObject;
using System;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Sdk;

namespace PageObject.Test
{
    public class WebDriverFactoryTests : IDisposable
    {
        public WebDriverFactoryTests()
        {
            WebDriverFactory.QuitDriver(); // Ensure clean state before each test
        }

        public void Dispose()
        {
            WebDriverFactory.QuitDriver(); // Cleanup after each test
        }

        [Theory]
        [InlineData("chrome")]
        [InlineData("firefox")]
        [InlineData("edge")]
        public void GetDriver_WithSupportedBrowser_ReturnsCorrectDriver(string browser)
        {
            // Act
            var driver = WebDriverFactory.GetDriver(browser);

            // Assert
            Assert.NotNull(driver);
            Assert.Contains(browser, driver.GetType().Name, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetDriver_WithSafari_DoesNotThrow()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return;

            var driver = WebDriverFactory.GetDriver("Safari");
            Assert.NotNull(driver);
            driver.Quit();
        }

        [Fact]
        public void GetDriver_WithInvalidBrowser_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => WebDriverFactory.GetDriver("opera"));
        }

        [Fact]
        public void GetDriver_WhenAlreadyInitialized_ReturnsSameInstance()
        {
            var driver1 = WebDriverFactory.GetDriver("chrome");
            var driver2 = WebDriverFactory.GetDriver("chrome");

            Assert.Same(driver1, driver2);
        }

        [Fact]
        public void QuitDriver_DisposesDriverCorrectly()
        {
            var driver = WebDriverFactory.GetDriver("Chrome");
            driver.Quit();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                var _ = driver.Url;
            });
        }
    }
}
