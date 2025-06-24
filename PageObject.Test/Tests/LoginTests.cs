using System;
using System.Collections.Generic;
using Xunit;
using OpenQA.Selenium;
using PageObject;

namespace PageObject.Test
{
    public class LoginTests : IDisposable
    {
        private IWebDriver _driver;
        private string _browser;

        public LoginTests()
        {

        }
        private void InitializeDriver(string browser)
        {
            _browser = browser;
            _driver = WebDriverFactory.GetDriver(browser);
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        // Cleanup after each test
        public void Dispose()
        {
            WebDriverFactory.QuitDriver();
        }

        // Data Provider - tests (browser, username, password, clearFields, expectedError, successExpected)
        public static IEnumerable<object[]> LoginTestData =>
            new List<object[]>
            {
                // UC-1: Both cleared
                new object[] { "edge", "someuser", "somepass", true, "Epic sadface: Username is required", false },

                // UC-2: Password cleared
                new object[] { "firefox", "someuser", "somepass", true, "Epic sadface: Password is required", false },

                // UC-3: Valid credentials (one accepted username is "standard_user")
                new object[] { "edge", "standard_user", "secret_sauce", false, null, true },

                new object[] { "firefox", "standard_user", "secret_sauce", false, null, true },
            };

        [Theory]
        [MemberData(nameof(LoginTestData))]
        public void TestLoginForm(string browser, string username, string password, bool clearFields, string expectedErrorMessage, bool successExpected)
        {
            InitializeDriver(browser);
            Console.WriteLine($"Starting test on browser: {browser}");

            var loginPage = new LoginPage(_driver);

            // Enter username and password
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);

            // Clear inputs if needed depending on test
            if (clearFields)
            {
                // For UC-1 clear username and password
                if (expectedErrorMessage.Contains("Username"))
                    loginPage.ClearUsername();

                if (expectedErrorMessage.Contains("Password"))
                    loginPage.ClearPassword();
            }

            loginPage.ClickLogin();

            if (successExpected)
            {
                // Check if the main page (dashboard) is loaded by checking the title
                var title = _driver.Title;
                Console.WriteLine($"Page title after login: {title}");
                Assert.Equal("Swag Labs", title);
            }
            else
            {
                var errorMsg = loginPage.GetErrorMessage();
                Console.WriteLine($"Error message shown: {errorMsg}");
                Assert.Contains(expectedErrorMessage, errorMsg);
            }
        }
    }
}
