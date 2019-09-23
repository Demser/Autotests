using _365AutomatedTests.UIObjects.Pages;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace _365AutomatedTests.Steps
{
    using Coypu;
    using OpenQA.Selenium;
    using _365AutomatedTests.Steps;
    using static NUnit.Framework.Assert;
    using System.Collections.Generic;
    using System.Linq;
    using _365AutomatedTests.Framework;

    [Binding]
    public class BDCreateTestinDictionariesSteps
    {
        ElementScope SecondTab;
         Models.BDTest createTest; // объявили переменную-ссылку на коллекцию для создания теста

        [When(@"I create test with parameters from the table")]
        public void WhenICreateTestWithParametersFromTheTable(Table table)
        {
            var BDTestInstance = table.CreateInstance<Models.BDTest>();
            //  BDTestInstance.Subgroups = table.CreateInstance<List<String>>(); 
            //  var result = BDTestInstance.GetSubrgoups;

            var resultListOfSubgroups = BDTestInstance.GetList(f => f.Subgroups);
            var resultListOfReagentsOT = BDTestInstance.GetList(f => f.ReagentsOT);
            var resultOfReagents = BDTestInstance.GetList(f => f.CellReagents);
            var resultOfStandarts = BDTestInstance.GetList(f => f.CellStandarts);
           
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenDictionaries();
            IsTrue(BDMain.AssertOpenDictTest(), "Не найден заголовок на странице Тесты");

            BDDictTestPage BDDictTest = new BDDictTestPage();
            BDDictTest.ClickAddTestBtn();

            CommonSteps commons = new CommonSteps();
            SecondTab = commons.ThenTheTabWithTitleShouldBeOpened("BatchDropper");
            BDNewTestPage BDNewTest = new BDNewTestPage(SecondTab);

            BDNewTest.SetNameOfProgAmpWithArg(BDTestInstance.AmpProgram); // Заполнение программы амплификации

            BDNewTest.SetNameOfTestWithArg(BDTestInstance.Name); // Заполнение имени теста
            System.Threading.Thread.Sleep(200);           

            foreach (string sg in resultListOfSubgroups){BDNewTest.SetNameOfSubgroup(sg);} // Заполнение подгруппы Выделения

            if (BDTestInstance.Volume != 0) { BDNewTest.SetVolumeOfTube(BDTestInstance.Volume); } // Заполнение объема пробирки
            else { throw new InvalidCastException("Объем пробирки не может быть нулевым. Измените значение в файле сценария"); }


            if (BDTestInstance.IsDNA == "true") {BDNewTest.SetCheckboxTubeIsolationForDNK();} // Чекбокс ДНК
            else if (BDTestInstance.IsDNA == "false") { }
            else { throw new ArgumentOutOfRangeException("Значение агрумента должно быть строго true либо false. Измените значение в файле сценария"); }

            if (BDTestInstance.IsWAX == "true") {BDNewTest.SetCheckboxHasWax();} // Чекбокс ВОСК
            else if (BDTestInstance.IsWAX == "false") { }
            else { throw new ArgumentOutOfRangeException("Значение агрумента должно быть строго true либо false. Измените значение в файле сценария"); }

            if (BDTestInstance.IsActive == "false") {BDNewTest.SetCheckboxIsActive();} // Чекбокс Активен
            else if (BDTestInstance.IsActive == "true") { }
            else { throw new ArgumentOutOfRangeException("Значение агрумента должно быть строго true либо false. Измените значение в файле сценария"); }

            if (BDTestInstance.Doubles != 0) {BDNewTest.SetCountOfDoubles(BDTestInstance.Doubles);} // Дубли
            else { throw new NullReferenceException("Количество дублей не может быть нулевым. Измените параметр в файле сценария"); }

            if (resultListOfReagentsOT.First() != "")
            { 
            foreach (string r in resultListOfReagentsOT) {BDNewTest.SetReagentsOT(r); } // Заполнение Реагентов ОТ.
            }

                //_________________________________________ЛУНКИ________________________________________________________________

                int count = BDTestInstance.CellsCount; // Получение указанного количества лунок для сценария

            if (count == 1) // Строго для однолуночного теста
            {
                //Заполняем канал только для первой лунки:
                if (BDTestInstance.Fam == "true") { BDNewTest.SetFirstChannelOfOneCell(); } else if (BDTestInstance.Fam == "false") { } else { throw new ArgumentOutOfRangeException("Значение агрумента fam должно быть строго true либо false. Измените значение в файле сценария"); }
                if (BDTestInstance.Hex == "true") { BDNewTest.SetSecondChannelOfOneCell(); } else if (BDTestInstance.Hex == "false") { } else { throw new ArgumentOutOfRangeException("Значение агрумента hex должно быть строго true либо false. Измените значение в файле сценария"); }
                if (BDTestInstance.Rox == "true") { BDNewTest.SetThirdChannelOfOneCell(); } else if (BDTestInstance.Rox == "false") { } else { throw new ArgumentOutOfRangeException("Значение агрумента rox должно быть строго true либо false. Измените значение в файле сценария"); }

                BDNewTest.SetNameOfFirstCell(); // Заполняем имя только для первой лунки

                BDNewTest.SetControlOption(BDTestInstance.CellControl); // Заполняем луночный контроль первой лунки
                System.Threading.Thread.Sleep(1000);
                if (resultOfReagents.First() != "")
                {
                    foreach (string r in resultOfReagents) { BDNewTest.SetReagentOption(r); System.Threading.Thread.Sleep(900); } // Заполняем реагенты первой лунки
                }
                if (resultOfStandarts.First() != "")
                {
                    foreach (string j in resultOfStandarts) { BDNewTest.SetStandartOption(j); } //Заполняем страндартs для первой лунки
                }
                BDNewTest.SaveTest(); // Сохраняем тест

            }
            else if (count > 1 && count <=20) // Если количество лунок в тесте не равно 1
            {
                for (int i = 1; i < count; i++) // Кликаем на кнопку добавления лунки нужное количество раз
                {
                    BDNewTest.ClickTheAddCellBtn();
                    System.Threading.Thread.Sleep(100);
                }

                for (int j = 0; j < count; j++) // Заполнение нужного количества лунок значениями
                {
                    BDNewTest.SetNameForSomeCell(j); //Заполнение имени каждой лунки теста
                    if (BDTestInstance.CellControl != "")
                    {BDNewTest.SetControlOfSomeCell(j, BDTestInstance.CellControl);} // Заполнение луночного контроля для лунок
                    else { }

                    if (BDTestInstance.Fam == "true") { BDNewTest.SetFirstChannelOfSomeCell(j); } else if (BDTestInstance.Fam == "false") { } else { throw new ArgumentOutOfRangeException("Значение агрумента fam должно быть строго true либо false. Измените значение в файле сценария"); }
                    if (BDTestInstance.Hex == "true") { BDNewTest.SetSecondChannelOfSomeCell(j); } else if (BDTestInstance.Hex == "false") { } else { throw new ArgumentOutOfRangeException("Значение агрумента hex должно быть строго true либо false. Измените значение в файле сценария"); }
                    if (BDTestInstance.Rox == "true") { BDNewTest.SetThirdChannelOfSomeCell(j); } else if (BDTestInstance.Rox == "false") { } else { throw new ArgumentOutOfRangeException("Значение агрумента rox должно быть строго true либо false. Измените значение в файле сценария"); }
                    System.Threading.Thread.Sleep(1100);
                }

                for (int k = 0; k <= count-1; k++) // Заполнение нужного количества лунок значениями
                {
                    if (resultOfStandarts.First() != "")
                    {
                    foreach (string st in resultOfStandarts)

                        {
                        BDNewTest.SetStandartOfSomeCell(k, st);
                        }
                    }
                }
                for (int p = 1; p <= count; p++) // Заполнение нужного количества лунок значениями
                {
                    foreach (string r in resultOfReagents)
                    {
                        BDNewTest.SetReagentOfSomeCell(p,r);
                    }
                }

                BDNewTest.SaveTest();
                
            }
            else { throw new ArgumentOutOfRangeException("Давайте не будем создавать более 20-ти лунок в тесте."); }










            System.Threading.Thread.Sleep(2000);

        }

       // [StepArgumentTransformation]
        //public List<String> TransformToListOfString(string commaSeparatedList)
        //{
        //    return commaSeparatedList.Split(',').ToList();
       // }
    }


}
