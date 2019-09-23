using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.UIObjects.Pages;
using System.Linq;
using TechTalk.SpecFlow.Assist;
using _365AutomatedTests.Framework;
using static NUnit.Framework.Assert;
using Coypu;
using NUnit.Framework;
using System.Diagnostics;


namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CCENCheckingTheAvailabilityOfEquipmentInDCSteps
    {
        [When(@"I set status ""(.*)"" in diagnostical center ""(.*)"" for equipment ""(.*)"" by script")]
        public void WhenISetStatusInDiagnosticalCenterForEquipmentByScript(int status, int dc, int eq)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"Update [AllCenterGlobalDictionariesWork].[dbo].[AllEquipmentsInAccounts] SET Status = {status} Where AccountCode = {dc} and Code = {eq}";
            var result = _msBDConnector.NonQueryExecutor(command);
        }
    }
}
