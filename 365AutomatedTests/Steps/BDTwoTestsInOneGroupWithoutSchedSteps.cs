using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System;
using TechTalk.SpecFlow;
using static NUnit.Framework.Assert;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class BDTwoTestsInOneGroupWithoutSchedSteps
    {
        [Then(@"The test ""(.*)"" will be dropped in same planshet")]
        public void ThenTheTestWillBeDroppedInSamePlanshet(string nameoftest)
        {
            BDManualDrippingPage manualDrippingPage = new BDManualDrippingPage();
            IsTrue(manualDrippingPage.AssertNameOfDrippingTest(nameoftest), "Что-то пошло не так. Не найден тест в раскапывании.");
        }

        [When(@"I confirm test-batch id for continue ManualDripping")]
        public void WhenIConfirmTest_BatchIdForContinueManualDripping()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenManualDripping();
            mainPage.AssertOpenManualDripping();
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"SELECT TOP(1) ID from [BatchDropperStab].[dbo].[Batches] Where [Name] like '%T-%' Order By ChangedOn desc";
            var tESTBATCH = _msBDConnector.QueryExecutorScalar(command);
            // Бэтчдроппер БД, вытащить айди активного тестового бэтча измененного самым последним.
            BDManualDrippingPage manualDrippingPage = new BDManualDrippingPage();
            manualDrippingPage.ConfirmTestBatchId(tESTBATCH);

        }


    }
}
