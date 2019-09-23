using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System.Collections.Generic;
using static NUnit.Framework.Assert;
using NUnit.Framework;
using Coypu;
using _365AutomatedTests.Steps;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CCENAutoLoadServicesSteps
    {
        [Given(@"I open UserSettings Page")]
        public void GivenIOpenUserSettingsPage()
        {
            MainPage mainPage = new MainPage();
            mainPage.OpenUserSettings();

           // ScenarioContext.Current.Pending();
        }


        [When(@"I switch ""(.*)"" ""(.*)"" in user settings")]
        public void WhenISwitchInUserSettings(string status, string element)
        {
            UserSettingsPage userSettingsPage = new UserSettingsPage();
            var result = userSettingsPage.CheckboxStatusCheck(element);
            switch (status)
            {
                case "on":
                    
                   
                   if (!result)
                    {
                        userSettingsPage.AutoLoadServicesCheckboxClick(element);
                        Console.WriteLine("Тест завалился");
                    }

                    break;

                case "off":
                    if (result)
                    {
                        userSettingsPage.AutoLoadServicesCheckboxClick(element);
                        Console.WriteLine("Тест завалился");
                    }

                    break;

                default:   // НАХРЕНА??
                    break; // НАХРЕНА??
            }
            //ScenarioContext.Current.Pending();
        }   


        
        [When(@"I Check exists services ""(.*)"" and price ""(.*)"" in cart")]
        public void WhenICheckExistsServicesAndPriceInCart(string HXID, string price)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.CheckHxidByNumberExistsInCart(HXID);
        }

        [When(@"I Check missing services ""(.*)"" in cart")]
        public void WhenICheckMissingServicesAndPriceInCart(string HXID)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.CheckHxidByNumberMissingInCart(HXID);
        }

        [When(@"I delete service ""(.*)"" in cart")]
        public void WhenIDeleteServiceInCart(string HXID)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.DeleteHxidByNumber(HXID);
        }
        
        [When(@"I open UserSettings Page")]
        public void WhenIOpenUserSettingsPage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I check counter in the cart ""(.*)""")]
        public void ThenICheckCounterInTheCart(string counter)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.CheckCartCounter(counter);
        }

        [Then(@"I click settings save button")]
        public void ThenIClickSettingsSaveButton()
        {
            UserSettingsPage userSettingsPage = new UserSettingsPage();
            userSettingsPage.SaveBtnClick();
        }
    }
}
