using System;
using OpenQA.Selenium;

namespace Kookboek.Specs.PageObjects
{
    public class KookboekRegisterPageObject
    {
        
        private const string KookboekUrl = "http://localhost:8080/User/Register";

        private IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 5;

        public KookboekRegisterPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _webDriver.Url = KookboekUrl;
        }

        private IWebElement UsernameField => _webDriver.FindElement(By.XPath("//*[@id=\"Username\"]"));
        private IWebElement PasswordField => _webDriver.FindElement(By.XPath("//*[@id=\"Password\"]"));

        private IWebElement RegisterButton =>
            _webDriver.FindElement(By.XPath("/html/body/div/main/div/div/div/div/div/div/form/button"));

        public void EnterUsername(string username)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
        }

        public void ClickRegister()
        {
            RegisterButton.Click();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

    }
}