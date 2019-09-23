using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.Framework
{
    using static Browser;
    using static ChromeDriverService;

    public class CustomChromeProfileSeleniumWebDriver : SeleniumWebDriver
    {
        private static readonly Func<ChromeDriverService> CustomDriverService = () =>
        {
            var service = CreateDefaultService();
            service.HideCommandPromptWindow = true;
            return service;
        };

        private static readonly Func<ChromeOptions> CustomOptions = () =>
        {
            String downloadFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"DownloadedFiles");
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0);
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadFilePath);
            //chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArguments("test-type");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-gpu");
            return chromeOptions;
        };

        public CustomChromeProfileSeleniumWebDriver()
            : base(new ChromeDriver(CustomDriverService(), CustomOptions()), Chrome)
        {
        }
    }
}
