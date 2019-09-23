using System;
using Coypu;
using Coypu.Drivers;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Framework
{

    using static Browser;

    using static TimeSpan;

    using static Tools;

    [Binding]
    public class Crawler
    {
        public const string QaAppHost = "https://365.stab.helix.ru/";
        
        public static BrowserSession Session { get; set; }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            var sessionConfiguration = new SessionConfiguration
            {
                Browser = Chrome,
                AppHost = QaAppHost,
                WaitBeforeClick = FromSeconds(1)
            };
            //Session = new BrowserSession(sessionConfiguration/*, new CustomPhantomJsSeleniumWebDriver()*/);
            Session = new BrowserSession(sessionConfiguration, new CustomChromeProfileSeleniumWebDriver());
           
            ResizeWindow(1920, 1080);                 
            Session.Visit("/");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Session.Dispose();
         }
    }
}
