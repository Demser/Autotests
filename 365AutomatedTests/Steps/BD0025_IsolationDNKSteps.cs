using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
namespace _365AutomatedTests.Steps
{
    using Coypu;
    using OpenQA.Selenium;
    using System.Threading;
    using static NUnit.Framework.Assert;
    [Binding]
    public class SortingWithDNK_IsolatedSampleSteps
    {
        [When(@"I add (.*) DNK-Sample of test ""(.*)"" to ""(.*)"" field")]
        public void WhenIAddDNK_SampleOfTestToField(int count, string name, string workplace)
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command = $@"select TOP({count}) s.TEXT_ID from test t with (nolock)
            inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
            where t.status='I' and t.ANALYSIS='{name}' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = 'HELIX-SPB' ";
            var result = _msBDConnectorLW.QueryExecutor(command);

            if (workplace.Equals("IsolationDNK")) // переход в выделение в ДНК и добавление образца
            {
                BDMainPage bDMainPage = new BDMainPage();
                bDMainPage.OpenDNKWorkplace();
                BDIsolationDNKPage bDIsolationDNKPage = new BDIsolationDNKPage();
                bDIsolationDNKPage.AssertTheButtonIsVisible();
                bDIsolationDNKPage.ClickCreateBatch();
                bDIsolationDNKPage.AssertBatchIsCreated();
                bDIsolationDNKPage.SetParentBatchID();
                BDPlanshetPositionPage bDPlanshetPositionPage = new BDPlanshetPositionPage();
                Thread.Sleep(500);
               // bDPlanshetPositionPage.NewWindowConfirmUsercode();
                bDIsolationDNKPage.AddSample(result);
            }
            else if (workplace.Equals("ProductionAcceptance")) // переход в Прием в постановке и добавление образца
            {
                BDMainPage bDMainPage = new BDMainPage();
                bDMainPage.OpenProductionAcceptance();
                BDProductionAcceptancePage bDProductionAcceptancePage = new BDProductionAcceptancePage();
                bDProductionAcceptancePage.AddSample(result);
            }
        }

    }
}
