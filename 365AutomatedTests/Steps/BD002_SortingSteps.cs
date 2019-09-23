using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
namespace _365AutomatedTests
{
    using Coypu;
    using static NUnit.Framework.Assert;
    [Binding]
    public class BD002_SortingSteps
    {
        ElementScope SecondTab;

       























        [When(@"I confirm parent-batch id for start Sorting")]
        public void WhenIConfirmParent_BatchIdForStartSorting()
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenSorting();
            IsTrue(BDMain.AssertOpenSorting(),"Заголовок рабочего места не найден");

            BDSortingPage sortingPage = new BDSortingPage();
            sortingPage.AssertTheButtonIsVisible();
            sortingPage.ClickCreateBatch();
            sortingPage.AssertTheConfirmBatchFieldIsVisible();
            sortingPage.SetParentBatchID();

        }

        [When(@"I enter usercode in the field on the planchet-position form for (.*)")]
        [Then(@"I enter usercode in the field on the planchet-position form for (.*)")]
        public void WhenIEnterUsercodeInTheFieldOnThePlanchet_PositionForm(string type)
        {
            BDPlanshetPositionPage bDPlanshetPositionPage = new BDPlanshetPositionPage();
            BDPositiveControlsWorkplacePage bDPositiveControlsWorkplace = new BDPositiveControlsWorkplacePage(); 
            //if (type.Equals("Sorting")) bDPlanshetPositionPage.NewWindowConfirmUsercode();//ConfirmUsercodeInPlanshetPositionPage();
            //else if (type.Equals("Manual")) bDPlanshetPositionPage.ConfirmUsercodeForManualDripping();
           // else bDPositiveControlsWorkplace.ConfirmUsercode();
        }
        
       





    }
}
