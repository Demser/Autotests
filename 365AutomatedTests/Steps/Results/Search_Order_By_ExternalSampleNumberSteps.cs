using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages.Results;
using Coypu;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Search_Order_By_ExternalSampleNumberSteps:Crawler
    {
        ElementScope pageInAnotherTab;

        [When(@"I go to tab by external sample number")]
        public void WhenIGoToTabByExternalSampleNumber()
        {
            SearchByExternalSampleNumberPage searchByExternalSampleNumberPage = new SearchByExternalSampleNumberPage();
            searchByExternalSampleNumberPage.ByExternalSampleNumberTabClick();
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter external sample number ""(.*)""")]
        public void WhenIEnterExternalSampleNumber(string externalSampleNumber)
        {
            SearchByExternalSampleNumberPage searchByExternalSampleNumberPage = new SearchByExternalSampleNumberPage();
            searchByExternalSampleNumberPage.FillExternalSampleNumberField(externalSampleNumber);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter external sample dateFrom ""(.*)""")]
        public void WhenIEnterExternalSampleDateFrom(string dateFrom)
        {
            SearchByExternalSampleNumberPage searchByExternalSampleNumberPage = new SearchByExternalSampleNumberPage();
            searchByExternalSampleNumberPage.FillDateFromField(dateFrom);
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter external sample dateTo ""(.*)""")]
        public void WhenIEnterExternalSampleDateTo(string dateTo)
        {
            SearchByExternalSampleNumberPage searchByExternalSampleNumberPage = new SearchByExternalSampleNumberPage();
            searchByExternalSampleNumberPage.FillDateToField(dateTo);
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I see orders by externalSampleNumber ""(.*)"":""(.*)"":""(.*)""")]
        public void ThenISeeOrdersByExternalSampleNumber(string externalSampleNumber, string dateFromStr, string dateToStr)
        {
            DBHelper dbHelper = new DBHelper();
            DateTime dateFrom = DateTime.ParseExact(dateFromStr, "dd/MM/yyyy",null);
            DateTime dateTo = DateTime.ParseExact(dateToStr, "dd/MM/yyyy",null);
            var orderData = dbHelper.getOrderDataByExternalSampleNumber(externalSampleNumber, dateFrom, dateTo);
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            Tools.WaitElementByClassName(searchByOrderNumberPage.OrderItemClassName);
            Assert.AreEqual(searchByOrderNumberPage.getCreatedOnDate(), orderData["createdOn"]);
            Assert.AreEqual(searchByOrderNumberPage.getOrderNumber(), orderData["orderNumber"]);
            Assert.AreEqual(searchByOrderNumberPage.getFIO(), orderData["lastName"] + ' ' + orderData["firstName"] + ' ' + orderData["middleName"]);
            Assert.AreEqual(searchByOrderNumberPage.getEmailAddress(), orderData["emailAddress"]);
            Assert.AreEqual(searchByOrderNumberPage.getCellPhoneNumber(), orderData["cellPhoneNumber"]);
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I see new page order by externalSampleNumber ""(.*)"":""(.*)"":""(.*)""")]
        public void ThenISeeNewPageOrderByExternalSampleNumber(string externalSampleNumber, string dateFromStr, string dateToStr)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            DBHelper dbHelper = new DBHelper();
            DateTime dateFrom = DateTime.ParseExact(dateFromStr, "dd/MM/yyyy", null);
            DateTime dateTo = DateTime.ParseExact(dateToStr, "dd/MM/yyyy", null);
            var orderData = dbHelper.getOrderDataByExternalSampleNumber(externalSampleNumber, dateFrom, dateTo);
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.AreEqual(orderMainPage.getOrderNumber(), orderData["orderNumber"]);
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I see orders by externalSampleNumberPart ""(.*)"":""(.*)"":""(.*)""")]
        public void ThenISeeOrdersByExternalSampleNumberPart(string externalSampleNumberPart, string dateFromStr, string dateToStr)
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            DBHelper dbHelper = new DBHelper();
            DateTime dateFrom = DateTime.ParseExact(dateFromStr, "dd/MM/yyyy", null);
            DateTime dateTo = DateTime.ParseExact(dateToStr, "dd/MM/yyyy", null);
            var orderCountFromDB = dbHelper.getOrderCountByExternalSampleNumberPart(externalSampleNumberPart, dateFrom, dateTo);
            Assert.AreEqual(searchByOrderNumberPage.getCountOfOrders(), orderCountFromDB);
            //ScenarioContext.Current.Pending();
        }

        [AfterScenario("SearchOrderByExternalSampleNumber")]
        public void CloseTab()
        {
            Session.FindWindow("Отчет").ExecuteScript("self.close();");
        }
    }
}
