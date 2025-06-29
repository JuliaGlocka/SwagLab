using OpenQA.Selenium;
using PageObject;
using System;
using Xunit;

namespace PageObject.Test
{
    public class LoginPageTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        // Constructor - create a new driver and navigate to the page
        public LoginPageTests()
        {
            // You can change the browser here: "firefox", "edge", etc.
            _driver = WebDriverFactory.CreateDriver("chrome");
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");  // Your tested site URL

            _loginPage = new LoginPage(_driver);
        }

        [Fact]
        public void Login_WithValidCredentials_ShouldNotShowError()
        {
            _loginPage.Login("standard_user", "secret_sauce");

            // Simple assertion that no error message is shown
            var pageSource = _driver.PageSource.ToLower();
            Assert.DoesNotContain("error", pageSource);
        }

        [Fact]
        public void Login_WithInvalidCredentials_ShouldShowErrorMessage()
        {
            _loginPage.Login("invalid_user", "wrong_password");

            string error = _loginPage.GetErrorMessage();
            Assert.False(string.IsNullOrEmpty(error));
            Assert.Contains("username and password do not match any user", error.ToLower());
        }

        // Clean up resources (close the browser)
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
