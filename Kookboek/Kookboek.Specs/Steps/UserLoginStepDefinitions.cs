using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kookboek.Specs.Drivers;
using Kookboek.Specs.PageObjects;
using TechTalk.SpecFlow;
using Xunit;

namespace Kookboek.Specs.Steps
{
    [Binding]
    public sealed class UserLoginStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly KookboekHomePageObject _homePageObject;
        private readonly KookboekLoginPageObject _loginPageObject;
        private readonly KookboekLoggedInPageObject _loggedInPageObject;

        public UserLoginStepDefinitions(BrowserDriver browserDriver)
        {
            _homePageObject = new KookboekHomePageObject(browserDriver.Current);
            _loginPageObject = new KookboekLoginPageObject(browserDriver.Current);
            _loggedInPageObject = new KookboekLoggedInPageObject(browserDriver.Current);

        }


        [When("the actor goes to the login page")]
        public void WhenTheActorGoesToTheLoginPage()
        {
            _homePageObject.ClickLogIn();
        }

        [Given("the username is (.*)")]
        public void GivenTheUsernameIsTestUser(string username)
        {
            _loginPageObject.EnterUsername(username);
        }

        [Given("password is (.*)")]
        public void GivenPasswordIsTestPassord(string password)
        {
            _loginPageObject.EnterPassword(password);
        }

        [When("the login button is pressed")]
        public void WhenTheLoginButtonIsPressed()
        {
            _loginPageObject.ClickLogin();
        }

        [Then("the actor should be logged in")]
        public void ThenTheActorShouldBeLoggedIn()
        {
            Assert.True(_loggedInPageObject.IsLoggedIn());
        }

        [Then("the actor should not be logged in")]
        public void ThenTheActorShouldNotBeLoggedIn()
        {
            Assert.False(_loggedInPageObject.IsLoggedIn());
        }
    }
}