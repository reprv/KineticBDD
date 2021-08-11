using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CalculatorSelenium.Specs.PageObjects
{
    /// <summary>
    /// Calculator Page Object
    /// </summary>
    public class KineticLoginPageObject
    {
        //The URL of the calculator to be opened in the browser
        private const string KineticUrl = "https://motiondevtest.motionkinetic.net/";
        
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;
        
        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 300;

        public KineticLoginPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _webDriver.Manage().Window.Maximize();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(300);
        }

        //Finding elements by ID
        private IWebElement UsernameElement => _webDriver.FindElement(By.Id("Username"));
        private IWebElement PasswordElement => _webDriver.FindElement(By.Id("Password"));
        private IWebElement LoginButtonElement => _webDriver.FindElement(By.TagName("button"));
        private IWebElement ErrorElement => _webDriver.FindElement(By.ClassName("validation-summary-errors"));
        private IWebElement UserError => _webDriver.FindElement(By.Id("Username-error"));
        private IWebElement PassError => _webDriver.FindElement(By.Id("Password-error"));
        public void EnterUsername(string username)
        {
            //Clear text box
            UsernameElement.Clear();
            //Enter text
            UsernameElement.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            //Clear text box
            PasswordElement.Clear();
            //Enter text            
            PasswordElement.SendKeys(password);
        }

        public void ClickLogin()
        {
            //Click the add button
            LoginButtonElement.Click();
            Thread.Sleep(3000);
        }

        public void EnsureLoginPageIsOpenAndReset()
        {
            //Open the calculator page in the browser if not opened yet
            if (_webDriver.Url != KineticUrl)
            {
                _webDriver.Url = KineticUrl;
            }
            //Otherwise reset the calculator by clicking the reset button
            else
            {
                //Wait until the result is empty again
                WaitForEmptyResult();
            }
        }

        public string WaitForNonEmptyResult()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => ErrorElement.GetAttribute("innerText"),
                result => !string.IsNullOrEmpty(result));
        }
        public string WaitForNonEmptyResult1()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => UserError.Text,
                result => !string.IsNullOrEmpty(result));
        }
        public string WaitForNonEmptyResult2()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => PassError.Text,
                result => !string.IsNullOrEmpty(result));
        }

        public string GetCurretUri()
        {
            return _webDriver.Url;
        }

        public string GetKineticUrl()
        {
            return KineticUrl;
        }

        public string WaitForEmptyResult()
        {
            //Wait for the result to be empty
            return WaitUntil(
                () => ErrorElement.GetAttribute("innerText"),
                result => result == string.Empty);
        }

        /// <summary>
        /// Helper method to wait until the expected result is available on the UI
        /// </summary>
        /// <typeparam name="T">The type of result to retrieve</typeparam>
        /// <param name="getResult">The function to poll the result from the UI</param>
        /// <param name="isResultAccepted">The function to decide if the polled result is accepted</param>
        /// <returns>An accepted result returned from the UI. If the UI does not return an accepted result within the timeout an exception is thrown.</returns>
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
