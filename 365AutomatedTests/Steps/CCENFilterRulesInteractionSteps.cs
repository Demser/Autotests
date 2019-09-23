using _365AutomatedTests.UIObjects.Pages;
using System;
using TechTalk.SpecFlow;
using System.Linq;
using TechTalk.SpecFlow.Assist;
using _365AutomatedTests.Framework;
using static NUnit.Framework.Assert;
using Coypu;
using NUnit.Framework;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CCENFilterRulesInteractionSteps
    {
        [When(@"I clear insuranceFilter")]
        public void WhenIClearInsuranceFilter()
        {
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            cCENFilterPanelPage.InsuranceFilterClear();
            System.Threading.Thread.Sleep(3000);
        }

        

        [Then(@"I check checkbox ""(.*)"" is ""(.*)"" status")]
        public void ThenICheckCheckboxIsStatus(string element, string status)
        {
          
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            var result = cCENFilterPanelPage.CheckboxStatusCheck(element);
            switch (status)
            {
                case "disabled":

                    IsTrue(result, "Поле/чекбокс " + element + " НЕ заблокировано");
                    Console.WriteLine("Тест завалился");
                    break;

                case "enabled":
                     IsFalse(result, "Поле/чекбокс " + element + " заблокировано");
                    Console.WriteLine("Тест завалился");
                    break;

                default:
                    break;
            }


        }
    }
}
