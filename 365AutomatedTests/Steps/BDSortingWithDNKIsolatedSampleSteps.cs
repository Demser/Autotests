using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;

namespace _365AutomatedTests
{
    using OpenQA.Selenium;
    using static NUnit.Framework.Assert;
    [Binding]
    
    public class SortingWithDNK_IsolatedSampleSteps
    {
        [When(@"I try to set (.*) DNK-Sample of test ""(.*)"" to sorting")]
        public void WhenITryToSetDNK_SampleOfTestToSorting(int count, string name)
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command = $@"select TOP({count}) s.TEXT_ID from test t with (nolock)
            inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
            where t.status='I' and t.ANALYSIS='{name}' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = 'HELIX-SPB' ";
            var result = _msBDConnectorLW.QueryExecutor(command);

            BDSortingPage sortingPage = new BDSortingPage();
            sortingPage.IsSampleCodeFieldActive();

            foreach (string i in result)
            {
                sortingPage.CodeInputField.SendKeys(i);
                sortingPage.CodeInputField.SendKeys(Keys.Enter);
                sortingPage.IsSampleCodeFieldActive();
            }
        }
        
        [Then(@"The sample dont add to sorting batch")]
        public void ThenTheSampleDontAddToSortingBatch()
        {
            BDSortingPage sortingPage = new BDSortingPage();
            IsTrue(sortingPage.AssertTheFirstCellIsGrey(),"ячейка не серого цвета");
        }
    }
}
