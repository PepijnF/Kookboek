using System;
using OpenQA.Selenium;

namespace Kookboek.Specs.PageObjects
{
    public class KookboekLoggedInPageObject
    {
        
        private const string KookboekUrl = "https://localhost:5001/";

        private IWebDriver _webDriver;
        public const int DefaultWaitInSeconds = 5;

        public KookboekLoggedInPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _webDriver.Url = KookboekUrl;
        }

        public bool IsLoggedIn()
        {
            try
            {
                _webDriver.FindElement(By.CssSelector(".rounded-circle"));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}