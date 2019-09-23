using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using _365AutomatedTests.Framework;

namespace _365AutomatedTests.Steps
{
    using Coypu;
    using System.Linq;
    using static Assert;

    using static Crawler;

    using static String;

    using static Tools;
    [Binding]
    public sealed class CommonSteps
    {


        [AfterStep]
        public static void TakeScreenshotAfterStep()
        {
            //TakeScreenshot();
        }

        // "GIVEN" section--------------------------------------------------------------------------------------------------------


        [Given(@"I go to the site as anonymous")]
        public void GivenIGoToTheSiteAsAnonymous()
        {
            Session.Visit("/");
        }

        // "WHEN" section--------------------------------------------------------------------------------------------------------
        [When(@"I navigate to the (.*) page by the (.*) url")]
        public void WhenINavigateToThePageByTheUrl(string page, string url)
        {
            Session.Visit(url);
        }

        // "THEN" section--------------------------------------------------------------------------------------------------------

        /*
        * binding for all URLs
        */
        [Then(@"The (.*) page should be opened")]
        public void ThenTheURLShouldBeOpenedIn(string url)
        {
            IsTrue(Session.Location.AbsoluteUri.Equals(Concat("http://", QaAppHost, url)));
        }


        [Then(@"The (.*) should be opened in the next tab")]
        public void ThenTheURLShouldBeOpenedInTheNextTab(string url)
        {
            var driver = (IWebDriver)Crawler.Session.Native;
            string _currentWindow = driver.CurrentWindowHandle;
            var linkWindow = driver.WindowHandles[1];
            driver.SwitchTo().Window(linkWindow);
            IsTrue(driver.Url.Equals(url));
            driver.Close();
            driver.SwitchTo().Window(_currentWindow);
        }

        public ElementScope ThenTheTabWithTitleShouldBeOpened(string title)
        {
            var selenium = (IWebDriver)Session.Driver.Native;
            return Session.FindWindow(selenium.WindowHandles.Last()).FindXPath("/html");
        }

        [Then(@"the (.*) title should be correct")]
        public void ThenThePageTitleShouldBeCorrect(string _)
        {
            IsTrue(Session.Title.Contains(_));
        }

        [Then(@"the popup with (.*) title should be closed")]
        // rename step to When user close etc
        public void ThenThePopupWithTitleShouldBeClosed()
        {
            System.Threading.Thread.Sleep(2000);

            var driver = (IWebDriver)Session.Driver.Native;
            string _currentWindow = driver.CurrentWindowHandle;
            //bool exists = false;

            var windows = driver.WindowHandles;
            foreach(var handle in windows)
            {
                driver.SwitchTo().Window(handle);
                if (driver.Url.Contains("/reports/batch/"))
                {
                    //exists=true;
                    break;
                }
                //else if (driver.Url.Contains("/dripSources/"))
                //{
                //    break;
               // }
            }
            //Assert.IsTrue(exists, $"Couldn't found the popup with title {_}");
            driver.Close();

            driver.SwitchTo().Window(_currentWindow);
        }

        public void RefreshPage()
        {
            var driver = (IWebDriver)Session.Driver.Native;
            driver.Navigate().Refresh();
        }

        public void DeleteAllCookies()

        {
            var driver = (IWebDriver)Session.Driver.Native;
            driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}
