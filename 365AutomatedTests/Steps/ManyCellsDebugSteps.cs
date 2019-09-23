using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace _365AutomatedTests
{
    using _365AutomatedTests.Steps;
    using Coypu;
    using static NUnit.Framework.Assert;
    [Binding]
    public class ManyCellsDebugSteps
    {
        ElementScope FirstTab;
        ElementScope SecondTab;

        [When(@"I create test with (.*) cells")]
        public void WhenICreateTestWithCells(int count)
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenDictionaries();
            IsTrue(BDMain.AssertOpenDictTest(), "Не найден заголовок на странице Тесты");

            BDDictTestPage BDDictTest = new BDDictTestPage();
            BDDictTest.ClickAddTestBtn();
           

            CommonSteps commons = new CommonSteps();
            SecondTab = commons.ThenTheTabWithTitleShouldBeOpened("BatchDropper");
            BDNewTestPage BDNewTest = new BDNewTestPage(SecondTab);
            BDNewTest.SetNameOfTest();

            //BDNewTest.SetControlOption();

            BDNewTest.SetNameOfProgAmp();
            BDNewTest.SetTubeVolume();
            BDNewTest.SetCheckboxIsActive();

            for (int i = 1; i < count; i++)
            {
                BDNewTest.ClickTheAddCellBtn();
                System.Threading.Thread.Sleep(200);
            }
            
            for (int j=0; j < count; j++)
            {
                BDNewTest.SetNameForSomeCell(j);
                //BDNewTest.SetControlOfSomeCell(j); //неактуально
                BDNewTest.SetFirstChannelOfSomeCell(j); 
                System.Threading.Thread.Sleep(500);
            }

            for (int k = 1; k < count; k++)
            {
               // BDNewTest.SetStandartOfSomeCell(k);
              //  BDNewTest.SetReagentOfSomeCell(k);
            }

            BDNewTest.SaveTest();
            
        }
        
        [Then(@"Test will be created")]
        public void ThenTestWillBeCreated()
        {
            //Переключаемся на первую вкладку, обновляем таблицу
            CommonSteps common = new CommonSteps();
            FirstTab = common.ThenTheTabWithTitleShouldBeOpened("BatchDropper");
            BDNewTestPage BDNewTest = new BDNewTestPage(FirstTab);
            BDDictTestPage BDDictTest = new BDDictTestPage();
            BDDictTest.CheckCountOfTests();
            // и проверяем что запись добавлена
            IsTrue(BDDictTest.AssertCountOfTestInfo(), "Не найдена информация о количестве записей");

        }
    }
}
