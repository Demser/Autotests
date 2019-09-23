using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages.Results;
using _365AutomatedTests.UIObjects.Pages;
using Coypu;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Search_Order_By_OrderNumberSteps:Crawler
    {
        ElementScope pageInAnotherTab;

        [Given(@"I login as ""(.*)"":""(.*)"" if not logged yet")]
        public void GivenILoginAsIfNotLoggedYet(string username, string password)
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            if (searchByOrderNumberPage.IsLogged())
            {
                searchByOrderNumberPage.LogoutClick();
            }
            LoginPage loginPage = new LoginPage();
            loginPage.LoginAsUser(username, password);
        }

        [When(@"I go to Results page")]
        public void WhenIGoToResultsPage()
        {
            MainPage mainPage = new MainPage();
            mainPage.GoToResultsView();
            //ScenarioContext.Current.Pending();
        }

        [When(@"I go to tab by order number")]
        public void WhenIGoToTabByOrderNumber()
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            searchByOrderNumberPage.ByOrderNumberTabClick();
            //ScenarioContext.Current.Pending();
        }

        [When(@"I enter order number ""(.*)""")]
        public void WhenIEnterOrderNumber(string orderNumber)
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            searchByOrderNumberPage.FillOrderNumberField(orderNumber);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see order by order number ""(.*)""")]
        public void ThenISeeOrderByOrderNumber(string orderNumber)
        {
            DBHelper dbHelper = new DBHelper();
            var orderData = dbHelper.getOrderDataByOrderNumber(orderNumber);
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            Tools.WaitElementByClassName(searchByOrderNumberPage.OrderItemClassName);
            Assert.AreEqual(searchByOrderNumberPage.getCreatedOnDate(), orderData["createdOn"]);
            Assert.AreEqual(searchByOrderNumberPage.getOrderNumber(), orderNumber);
            Assert.AreEqual(searchByOrderNumberPage.getFIO(), orderData["lastName"] + ' ' + orderData["firstName"] + ' ' + orderData["middleName"]);
            Assert.AreEqual(searchByOrderNumberPage.getEmailAddress(), orderData["emailAddress"]);
            Assert.AreEqual(searchByOrderNumberPage.getCellPhoneNumber(), orderData["cellPhoneNumber"]);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see new page order ""(.*)""")]
        public void ThenISeeNewPageOrder(string orderNumber)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.AreEqual(orderMainPage.getOrderNumber(), orderNumber);
            //ScenarioContext.Current.Pending();
        }

        [When(@"I click Search button")]
        public void WhenIClickSearchButton()
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            searchByOrderNumberPage.SearchButtonClick();
            //ScenarioContext.Current.Pending();
        }

        [When(@"I click OrderDetailsIconShow")]
        public void WhenIClickOrderDetailsIconShow()
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            searchByOrderNumberPage.OrderDetailsIconShowClick();
            //ScenarioContext.Current.Pending();
        }

        [Then(@"order details show")]
        public void ThenOrderDetailsShow()
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            Assert.IsTrue(searchByOrderNumberPage.OrderDetailsExist());
            //ScenarioContext.Current.Pending();
        }

        [When(@"I click OrderDetailsIconHide")]
        public void WhenIClickOrderDetailsIconHide()
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            searchByOrderNumberPage.OrderDetailsIconHideClick();
            //ScenarioContext.Current.Pending();
        }

        [Then(@"order details hide")]
        public void ThenOrderDetailsHide()
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            Assert.IsFalse(searchByOrderNumberPage.OrderDetailsExist());
            //ScenarioContext.Current.Pending();
        }

        [When(@"I click order and go to OrderMainPage")]
        public void WhenIClickOrderAndGoToOrderMainPage()
        {
            if (Session.FindWindow("Отчет").Exists())
            {
                Session.FindWindow("Отчет").ExecuteScript("self.close();");
            }
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            Tools.WaitElementByClassName(searchByOrderNumberPage.OrderItemClassName);
            searchByOrderNumberPage.OrderItemClick();
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see orders by orderNumberPart ""(.*)""")]
        public void ThenISeeOrdersByOrderNumberPart(string orderNumberPart)
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            DBHelper dbHelper = new DBHelper();
            var orderCountFromDB = dbHelper.getOrderCountByOrderNumberPart(orderNumberPart);
            Assert.AreEqual(searchByOrderNumberPage.getCountOfOrders(), orderCountFromDB);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see message ""(.*)""")]
        public void ThenISeeMessage(string message)
        {
            SearchByOrderNumberPage searchByOrderNumberPage = new SearchByOrderNumberPage();
            Assert.AreEqual(searchByOrderNumberPage.getEmptyOrderNumberMessage(), message);
            //ScenarioContext.Current.Pending();
        }

        [AfterScenario("SearchOrderByOrderNumber")]
        public void CloseTab()
        {
            Session.FindWindow("Отчет").ExecuteScript("self.close();");
        }
    }
}
