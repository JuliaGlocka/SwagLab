using OpenQA.Selenium;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace PageObject.Test.Tests;
public class WebDriverFactoryTests : IDisposable
{
    private IWebDriver? _driver;

    public void Dispose()
    {
        // Cleanup driver after each test if created
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
        _driver = WebDriverFactory.CreateDriver(browser);
        Assert.NotNull(_driver);
        Assert.Contains(browser, _driver.GetType().Name, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CreateDriver_WithUnsupportedBrowser_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => WebDriverFactory.CreateDriver("opera"));
    }

    [Fact]
    public void CreateDriver_Safari_OnNonMac_ThrowsPlatformNotSupportedException()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // Skip on macOS because Safari is supported
            return;
        }
        Assert.Throws<PlatformNotSupportedException>(() => WebDriverFactory.CreateDriver("safari"));
    }
}
