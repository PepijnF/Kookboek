using BoDi;
using Kookboek.Specs.Drivers;
using Kookboek.Specs.PageObjects;
using TechTalk.SpecFlow;

namespace Kookboek.Specs.Hooks
{
    [Binding]
    public class CalculatorHooks
    {
        //[Binding]
        //public class SharedBrowserHooks
        //{
        //    [BeforeTestRun]
        //    public static void BeforeTestRun(ObjectContainer testThreadContainer)
        //    {
        //        //Initialize a shared BrowserDriver in the global container
        //        testThreadContainer.BaseContainer.Resolve<BrowserDriver>();
        //    }
        //}
    }
}