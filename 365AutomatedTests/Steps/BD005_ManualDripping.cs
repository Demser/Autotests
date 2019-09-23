using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using static NUnit.Framework.Assert;
using Coypu;
using OpenQA.Selenium;
namespace _365AutomatedTests.Steps
{
    [Binding]
    public class BD005_ManualDripping
    {
        public string cHILDBATCH;


        [When(@"I go to Manual Dripping Page")]
        public void WhenIGoToManualDrippingPage()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenManualDripping();
            IsTrue(mainPage.AssertOpenManualDripping(), "Отсутствует заголовок Ручное Раскапывание");
        }

        [Then(@"I fill-out the test planchetes by manual dripping")]
        public void ThenIFill_OutTheTestPlanchetesByManualDripping()
        {
            BDManualDrippingPage bDManualDrippingPage = new BDManualDrippingPage();
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 22";
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command);
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches);

            if (intCount != 0)
            {
                string command1 = $@"SELECT [Id] FROM[BatchDropperStab].[dbo].[Batches] Where Status = 22 Order by ChangedOn desc";
                var listOfID = _msBDConnector.QueryExecutor(command1);
                foreach (string i in listOfID)
                {
                    bDManualDrippingPage.ConfirmTestBatchField.WaitForClickability();
                    bDManualDrippingPage.ConfirmTestBatchField.SendKeys(i).SendKeys(Keys.Enter);
                    CommonSteps commonSteps = new CommonSteps();
                    commonSteps.ThenThePopupWithTitleShouldBeClosed();
                    BDReagentsWorkplacePage bDReagentsWorkplacePage = new BDReagentsWorkplacePage();
                    bDReagentsWorkplacePage.SetUsercode();
                    bDManualDrippingPage.AddClipboardCopyOfBatches();
                    bDManualDrippingPage.AddClipboardCopyOfTubes();
                    bDManualDrippingPage.ConfimEndOfDripping();
                    commonSteps.ThenThePopupWithTitleShouldBeClosed();
                }

            }
        }
    }
}
 