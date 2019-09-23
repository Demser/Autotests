using _365AutomatedTests.Framework.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coypu;
using OpenQA.Selenium;
using _365AutomatedTests.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace _365AutomatedTests.UIObjects.Pages
{
    class BDTerminalsPage : Page
    {
        public BDTerminalsPage() : base()
        {
        }
        //объекты на странице Терминалы
        private ElementScope TerminalsPageAddButton => Scope.FindXPath("//a[@class='k-button k-button-icontext k-grid-add']"); //кнопка добавить
        private ElementScope EmrtyTerminalTitle => Scope.FindXPath("//input[@name='title']"); // пустое поле Название
        private ElementScope EmrtyTerminalName => Scope.FindXPath("//input[@name='name']"); // пустое поле Системное имя терминала
        private ElementScope ChoseLaboratory => Scope.FindXPath("//span[@class='k-input']"); // выбор Лаборатории
        private ElementScope ChoseFirstLaboratoryInList => Scope.FindXPath("//li[@tabindex][1]"); // выбрать в выпадающем списке первую лабораторию
        private ElementScope ChoseSecondLaboratoryInList => Scope.FindXPath("//li[@tabindex][2]"); // выбрать в выпадающем списке вторую лабораторию
        private ElementScope SaveNewTerminalButton => Scope.FindXPath("//a[@class='k-button k-button-icontext k-primary k-grid-update']"); // кнопка сохранить
        private ElementScope CancelAddNewTerminalButton => Scope.FindXPath("//a[@class='k-button k-button-icontext k-grid-cancel']"); // кнопка Отменить
        private ElementScope AddNewTerminalBtn => Scope.FindXPath("//a[.='Добавить']"); //кнопка Добавить
        private ElementScope ChangeButton => Scope.FindXPath("//a[contains(text(),'Изменить')] [1]"); // кнопка изменить
        private ElementScope CloseWindowOfConfirmDeleteTerminalButton => Scope.FindXPath("//a[@class='k-window-action k-link']"); // кнопка закрытия окна подтверждения удаления терминала
        private ElementScope ConfirmDeleteTerminalButton => Scope.FindXPath("//button[@ng-click='vm.deleteRow()']"); //подтвердить удаление терминала
        private ElementScope CancelDeleteTerminalButton => Scope.FindXPath("//button[@ng-click='vm.closeEditWindow()']"); //отменить удаление терминала
        private ElementScope ToLongNameWarningMessage => Scope.FindXPath("//div[contains(text(),'Длина поля должна быть меньше 255 символов')]"); // всплывашка предупреждение больше 255
        private ElementScope NeedsSystemNameWarningMessage => Scope.FindXPath("//div[contains(text(),'Системное имя обязательно')]"); // всплывашка, нужно добавить системное имя терминала
        private ElementScope NeedsTitleWarningMessage => Scope.FindXPath("//div[contains(text(),'Название обязательно')]"); // всплывашка название терминала обязательно
        private ElementScope NameNotUniqueMessage => Scope.FindXPath("//div[@class = 'toast-message' and contains(text(),'Имя терминала неуникально')]"); // всплывашка ошибка
        private ElementScope TitleNotUniqueMessage => Scope.FindXPath("//div[@class = 'toast-message' and contains(text(),'Название терминала неуникально')]"); //всплывашка ошибка


        //для проверки на появление нового терминала
        public ElementScope CheckNewTerminal(string _) => Scope.FindXPath($"//span[.='{_}']");
        // найти кнопку изменить для терминала с названием
        public ElementScope ChangeTerminalWithThisTitleButton(string _) => Scope.FindXPath($"//td[.='{_}']//.. //a[.='Изменить']");
        // найти кнопку удалить для терминала с названием
        public ElementScope DeleteTerminalWithThisTitleButton(string _) => Scope.FindXPath($"//td[.='{_}']//.. //a[.='Удалить']");

        public void ClikToAddTerminal() // нажать на добавить
        {
            TerminalsPageAddButton.Click();
        }

        public void ClikCancelAddNewTerminalButton() // нажать на отменить
        {
            CancelAddNewTerminalButton.Click();
        }

        public void ClickSaveNewTerminalButton() // нажать сохранить
        {
            SaveNewTerminalButton.Click();
        }

        public void ClickChangeButton() // нажать изменить
        {
            ChangeButton.Click();
        }

        public void ClickCloseWindowOfConfirmDeleteTerminalButton() // нажать закрыть окно подтверждения удаления терминала
        {
            CloseWindowOfConfirmDeleteTerminalButton.Click();
        }

        public void ClickConfirmDeleteTerminalButton() // нажать удалить в окне подтверждения удаления терминала
        {
            ConfirmDeleteTerminalButton.WaitForClickability();
            ConfirmDeleteTerminalButton.Click();
        }

        public void ClickCancelDeleteTerminalButton() // нажать отменить в окне подтверждения удаления терминала
        {
            CancelDeleteTerminalButton.Click();
        }

        //появилсь пустая строка для добавления терминала
        public bool AssertOpenAddingNewTerminal()
        {
            if (EmrtyTerminalTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }) &
                EmrtyTerminalName.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }) &
                ChoseLaboratory.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // ввести в поле Название больше 255 символов и нажать сохранить
        public void AddInvalidTitle()
        {
            EmrtyTerminalTitle.Click();
            var words = string.Concat(Enumerable.Repeat("На все воля...", 200));
            EmrtyTerminalTitle.SendKeys(words);
            SaveNewTerminalButton.Click();
        }

        // ввести в поле Системное имя терминала больше 255 символов и нажать сохранить
        public void AddInvalidTerminalName()
        {
            EmrtyTerminalName.Click();
            var words = string.Concat(Enumerable.Repeat("На все воля...", 200));
            EmrtyTerminalName.SendKeys(words);
            SaveNewTerminalButton.Click();
        }

        //проверка на всплывашку в ограничение в 255 символов
        public bool AssertToLongNameWarningMessage()
        {
            if (ToLongNameWarningMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //проверка на всплывашку при пустом обязателньом поле Системное имя
        public bool AssertNeedsSystemNameWarningMessage()
        {
            if (NeedsSystemNameWarningMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //проверка на всплывашку при пустом обязателньом поле Название
        public bool AssertNeedsTitleWarningMessage()
        {
            if (NeedsTitleWarningMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //проверка на всплывашку об неуникальном имени
        public bool AssertNameNotUniqueMessage()
        {
            if (NameNotUniqueMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //проверка на всплывашку об неуникальном названии
        public bool AssertTitleNotUniqueMessage()
        {
            if (TitleNotUniqueMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Добавить новый Терминал
        public void AddNewTerminalTitle(string _)
        {
            EmrtyTerminalTitle.Click();
            EmrtyTerminalTitle.SendKeys(_);
        }
        public void AddNewTerminalName(string _)
        {
            EmrtyTerminalName.Click();
            EmrtyTerminalName.SendKeys(_);
        }
        public void AddLaboratory() // выбрать первую в списке лабораторию
        {
            ChoseLaboratory.Click();
            ChoseFirstLaboratoryInList.Click();
        }

        public void AddAnotherLaboratory() // выбрать вторую в списке лабораторию
        {
            ChoseLaboratory.Click();
            ChoseSecondLaboratoryInList.Click();
        }

        //очистить поле название
        public void CleanEmrtyTerminalTitle()
        {
           EmrtyTerminalTitle.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
        }

        //очистить поле системное  имя терминала
        public void CleanEmrtyTerminalName()
        {
            EmrtyTerminalName.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
        }

        public void WaitAfterRefresh() // подождать пока кнопка Добавить не станет кликабельной
        {
            AddNewTerminalBtn.WaitForClickability();
        }
    }
}
