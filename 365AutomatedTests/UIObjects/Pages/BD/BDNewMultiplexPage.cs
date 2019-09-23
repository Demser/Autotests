using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;

namespace _365AutomatedTests.UIObjects.Pages
{
    class BDNewMultiplexPage : Page
    {
        public BDNewMultiplexPage() : base()
        {
        }
        private ElementScope ButtonSaveMultiplex => Scope.FindButton("Сохранить");
        private ElementScope FieldToSetNameOfMultiplex => Scope.FindId("multiplexName");
        private ElementScope FieldToSetNameOfProgApm => Scope.FindXPath("//label[.='Программа амплификации']/../div/input");
        private ElementScope FieldToSetIsolationControl => Scope.FindXPath("//label[.='Контроль выделения']/../div");
        private ElementScope FieldToSetTubeVolume => Scope.FindId("multiplexSampleVolume");
        private ElementScope FieldToSetMultiplexVolume => Scope.FindXPath("//label[.='Объем пробы (мкл)']/../div//span/input");
        private ElementScope FieldMinimumOfTestsCount(string _) => Scope.FindXPath("//label[.='Минимальное количество тестов']/../div//span/input");
        private ElementScope CheckboxIsActive => Scope.FindId("multiplexIsActive");
        private ElementScope CheckboxHasWax => Scope.FindId("multiplexHasWax");
        private ElementScope AddGroupButton => Scope.FindId("multiplexAddGroup"); // кнопка Добавить лунку

        //для всех лунок
        private ElementScope FieldToSetCellName(string _) => Scope.FindXPath($"//input[@id='group{_}name']");
        private ElementScope FieldToSetTest(string A, string B) => Scope.FindXPath($"//input[@id='group{A}channel{B}test']");
        //для первой лунки
        private ElementScope FieldToOpenControlOptions => Scope.FindXPath("//input[@placeholder='Выберите контрольный материал...']");//Scope.FindXPath("//span[@aria-controls='{{'group' + groupIndex + 'controlMaterial' }}_listbox']");
        private ElementScope ValueToSetControlOption => Scope.FindXPath("//li[@data-offset-index='2']"); // выбираем 3-ю опцию из выпадающего списка
        private ElementScope FieldToSetReagentOption => Scope.FindXPath("//label[.='Реагенты:']/../div/div/div/input");
        private ElementScope FieldToSetStandartOption => Scope.FindXPath("//label[.='Стандарты:']/../div/div/div/input");
        //для следующих лунок
        private ElementScope FieldToSetControlOfSomeCell(int _) => Scope.FindXPath($"//div[@id='group{_}controlMaterial']/span/span/input"); // где _  это номер АЙДИШНИКА от 0 до ...
        private ElementScope FieldToSetStandartOfSomeCell(string _) => Scope.FindXPath($"//div[{_}]/div[4]/div/div/div/input");// где _ это номер лунки
        private ElementScope FieldToSetReagentOfSomeCell(string _) => Scope.FindXPath($"//div[{_}]/div[5]/div/div/div/input");// где _ это номер лунки

        
        public void SetNameOfMultiplex()
        {
            FieldToSetNameOfMultiplex.Click();
            FieldToSetNameOfMultiplex.SendKeys("AutoMultiplex1"); //задает имя для мультиплекса AutoMultiplex1
            FieldToSetNameOfMultiplex.SendKeys(Keys.Enter);
        }

        public void SetMinimumOfTestsCount(string _)
        {
            FieldMinimumOfTestsCount(_).Click();
            FieldMinimumOfTestsCount(_).SendKeys(_);//задает минимальное число тестов
        }


    }
}
