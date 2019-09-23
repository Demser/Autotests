﻿using System;
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
    public class BDTestGetPKOOKOSteps
    {
        [Then(@"I fill-out the Sorting-planchet by ""(.*)"" samples ""(.*)""-own of ""(.*)""-union test ""(.*)"" with biomaterial ""(.*)"" and location ""(.*)"" and '(.*)' VK and '(.*)' OKO and '(.*)' PKO")]
        public void ThenIFill_OutTheSorting_PlanchetBySamplesOwnOfUnionTestWithBiomaterialAndLocationAndVKAndOKOAndPKO(int count, string own, string union, List<String> testname, string bm, string location, List<String> VKName, List<String> OKOName, List<String> PKOName)
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
                int countOfTests = testname.Count();
                MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
                string command = "";

                if (own == "only") // для случая, когда образец назначен ТОЛЬКО на тест/тесты
                {
                    if (countOfTests == 1) {
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
                    and s.LOCATION = '{location}' and t.ANALYSIS='{testname[0]}' and s.SAMPLE_TYPE ='{bm}'"; }
                    if (countOfTests == 2)
                    {
                        if (union == "")
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
                        else if (union == "on")
                        {
                            command = $@"USE LW08v6 
                    select top({count}) s.TEXT_ID, s.STATUS, t.STATUS, s.RECD_DATE, s.ORDER_NUM, s.sample_type, t.ANALYSIS from test t with(nolock)
                    inner join SAMPLE s with(nolock) on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    where t.status='I' and t.ANALYSIS='{testname[0]}' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = '{location}' and sample_type='{bm}'
                    union
                    select top({count}) s.TEXT_ID, s.STATUS, t.STATUS, s.RECD_DATE, s.ORDER_NUM, s.sample_type, t.ANALYSIS from test t with(nolock)
                    inner join SAMPLE s with(nolock) on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    where t.status='I' and t.ANALYSIS='{testname[1]}' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = '{location}' and sample_type='{bm}'";
                        }
                    }
                    
                }

                if (countOfTests == 2) // если 2 теста в коллекции, выполняем скрипт:
                {
                    if (union == "")
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
                    else if (union == "on")
                    {
                        command = $@"USE LW08v6 
                    select top({count}) s.TEXT_ID from test t with(nolock)
                    inner join SAMPLE s with(nolock) on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    where t.status='I' and t.ANALYSIS='{testname[0]}' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = '{location}' and sample_type='{bm}'
                    union
                    select top({count}) s.TEXT_ID from test t with(nolock)
                    inner join SAMPLE s with(nolock) on t.SAMPLE_NUMBER=s.SAMPLE_NUMBER
                    where t.status='I' and t.ANALYSIS='{testname[1]}' and s.TEMPLATE = 'smp_in' and s.status='I' and s.LOCATION = '{location}' and sample_type='{bm}'";
                    }
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
                                                             
                //sortingPage.IsSampleCodeFieldActive(); // закомментировано. 110719 стало зависать. Ранее работало.
                sortingPage.CodeInputField.WaitForClickability();

                foreach (string i in result)
                {
                    sortingPage.CodeInputField.SendKeys(i);
                    sortingPage.CodeInputField.SendKeys(Keys.Enter);
                    //sortingPage.IsSampleCodeFieldActive(); // закомментировано. 110719 стало зависать. Ранее работало.
                    sortingPage.CodeInputField.WaitForClickability();
                    if (sortingPage.FieldWithUsercodePlaceholder.Exists())
                    { sortingPage.FieldWithUsercodePlaceholder.SendKeys(Config.UserBarCode);
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
                        Debug.WriteLine("Раскапан "+k);
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
        [StepArgumentTransformation]
        public List<String> TransformToListOfString(string commaSeparatedList)
        {
            return commaSeparatedList.Split(',').ToList();
        }
    }
}