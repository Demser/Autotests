using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Coypu;
using System.Diagnostics;

namespace _365AutomatedTests.Steps
{
    using OpenQA.Selenium;
    using _365AutomatedTests.Steps;
    using static NUnit.Framework.Assert;
    [Binding]
    public class BD301DictionariesTerminalSteps
    {
        [Given(@"I have opened Settings Terminals")]
        public void GivenIHaveOpenedSettingsTerminals()
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenTerminals();
            IsTrue(BDMain.AssertOpenTerminals(), "Не найден заголовок на странице Терминалы");
         }

        [Then(@"I check input fields on the terminal page")]
        public void ThenICheckInputFieldsOnTheTerminalPage()
        {
            BDTerminalsPage BDTerminals = new BDTerminalsPage();
            // добавить терминал с незаполненными полями
            BDTerminals.ClikToAddTerminal();
            BDTerminals.ClickSaveNewTerminalButton();
            IsTrue(BDTerminals.AssertNeedsTitleWarningMessage(), "Не пришло предупреждение о незаполненном обязательном поле Название");
            IsTrue(BDTerminals.AssertNeedsSystemNameWarningMessage(), "Не пришло предупреждение о незаполненном обязательном поле Системное имя терминала");
            BDTerminals.ClikCancelAddNewTerminalButton();
            // добавить невалидное значение в поле Название и нажать сохранить
            BDTerminals.ClikToAddTerminal();
            IsTrue(BDTerminals.AssertOpenAddingNewTerminal(), "Не появилось пустое поле для добавления терминала");
            BDTerminals.AddInvalidTitle();
            IsTrue(BDTerminals.AssertToLongNameWarningMessage(), "Не пришло предупреждение об ограничении в 255 символов");
            IsTrue(BDTerminals.AssertNeedsSystemNameWarningMessage(), "Не пришло предупреждение о незаполненном обязательном поле Системное имя терминала");
            BDTerminals.ClikCancelAddNewTerminalButton();
            // добавить невалидное значение в поле Системное имя терминала и нажать сохранить
            BDTerminals.ClikToAddTerminal();
            BDTerminals.AddInvalidTerminalName();
            IsTrue(BDTerminals.AssertToLongNameWarningMessage(), "Не пришло предупреждение об ограничении в 255 символов");
            IsTrue(BDTerminals.AssertNeedsTitleWarningMessage(), "Не пришло предупреждение о незаполненном обязательном поле Название");
            BDTerminals.ClikCancelAddNewTerminalButton();
        }

        [Then(@"create new terminal, refresh page and check that it exist")]
        public void ThenCreateNewTerminalRefreshPageAndCheckThatItExist()
        {
            BDTerminalsPage BDTerminals = new BDTerminalsPage();
            // добавить новый тестовый терминал
            BDTerminals.ClikToAddTerminal();
            BDTerminals.AddNewTerminalTitle("Title of Autotest Terminal");
            BDTerminals.AddNewTerminalName("System name of Autotest Terminal");
            BDTerminals.AddLaboratory();
            BDTerminals.ClickSaveNewTerminalButton();
            CommonSteps commons = new CommonSteps();
            commons.RefreshPage(); // обновили страницу
            BDTerminals.WaitAfterRefresh();
            if (BDTerminals.CheckNewTerminal("Title of Autotest Terminal").Exists())
            {
                Debug.WriteLine("ОК. Терминал добавлен");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, новый терминал не сохранился");
            }
        }

        [Then(@"I try to create terminal with not unique filds")]
        public void ThenIRefreshPageAndTryToCreateTerminalWithNotUniqueFilds()
        {    
            BDTerminalsPage BDTerminals = new BDTerminalsPage();
            // создать полностью такой же терминал как в предыдущем шаге
            BDTerminals.ClikToAddTerminal();
            BDTerminals.AddNewTerminalTitle("Title of Autotest Terminal");
            BDTerminals.AddNewTerminalName("System name of Autotest Terminal");
            BDTerminals.AddLaboratory();
            BDTerminals.ClickSaveNewTerminalButton();
            IsTrue(BDTerminals.AssertNameNotUniqueMessage(), "Нет сообщения о неуникальном имени"); // проверили, что ругнулся
            BDTerminals.AddAnotherLaboratory(); // сменили на вторую в списке лабораторию
            BDTerminals.ClickSaveNewTerminalButton(); // попробовали снова сохранить 
            IsTrue(BDTerminals.AssertNameNotUniqueMessage(), "Нет сообщения о неуникальном имени"); // проверили, что ругнулся
            BDTerminals.ClikCancelAddNewTerminalButton(); // отменили добавледение терминала
            // неуникальное название терминала
            BDTerminals.ClikToAddTerminal();
            BDTerminals.AddNewTerminalTitle("Title of Autotest Terminal"); //добавили уже существующее название терминала
            BDTerminals.AddNewTerminalName("unique"); //добавили уникальное имя
            BDTerminals.AddLaboratory(); //выбрали первую в списке лабораторию
            BDTerminals.ClickSaveNewTerminalButton();
            IsTrue(BDTerminals.AssertTitleNotUniqueMessage(), "Нет сообщения о неуникальном имени"); // проверили, что ругнулся
            BDTerminals.AddAnotherLaboratory(); // сменили на вторую в списке лабораторию
            BDTerminals.ClickSaveNewTerminalButton(); // попробовали снова сохранить 
            IsTrue(BDTerminals.AssertTitleNotUniqueMessage(), "Нет сообщения о неуникальном имени"); // проверили, что ругнулся
            BDTerminals.ClikCancelAddNewTerminalButton(); // отменили добавледение терминала
            // проверяем аналогично неуникальное Системное имя терминала
            BDTerminals.ClikToAddTerminal();
            BDTerminals.AddNewTerminalTitle("unique"); 
            BDTerminals.AddNewTerminalName("System name of Autotest Terminal"); 
            BDTerminals.AddLaboratory(); 
            BDTerminals.ClickSaveNewTerminalButton();
            IsTrue(BDTerminals.AssertNameNotUniqueMessage(), "Нет сообщения о неуникальном имени"); 
            BDTerminals.AddAnotherLaboratory(); 
            BDTerminals.ClickSaveNewTerminalButton(); 
            IsTrue(BDTerminals.AssertNameNotUniqueMessage(), "Нет сообщения о неуникальном имени"); 
            BDTerminals.ClikCancelAddNewTerminalButton(); 
        }

        [Then(@"I try to change input fields in new terminal")]
        public void ThenITryToChangeInputFieldsInNewTerminal()
        {
            BDTerminalsPage BDTerminals = new BDTerminalsPage();
            // нажать добавить и отменить 
            BDTerminals.ClikToAddTerminal();
            BDTerminals.AddNewTerminalTitle("New Title of Autotest Terminal");
            BDTerminals.AddNewTerminalName("New System name of Autotest Terminal");
            BDTerminals.AddAnotherLaboratory();
            BDTerminals.ClikCancelAddNewTerminalButton();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("Что-то пошло не так, сохранен новый терминал при нажатии на Отмену");
            }
            else
            {
                Debug.WriteLine("ОК. При отмене новый терминал не добавляется");
            }
            // изменить поля и нажать отменить
            BDTerminals.ChangeTerminalWithThisTitleButton("Title of Autotest Terminal").Click();
            BDTerminals.CleanEmrtyTerminalTitle();
            BDTerminals.CleanEmrtyTerminalName();
            BDTerminals.AddNewTerminalTitle("New Title of Autotest Terminal");
            BDTerminals.AddNewTerminalName("New System name of Autotest Terminal");
            BDTerminals.AddAnotherLaboratory();
            BDTerminals.ClikCancelAddNewTerminalButton();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("Что-то пошло не так, сохренены изменения при отмене");
            }
            else
            {
                Debug.WriteLine("ОК. Изменения при отмене не применились");
            }
            // изменить поля и нажать сохранить
            BDTerminals.ChangeTerminalWithThisTitleButton("Title of Autotest Terminal").Click();
            BDTerminals.CleanEmrtyTerminalTitle();
            BDTerminals.CleanEmrtyTerminalName();
            BDTerminals.AddNewTerminalTitle("New Title of Autotest Terminal");
            BDTerminals.AddNewTerminalName("New System name of Autotest Terminal");
            BDTerminals.AddAnotherLaboratory();
            BDTerminals.ClickSaveNewTerminalButton();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("ОК. Изменения в полях сохранены");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, изменения не сохранены");
            }
            CommonSteps commons = new CommonSteps();
            commons.RefreshPage(); // обновили страницу
            BDTerminals.WaitAfterRefresh();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("ОК. Изменения в полях отображаются после обновления страницы");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, изменения не сохранены, не отображаются после обноления страницы");
            }
        }
        
        [Then(@"I try to delete new terminal")]
        public void ThenITryToDeleteNewTerminal()
        {
            BDTerminalsPage BDTerminals = new BDTerminalsPage();
            // нажать удалить и закрыть окно подтверждения удаления
            BDTerminals.DeleteTerminalWithThisTitleButton("New Title of Autotest Terminal").Click();
            BDTerminals.ClickCloseWindowOfConfirmDeleteTerminalButton();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("ОК. Терминал не удалился");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, терминал удалился после закрытия окна подтверждения удаления");
            }
            //нажать удалить и Отмена в окне подтверждения
            BDTerminals.DeleteTerminalWithThisTitleButton("New Title of Autotest Terminal").Click();
            BDTerminals.ClickCancelDeleteTerminalButton();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("ОК. Терминал не удалился");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, терминал удалился после отмены подтверждения удаления");
            }
            //нажать удалить и подтвердить удаление
            BDTerminals.DeleteTerminalWithThisTitleButton("New Title of Autotest Terminal").Click();
            BDTerminals.ClickConfirmDeleteTerminalButton();
            BDTerminals.WaitAfterRefresh();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("Что-то пошло не так, терминал не удалился после подтверждения удаления");
            }
            else
            {
                Debug.WriteLine("ОК. Терминал удален");
            }
            CommonSteps commons = new CommonSteps();
            commons.RefreshPage(); // обновили страницу
            BDTerminals.WaitAfterRefresh();
            if (BDTerminals.CheckNewTerminal("New Title of Autotest Terminal").Exists()
                & BDTerminals.CheckNewTerminal("New System name of Autotest Terminal").Exists())
            {
                Debug.WriteLine("Что-то пошло не так, терминал не удалился после подтверждения удаления и обновления страницы");
            }
            else
            {
                Debug.WriteLine("ОК. Терминал удален, не отображается после обновления страницы");
            }
        }
    }
}

