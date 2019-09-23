using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NUnit.Framework;
using _365AutomatedTests.Steps;
using Coypu;
using OpenQA.Selenium;

namespace _365AutomatedTests.Steps
{
    using static NUnit.Framework.Assert;
    [Binding]
    public class BDReagentsSmokeWithSchedSteps
    {

          
        [Then(@"I fill-out the Sorting-planchet by (.*) samples of test ""(.*)""")]
        public void ThenIFill_OutTheSorting_PlanchetBySamplesOfTest(int count, string testname)
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command = $@"select TOP({count}) s.TEXT_ID from test t with (nolock)
            inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
            where t.status='I' and t.ANALYSIS='{testname}' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = 'HELIX-SPB' ";
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
        
        [Then(@"I remove all schedules from dictionaries")]
        public void ThenIRemoveAllSchedulesFromDictionaries()
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command = $@"DELETE FROM[BatchDropperStab].[dbo].[Schedules]";
            var result = _msBDConnectorLW.NonQueryExecutor(command);
        }
        
        [Then(@"I check task for reagents")]
        public void ThenICheckTaskForReagents()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenReagentsWorkplace();
            mainPage.AssertOpenReagentsWorkplace();
            BDReagentsWorkplacePage bDReagents = new BDReagentsWorkplacePage();
            bDReagents.AssertTheBusyIndicatorIsDisabled();
            bDReagents.GetTaskForReagents();
            IsTrue(bDReagents.AssertThereAreNotSamplesMessage(),"Сообщение о запрете раскапывания реагентов отсутствует");         
        }

        [When(@"I create new sched by database")]
        public void WhenICreateNewSchedByDatabase()
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command = $@"INSERT INTO [BatchDropperStab].[dbo].[Schedules] Values ('autotest','11:57:00.000','11:41:00.000',1)";
            var result = _msBDConnectorLW.NonQueryExecutor(command);
            string command2 = $@"INSERT INTO [BatchDropperStab].[dbo].[Schedules] Values ('autotestmsk','11:47:00.000','11:40:00.000',2)";
            var result2 = _msBDConnectorLW.NonQueryExecutor(command2);
        }

        [When(@"I create new sched by database for ""(.*)"" hub")]
        public void WhenICreateNewSchedByDatabaseForHub(string hub)
        {
            BDSchedulesPage bDSchedulesPage = new BDSchedulesPage();
            if (hub.Equals("MSK")) bDSchedulesPage.AddScheduleForMSK();
            else if (hub.Equals("SPB")) bDSchedulesPage.AddScheduleForSPB();
        }

        [Then(@"Task for reagents will be create")]
        public void ThenTaskForReagentsWillBeCreate()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenReagentsWorkplace();
            BDReagentsWorkplacePage bDReagents = new BDReagentsWorkplacePage();
            bDReagents.SetTestBatchID();
            bDReagents.SetUsercode();
        }
    }
}
