using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps
{
    using System;
    using static NUnit.Framework.Assert;
    [Binding]
    class BD006_PositiveControls
    {

        [When(@"I go to Positive Controls Page")]
        public void WhenIGoToPositiveControlsPage()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenPositiveControls();
            IsTrue(mainPage.AssertOpenPositiveControls(), "Отсутствует заголовок Положительные контроли");
        }

        [Then(@"I fill-out the test planchetes by positive controls")]
        public void ThenIFill_OutTheTestPlanchetesByPositiveControls()
        {
            //Находим в базе бэтчдроппера количество бэтчей, с нужным статусом:
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 20";
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command);
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches);

            if (intCount != 0)
            {
                string command1 = $@"SELECT [Id] FROM[BatchDropperStab].[dbo].[Batches] Where Status = 20 Order by ChangedOn desc";
                var listOfID = _msBDConnector.QueryExecutor(command1);
                BDPositiveControlsWorkplacePage bDPositiveControlsPage = new BDPositiveControlsWorkplacePage();
                foreach (string i in listOfID)
                {
                    bDPositiveControlsPage.SetTestBatch(i);
                    BDReagentsWorkplacePage bDReagentsWorkplacePage = new BDReagentsWorkplacePage();
                    bDReagentsWorkplacePage.SetUsercode();
                    bDPositiveControlsPage.DrippingPositiveControls();
                }
            }
            else { throw new NullReferenceException("Ошибочка вышла! В базе не найдено бэтчей со статусом: Отрицательные контроли собраны"); }
        }

    }
}
