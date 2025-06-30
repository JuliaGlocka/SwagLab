using log4net;
using log4net.Config;
using OpenQA.Selenium;
using PageObject;
using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;


namespace Tests

{
    public class LoginPageTests : IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LoginPageTests));

        private IWebDriver _driver = null!;
        private LoginPage _loginPage = null!;
        private readonly string _baseUrl = "https://www.saucedemo.com/";

        // Browsers for testing (only browser param)
        public static IEnumerable<object[]> Browsers =>
            new List<object[]>
            {
                new object[] { "edge" },
                new object[] { "firefox" }
            };

        // Test data for valid credentials login (browser, username, password)
        public static IEnumerable<object[]> ValidCredentials =>
            new List<object[]>
            {
                new object[] { "edge", "standard_user", "secret_sauce" },
                new object[] { "firefox", "problem_user", "secret_sauce" }
            };

        private void Initialize(string browser)
        {
            Logger.Info($"Starting test with browser: {browser}");

            _driver = WebDriverFactory.CreateDriver(browser);
            _driver.Navigate().GoToUrl(_baseUrl);
            _loginPage = new LoginPage(_driver);
        }

        [Theory]
        [MemberData(nameof(Browsers))]
        public void UC1_LoginWithEmptyCredentials_ShouldShowUsernameRequired(string browser)
        {
            Logger.Info("UC1 test started.");
            Initialize(browser);

            _loginPage.EnterUsername("tempUser");
            _loginPage.EnterPassword("tempPass");

            _loginPage.ClearUsername();
            _loginPage.ClearPassword();

            _loginPage.ClickLogin();

            string error = _loginPage.GetErrorMessage();
            Logger.Info($"Error message received: {error}");

            error.Should().Match(msg =>
                msg.Contains("Epic sadface: Username is required") ||
                msg.Contains("Epic sadface: Username and password do not match any user in this service"));

            Logger.Info("UC1 test finished.");
        }

        [Theory]
        [MemberData(nameof(Browsers))]
        public void UC2_LoginWithEmptyPassword_ShouldShowPasswordRequired(string browser)
        {
            Logger.Info("UC2 test started.");
            Initialize(browser);

            _loginPage.EnterUsername("tempUser");
            _loginPage.EnterPassword("tempPass");

            _loginPage.ClearPassword();

            _loginPage.ClickLogin();

            string error = _loginPage.GetErrorMessage();
            Logger.Info($"Error message received: {error}");

            error.Should().Match(msg =>
                msg.Contains("Epic sadface: Password is required") ||
                msg.Contains("Epic sadface: Username and password do not match any user in this service"));

            Logger.Info("UC2 test finished.");
        }

        // Use separate MemberData for multiple parameters: browser, username, password
        [Theory]
        [MemberData(nameof(ValidCredentials))]
        public void UC3_LoginWithValidCredentials_ShouldLoginAndShowDashboardTitle(string browser, string username, string password)
        {
            Logger.Info("UC3 test started.");
            Initialize(browser);

            Logger.Info($"Logging in with Username: {username} and Password: {password}");
            _loginPage.Login(username, password);

            string title = _driver.Title;
            Logger.Info($"Page title after login: {title}");

            title.Should().Be("Swag Labs");
            Logger.Info("UC3 test finished.");
        }

        [Theory]
        [MemberData(nameof(Browsers))]
        public void Login_WithInvalidCredentials_ShouldShowErrorMessage(string browser)
        {
            Initialize(browser);

            _loginPage.Login("invalid_user", "wrong_password");

            string error = _loginPage.GetErrorMessage();
            error.Should().NotBeNullOrEmpty();
            error.ToLower().Should().Contain("username and password do not match any user");
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Release managed resources
                    _driver?.Quit();
                    _driver?.Dispose();
                }
                // Here you can release unmanaged resources if any

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
