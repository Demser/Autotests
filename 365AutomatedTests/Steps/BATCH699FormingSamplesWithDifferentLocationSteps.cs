using _365AutomatedTests.UIObjects.Pages;
using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using Coypu;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using static NUnit.Framework.Assert;
using System.Diagnostics;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class BATCH699FormingSamplesWithDifferentLocationSteps
    {
        [Then(@"I change Terminal to ""(.*)""")]
        public void ThenIChangeTerminalTo(string terminal)
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.SetAnotherTerminal(terminal);
        }

        [Then(@"I check fill-out the isolation-planchet by ""(.*)"" samples of test ""(.*)"" with biomaterial ""(.*)"" from first location ""(.*)"" and second location ""(.*)"" and '(.*)' VK and '(.*)' OKO and '(.*)' PKO")]
        public void ThenIFill_OutTheIsolation_PlanchetBySamplesOfTestWithBiomaterialFromFirstLocationAndSecondLocationAndVKAndOKOAndPKO(int count, string testname, string bm, string location1, string location2, List<String> VKName, List<String> OKOName, List<String> PKOName)
        {
            //переходим в РМ сортировка
            BDMainPage BDMain = new BDMainPage();
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
            {
                //int countOfTests = testname.Count();
                MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
                string command1 = "";
                command1 = $@"USE LW08v6
                    select TOP({count}) s.TEXT_ID
                    from
                    (select SAMPLE_NUMBER, count(ANALYSIS) as TestCommonCount
                    from [LW08v6].[dbo].[TEST] t with (nolock) 
                    group by SAMPLE_NUMBER
                    having count(ANALYSIS) = 1) MonoSamples
                    join TEST t on t.SAMPLE_NUMBER = MonoSamples.SAMPLE_NUMBER
                    join SAMPLE s on S.SAMPLE_NUMBER = t.SAMPLE_NUMBER
                    where t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I' 
                    and s.LOCATION = '{location1}' and t.ANALYSIS='{testname}' and s.SAMPLE_TYPE ='{bm}'";
                string command2 = "";
                command2 = $@"USE LW08v6
                    select TOP({count}) s.TEXT_ID
                    from
                    (select SAMPLE_NUMBER, count(ANALYSIS) as TestCommonCount
                    from [LW08v6].[dbo].[TEST] t with (nolock) 
                    group by SAMPLE_NUMBER
                    having count(ANALYSIS) = 1) MonoSamples
                    join TEST t on t.SAMPLE_NUMBER = MonoSamples.SAMPLE_NUMBER
                    join SAMPLE s on S.SAMPLE_NUMBER = t.SAMPLE_NUMBER
                    where t.status='I' and s.TEMPLATE = 'smp_in' and s.status='I' 
                    and s.LOCATION = '{location2}' and t.ANALYSIS='{testname}' and s.SAMPLE_TYPE ='{bm}'";

                var result1 = _msBDConnectorLW.QueryExecutor(command1); // образцы для первой локации
                var result2 = _msBDConnectorLW.QueryExecutor(command2); // образцы для второй локации
                //result.AddRange(result2); //объединить списки

                sortingPage.CodeInputField.WaitForClickability();

                foreach (string i in result1)
                {
                    sortingPage.CodeInputField.SendKeys(i);
                    sortingPage.CodeInputField.SendKeys(Keys.Enter);
                    sortingPage.CodeInputField.WaitForClickability();
                    IsFalse(sortingPage.AssertSampleFromAnotherLocationMessage(location1), $"Что то пришло не так, не проходят пробы из локации {location1}");
                    if (sortingPage.FieldWithUsercodePlaceholder.Exists())
                    {
                        sortingPage.FieldWithUsercodePlaceholder.SendKeys(Config.UserBarCode);
                        sortingPage.FieldWithUsercodePlaceholder.SendKeys(Keys.Enter);
                    }
                }
                foreach (string n in result2)
                {
                    sortingPage.CodeInputField.SendKeys(n);
                    sortingPage.CodeInputField.SendKeys(Keys.Enter);
                    sortingPage.CodeInputField.WaitForClickability();
                    IsTrue(sortingPage.AssertSampleFromAnotherLocationMessage(location2), $"Что то пришло не так, не так - не приходит ошибка о добавлении пробы из локации {location2}");
                    if (sortingPage.FieldWithUsercodePlaceholder.Exists())
                    {
                        sortingPage.FieldWithUsercodePlaceholder.SendKeys(Config.UserBarCode);
                        sortingPage.FieldWithUsercodePlaceholder.SendKeys(Keys.Enter);
                    }
                }
                if (sortingPage.CompleteAddSamplesButton.Exists() && !sortingPage.CompleteAddSamplesButtonDisable.Disabled)
                {
                    sortingPage.CompleteAddSamplesButton.Click();
                    sortingPage.ConfirmEndOfSamplesButton.Click();
                }
                else if (sortingPage.CompleteAddSamplesButton.Exists() && sortingPage.CompleteAddSamplesButtonDisable.Disabled)
                {
                    sortingPage.CodeInputField.SendKeys(Config.UserBarCode);  // 
                    sortingPage.CodeInputField.SendKeys(Keys.Enter);
                }
                else if (sortingPage.CompleteBatchButton.Exists())
                {
                    sortingPage.CompleteBatchButton.Click();
                    sortingPage.ConfirmEndOfSamplesButton.Click();
                }
                System.Threading.Thread.Sleep(500);

                if (sortingPage.CodeInputField.Exists())
                {
                    sortingPage.CodeInputField.WaitForAvailability();
                    sortingPage.CodeInputField.WaitForVisibility();
                    sortingPage.CodeInputField.WaitForClickability();
                }
                // Подтверждается раскапывание ВК
                if (VKName[0] != "")
                {
                    foreach (string j in VKName)
                    {
                        sortingPage.SetValueOfKM(j);
                        Debug.WriteLine("Раскапан " + j);
                    }
                    //     sortingPage.SetValueOfKM(VKName);
                }
                // Подтверждается раскапывание ОКО
                if (OKOName[0] != "")
                {
                    foreach (string k in OKOName)
                    {
                        sortingPage.SetValueOfKM(k);
                        Debug.WriteLine("Раскапан " + k);
                    }
                }
                // Подтверждается раскапывание ПКО
                if (PKOName[0] != "")
                {
                    foreach (string m in PKOName)
                    {
                        sortingPage.SetValueOfKM(m);
                        Debug.WriteLine(DateTime.Now + "Раскапан " + m);
                    }
                }
            }
        }
    

        [Then(@"I go to the Batches page to check that from location ""(.*)"" is ""(.*)""")]
        public void ThenIGoToTheBatchesPageToCheckThatFromLocationIs(string location, string condition)
        {
            BDMainPage bDMain = new BDMainPage();
            bDMain.OpenBatches();
            IsTrue(bDMain.AssertOpenBatches(), "Что-то пошло не так. не найден заголовок Бэтчи");
            //проверить, что отображается бэтч только из своей локации
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"use BatchDropperStab
            SELECT TOP (1) b.id FROM batches  b with (nolock)
            inner join Locations l on l.id=b.LocationId
            WHERE l.name = '{location}'";
            var BatchId = _msBDConnector.QueryExecutorScalar(command);
            BDBatchesPage bDBatches = new BDBatchesPage();
            if (condition == "exists")
            {
                IsTrue(bDBatches.AssertBatchWithId(BatchId), $"Что то пошло не так - не находит бэтч {BatchId} в локации {location}");
            }
            else
            {
                IsFalse(bDBatches.AssertBatchWithId(BatchId), $"Что то пошло не так - найден бэтч {BatchId} в локации {location}");
            }
        }



    }
}
