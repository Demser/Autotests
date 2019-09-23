using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Coypu;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Remote;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace _365AutomatedTests.Framework
{
    using static Crawler;

    using static DateTime;

    using static ExpectedConditions;

    using static ImageFormat;

    using static Path;

    using static ScenarioContext;

    using static String;

    using static TestContext;

    using static TimeSpan;

    public static class Tools
    {
        private static readonly Func<string> RawScreenName =
            () =>
                Join(" – ", FeatureContext.Current.FeatureInfo.Title, Current.ScenarioInfo.Title,
                    Current.StepContext.StepInfo.Text);

        private static readonly Func<string, string> ReplaceInvalidChars =
            _ => GetInvalidFileNameChars().Aggregate(_, (s, c) => s.Replace(c, '_'));

        private static readonly Func<string, string> CombineWithScreenDir =
            _ => Combine(CurrentContext.TestDirectory, "Screenshots", _);

        private static readonly Func<string, int, string, string> ShortenAndAddExtension =
            (s, max, ext) => Concat(s.Length > max ? s.Substring(0, max - 1) + "…" : s, ext);

        private static readonly Func<string, string> ShortenScreenName = _ => ShortenAndAddExtension(_, 255, ".png");

        public static void TakeScreenshot()
        {
            var _ = ShortenScreenName(CombineWithScreenDir(ReplaceInvalidChars(RawScreenName())));
            WriteLine(_);
            int ScrollTopPosition = GetScrollPosition();
            if (Session.Native.ToString() != "OpenQA.Selenium.PhantomJS.PhantomJSDriver")
            {
                Session.SaveScreenshot(_);
            }
            else
            {
                var driver = ((RemoteWebDriver)Session.Native);
                ITakesScreenshot screenshoter = driver;
                var curWidth = driver.Manage().Window.Size.Width;
                var curHeight = driver.Manage().Window.Size.Height;
                var srcRect = new Rectangle(0, Convert.ToInt32(ScrollTopPosition * 0.795), curWidth, curHeight);
                var destRect = new Rectangle(0, 0, curWidth, curHeight);

                Image image, imageRes = new Bitmap(curWidth, curHeight);
                image = BytesToImage(screenshoter.GetScreenshot().AsByteArray);
                Graphics graphic = Graphics.FromImage(imageRes);

                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image: image, destRect: destRect, srcRect: srcRect, srcUnit: GraphicsUnit.Pixel);

                imageRes.Save(_, Jpeg);

            }
        }
        public static Image BytesToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        private static int GetScrollPosition()
        {
            var GetQuery = "return window.pageYOffset";
            var ScrollTop = new int();
            var driver = ((RemoteWebDriver)Session.Native);
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            var Result = js.ExecuteScript(GetQuery).ToString();
            ScrollTop = Convert.ToInt32(Result);
            return ScrollTop;
        }

        public static void ResizeWindow(int x = 1024, int y = 600) => Session.ResizeTo(x, y);

        public static void CheckInFullHd(Action _)
        {
            ResizeWindow(1920, 1080);
            _.Invoke();
            ResizeWindow();
        }

        public static void JsClick(IWebElement _)
        {
            WriteLine(_.Location);
            ((IJavaScriptExecutor)Session.Native).ExecuteScript("arguments[0].click();", _);
        }

        public static void JsClick(ElementScope _) => JsClick((IWebElement)_.Native);

        public static void JsScroll(int _)
            => ((IJavaScriptExecutor)Session.Native).ExecuteScript($"window.scrollBy(0, {_})");

        public static void SwitchToFrames(IWebDriver driver, string[] frames, int timeout = 10)
        {
            foreach (var f in frames)
            {
                new WebDriverWait(driver, FromSeconds(timeout)).Until(FrameToBeAvailableAndSwitchToIt(f));
            }
        }

        public static void AddAcceptanceCookie(string domain)
        {
            Session.ExecuteScript(
                $"document.cookie = 'CookiesAccepted=CookiesAccepted; domain={domain}; path=/; expires={Now.AddYears(2).ToUniversalTime()};'");
        }

        public static string[] AttributesByName(IEnumerable<ElementScope> elements, string attributeName)
            => elements.Select(_ => _[attributeName]).ToArray();

        public static void ClearTextFromField (ElementScope scope)
        {
            var id = scope.Id;
            var driver = ((RemoteWebDriver)Session.Native);
            driver.FindElementById(id).Clear();
        }

        public static void ClearTextFromFieldByXPath(string xpath)
        {
            var driver = ((RemoteWebDriver)Session.Native);
            driver.FindElementByXPath(xpath).Clear();
        }

        public static void WaitElementByClassName(string className)
        {
            var driver = ((RemoteWebDriver)Session.Native);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(className)));
        }

        public static void WaitClickableElementByXPath(string xpath)
        {
            var driver = ((RemoteWebDriver)Session.Native);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
        }

        public static void WaitInvisibilityOfElementByClassName(string className)
        {
            var driver = ((RemoteWebDriver)Session.Native);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName(className)));
        }

        public static void WaitVisibleElementByXPath(string xpath)
        {
            var driver = ((RemoteWebDriver)Session.Native);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
        }

        public static int getCountElementsByXPath(string xpath)
        {
            var driver = ((RemoteWebDriver)Session.Native);
            //return driver.FindElementsByXPath(xpath).Count();
            return driver.FindElements(By.XPath(xpath)).Count();
        }
    }
}
