using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using _365AutomatedTests.UIObjects.Pages.Results;
using Coypu;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Add_Remove_ConclusionSteps:Crawler
    {
        ElementScope pageInAnotherTab;

        [Given(@"I remove all conclusions in order ""(.*)""")]
        public void GivenIRemoveAllConclusionsInOrder(string orderNumber)
        {
            DBHelper dBHelper = new DBHelper();
            dBHelper.removeAllConclusionsFromOrder(orderNumber);
            //ScenarioContext.Current.Pending();
        }

        [When(@"I go to Conclusions page")]
        public void WhenIGoToConclusionsPage()
        {
            MainPage mainPage = new MainPage();
            mainPage.GoToConclusions();
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter order number ""(.*)"" on ConclusionsPage")]
        public void WhenIEnterOrderNumberOnConclusionsPage(string orderNumber)
        {
            ConclusionsPage conclusionsPage = new ConclusionsPage();
            conclusionsPage.FillOrderNumberField(orderNumber);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter order dateFrom ""(.*)""")]
        public void WhenIEnterOrderDateFrom(string dateFrom)
        {
            ConclusionsPage conclusionsPage = new ConclusionsPage();
            conclusionsPage.FillDateFromField(dateFrom);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter order dateTo ""(.*)""")]
        public void WhenIEnterOrderDateTo(string dateTo)
        {
            ConclusionsPage conclusionsPage = new ConclusionsPage();
            conclusionsPage.FillDateFromField(dateTo);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I click conclusion items and go to OrderMainPage")]
        public void WhenIClickConclusionItemsAndGoToOrderMainPage()
        {
            ConclusionsPage conclusionsPage = new ConclusionsPage();
            conclusionsPage.ConclusionItemsClick();
            //ScenarioContext.Current.Pending();
        }

        [When(@"I upload conclusions by ""(.*)"" and ""(.*)""")]
        public void WhenIUploadConclusionsByAnd(string hxid, string conclusionFileName)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.Conclusion_Upload(hxid, conclusionFileName);
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I see conclusion block")]
        public void ThenISeeConclusionBlock()
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.IsTrue(orderMainPage.ConclusionBlockExist());
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see conclusion by ""(.*)"" in conclusion block")]
        public void ThenISeeConclusionByInConclusionBlock(string hxid)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.IsTrue(orderMainPage.ConclusionExist(hxid));
            //ScenarioContext.Current.Pending();
        }

        [Given(@"I see conclusions in database by ""(.*)""")]
        public void GivenISeeConclusionsInDatabaseBy(string orderNumber)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            DBHelper dBHelper = new DBHelper();
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            var test = orderMainPage.getCounclusionCount();
            Assert.AreEqual(Convert.ToInt32(dBHelper.getCounclusionCountByOrderNumber(orderNumber)), 3/*orderMainPage.getCounclusionCount()*/);
            //ScenarioContext.Current.Pending();
        }

        [When(@"I remove conclusions by ""(.*)"" and ""(.*)""")]
        public void WhenIRemoveConclusionsByAnd(string hxid, string conclusionFileName)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.Conclusion_Delete(hxid, conclusionFileName);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I not see conclusion by ""(.*)"" in conclusion block")]
        public void ThenINotSeeConclusionByInConclusionBlock(string hxid)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.IsFalse(orderMainPage.ConclusionExist(hxid));
            //ScenarioContext.Current.Pending();
        }

        [Given(@"I not see conclusions in database by ""(.*)""")]
        public void GivenINotSeeConclusionsInDatabaseBy(string orderNumber)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            DBHelper dBHelper = new DBHelper();
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.AreEqual(Convert.ToInt32(dBHelper.getCounclusionCountByOrderNumber(orderNumber)), 0/*orderMainPage.getCounclusionCount()*/);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see remove confirm dialog")]
        public void ThenISeeRemoveConfirmDialog()
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.IsTrue(orderMainPage.ConclusionRemoveConfirmDialogTitleExist());
            //ScenarioContext.Current.Pending();
        }

        [When(@"I not confirm remove")]
        public void WhenINotConfirmRemove()
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.NoConfirm();
            //ScenarioContext.Current.Pending();
        }

        [When(@"I confirm remove")]
        public void WhenIConfirmRemove()
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.Confirm();
            //ScenarioContext.Current.Pending();
        }

        [When(@"I donwload conclusions by ""(.*)""")]
        public void WhenIDonwloadConclusionsBy(string hxid)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.Conclusion_Download(hxid);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see conclusions in DownloadedFiles")]
        public void ThenISeeConclusionsInDownloadedFiles()
        {
            bool exists = false;
            String downloadFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"DownloadedFiles");
            var downloadedFiles = Directory.GetFiles(downloadFilePath);
            foreach (var filename in downloadedFiles)
            {
                var lastWrite = File.GetLastWriteTime(filename);
                if (lastWrite > DateTime.Now.AddMinutes(-5))
                {
                    string shortFileName = filename.Replace(downloadFilePath + "\\", "");
                    if (shortFileName.EndsWith(".pdf"))
                    {
                        string preguid = shortFileName.Replace(".pdf", "");
                        if (Guid.TryParse(preguid, out Guid guid))
                        {
                            exists = true;
                            break;
                        }
                    }
                }
            }
            Assert.IsTrue(exists, "File not found");
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see message ""(.*)"" in OrderMainPage")]
        public void ThenISeeMessageInOrderMainPage(string message)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.AreEqual(orderMainPage.getUploadConclusionMessage(), message);
            Tools.WaitInvisibilityOfElementByClassName(orderMainPage.MessageClassName);
            //ScenarioContext.Current.Pending();
        }
    }
}
