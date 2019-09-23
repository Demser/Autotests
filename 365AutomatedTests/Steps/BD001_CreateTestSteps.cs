using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Coypu;

namespace _365AutomatedTests.Steps
{   
    using static NUnit.Framework.Assert;
    [Binding]
    public class BD001_CreateTestSteps
    {
        ElementScope FirstTab;
        ElementScope SecondTab;

        [Given(@"I clear the data from BatchDropper database")]
        public void GivenIClearTheDataFromBatchDropperDatabase()
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            List<string> tables = new List<string>() { "[ReasonCancellationSampleCancellations]", "[SampleCancellationSampleTests]", "[SampleCancellations]", "[Batches]", "[Samples]", "[Multiplexes]", "[MultiplexesOnSamples]" };

            foreach (string table in tables)
            {
                string command = $@"DELETE FROM {table}";
                _msBDConnector.NonQueryExecutor(command);
            }
        }

        [Given(@"I clear all data except multiplexes from BatchDropper database")]
        public void GivenIClearAllDataExceptMultiplexesFromBatchDropperDatabase()
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            List<string> tables = new List<string>() { "[ReasonCancellationSampleCancellations]", "[SampleCancellationSampleTests]", "[SampleCancellations]", "[Batches]", "[Samples]", "[MultiplexesOnSamples]" };

            foreach (string table in tables)
            {
                string command = $@"DELETE FROM {table}";
                _msBDConnector.NonQueryExecutor(command);
            }
        }


        [Given(@"I login as admin ""(.*)"", ""(.*)""")]
        public void GivenILoginAsAdmin(string login, string termID)

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
            BDMain.SetTerminal(termID);
           // IsTrue(BDMain.AssertTermName(), "Терминал не применился. Проверьте наличие терминала в справочниках.");
            IsTrue(BDMain.AssertMainTitle(), "Не найден заголовок на главной странице.");
        }
        
        private List<KeyValuePair<string,string>> InitComponents (Table table, int set)
        {
            table.Rows[set - 1].TryGetValue("Fam", out string Fam);
            table.Rows[set - 1].TryGetValue("Hex", out string Hex);
            table.Rows[set - 1].TryGetValue("Rox", out string Rox);
            table.Rows[set - 1].TryGetValue("Cy5", out string Cy5);
            table.Rows[set - 1].TryGetValue("Cy5.5", out string Cy55);

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Fam", Fam),
                new KeyValuePair<string, string>("Hex", Hex),
                new KeyValuePair<string, string>("Rox", Rox),
                new KeyValuePair<string, string>("Cy5", Cy5),
                new KeyValuePair<string, string>("Cy5.5", Cy55)
            };
            return list;
        }

        
    }
}
