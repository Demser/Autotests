using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;

using Coypu;
using OpenQA.Selenium;
using System.Linq;
using System.Diagnostics;



namespace _365AutomatedTests.Steps
{
    using Coypu;
    using OpenQA.Selenium;
    using System.Threading;
    using static NUnit.Framework.Assert;
    [Binding]
    public class BD_Isolation_DNA_WorkPlaceSteps
    {
        [When(@"I open isolation DNA workplace and start new batch")]
        public void WhenIConfirmParent_BatchIdForStartIsalatiion()
        {
            {
                BDMainPage bDMainPage = new BDMainPage();
                bDMainPage.OpenDNKWorkplace();
                IsTrue(bDMainPage.AssertOpenDNKWorkplace(), "Заголовок рабочего места не найден");

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
            }
        }

        [Then(@"I fill-out the isolation-planchet by ""(.*)"" samples ""(.*)"" of test ""(.*)"" with biomaterial ""(.*)"" and location ""(.*)"" and '(.*)' VK and '(.*)' OKO and '(.*)' PKO")]
        public void ThenIFill_OutTheSorting_PlanchetBySamplesOfTestWithBiomaterialAndLocationAndVKAndOKOAndPKO(int count, string own, List<String> testname, string bm, string location, List<String> VKName, List<String> OKOName, List<String> PKOName)
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            //if(testname[0] != "")
            int countOfTests = testname.Count();

            string command = "";

            if (own == "only") // для случая, когда образец назначен ТОЛЬКО на тест/тесты
            {
                if (countOfTests == 1)
                {
                    command = $@"USE LW08v6
                 select TOP({count}) s.TEXT_ID
                 from
                 (select SAMPLE_NUMBER, count(ANALYSIS) as TestCommonCount
                 from [LW08v6].[dbo].[TEST] t with (nolock) 
                 group by SAMPLE_NUMBER
                 having count(ANALYSIS) = 1) MonoSamples
                 join TEST t on t.SAMPLE_NUMBER = MonoSamples.SAMPLE_NUMBER
                 join SAMPLE s on S.SAMPLE_NUMBER = t.SAMPLE_NUMBER
                 where t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I' 
                 and s.LOCATION = '{location}' and t.ANALYSIS='{testname[0]}' and s.SAMPLE_TYPE ='{bm}'";
                }
                if (countOfTests == 2)
                {
                    command = $@"USE LW08v6
                 select TOP({count}) s.TEXT_ID as Sample
                 from TEST t
                 join SAMPLE s on t.SAMPLE_NUMBER = s.SAMPLE_NUMBER
                 where  t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = '{location}' and s.SAMPLE_TYPE ='{bm}'
                 and t.ANALYSIS = '{testname[0]}' 
                 and exists (select top 1 1 from TEST t1 where  t1.SAMPLE_NUMBER = s.SAMPLE_NUMBER
                 and t1.ANALYSIS = '{testname[1]}') 
                 and not exists (select top 1 1 from TEST t1 where  t1.SAMPLE_NUMBER = s.SAMPLE_NUMBER
                 and t1.ANALYSIS not in  ('{testname[0]}', '{testname[1]}'))";
                }
            }
            if (countOfTests == 2) // если 2 теста в коллекции, выполняем скрипт:
            {
                command = $@"USE LW08v6
                select TOP({count}) Sample
                from
                (select s.TEXT_ID as Sample, t.ANALYSIS as Test
                from TEST t
                join SAMPLE s on t.SAMPLE_NUMBER = s.SAMPLE_NUMBER
                where  t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = '{location}' and s.SAMPLE_TYPE ='{bm}'
                and t.ANALYSIS in ('{testname[0]}', '{testname[1]}')
                group by TEXT_ID, ANALYSIS) sub
                group by Sample
                having count(Test) = 2";
            }
            else if (countOfTests == 1) // если 1 тест в коллекции, выполняем скрипт:
            {
                command = $@"USE LW08v6
                SELECT TOP({count}) s.TEXT_ID FROM test t with (nolock)
                inner join SAMPLE s on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                WHERE t.status='I' and t.ANALYSIS='{testname[0]}' 
                and s.TEMPLATE = 'smp_in' and s.status='I' and s.SAMPLE_TYPE ='{bm}' 
                and s.LOCATION = '{location}'";
            }
            else if (countOfTests == 3) // если 3 теста в коллекции, выполняем скрипт:
            {
                command = $@"USE LW08v6
                select TOP ({count})Sample
                from
                (select s.TEXT_ID as Sample, t.ANALYSIS as Test
                from TEST t
                join SAMPLE s on t.SAMPLE_NUMBER = s.SAMPLE_NUMBER
                where  t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = '{location}' and s.SAMPLE_TYPE ='{bm}'
                and t.ANALYSIS in ('{testname[0]}', '{testname[1]}','{testname[2]}')
                group by TEXT_ID, ANALYSIS) sub
                group by Sample
                having count(Test) = 3";
            }

            var result = _msBDConnectorLW.QueryExecutor(command);
            BDIsolationDNKPage bDIsolationDNKPage = new BDIsolationDNKPage();
            bDIsolationDNKPage.AddSample(result);
            bDIsolationDNKPage.TakeThisSamples();

            if (VKName[0] != "") // если есть ВК - раскапать 
            {
                foreach (string v in VKName)
                {
                    bDIsolationDNKPage.AddControlMaterial(v);
                }
            }
            if (bDIsolationDNKPage.NeedActiveTestTubes()) // раскапывать ВК в пробирки пока есть поле "ввести ШК пробы"
            {
                foreach (string n in result)
                {
                    bDIsolationDNKPage.AddActiveIsolationControl(n);
                }
                foreach (string o in OKOName)
                {
                    bDIsolationDNKPage.AddActiveIsolationControl(o);
                }
                foreach (string p in PKOName)
                {
                    bDIsolationDNKPage.AddActiveIsolationControl(p);
                }
            }
            if (OKOName[0] != "") // если есть ОКО - раскапать 
            { 
                foreach (string o in OKOName)
                {
                    bDIsolationDNKPage.ControlsBoardCodeEnter(o);
                }
            }
            if (PKOName[0] != "") // если есть ПКО - раскапать
            {
                foreach (string p in PKOName)
                {
                    bDIsolationDNKPage.ControlsBoardCodeEnter(p);
                }
            }
        }
    }
 }

