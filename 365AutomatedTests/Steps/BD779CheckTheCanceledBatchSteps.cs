using _365AutomatedTests.UIObjects.Pages;
using System;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps
{
    using _365AutomatedTests.Framework;
    using Coypu;
    using OpenQA.Selenium;
    using static NUnit.Framework.Assert;

    [Binding]
    public class BD779CheckTheCanceledBatchSteps
    {
        [When(@"Being in ""(.*)"" workplace I add ""(.*)"" sample of test ""(.*)"" and ""(.*)"" location and ""(.*)"" biomaterial ""(.*)"" results")]
        public void WhenBeingInWorkplaceIAddSampleOfTestAndLocationAndBiomaterialResults(string wp, int count, string test, string location, string bm, string type)
        {   
            // ОПИСЫВАЕТ ДОБАВЛЕНИЕ В ОТКРЫТОЕ РМ СОРТИРОВКА/ВЫДЕЛЕНИЕ НЕСКОЛЬКИХ ОБРАЗЦОВ, КОТОРЫХ ЕЩЕ НЕ ДОБАВЛЯЛИ.
            // ПРОСТО ДОБАВЛЕНИЕ, ПОПЫТКА. НЕГАТИВНЫЙ КЕЙС.
            BDMainPage BDMain = new BDMainPage();
            if (wp == "sorting")
            {
                IsTrue(BDMain.AssertOpenSorting(), "Заголовок рабочего места не найден");
                MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
                string command = "";
                if (type == "with") // выбор образцов с результатами, которые еще НЕ были добавлены в базу BD
                {
                    command = $@"select top 1 s.TEXT_ID
                    from test t with (nolock)
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    left join RESULT r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='A' and s.TEMPLATE = 'smp_in' and r.RESULT_NUMBER is not NULL and r.ENTRY is not NULL 
                    and s.LOCATION = '{location}' and t.ANALYSIS = '{test}' and s.SAMPLE_TYPE ='{bm}'
                    and s.SAMPLE_NUMBER not in (select [LwSampleNumber] from [BatchDropperStab].[dbo].[Samples] )";
                }
                else if (type == "without") // выбор образцов без результатов, которые еще НЕ были добавлены в базу BD
                {
                    command = $@"select top 1 s.TEXT_ID from test t 
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    inner join [RESULT] r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I'
                    and s.LOCATION = '{location}'and t.ANALYSIS = '{test}'and s.SAMPLE_TYPE ='{bm}'
                    and s.SAMPLE_NUMBER not in (select [LwSampleNumber] from [BatchDropperStab].[dbo].[Samples] )";
                }
                var result = _msBDConnectorLW.QueryExecutor(command);
                BDSortingPage sortingPage = new BDSortingPage();
                sortingPage.CodeInputField.WaitForClickability();

                foreach (string i in result)
                {
                    sortingPage.CodeInputField.SendKeys(i);
                    sortingPage.CodeInputField.SendKeys(Keys.Enter);
                }
            }
            else if (wp == "isolation")
            {
                IsTrue(BDMain.AssertOpenDNKWorkplace(), "Заголовок рабочего места не найден");
                MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
                string command = "";
                if (type == "with") // выбор образцов с результатами, которые еще НЕ были добавлены в базу BD
                {
                    command = $@"select top 1 s.TEXT_ID
                    from test t with (nolock)
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    left join RESULT r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='A' and s.TEMPLATE = 'smp_in' and r.RESULT_NUMBER is not NULL and r.ENTRY is not NULL 
                    and s.LOCATION = '{location}' and t.ANALYSIS = '{test}' and s.SAMPLE_TYPE ='{bm}'
                    and s.SAMPLE_NUMBER not in (select [LwSampleNumber] from [BatchDropperStab].[dbo].[Samples] )";
                }
                else if (type == "without") // выбор образцов без результатов, которые еще НЕ были добавлены в базу BD
                {
                    command = $@"select top 1 s.TEXT_ID from test t 
                    inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    inner join [RESULT] r on s.SAMPLE_NUMBER = r.SAMPLE_NUMBER
                    where t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I'
                    and s.LOCATION = '{location}'and t.ANALYSIS = '{test}'and s.SAMPLE_TYPE ='{bm}'
                    and s.SAMPLE_NUMBER not in (select [LwSampleNumber] from [BatchDropperStab].[dbo].[Samples] )";
                }
                var result = _msBDConnectorLW.QueryExecutor(command);
                BDIsolationDNKPage isolationDNKPage = new BDIsolationDNKPage();
                isolationDNKPage.AddSample(result);
            }
        }
    }
}
