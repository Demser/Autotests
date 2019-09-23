using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages.Results;
using Coypu;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Display_Birthdate_Height_WeightSteps: Crawler
    {
        ElementScope pageInAnotherTab;

        [When(@"I change height as ""(.*)""")]
        public void WhenIChangeHeightAs(string height)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.EditButtonClick();
            OrderEditPage orderEditPage = new OrderEditPage(pageInAnotherTab);
            orderEditPage.FillHeightField(height);
            orderEditPage.SaveButtonClick();
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I change weight as ""(.*)""")]
        public void WhenIChangeWeightAs(string weight)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            orderMainPage.EditButtonClick();
            OrderEditPage orderEditPage = new OrderEditPage(pageInAnotherTab);
            orderEditPage.FillWeigthField(weight);
            orderEditPage.SaveButtonClick();
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I see birthdate is ""(.*)""")]
        public void ThenISeeBirthdateIs(string birthdate)
        {
            pageInAnotherTab = Session.FindWindow("Отчет").FindXPath("/html");
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.AreEqual(orderMainPage.getBirthdate(), birthdate);
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I see height is ""(.*)""")]
        public void ThenISeeHeightIs(string height)
        {
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            if (Convert.ToInt32(height) > 0)
            {
                Assert.AreEqual(orderMainPage.getHeight(), height);
            }
            else
            {
                Assert.IsFalse(orderMainPage.IsVisibleHeight());
            }
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I see weight is ""(.*)""")]
        public void ThenISeeWeightIs(string weigth)
        {
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            if (Convert.ToInt32(weigth) > 0)
            {
                Assert.AreEqual(orderMainPage.getWeigth(), weigth);
            }
            else
            {
                Assert.IsFalse(orderMainPage.IsVisibleWeigth());
            }
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I not see birthdate")]
        public void ThenINotSeeBirthdate()
        {
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.IsFalse(orderMainPage.IsVisibleBirthdate());
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I not see height")]
        public void ThenINotSeeHeight()
        {
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.IsFalse(orderMainPage.IsVisibleHeight());
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I not see weight")]
        public void ThenINotSeeWeight()
        {
            OrderMainPage orderMainPage = new OrderMainPage(pageInAnotherTab);
            Assert.IsFalse(orderMainPage.IsVisibleWeigth());
            //ScenarioContext.Current.Pending();
        }

        [AfterScenario("DisplayBirthdateAgeHeightWeight")]
        public void CloseTab()
        {
            Session.FindWindow("Отчет").ExecuteScript("self.close();");
        }
    }
}
