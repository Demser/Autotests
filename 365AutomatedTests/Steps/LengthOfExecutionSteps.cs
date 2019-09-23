using System;
using TechTalk.SpecFlow;
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
    public class LengthOfExecutionSteps
    {
        [When(@"I create simple preorder with ""(.*)"" , ""(.*)"" , ""(.*)"" nomenclatures and choose ""(.*)"" diagnostical center")]
        public void WhenICreateSimplePreorderWithNomenclaturesAndChooseDiagnosticalCenter(string hxid1, string hxid2, string hxid3,string dc)
        {
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            cCENFilterPanelPage.SetSPB();
            System.Threading.Thread.Sleep(3000);
            CCENNomenclaturesPage cCENNomenclaturesPage = new CCENNomenclaturesPage();
            cCENNomenclaturesPage.SearchSomeHXID(hxid1);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid1);
            cCENNomenclaturesPage.SearchSomeHXID(hxid2);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid2);
            cCENNomenclaturesPage.SearchSomeHXID(hxid3);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid3);
            CCENDelayConfirmWindow cCENDelay = new CCENDelayConfirmWindow();
            var a = cCENDelay.AssertConfirmDialogWindow();
            if (a)
            {
                cCENDelay.AddDelayedNomenclature();
            }
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.GoToCartTab();
            cCENCartPage.ChooseDiagnosticalCenter(dc);
        }
        
        [Then(@"I see length of execution ""(.*)""")]
        public void ThenISeeLengthOfExecution(string period)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            IsTrue(cCENCartPage.AssertExecLongValue(period), "Строка со сроком исполнения еще не подгрузилась");
        }
    }
}
