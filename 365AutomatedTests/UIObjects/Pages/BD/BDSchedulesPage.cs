using System;
using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using TechTalk.SpecFlow;
using Coypu;

namespace _365AutomatedTests.UIObjects.Pages
{
    using OpenQA.Selenium;
    using System.Collections.Generic;

    [Binding]
    class BDSchedulesPage : Page
    {
        public BDSchedulesPage() : base()
        {
        }
        private ElementScope AddScheduleButton => Scope.FindXPath("//a[.='Добавить']");
        private ElementScope SaveScheduleButton => Scope.FindXPath("//span[@class='k-icon k-update']");//FindLink("Cохранить");//FindButton("Cохранить");//FindXPath("//a[contains(text(),'Cохранить']");
        private ElementScope DeleteScheduleButton => Scope.FindXPath("//a[@class='k-button k-button-icontext k-grid-Удалить')]");
        private ElementScope ApplyDeleteScheduleButton => Scope.FindXPath("//button[@ng-click='vm.deleteRow()']");
        private ElementScope NameSheduleInput => Scope.FindXPath("//input[@name='name']");
        private ElementScope StartDateScheduleInput => Scope.FindXPath("//input[@data-text-field='startTime']");
        private ElementScope StartDateSortingScheduleInput => Scope.FindXPath("//input[@data-text-field='sortingTime']");
        private ElementScope CheckSchedNameSpan => Scope.FindXPath("//span[.='autoshed']");
    


        //Добавить расписание

        public void AddSchedule()
        {
            AddScheduleButton.Click();
            NameSheduleInput.SendKeys("autoshed");
            StartDateScheduleInput.Click();
            StartDateScheduleInput.SendKeys(Keys.Backspace).SendKeys("1");
            StartDateSortingScheduleInput.SendKeys(Keys.Backspace).SendKeys("0");
            SaveScheduleButton.Click();
        }

        // Добавить расписание для определенного хаба через базу данных
        public void AddScheduleForMSK()
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command = $@"INSERT INTO [BatchDropperStab].[dbo].[Schedules] Values ('autotest','11:57:00.000','11:41:00.000',2)";
            var result = _msBDConnectorLW.NonQueryExecutor(command);
        }

        public void AddScheduleForSPB()
        {
            MSDatabaseConnector _msBDConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            string command = $@"INSERT INTO [BatchDropperStab].[dbo].[Schedules] Values ('autotest','11:50:00.000','11:40:00.000',1)";
            var result = _msBDConnectorLW.NonQueryExecutor(command);
        }


        // Проверка, что расписание добавилось
        public bool CheckSchedule()
        {
            if (CheckSchedNameSpan.Exists((new Options { Timeout = System.TimeSpan.FromSeconds(2) }))) return true;
            else return false;
        }
        //Удаление расписания
        public void DeleteSchedule()
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            List<string> tables = new List<string>() { "[Schedules]" };

            foreach (string table in tables)
            {
                string command = $@"DELETE FROM {table} Where Name = 'autoshed'";
                _msBDConnector.NonQueryExecutor(command);
            }

        }
    }
}


