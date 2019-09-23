using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages.Results;
using Coypu;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Search_Order_By_SampleNumberSteps:Crawler
    {
        ElementScope pageInAnotherTab;

        [When(@"I go to tab by sample number")]
        public void WhenIGoToTabBySampleNumber()
        {
            SearchBySampleNumberPage searchBySampleNumberPage = new SearchBySampleNumberPage();
            searchBySampleNumberPage.BySampleNumberTabClick();
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter sample number ""(.*)""")]
        public void WhenIEnterSampleNumber(string sampleNumber)
        {
            SearchBySampleNumberPage searchBySampleNumberPage = new SearchBySampleNumberPage();
            searchBySampleNumberPage.FillSampleNumberField(sampleNumber);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter sample dateFrom ""(.*)""")]
        public void WhenIEnterSampleDateFrom(string dateFrom)
        {
            SearchBySampleNumberPage searchBySampleNumberPage = new SearchBySampleNumberPage();
            searchBySampleNumberPage.FillDateFromField(dateFrom);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter sample dateTo ""(.*)""")]
        public void WhenIEnterSampleDateTo(string dateTo)
        {
            SearchBySampleNumberPage searchBySampleNumberPage = new SearchBySampleNumberPage();
            searchBySampleNumberPage.FillDateToField(dateTo);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see orders by sampleNumber ""(.*)"":""(.*)"":""(.*)""")]
        public void ThenISeeOrdersBySampleNumber(string sampleNumber, string dateFromStr, string dateToStr)
        {
            DBHelper dbHelper = new DBHelper();
            DateTime dateFrom = DateTime.ParseExact(dateFromStr, "dd/MM/yyyy", null);
            DateTime dateTo = DateTime.ParseExact(dateToStr, "dd/MM/yyyy", null);
            var orderData = dbHelper.getOrderDataBySampleNumber(sampleNumber, dateFrom, dateTo);
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            Tools.WaitElementByClassName(searchByOrderNumberPage.OrderItemClassName);
            Assert.AreEqual(searchByOrderNumberPage.getCreatedOnDate(), orderData["createdOn"]);
            Assert.AreEqual(searchByOrderNumberPage.getOrderNumber(), orderData["orderNumber"]);
            Assert.AreEqual(searchByOrderNumberPage.getFIO(), orderData["lastName"] + ' ' + orderData["firstName"] + ' ' + orderData["middleName"]);
            Assert.AreEqual(searchByOrderNumberPage.getEmailAddress(), orderData["emailAddress"]);
            Assert.AreEqual(searchByOrderNumberPage.getCellPhoneNumber(), orderData["cellPhoneNumber"]);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see orders by sampleNumberPart ""(.*)"":""(.*)"":""(.*)""")]
        public void ThenISeeOrdersBySampleNumberPart(string sampleNumberPart, string dateFromStr, string dateToStr)
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            DBHelper dbHelper = new DBHelper();
            DateTime dateFrom = DateTime.ParseExact(dateFromStr, "dd/MM/yyyy", null);
            DateTime dateTo = DateTime.ParseExact(dateToStr, "dd/MM/yyyy", null);
            var orderCountFromDB = dbHelper.getOrderCountBySampleNumberPart(sampleNumberPart, dateFrom, dateTo);
            Assert.AreEqual(searchByOrderNumberPage.getCountOfOrders(), orderCountFromDB);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see new page order by sampleNumber ""(.*)"":""(.*)"":""(.*)""")]
        public void ThenISeeNewPageOrderBySampleNumber(string sampleNumber, string dateFromStr, string dateToStr)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            DBHelper dbHelper = new DBHelper();
            DateTime dateFrom = DateTime.ParseExact(dateFromStr, "dd/MM/yyyy", null);
            DateTime dateTo = DateTime.ParseExact(dateToStr, "dd/MM/yyyy", null);
            var orderData = dbHelper.getOrderDataBySampleNumber(sampleNumber, dateFrom, dateTo);
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.AreEqual(orderMainPage.getOrderNumber(), orderData["orderNumber"]);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see no orders")]
        public void ThenISeeNoOrders()
        {
            SearchBySampleNumberPage searchBySampleNumberPage = new SearchBySampleNumberPage();
            Assert.IsTrue(searchBySampleNumberPage.NoOrdersMessageExist());
            //ScenarioContext.Current.Pending();
        }

        [AfterScenario("SearchOrderBySampleNumber")]
        public void CloseTab()
        {
            Session.FindWindow("Отчет").ExecuteScript("self.close();");
        }
    }
}
