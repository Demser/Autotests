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
    class BDSettingsPage : Page
    {
        public BDSettingsPage() : base()
        {
        }
        private ElementScope NameFilter => Scope.FindXPath("//a[.='Название']/../a/span"); //находит первый фильтр, то есть тот, который в графе Название

        private List<ElementScope> FilterFields => Scope.FindAllXPath(".//span[@class='k-input' and .='равно']").Select(i=>(ElementScope)i).ToList();
        private ElementScope FilterFieldName => FilterFields.ElementAt(0);
        private ElementScope FilterFieldValue => FilterFields.ElementAt(1);
        private ElementScope ExperimentModeStatus => Scope.FindXPath("//td[contains(text(),'Включен')]");/*поле Значение для режима испытаний, ищем по тексту Включен,
        после того, как уже отфильтровали по названию*/

        private ElementScope InputforSetName => Scope.FindXPath("//input[@data-bind='value:filters[0].value']");





        public string ExperimentModeStatusText => ExperimentModeStatus.Text; //записывается значение из ячейки "Включен"

        private ElementScope SettingEditBtn => Scope.FindXPath("//a[.='Изменить']");//кнопка Изменить
        private ElementScope SettingSaveBtn => Scope.FindXPath("//a[.='Сохранить']");//кнопка Сохранить
        private ElementScope SettingCancelBtn => Scope.FindXPath("//a[.='Отменить']");//кнопка Отменить

        private ElementScope SettingOn => Scope.FindXPath("//span[.='Включен']"); //находим элемент "Включен" в выпадающем списке, в случае, когда опция включена
        private ElementScope ModeSettingIsOff => Scope.FindXPath("//li[.='Выключен']"); // находим элемент "Выключен" в выпадающем списке, в случае когда опция включена
        private ElementScope SettingOff => Scope.FindXPath("//span[.='Выключен']"); //находим элемент "Выключен" в выпадающем списке, когда опция выключена
        private ElementScope ModeSettingIsOn => Scope.FindXPath(("//li[.='Включен']")); // находим элемент "Включен" в выпадающем списке, когда опция выключена

        public void OpenNameFilter()
        {
            NameFilter.Click(); //открыли фильтр по Названию
        }


        public void SetOptionOfName()
        {
            InputforSetName.Click();
            System.Threading.Thread.Sleep(1000);
            InputforSetName.SendKeys("Режим испытаний (можно пустить в работу пробы с результатами)");
            System.Threading.Thread.Sleep(1000);
            InputforSetName.SendKeys(Keys.Enter);
        }

        public void SelectOption()
        {
            FilterFieldName.FillInWith("Режим испытаний");//ввели значение и нажали Фильтровать (или не нажали)(проверить)
        }
        public void ClickSettingEditBtn()
        {
            SettingEditBtn.Click();//нажимаем на кнопку "Изменить"
        }
        public void ClickSettingSaveBtn()
        {
            SettingSaveBtn.Click();//нажимаем на кнопку Сохранить
        }
        public void ClickSettingCancelBtn()
        {
            SettingCancelBtn.Click();//Нажимаем кнопку "Отменить"
        }
        public void ClickSettingOn()
        {
            SettingOn.Click(); //нажимаем на "Включен", когда опция включена, чтобы открыть выпадающий список
        }
        public void ClickModeSettingIsOff()
        {
            ModeSettingIsOff.Click();// нажимаем на "Выключен" в выпадающем списке (когда опция включена, чтобы отключить ее)
        }
        public void ClickSettingOff()
        {
            SettingOff.Click();//нажимаем на "Выключен", когда опция выключена, чтобы открыть выпадающий список
        }
        public void ClickModeSettingIsOn()
        {
            ModeSettingIsOn.Click();// нажимаем на "Включен" в выпадающем списке (когда опция выключена, чтобы включить ее)
        }
       
    }
}
