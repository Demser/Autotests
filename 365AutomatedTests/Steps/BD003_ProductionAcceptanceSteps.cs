using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using System.Diagnostics;
using static NUnit.Framework.Assert;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class BD003_ProductionAcceptanceSteps
    {
        [When(@"I confirm parent-batch id for start ProductionAcceptance")]
        public void WhenIConfirmParent_BatchIdForStartProductionAcceptance()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenProductionAcceptance();
            BDProductionAcceptancePage bDProductionAcceptancePage = new BDProductionAcceptancePage();
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"SELECT [Id] FROM[BatchDropperStab].[dbo].[Batches] Order by ChangedOn desc";
            var Parent = _msBDConnector.QueryExecutorScalar(command);

            bDProductionAcceptancePage.SetParentBatch(Parent);
            System.Threading.Thread.Sleep(500);
            IsTrue(bDProductionAcceptancePage.AssertCountOfChildBatches(),"Количество элементов не соответствует ожидаемому");
        }

        [When(@"I confirm all parent-batches id for start ProductionAcceptance")]
        public void WhenIConfirmAllParent_BatchesIdForStartProductionAcceptance()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenProductionAcceptance();
            BDProductionAcceptancePage bDProductionAcceptancePage = new BDProductionAcceptancePage();
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 10";
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command);
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches);

            if (intCount != 0)
            {      
                string command1 = $@"SELECT [Id] FROM[BatchDropperStab].[dbo].[Batches] Where Status = 10 Order by ChangedOn desc";
                var listOfID = _msBDConnector.QueryExecutor(command1);
                foreach (string i in listOfID)
                {
                    Debug.WriteLine(i);
                    bDProductionAcceptancePage.SetParentBatch(i);
                }

            }
            IsTrue(bDProductionAcceptancePage.AssertCountOfElementsInfo(countOfBatches), "Количество элементов не соответствует ожидаемому");
        }
    }
}
