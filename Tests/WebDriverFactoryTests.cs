using log4net;
using OpenQA.Selenium;
using PageObject.Test;

namespace Tests;

public class WebDriverFactoryTests : IDisposable
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(WebDriverFactoryTests));
    private IWebDriver? _driver;
    private bool _disposed = false;  // flaga zabezpieczająca przed wielokrotnym Dispose

    static WebDriverFactoryTests()
    {
        LogInitializer.Initialize();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Logger.Info("Disposing WebDriver...");
                _driver?.Quit();
                _driver?.Dispose();
                _driver = null;
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // ... reszta testów i metod ...
}
