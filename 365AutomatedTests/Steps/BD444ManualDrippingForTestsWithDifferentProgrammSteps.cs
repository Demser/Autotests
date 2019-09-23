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
    using static NUnit.Framework.Assert;
    [Binding]
    public class BD444ManualDrippingForTestsWithDifferentProgrammSteps
    {
        ElementScope FirstTab;
        ElementScope SecondTab;

        [When(@"I go to bathes page, I check that the tablet consist of ""(.*)"" samples and batch has status ""(.*)""")]
        public void WhenIGoToBathesPageICheckThatTheTabletConsistOfSamplesAndBatchHasStatus(int countSamples, string batchStatus)
        {
            BDMainPage bDMain = new BDMainPage();
            bDMain.OpenBatches();
            IsTrue(bDMain.AssertOpenBatches(), "Что-то пошло не так. не найден заголовок Бэтчи");
            //Проверить, что бэтч в статусе Собран
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select [Id] FROM [BatchDropperStab].[dbo].[Batches] Where Status = 10 and name like 'P-%'";
            var BatchId = _msBDConnector.QueryExecutorScalar(command);
            BDBatchesPage bDBatches = new BDBatchesPage();
            if (bDBatches.BatchWithIdAndStatus(BatchId, batchStatus).Exists())
            {
                Debug.WriteLine($"ОК. Бэтч {BatchId} в статусе {batchStatus}");
            }
            else
            {
                throw new NullReferenceException($"Ошибочка вышла! Не найден бэтч '{BatchId}' в статусе '{batchStatus}'");
            }
            //проверить, что в содержимом бэтча под образцы занимают N колитчество лунок
            bDBatches.ClickToShowContentBatchWithIdButton(BatchId);
            CommonSteps common = new CommonSteps();
            SecondTab = common.ThenTheTabWithTitleShouldBeOpened("BatchDropper");
            BDContentOfBatchPage bDContent = new BDContentOfBatchPage(SecondTab);
            int countSamplesInBatch = bDContent.ListOfDrippingSamples.Count;
            System.Threading.Thread.Sleep(2000);
            if (countSamplesInBatch == countSamples)
            {
                Debug.WriteLine($"ОК. Пробами занято {countSamples} лунок в бэтче");
            }
            else
            {
                throw new NullReferenceException($"Ошибочка вышла! В бэтче {BatchId} пробами занято {countSamplesInBatch} лунок вместо '{countSamples}'!");
            }
        }

        [Then(@"I check that child-batch was created")]
        public void ThenICheckThatChild_BatchWasCreated()
        {
            BDMainPage bDMain = new BDMainPage();
            bDMain.OpenBatches();
            IsTrue(bDMain.AssertOpenBatches(), "Что-то пошло не так. не найден заголовок Бэтчи");
            // проверяем, что появился дочерний бэтч
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select [Id] FROM [BatchDropperStab].[dbo].[Batches] Where Status = 31 and name like 'C-%'";
            var BatchId = _msBDConnector.QueryExecutorScalar(command);
            BDBatchesPage bDBatches = new BDBatchesPage();
            IsTrue(bDBatches.AssertChildBatch(BatchId), "Ошибка! Дочерний бэтч не создан!");
        }

        [When(@"I check tests with ""(.*)"" amplification programms on the Formig page")]
        public void WhenICheckTestsWithAmplificationProgrammsOnTheFormigPage(int p0)
        {
            // проверяем, что на странице Формирование бэтчей отображаются 2 программы аплификации
            BDMainPage bDMain = new BDMainPage();
            bDMain.OpenBatchFormingWorkplace();
            IsTrue(bDMain.AssertOpenBatchFormingWorkplace(), "Что-то пошло не так. не найден заголовок Формирование бэтчей");
            BDFormingPage bDForming = new BDFormingPage();
            bDForming.WaitForVisibility();
            int countOfAmplificationProgramms = bDForming.ListOfAmplificationProgramms.Count;
            if (countOfAmplificationProgramms == p0)
            {
                Debug.WriteLine($"ОК. Тесты разделены на {p0} программы амплификации");
            }
            else
            {
                throw new NullReferenceException($"Ошибочка вышла! На странице Формирование бэтчей однаружено {countOfAmplificationProgramms} программ аплификации вместо '{p0}'!");
            }
        }

        [Then(@"I create all available test batches on the Formig page")]
        public void ThenICreateAllAvailableTestBatchesOnTheFormigPage()
        {
            BDMainPage bDMain = new BDMainPage();
            bDMain.OpenBatchFormingWorkplace();
            IsTrue(bDMain.AssertOpenBatchFormingWorkplace(), "Что-то пошло не так. не найден заголовок Формирование бэтчей");
            BDFormingPage bDForming = new BDFormingPage();
            bDForming.WaitForVisibility();
            int countOfAmplificationProgramms = bDForming.ListOfAmplificationProgramms.Count;
            if (countOfAmplificationProgramms != 0)
            {
                while (bDForming.AssertWarningCanNotForming() & bDForming.AssertFirstSampleInListForForming())
                {
                    bDForming.ClickCreateTablet();
                    IsTrue(bDForming.AssertPreviewBatchMessage(), "Нет Сообщения о получении превью бэтча!");
                    bDForming.ClickToFormBatchButton();
                    IsTrue(bDForming.AssertBatchHaveFormedMessage(), "Ошибка! Нет сообщения о создание бэтча!");
                    Debug.WriteLine("ОК. Планшет собран");
                }
            }
            if (countOfAmplificationProgramms != 0)
            {
                while (bDForming.AssertWarningCanNotForming() & bDForming.AssertFirstSampleInListForForming())
                {
                    bDForming.ClickCreateTripod();
                    IsTrue(bDForming.AssertPreviewBatchMessage(), "Нет Сообщения о получении превью бэтча!");
                    bDForming.ClickToFormBatchButton();
                    IsTrue(bDForming.AssertBatchHaveFormedMessage(), "Ошибка! Нет сообщения о создание бэтча!");
                    Debug.WriteLine("ОК. Штатив Собран");
                }
            }
            if (bDForming.AssertFirstSampleInListForForming())
            {
                Debug.WriteLine("Что-то пошло не так - в списке еще есть пробы для формирования бэтчей");
            }
            else
            {
                Debug.WriteLine("Нет образцов для формирования бэтча");
            }
        }

        [Then(@"go to the bathes page to check for the presence of ""(.*)"" bathes in the status ""(.*)""")]
        public void ThenGoToTheBathesPageToCheckForThePresenceOfBathesInTheStatus(int p0, string p1)
        {
            // проверяем, что сформировано 3 бэтча в статусе Сформирован
            BDMainPage bDMain = new BDMainPage();
            bDMain.OpenBatches();
            IsTrue(bDMain.AssertOpenBatches(), "Что-то пошло не так. не найден заголовок Бэтчи");
            BDBatchesPage bDBatches = new BDBatchesPage();
            bDBatches.WaitSearchButtonClickability();
            int countOfListOfBatchesWithThisStatus = bDBatches.ListOfBatchesWithThisStatus(p1).Count;
            if (countOfListOfBatchesWithThisStatus == p0)
            {
                Debug.WriteLine($"ОК. На странице Бэтчи отображаются {p0} бэтчей в статусе {p1}");
            }
            else
            {
                Debug.WriteLine($"Что-то пошло не так! Выведено {countOfListOfBatchesWithThisStatus} в статусе {p1}. Должно быть {p0}");
            }
        }

        [Then(@"I check that batches with status ""(.*)"" had samples only with test ""(.*)"" or only with ""(.*)""")]
        public void ThenICheckThatBatchesWithStatusHadSamlesOnlyWithTestOrOnlyWith(string status, string testName1, string testName2)
        {
            // проверяем, что в каждом бэтче только один тест
            BDMainPage bDMain = new BDMainPage();
            bDMain.OpenBatches();
            IsTrue(bDMain.AssertOpenBatches(), "Что-то пошло не так. не найден заголовок Бэтчи");
            BDBatchesPage bDBatches = new BDBatchesPage();
            bDBatches.WaitSearchButtonClickability();
            // проходим по всем бэтчам в статусе Собран и проверяем, что внутри есть только один из тестов
            int counter = 1;
            foreach (Object i in bDBatches.ListOfBatchesWithThisStatus(status))
            {   
                bDBatches.OpenContentOfBatchForAnyStatus(status, counter);
                CommonSteps common = new CommonSteps();
                SecondTab = common.ThenTheTabWithTitleShouldBeOpened("BatchDropper");
                BDContentOfBatchPage bDContent = new BDContentOfBatchPage(SecondTab);
                IsFalse(bDContent.AssertFindSampleWithThisName(testName1) 
                & bDContent.AssertFindSampleWithThisName(testName2), "Что то пошло не так, в бэтче есть тесты с разными программами амплификации");
                Debug.WriteLine($"Проверка бэтча {counter} завершена");
                ++counter;
                bDMain.OpenBatches();
            }
        }

        [When(@"I go to database, I check that ""(.*)"" batches has status ""(.*)""")]
        public void WhenIGoToDatabaseICheckThenBatchesHasStatus(int counBatches, int status)
        {
            //Проверить, что бэтч в статусе Собран
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = {status}";
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command); // Сколько бэтчей со статусом "Сформирован"
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches);
            if (intCount == counBatches)
            {
                Debug.WriteLine($"ОК. В базе отображаются {counBatches} бэтча в статусе {status}");
            } 
            else
            {
                Debug.WriteLine($"Что-то пошло не так. В базе в статусе {status} отображаются {intCount} вместо {counBatches}");
            }
        }
    }
}

