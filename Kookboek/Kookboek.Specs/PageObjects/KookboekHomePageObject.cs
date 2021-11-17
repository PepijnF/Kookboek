using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Kookboek.Specs.PageObjects
{
    public class KookboekHomePageObject
    {
        private const string KookboekUrl = "https://localhost:5001/";

        private IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 5;

        public KookboekHomePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _webDriver.Url = KookboekUrl;
        }

        private IWebElement LogInButton => _webDriver.FindElement(By.CssSelector("a.btn:nth-child(1)"));
        private IWebElement SignUpButton => _webDriver.FindElement(By.CssSelector("a.btn:nth-child(2)"));

        public void ClickLogIn()
        {
            LogInButton.Click();
        }

        public void ClickSignUp()
        {
            SignUpButton.Click();
        }
        
        private T WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T: class
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            return wait.Until(driver =>
            {
                var result = getResult();
                if (!isResultAccepted(result))
                    return default;

                return result;
            });
        }
    }
}