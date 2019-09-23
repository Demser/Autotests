using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System.Collections.Generic;
using Coypu;
using OpenQA.Selenium;
using System.Linq;
using System.Diagnostics;

namespace _365AutomatedTests.Steps
{
    using NUnit.Framework;
    using System.Threading;
    using static NUnit.Framework.Assert;
    [Binding]
    public class BD695ExperimentsStatusModeTestingSteps
    {
        [Then(@"I try to add a sample of test ""(.*)"" and ""(.*)"" location and ""(.*)"" biomaterial ""(.*)"" results in ""(.*)"" workplace")]
        public void ThenITryToAddASampleOfTestAndLocationAndBiomaterialResultsInSortingWorkplace(string test, string location, string bm, string type, string wp)
        {
            
            BDMainPage BDMain = new BDMainPage();
            if (wp == "sorting")
            {
                BDMain.OpenSorting();
                IsTrue(BDMain.AssertOpenSorting(), "Заголовок рабочего места не найден");
                // создаем новый бэтч
                BDSortingPage sortingPage = new BDSortingPage();
                sortingPage.AssertTheButtonIsVisible();
                sortingPage.ClickCreateBatch();
                sortingPage.AssertTheConfirmBatchFieldIsVisible();
                sortingPage.SetParentBatchID();
                // подтверждаем своим ШК
                BDPlanshetPositionPage bDPlanshetPositionPage = new BDPlanshetPositionPage();
                IsTrue(bDPlanshetPositionPage.AssertConfirmBatchWindow(), "Нет окна для подтверждения установки планшета");
                bDPlanshetPositionPage.ConfirmUsercodeInPlanshetPositionPage();
                MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
                string command = "";
                if (type == "with")
                {
                    command = $@"select top 1 s.TEXT_ID
                    from test t with (nolock)
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    left join RESULT r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='A' and s.TEMPLATE = 'smp_in' and r.RESULT_NUMBER is not NULL and r.ENTRY is not NULL 
                    and s.LOCATION = '{location}' and t.ANALYSIS = '{test}' and s.SAMPLE_TYPE ='{bm}'";
                }
                else if (type == "without")
                {
                    command = $@"select top 1 s.TEXT_ID from test t 
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    inner join [RESULT] r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I'
                    and s.LOCATION = '{location}'and t.ANALYSIS = '{test}'and s.SAMPLE_TYPE ='{bm}'";
                }
                var result = _msBDConnectorLW.QueryExecutor(command);
                sortingPage.CodeInputField.WaitForClickability();

                foreach (string i in result)
                {
                    sortingPage.CodeInputField.SendKeys(i);
                    sortingPage.CodeInputField.SendKeys(Keys.Enter);
                    Thread.Sleep(1500);
                }

            }
            else if (wp == "isolation")
            {
                BDMain.OpenDNKWorkplace();
                IsTrue(BDMain.AssertOpenDNKWorkplace(), "Заголовок рабочего места не найден");
                BDIsolationDNKPage bDIsolationDNKPage = new BDIsolationDNKPage();
                bDIsolationDNKPage.AssertTheButtonIsVisible();
                bDIsolationDNKPage.ClickCreateBatch();
                bDIsolationDNKPage.AssertBatchIsCreated();
                bDIsolationDNKPage.SetParentBatchID();
                BDPlanshetPositionPage bDPlanshetPositionPage = new BDPlanshetPositionPage();
                Thread.Sleep(500);
                // подтверждаем своим ШК
                IsTrue(bDPlanshetPositionPage.AssertConfirmBatchWindow(), "Нет окна для подтверждения установки планшета");
                bDPlanshetPositionPage.ConfirmUsercodeInPlanshetPositionPage();
                // bDPlanshetPositionPage.NewWindowConfirmUsercode();

                MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
                string command = "";
                if (type == "with")
                {
                    command = $@"select top 1 s.TEXT_ID
                    from test t with (nolock)
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    left join RESULT r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='A' and s.TEMPLATE = 'smp_in' and r.RESULT_NUMBER is not NULL and r.ENTRY is not NULL 
                    and s.LOCATION = '{location}' and t.ANALYSIS = '{test}' and s.SAMPLE_TYPE ='{bm}'";
                }
                else if (type == "without")
                {
                    command = $@"select top 1 s.TEXT_ID from test t 
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    inner join [RESULT] r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I'
                    and s.LOCATION = '{location}'and t.ANALYSIS = '{test}'and s.SAMPLE_TYPE ='{bm}'";
                }
                var result = _msBDConnectorLW.QueryExecutor(command);
                bDIsolationDNKPage.AddSample(result);
            }

        }

        [Then(@"Message ""(.*)"" will be ""(.*)""")]
        public void ThenMessageWillBe(string text, string action)
        {
            BDIsolationDNKPage bDIsolationDNKPage = new BDIsolationDNKPage();
            if (action=="shown")
            {
                IsTrue(bDIsolationDNKPage.NoTestsForWorkMessage(text).Exists(), "Не отобразилось в теч. 3 секунд");
            }
            else if (action == "hidden")
            {
                IsFalse(bDIsolationDNKPage.NoTestsForWorkMessage(text).Exists(), "Сообщение отображается, но не должно");
            }
        }

    }
}
