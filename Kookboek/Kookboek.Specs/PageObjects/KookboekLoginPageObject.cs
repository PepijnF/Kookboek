using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;

namespace Kookboek.Specs.PageObjects
{
    public class KookboekLoginPageObject
    {
        
        private const string KookboekUrl = "https://localhost:5001/User/Login";

        private IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 5;

        public KookboekLoginPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _webDriver.Url = KookboekUrl;
        }

        private IWebElement UserNameField => _webDriver.FindElement(By.Id("Username"));
        private IWebElement PasswordField => _webDriver.FindElement(By.Id("Password"));
        private IWebElement LoginButton => _webDriver.FindElement(By.XPath("/html/body/div/main/div/div/div/div/div/div/form/button"));

        public void EnterUsername(string username)
        {
            UserNameField.Clear();
            UserNameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
        }

        public void ClickLogin()
        {
            LoginButton.Click();
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
    }
}