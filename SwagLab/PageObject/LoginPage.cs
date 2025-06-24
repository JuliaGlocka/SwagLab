using OpenQA.Selenium;

namespace PageObject
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Constructors
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators
        private By UsernameInput => By.CssSelector("#user-name");
        private By PasswordInput => By.CssSelector("#password");
        private By LoginButton => By.CssSelector("#login-button");
        private By ErrorMessage => By.CssSelector("h3[data-test='error']");

        // Actions
        public void EnterUsername(string username)
        {
            var el = _driver.FindElement(UsernameInput);
            el.Clear();
            el.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            var el = _driver.FindElement(PasswordInput);
            el.Clear();
            el.SendKeys(password);
        }

        public void ClearUsername() => _driver.FindElement(UsernameInput).Clear();

        public void ClearPassword() => _driver.FindElement(PasswordInput).Clear();

        public void ClickLogin() => _driver.FindElement(LoginButton).Click();

        public string GetErrorMessage() => _driver.FindElement(ErrorMessage).Text;
    }
}
