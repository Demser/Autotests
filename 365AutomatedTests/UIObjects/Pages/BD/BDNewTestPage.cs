using System;
using System.Collections.Generic;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;

namespace _365AutomatedTests.UIObjects.Pages
{
    class BDNewTestPage : Page
    {
        public BDNewTestPage() : base()
        {
        }

        public BDNewTestPage (ElementScope scope) : base (scope)
        {
        }
        //Общие элементы:
        private ElementScope TittleCells => Scope.FindXPath("//h3[.='Лунки']");
        private ElementScope ButtonSaveTest => Scope.FindButton("Сохранить");
        private ElementScope FieldToSetNameOfTest => Scope.FindXPath("//label[.='Название']/../div//span/input");
        private ElementScope FieldToSetNameOfProgApm => Scope.FindXPath("//label[.='Программа амплификации']/../div/input");
        private ElementScope FieldToSetSubgroup => Scope.FindXPath("//label[.='Подгруппы выделения']/../div/div/div/input");
        private ElementScope FieldToSetTubeVolume => Scope.FindXPath("//label[.='Объем пробы (мкл)']/../div//span/input");
        private ElementScope CheckboxTubeIsolationForDNK => Scope.FindId("multiplexIsAllocateInTube");
        private ElementScope CheckboxIsActive => Scope.FindId("multiplexIsActive");
        private ElementScope CheckboxHasWax => Scope.FindId("multiplexHasWax");
        private ElementScope FieldToSetDoubles => Scope.FindXPath("//input[@id='sampleCopyCountId']//../input");
        private ElementScope FieldToSetReagentsOT => Scope.FindXPath("//ul[@id='multiplexReagents_taglist']//../input");
        private ElementScope OptionLiWithContainsTextParameter(string _) => Scope.FindXPath($"//li[contains(text(),'{_}')]"); // li cодержащий текст
        private ElementScope OptionLiWithTextParameter(string _) => Scope.FindXPath($"//li[.='{_}']"); // li c явно указанны текстом
        private ElementScope FieldForFillIn => Scope.FindXPath("//ul[@aria-hidden='false']"); // Область с опциями выпадающего списка из FieldToSetFirstChannelOfSomeCell
        private ElementScope FirstOptionInOpenedField => Scope.FindXPath("//ul[@aria-hidden='false']/li[@data-offset-index='0']");
        private ElementScope AddGroupButton => Scope.FindId("multiplexAddGroup");

        private ElementScope OptionWithTextIsParameter(string _) => Scope.FindXPath($"//li[contains(text(),'{_}')]");

        // Для единственной лунки в тесте:
        private ElementScope LabelStandarts => Scope.FindXPath("//label[.='Стандарты:']");
        private ElementScope FieldToSetNameOfFirstCell => Scope.FindXPath("//label[.='Название:']/../div/input");
        private ElementScope FieldToOpenControlOptionOneCell => Scope.FindXPath("//div[@id='group0controlMaterial']/span/span/input");
        private ElementScope FieldToSetReagentOption => Scope.FindXPath("//label[.='Реагенты:']/../div/div/div/input");
        private ElementScope FieldToOpenStandartOptionsOneCell => Scope.FindXPath("//control-materials-editor[@id='group0standards']/div/input");//[@class='k-textbox ng-pristine ng-valid ng-touched ng-empty']");
        private ElementScope FieldToChooseStandartOptionsOneCell => Scope.FindXPath("//control-materials-editor[@id='group0standards']/div/div/span/span/span[.='Выберите контрольный материал...']");
        // Для заполнения компонентов-каналов в однолуночном тесте:
        private ElementScope FieldToOpenComponents(string _) => Scope.FindXPath($"//td[contains(.,'{_}')]/../td//span[@role='listbox']"); // используется только в методе c KeyValue
        private ElementScope FieldListSelectValue(string _) => Scope.FindXPath($"//ul[@data-role='staticlist' and @role='listbox']/li[contains(.,'{_}')]"); // используется только в методе c KeyValue
        private ElementScope FieldToSetFirstChannelOfFirstCell => Scope.FindXPath("//select[@id='group0channel0component']/..");
        private ElementScope FieldToSetSecondChannelOfFirstCell => Scope.FindXPath("//select[@id='group0channel1component']/..");
        private ElementScope FieldToSetThirdChannelOfFirstCell => Scope.FindXPath("//select[@id='group0channel2component']/..");

        // Для теста, в котором две или больше лунок:
        private ElementScope FieldToSetCellName(int _) => Scope.FindXPath($"//input[@id='group{_}name']");      
        private ElementScope FieldToSetControlOfSomeCell(int _) => Scope.FindXPath($"//div[@id='group{_}controlMaterial']/span/span/input"); // где _ это номер АЙДИШНИКА от 0 до ...
        private ElementScope FieldToOpenStandartOptionsSomeCell(int _) => Scope.FindXPath($"//control-materials-editor[@id='group{_}standards']/div/input");
        private ElementScope FieldToChooseStandartOptionsSomeCell(int _) => Scope.FindXPath($"//control-materials-editor[@id='group{_}standards']/div/div/span/span/span[.='Выберите контрольный материал...']");
        private ElementScope FieldToSetReagentOfSomeCell(int _) => Scope.FindXPath($"//div[{_}]/div[5]/div/div/div/input"); // где _ это номер лунки
        private ElementScope FieldToSetFirstChannelOfSomeCell(int _) => Scope.FindXPath($"//select[@id='group{_}channel0component']/..");
        private ElementScope FieldToSetSecondChannelOfSomeCell(int _) => Scope.FindXPath($"//select[@id='group{_}channel1component']/..");
        private ElementScope FieldToSetThirdChannelOfSomeCell(int _) => Scope.FindXPath($"//select[@id='group{_}channel2component']/..");

        public void SetNameOfTestWithArg(string _) // Заполнить имя теста из аргумента
        {
            FieldToSetNameOfTest.Click();
            FieldToSetNameOfTest.SendKeys(_);
            FieldToSetNameOfTest.SendKeys(Keys.Tab);
        }
        public void SetNameOfProgAmpWithArg(string _) // Установить программу амплификации из аргумента
        {
            FieldToSetNameOfProgApm.Click();
            FieldToSetNameOfProgApm.SendKeys(_);
            FieldToSetNameOfProgApm.SendKeys(Keys.Tab);
        }
        public void SetNameOfSubgroup(string _) // Установить подгруппу выделения
        {
            FieldToSetSubgroup.Click();
            FieldToSetSubgroup.SendKeys(_);
            OptionLiWithContainsTextParameter(_).Click();
        }
        public void SetVolumeOfTube(int _) // Установить объем пробирки
        {
            FieldToSetTubeVolume.Click();
            string s = _.ToString();
            FieldToSetTubeVolume.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            FieldToSetTubeVolume.SendKeys(s);
        }
        public void SetCheckboxIsActive() // Установить чекбокс Активен
        {CheckboxIsActive.Click();}

        public void SetCheckboxHasWax() // Установить чекбокс ПЦР с воском
        {CheckboxHasWax.Click();}

        public void SetCheckboxTubeIsolationForDNK() // Установить чекбокс Выделяется в пробирке
        {CheckboxTubeIsolationForDNK.Click();}

        public void SetCountOfDoubles(int _) // Установить количество дублей
        {
            FieldToSetDoubles.Click();
            string s = _.ToString();
            FieldToSetDoubles.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            FieldToSetDoubles.SendKeys(s);
        }
        public void SetReagentsOT(string _) // Установить реагенты ОТ
        {
            FieldToSetReagentsOT.Click();
            FieldToSetReagentsOT.SendKeys(_);
            OptionLiWithContainsTextParameter(_).Click();
        }
        public void ClickTheAddCellBtn() // Нажать на кнопку добавления лунки
        {
            AddGroupButton.Click();
        }
        public void SaveTest() // Нажать на кнопку Сохранить
        {
            ButtonSaveTest.Click();
            System.Threading.Thread.Sleep(300);
        }

        //┏ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━  ━ ━ МЕТОДЫ ДЛЯ ОДНОЛУНОЧНОГО ТЕСТА ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ┓

        public void SetNameOfFirstCell() // Имя лунки
        {
            FieldToSetNameOfFirstCell.Click();
            FieldToSetNameOfFirstCell.SendKeys("autoCell");
        }
        public void SetControlOption(string _) // Установить контроль для лунки однолуночного теста
        {
            FieldToOpenControlOptionOneCell.Click();
            FieldToOpenControlOptionOneCell.SendKeys(_);
            System.Threading.Thread.Sleep(500);
            FieldToOpenControlOptionOneCell.SendKeys(Keys.Enter);
        }
        public void SetStandartOption(string _) // Установить стандарт для лунки однолуночного теста
        {

            System.Threading.Thread.Sleep(500);
            FieldToOpenStandartOptionsOneCell.Click();
            System.Threading.Thread.Sleep(500);
            FieldToChooseStandartOptionsOneCell.Click();
            System.Threading.Thread.Sleep(500);
            OptionLiWithTextParameter(_).Click();
            System.Threading.Thread.Sleep(500);
            LabelStandarts.Click();
        }

        public void SetReagentOption(string _) // Установить реагент для лунки однолуночного теста
        {
            FieldToSetReagentOption.Click();
            //System.Threading.Thread.Sleep(500);
            FieldToSetReagentOption.SendKeys(_);
            System.Threading.Thread.Sleep(500);
            FieldToSetReagentOption.SendKeys(Keys.Enter);
        }
        public void SetFirstChannelOfOneCell() // Компонент первый
        {
            FieldToSetFirstChannelOfFirstCell.Click();
            System.Threading.Thread.Sleep(200);
            FirstOptionInOpenedField.Click();
        }
        public void SetSecondChannelOfOneCell() // Компонент второй
        {
            FieldToSetSecondChannelOfFirstCell.Click();
            System.Threading.Thread.Sleep(200);
            FirstOptionInOpenedField.Click();
        }
        public void SetThirdChannelOfOneCell() // Компонент третий
        {
            FieldToSetThirdChannelOfFirstCell.Click();
            System.Threading.Thread.Sleep(200);
            FirstOptionInOpenedField.Click();
        }

        //┏ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━  ━ ━ МЕТОДЫ ДЛЯ МНОГОЛУНОЧНОГО ТЕСТА ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ┓

        public void SetNameForSomeCell(int _) // Установить имена n-ного количества лунок
        {
            FieldToSetCellName(_).Click();
            FieldToSetCellName(_).SendKeys("autocell" + _);
        }
        public void SetControlOfSomeCell(int _, string value) // Установить КОНТРОЛЬ для каждой из n-ного количества лунок
        {
            FieldToSetControlOfSomeCell(_).Click();
            FieldToSetControlOfSomeCell(_).SendKeys(value);
            System.Threading.Thread.Sleep(300);
            FieldToSetControlOfSomeCell(_).SendKeys(Keys.Enter);
        }
        public void SetStandartOfSomeCell(int _, string value) // Установить СТАНДАРТ для каждой из n-ного количества лунок
        {
            FieldToOpenStandartOptionsSomeCell(_).Click();
            System.Threading.Thread.Sleep(500);
            FieldToChooseStandartOptionsSomeCell(_).Click();
            System.Threading.Thread.Sleep(500);
            OptionLiWithTextParameter(value).Click();
            System.Threading.Thread.Sleep(500);
            TittleCells.Click();
        }
        public void SetReagentOfSomeCell(int _, string value) // Установить РЕАГЕНТ для каждой из n-ного количества лунок
        {
            FieldToSetReagentOfSomeCell(_).Click();
            System.Threading.Thread.Sleep(500);
            FieldToSetReagentOfSomeCell(_).SendKeys(value);
            System.Threading.Thread.Sleep(500);
            FieldToSetReagentOfSomeCell(_).SendKeys(Keys.Enter);
        }
        public void SetFirstChannelOfSomeCell(int _) // Компонент первый для каждой из лунок
        {
            FieldToSetFirstChannelOfSomeCell(_).Click();
            System.Threading.Thread.Sleep(200);
            FirstOptionInOpenedField.Click();
        }
        public void SetSecondChannelOfSomeCell(int _) // Компонент второй для каждой из лунок
        {
            System.Threading.Thread.Sleep(200);
            FieldToSetSecondChannelOfSomeCell(_).Click();
            System.Threading.Thread.Sleep(200);
            FirstOptionInOpenedField.Click();
        }
        public void SetThirdChannelOfSomeCell(int _) // Компонент третий для каждой из лунок
        {
            FieldToSetThirdChannelOfSomeCell(_).Click();
            System.Threading.Thread.Sleep(200);
            FirstOptionInOpenedField.Click();
        }

        //┏ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━  ━ ━ СТАРЫЕ МЕТОДЫ С ЗАХАРДКОЖЕННЫМИ ЗНАЧЕНИЯМИ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ━ ┓

        public void SetNameOfTest() // Заполнить имя теста
        {
            FieldToSetNameOfTest.Click();
            FieldToSetNameOfTest.SendKeys("CYTOMEGALOVIRUS_ДНК");
            FieldToSetNameOfTest.SendKeys(Keys.Enter);
        }
        public void SetNameOfProgAmp() // Установить программу амплификации
        {
            FieldToSetNameOfProgApm.Click();
            FieldToSetNameOfProgApm.SendKeys("autotest");
        }
        public void SetTubeVolume() // Установить объем пробирки
        {
            FieldToSetTubeVolume.Click();
            FieldToSetTubeVolume.SendKeys("5");
        }
        public void SetComponentForFirstCell(List<KeyValuePair<string, string>> list)
        {
            foreach(var i in list)
            {
                if(!string.IsNullOrEmpty(i.Value))
                {
                    FieldToOpenComponents(i.Key).Click();
                    FieldListSelectValue(i.Value).Click();
                }
            }

        }
    }       
}
