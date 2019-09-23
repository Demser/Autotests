using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;

// Отмена бэтча через базу данных. Проверка сообщения в РМ об отмененном он-лайн бэтче
namespace _365AutomatedTests.Steps
{
    using static NUnit.Framework.Assert;
    [Binding]
    public class BDProhibitionOfDrippingTheCanceledBatchSteps
    {
        [When(@"I canceled ""(.*)"" batch by database")]
        public void WhenICanceledBatchByDatabase(string type)
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            if (type.Equals("parent")) // Сделать неактивным родительский бэтч
            {
                string command = $@"UPDATE [BatchDropperStab].[dbo].[Batches] SET Status = 110 Where ID = (SELECT TOP(1) ID from [BatchDropperStab].[dbo].[Batches] Where [Name] like '%P-%' Order By ChangedOn desc)";
                var result = _msBDConnectorLW.NonQueryExecutor(command);
            }
            else if (type.Equals("child")) // Сделать неактивным дочерний бэтч
            {
                string command2 = $@"UPDATE [BatchDropperStab].[dbo].[Batches] SET Status = 110 Where ID = (SELECT TOP(1) ID from [BatchDropperStab].[dbo].[Batches] Where [Name] like '%C-%' Order By ChangedOn desc)";
                var result2 = _msBDConnectorLW.NonQueryExecutor(command2);
            }
            else if (type.Equals("test")) // Сделать неактивным тестовый бэтч
            {
                string command3 = $@"UPDATE [BatchDropperStab].[dbo].[Batches] SET Status = 110 Where ID = (SELECT TOP(1) ID from [BatchDropperStab].[dbo].[Batches] Where [Name] like '%T-%' Order By ChangedOn desc)";
                var result3 = _msBDConnectorLW.NonQueryExecutor(command3);
            }
            else if (type.Equals("PT")) // Сделать неактивным PT бэтч
            {
                string command3 = $@"UPDATE [BatchDropperStab].[dbo].[Batches] SET Status = 110 Where ID = (SELECT TOP(1) ID from [BatchDropperStab].[dbo].[Batches] Where [Name] like '%PT-%' Order By ChangedOn desc)";
                var result3 = _msBDConnectorLW.NonQueryExecutor(command3);
            }
        }
        
        [Then(@"I check title for canceled batch")]
        public void ThenICheckTitleForCanceledBatch()
        {
            BDSortingPage sortingPage = new BDSortingPage();
            IsTrue(sortingPage.AssertCanseledBatchTitle(),"Не найден заголовок об отмененном бэтче");
        }             
        
    }
}
