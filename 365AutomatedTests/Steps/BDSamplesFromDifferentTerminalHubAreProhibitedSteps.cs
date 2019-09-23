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
    public class BDSamplesFromDifferentTerminalHubAreProhibitedSteps
    {
        [Then(@"The sample with wrong location dont add to sorting")]
        public void ThenTheSampleWithWrongLocationDontAddToSorting()
        {

            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command1 = $@"select TOP(1) s.TEXT_ID from test t with (nolock)
            inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
            where t.status='I' and t.ANALYSIS='HELICOBACTER_PYLORI_ДНК' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = 'HELIX-SPB' ";
            var result_spb = _msBDConnectorLW.QueryExecutorScalar(command1);
            string command2 = $@"select TOP(1) s.TEXT_ID from test t with (nolock)
            inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
            where t.status='I' and t.ANALYSIS='HELICOBACTER_PYLORI_ДНК' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = 'HELIX-MSK' ";
            var result_msk = _msBDConnectorLW.QueryExecutorScalar(command2);

            BDSortingPage sortingPage = new BDSortingPage();
            sortingPage.IsSampleCodeFieldActive();
            // Добавление образца с локацией Санкт-Петербург
            sortingPage.CodeInputField.SendKeys(result_spb);
            sortingPage.CodeInputField.SendKeys(Keys.Enter);
            sortingPage.IsSampleCodeFieldActive();
            // Добавление образца с локацией Москва
            sortingPage.CodeInputField.SendKeys(result_msk);
            sortingPage.CodeInputField.SendKeys(Keys.Enter);
            sortingPage.IsSampleCodeFieldActive();

            sortingPage.EndBatch();
        }

        [Then(@"I check count of batches")]
        public void ThenICheckCountOfBatches()
        {
            BDMainPage bDMainPage = new BDMainPage();
            bDMainPage.OpenBatches();
            IsTrue(bDMainPage.AssertOpenBatches(),"Не найден заголовок на странице");
            BDBatchesPage bDBatchesPage = new BDBatchesPage();
            IsTrue(bDBatchesPage.TheTableIsEmpty(),"В таблице есть бэтчи");
        }

    }
}
