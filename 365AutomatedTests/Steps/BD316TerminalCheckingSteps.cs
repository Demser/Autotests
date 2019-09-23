using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using Coypu;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using static NUnit.Framework.Assert;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class BD316TerminalCheckingSteps

    {
        [Given(@"I login as admin ""(.*)"" and don't select the terminal")]
        public void GivenILoginAsAdminAndDonTSelectTheTerminal(string login)
        {
            NewLogin365Page testLogin365Page = new NewLogin365Page(); // стандартная форма авторизации
            testLogin365Page.LoginAsUser(login);
            TestLoginChangePasswordPage testLoginChangePasswordPage = new TestLoginChangePasswordPage(); // Форма смены пароля 
            IsFalse(testLoginChangePasswordPage.AssertNewPasswordField(), "Пароль устарел! Смените пароль для этого пользователя в администрировании 365, а затем пропишите его в файле App.config в разделе UserSettings. Рекомендуемый пароль: Autotests+инкремент, например Autotests5");
            TestLoginWrongPasswordPage testWrongPasswordPage = new TestLoginWrongPasswordPage(); // Форма неправильного пароля
            IsFalse(testWrongPasswordPage.AssertWrongPassword(), "Вы ввели неправильный пароль. Проверьте правильность пароля");

            MainPage MainP = new MainPage();
            IsTrue(MainP.AssertLeftMenu(), "Левое меню не подгрузилось в течение таймаута. Лагает главный сайт!");
            Console.WriteLine("Вход на сайт осуществлен");
            MainP.GoToBatchDropper();
            BDMainPage BDMain = new BDMainPage();
            IsTrue(BDMain.AssertMainPageIsUnlocked(), "Таймаут. Модуль BatchDropper не успел подгрузиться.");
            TerminalSettingPage TSettings = new TerminalSettingPage();
            IsTrue(TSettings.AssertLinkDefaultTerminalName(), "Что-то пошло не так, терминал должен быть не выбран");
        }

        [When(@"I introduce the terminal, I check terminal name field")]
        public void WhenIIntroduceTheTerminalICheckTerminalNameField()
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenSorting();
            TerminalSettingPage TSettings = new TerminalSettingPage();
            IsTrue(TSettings.AssertForChooseTerminalName(), "Что-то пошло не так, нет поля или заголовка выбора терминала");
        }
        
        [Then(@"I go to workplace ""(.*)"" and check that field for choosing terminal is exists")]
        public void ThenIGoToWorkplaceAndCheckThatFieldForChoosingTerminalIsExists(string workplace)
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenWorkplaceWithName(workplace);
            //ввести терминал и обновить страницу 
            TerminalSettingPage TSettings = new TerminalSettingPage();
            TSettings.SetTerminalWithoutSave("1");
            CommonSteps commons = new CommonSteps();
            commons.RefreshPage(); // обновили страницу
            TSettings.WaitAfterRefresh(); // костыль, нужно заменить неявным ожиданием
            IsTrue(TSettings.AssertForChooseTerminalName(), "Что-то пошло не так, нет поля или заголовка выбора терминала");
            var teminalID = "abc";
            BDMain.SetTerminal(teminalID);
            IsTrue(TSettings.AssertWrongNameOfTerminal(teminalID), $"Что-то пошло не так, нет сообщения о том, что {teminalID} не существует");
            teminalID = "999";
            BDMain.SetTerminal(teminalID);
            IsTrue(TSettings.AssertWrongNameOfTerminal(teminalID), $"Что-то пошло не так, нет сообщения о том, что {teminalID} не существует");
        }

        [Then(@"I set Terminal number ""(.*)""")]
        public void ThenISetTerminalNumber(string terminal)
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.SetTerminal(terminal);
            IsTrue(BDMain.AssertTermName(), "Терминал не применился. Проверьте наличие терминала в справочниках.");
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"select [Name] FROM [BatchDropperStab].[dbo].[Terminals] where id={terminal}";
            var name = _msBDConnector.QueryExecutorScalar(command);
            TerminalSettingPage TSettings = new TerminalSettingPage();
            IsTrue(TSettings.AssertLinkWitnThisTerminalName(name), $"Что-то пошло не так, терминал {terminal} c именем {name} не выбран");
            CommonSteps commons = new CommonSteps();
            commons.RefreshPage(); // обновили страницу
            TSettings.WaitAfterRefresh(); // костыль, нужно заменить неявным ожиданием
            IsTrue(TSettings.AssertLinkWitnThisTerminalName(name), $"Что-то пошло не так, терминал {terminal} c именем {name} не выбран");
        }

        [When(@"I go to workplace ""(.*)"", I check that Terminal number ""(.*)"" exists")]
        public void WhenIGoToWorkplaceICheckThatTerminalNumberExists(string workplace, int terminal)
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenWorkplaceWithName(workplace);
            TerminalSettingPage TSettings = new TerminalSettingPage();
            IsTrue(BDMain.AssertWorkplaceWithName(workplace), $"Что-то пошло не так, на странице не отображается заголовок {workplace}");
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"select [Name] FROM [BatchDropperStab].[dbo].[Terminals] where id={terminal}";
            var name = _msBDConnector.QueryExecutorScalar(command);
            IsTrue(TSettings.AssertLinkWitnThisTerminalName(name), $"Что-то пошло не так, терминал {terminal} c именем {name} не выбран");
        }

        [Then(@"I delete all cookies and refresh page")]
        public void ThenIDeleteAllCookiesAndRefreshPage()
        {
            CommonSteps commons = new CommonSteps();
            commons.DeleteAllCookies();
            commons.RefreshPage();
        }
    }
}
