using CalculatorSelenium.Specs.Drivers;
using CalculatorSelenium.Specs.PageObjects;
using TechTalk.SpecFlow;

namespace CalculatorSelenium.Specs.Hooks
{
    /// <summary>
    /// Calculator related hooks
    /// </summary>
    [Binding]
    public class LoginPageHooks
    {
        ///<summary>
        ///  Reset the calculator before each scenario tagged with "Calculator"
        /// </summary>
        [BeforeScenario("Login")]
        public static void BeforeScenario(BrowserDriver browserDriver)
        {
            var loginPageObject = new KineticLoginPageObject(browserDriver.Current);
            loginPageObject.EnsureLoginPageIsOpenAndReset();
        }
    }
}