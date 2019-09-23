using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.UIObjects.Pages.Results;
using _365AutomatedTests.UIObjects.Pages;
using NUnit.Framework;
using Coypu;
using _365AutomatedTests.Framework;
using System.Threading;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Calculating_Age_In_OrderSteps:Crawler
    {
        ElementScope pageInAnotherTab;

        [When(@"I change birthdate as ""(.*)""")]
        public void WhenIChangeBirthdateAs(string birthDate)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.EditButtonClick();
            OrderEditPage orderEditPage = new OrderEditPage(pageInAnotherTab);
            orderEditPage.FillBirthdateField(birthDate);
            orderEditPage.SaveButtonClick();
            //ScenarioContext.Current.Pending();
        }

        [Then(@"age is ""(.*)""")]
        public void ThenAgeIs(string age)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.AreEqual(orderMainPage.getFactAge(), age);
            //ScenarioContext.Current.Pending();
        }

        [AfterScenario("CalculatingAgeInOrder")]
        public void CloseTab()
        {
            Session.FindWindow("Отчет").ExecuteScript("self.close();");
        }
    }
}
