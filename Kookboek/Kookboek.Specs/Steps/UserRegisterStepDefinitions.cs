using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kookboek.Specs.Drivers;
using Kookboek.Specs.PageObjects;
using TechTalk.SpecFlow;

namespace Kookboek.Specs.Steps
{
    [Binding]
    public sealed class UserRegisterStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly KookboekHomePageObject _homePageObject;
        private readonly KookboekRegisterPageObject _registerPageObject;

        public UserRegisterStepDefinitions(BrowserDriver browserDriver)
        {
            _homePageObject = new KookboekHomePageObject(browserDriver.Current);
            _registerPageObject = new KookboekRegisterPageObject(browserDriver.Current);
        }
        
        [When("the actor goes to the register page")]
        public void WhenTheActorGoesToTheRegisterPage()
        {
            _homePageObject.ClickSignUp();    
        }

        [Given("the new username is (.*)")]
        public void GivenTheNewUsernameIsTestregiser(string username)
        {
            _registerPageObject.EnterUsername(username);
        }

        [Given(@"the new password is (.*)")]
        public void GivenTheNewPasswordIsTestregister(string password)
        {
            _registerPageObject.EnterPassword(password);
        }

        [When(@"the register button is pressed")]
        public void WhenTheRegisterButtonIsPressed()
        {
            _registerPageObject.ClickRegister();
        }
    }
}