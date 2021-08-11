using CalculatorSelenium.Specs.Drivers;
using CalculatorSelenium.Specs.PageObjects;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CalculatorSelenium.Specs.Steps
{
    [Binding]
    public sealed class KineticLoginStepDefinitions
    {
        //Page Object for Calculator
        private readonly KineticLoginPageObject _loginPageObject;

        public KineticLoginStepDefinitions(BrowserDriver browserDriver)
        {
            _loginPageObject = new KineticLoginPageObject(browserDriver.Current);
        }

        [Given("the username is (.*)")]
        public void GivenTheUsername(string username)
        {
            //delegate to Page Object
            _loginPageObject.EnterUsername(username);
        }

        [Given("the password is (.*)")]
        public void GivenThePassword(string password)
        {
            //delegate to Page Object
            _loginPageObject.EnterPassword(password);
        }

        [When("the login button is clicked")]
        public void WhenTheLoginButtonClicked()
        {
            //delegate to Page Object
            _loginPageObject.ClickLogin();
            
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string expectedResult)
        {
            if (expectedResult.Equals("Invalid username or password."))
            {
                var actualResult = _loginPageObject.WaitForNonEmptyResult();
                actualResult.Should().Be(expectedResult);
            }
            else if (expectedResult.Equals("The Username field is required."))
            {
                var actualResult = _loginPageObject.WaitForNonEmptyResult1();
                actualResult.Should().Be(expectedResult);
            }
            else if (expectedResult.Equals("The Password field is required."))
            {
                var actualResult = _loginPageObject.WaitForNonEmptyResult2();
                actualResult.Should().Be(expectedResult);
            }
            else
            {
                var expectedUrl = _loginPageObject.GetKineticUrl();
                var actualUrl = _loginPageObject.GetCurretUri();
                Assert.AreNotEqual(expectedUrl, actualUrl);
            }
        }
    }
}
