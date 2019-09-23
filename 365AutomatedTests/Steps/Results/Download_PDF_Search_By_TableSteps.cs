using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages.Results;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Download_Pdf_Search_By_TableSteps
    {
        [When(@"I go to tab by table")]
        public void WhenIGoToTabByTable()
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            searchByTablePage.ByTableTabClick();
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter dateFrom ""(.*)""")]
        public void WhenIEnterDateFrom(string dateFrom)
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            searchByTablePage.FillDateFromField(dateFrom);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter dateTo ""(.*)""")]
        public void WhenIEnterDateTo(string dateTo)
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            searchByTablePage.FillDateToField(dateTo);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter account ""(.*)""")]
        public void WhenIEnterAccount(string account)
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            searchByTablePage.FillAccountField(account);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter hxid ""(.*)""")]
        public void WhenIEnterHxid(string hxid)
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            searchByTablePage.FillHxidField(hxid);
            //ScenarioContext.Current.Pending();
        }

        [Given(@"I remove PDF file if exists from DownloadedFiles")]
        public void GivenIRemovePDFFileIfExistsFromDownloadedFiles()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DownloadedFiles\\results.pdf");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            //ScenarioContext.Current.Pending();
        }


        [When(@"I click button save PDF")]
        public void WhenIClickButtonSavePDF()
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            searchByTablePage.PDFSaveButtonClick();
            //ScenarioContext.Current.Pending();
        }

        [Then(@"button save PDF unactive")]
        public void ThenButtonSavePDFUnactive()
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            Assert.IsTrue(searchByTablePage.PDFSaveButtonIsDisabled(), "PDFSaveButton is enabled");
            //ScenarioContext.Current.Pending();
        }

        [Then(@"button save PDF active")]
        public void ThenButtonSavePDFActive()
        {
            SearchByTablePage searchByTablePage = new SearchByTablePage();
            Tools.WaitElementByClassName(searchByTablePage.OrderItemClassName);
            Assert.IsFalse(searchByTablePage.PDFSaveButtonIsDisabled(), "PDFSaveButton is disabled");
            //ScenarioContext.Current.Pending();
        }

        [Then(@"download PDF file")]
        public void ThenDownloadPDFFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DownloadedFiles\\results.pdf");
            Thread.Sleep(3000);
            Assert.IsTrue(File.Exists(path), "File has not been downloaded.");
            //ScenarioContext.Current.Pending();
        }
    }
}
