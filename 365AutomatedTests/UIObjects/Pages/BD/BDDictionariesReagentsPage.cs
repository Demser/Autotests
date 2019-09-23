using _365AutomatedTests.Framework.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coypu;
using OpenQA.Selenium;
using _365AutomatedTests.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace _365AutomatedTests.UIObjects.Pages
{
    class BDDictionariesReagentsPage : Page
    {
        public BDDictionariesReagentsPage() : base()
        {
        }
        // базовые элементы на странице
        private ElementScope AddNewReagentButton => Scope.FindXPath("//a[@class ='k-button k-button-icontext k-grid-add']"); //кнопка добавить
        private ElementScope NewReagentsNameField => Scope.FindXPath("//input[@name ='name']"); // пустое поле Название реагента
        private ElementScope NewReagentsTubeVolume => Scope.FindXPath("(//input[@class ='k-formatted-value k-input'])[1]"); // пустое поле объем пробирки
        private ElementScope NewReagentsSampleVolume => Scope.FindXPath("(//input[@class ='k-formatted-value k-input'])[2]"); // пустое поле объем пробы
        private ElementScope SaveReagentButton => Scope.FindXPath("//a[@class ='k-button k-button-icontext k-primary k-grid-update']"); // кнопка Сохранить
        private ElementScope CancelSaveReagentButton => Scope.FindXPath("//a[@class ='k-button k-button-icontext k-grid-cancel']"); // кнопка Отменить
        private ElementScope ConfurmDeleteReagentButton => Scope.FindXPath("//button[.='Удалить']"); // кнопка удалит в окне подтверждения
        private ElementScope CancelDeleteReagentButton => Scope.FindXPath("//button[.='Отменить']"); // кнопка отменить в окне подтверждения
        private ElementScope CloseComfurmDeleteWindowButton => Scope.FindXPath("//span[@class='k-icon k-i-close']"); // кнопка закрыть окно подтверждения

        //сообщения об ошибках
        private ElementScope ErrorNotCorrectValueMessage => Scope.FindXPath("//div[contains(text(),'Значение некорректно! " +
            "Ошибки: Не заполнено Название,Объем пробы должен быть в диапазоне (0;100),Объем пробирки " +
            "должен быть в диапазоне (60;10000),Объем пробирки с учетом зарезервированного объема (60) должен быть больше объема образца')]");
        private ElementScope ErrorNotCorrectNameMessage => Scope.FindXPath("//div[contains(text(),'Значение некорректно! " +
            "Ошибки: Не заполнено Название')]");
        private ElementScope ErrorNotCorrectTubeVolumeMessage => Scope.FindXPath("//div[contains(text(),'Значение некорректно! " +
            "Ошибки: Объем пробирки должен быть в диапазоне (60;10000)')]");
        private ElementScope ErrorNotCorrectSampleVolumeMessage => Scope.FindXPath("//div[contains(text(),'Не удалось создать! Значение" +
            " некорректно! Ошибки: Объем пробы должен быть в диапазоне (0;100)')]");
        private ElementScope ErrorMessage => Scope.FindXPath("//div[@class='toast toast-error']");

        //проверить наличие Реагента с заданным названием
        public ElementScope CheckNewCreatedReagent(string _) => Scope.FindXPath($"//span[.='{_}'and @ng-bind='dataItem.name']");
        //наличие реагента с заданным названием и объемом
        public ElementScope CheckNewCreatedReagentWithVolume(string a, string n) => Scope.FindXPath($"//td[.='{a}']//..//span[.='{n}']");
        // найти кнопку изменить для Реагента с заданным названием
        public ElementScope ChangeReagentWithThisNameButton(string _) => Scope.FindXPath($"//td[.='{_}']//.. //a[.='Изменить']");
        public ElementScope DeleteReagentWithThisNameButton(string _) => Scope.FindXPath($"//td[.='{_}']//.. //a[.='Удалить']");



        public void CloseErrorMessage() // убрать окошко с ошибкой
        {
            ErrorMessage.Click();
        }
      
        public void ClickToAddReagent() // нажать на добавить
        {
            AddNewReagentButton.Click();
        }

        public void ClickToSaveReagentButton() // нажать Сохранить
        {
            SaveReagentButton.Click();
        }

        public void ClickToCancelSaveReagentButton() // нажать Отменить
        {
             CancelSaveReagentButton.Click();
        }

        public void ClickToConfurmDeleteReagentButton() // подтвердить удаление
        {
            ConfurmDeleteReagentButton.Click();
        }

        public void ClickToCancelDeleteReagentButton() // отменить удаление
        {
            CancelDeleteReagentButton.Click();
        }

        public void ClickToCloseComfurmDeleteWindowButton() // закрыть окно подтверждения удаления
        {
            CloseComfurmDeleteWindowButton.Click();
        }

        //появилсь пустая строка для добавления реагента
        public bool AssertOpenAddingNewReagent()
        {
            if (NewReagentsNameField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }) &
                NewReagentsTubeVolume.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }) &
                NewReagentsSampleVolume.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //добавить новый реагент
        public void AddNewReagentName(string _)  //название
        {
            NewReagentsNameField.SendKeys(_);
        }
        public void AddNewReagentTubeVolume(string _) //объем пробирки
        {
            NewReagentsTubeVolume.SendKeys(_);
        }
        public void AddNewReagentsSampleVolume(string _) //объем пробы
        {
            NewReagentsSampleVolume.SendKeys(_);
        }

        //подождать пока не разблокирыется кнопка добавить после обновления
        public void WaitAfterRefresh()
        {
            AddNewReagentButton.WaitForClickability();
        }

        //отлавливаем сообщение об ошибке (не заполнены все поля)
        public bool AccertErrorNotCorrectValueMessage()
        {
            if (ErrorNotCorrectValueMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //отлавливаем сообщение об ошибке (не заполнено корректно поле Название)
        public bool AccertErrorNotCorrectNameMessage()
        {
            if (ErrorNotCorrectNameMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //отлавдиваем сообщение об ошибке (не заполнено корректно поле Объем пробирки)
        public bool AccertErrorNotCorrectTubeVolumeMessage()
        {
            if (ErrorNotCorrectTubeVolumeMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //отлавдиваем сообщение об ошибке (не заполнено корректно поле Объем пробы)
        public bool AccertErrorNotCorrectSampleVolumeMessage()
        {
            if (ErrorNotCorrectSampleVolumeMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public void CleanReagentsNameField()  // очистить поле Название
        {
            NewReagentsNameField.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
        }

 
        // добавить больше 255 символов в название
        public void AddInvalidName()
        {
            NewReagentsNameField.SendKeys("Цель феноменологической редукции — освободить сознание" +
                " от «естественной установки», цель экзистенциальной диалектики — преодолеть " +
                "объективацию свободы (или, если угодно, трансценденции). Сходство целей сомнений " +
                "не вызывает — и в том и в другом случае это попытка снять субъект-объектное разделение.");
            ClickToSaveReagentButton();
        }

        public bool ValidationNameFieldVolume()
        {
            if(CheckNewCreatedReagent("Цель феноменологической редукции — освободить сознание от «естественной " +
                "установки», цель экзистенциальной диалектики — преодолеть объективацию свободы (или, если угодно," +
                " трансценденции). Сходство целей сомнений не вызывает — и в том и в другом случае " +
                "это ").Exists((new Options { Timeout = System.TimeSpan.FromSeconds(2) }))) return true;
            else return false;

        }

    }

}

