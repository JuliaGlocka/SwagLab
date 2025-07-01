using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;  // WebDriverWait
using SeleniumExtras.WaitHelpers; // ExpectedConditions
using System;

namespace UI.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Constructor with WebDriverWait initialization (timeout 10 seconds)
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Readonly fields as locators
#pragma warning disable S1450
        private readonly By _usernameInput = By.CssSelector("#user-name");
        private readonly By _passwordInput = By.CssSelector("#password");
        private readonly By _loginButton = By.CssSelector("#login-button");
        private readonly By _errorMessage = By.CssSelector(".error-message-container");
#pragma warning restore S1450
        // Wait and find element with wait until
        private IWebElement WaitAndFindElement(By locator)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        // Enter text to Username field and clean with wait
        public void EnterUsername(string username)
        {
            var el = WaitAndFindElement(_usernameInput);
            el.Clear();
            el.SendKeys(username);
        }

        // Enter text to Password field and clean with wait
        public void EnterPassword(string password)
        {
            var el = WaitAndFindElement(_passwordInput);
            el.Clear();
            el.SendKeys(password);
        }

        // Clean Username field with wait
        public void ClearUsername()
        {
            var el = WaitAndFindElement(_usernameInput);
            el.Clear();
        }

        // Clean Password field with wait
        public void ClearPassword()
        {
            var el = WaitAndFindElement(_passwordInput);
            el.Clear();
        }

        // Click login button and wait until clickable
        public void ClickLogin()
        {
            var el = _wait.Until(ExpectedConditions.ElementToBeClickable(_loginButton));
            el.Click();
        }

        // Get error message from the page and wait until visible
        public string GetErrorMessage()
        {
            var el = _wait.Until(ExpectedConditions.ElementIsVisible(_errorMessage));
            return el.Text;
        }

        // Login method that combines username and password entry with login button click
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }
    }
}