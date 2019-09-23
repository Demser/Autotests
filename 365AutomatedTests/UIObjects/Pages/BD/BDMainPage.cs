using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;
using System.Diagnostics;

namespace _365AutomatedTests.UIObjects.Pages
{
    class BDMainPage : Page
    {
        public BDMainPage() : base()
        {
        }
        public string TerminalName = "";
        //верхняя менюха:
        private ElementScope Batches => Scope.FindXPath("//a[contains(text(),'Бэтчи')]");
        private ElementScope Settings => Scope.FindXPath("//a[contains(text(),'Настройки')]");
        private ElementScope Workflows => Scope.FindXPath("//a[contains(text(),'Рабочее место')]");
        private ElementScope Dictionaries => Scope.FindXPath("//a[contains(text(),'Справочники')]");
        private ElementScope ChangeTerminalButton => Scope.FindXPath("//span[@ng-click='vm.needSetTerminal()']"); // кнопка смены терминала
        //под-пункты Настройки:
        private ElementScope Settings_Laboratories => Scope.FindXPath("//a[@ui-sref ='locations']"); // лаборатории
        private ElementScope Settings_Terminals => Scope.FindXPath("//a[@ui-sref ='terminals']"); // терминалы
        private ElementScope DeviceSettings => Scope.FindXPath("//a[contains(text(),'Оборудование')]"); // оборудование
        private ElementScope SettingsItem => Scope.FindXPath("//a[@ui-sref ='applicationSettings']"); // другие настройки
        // под-пункты Рабочие места:
        private ElementScope Sorting => Scope.FindXPath("//a[contains(text(),'Сортировка')]");
        private ElementScope DNK => Scope.FindXPath("//a[contains(text(),'Выделение ДНК в пробирке')]");
        private ElementScope ProductionAcceptance => Scope.FindXPath("//a[contains(text(),'Прием в постановке')]");
        private ElementScope BatchFormingWorkplace => Scope.FindXPath("//a[@ui-sref='testBatchFactory']"); // РМ формирование бэтчей
        private ElementScope ReagentsWorkplace => Scope.FindXPath("//a[contains(text(),'Реагенты')]");
        private ElementScope ManualDripping => Scope.FindXPath("//a[contains(text(),'Ручное раскапывание')]");
        private ElementScope TubesManualDripping => Scope.FindXPath("//a[contains(text(),'Постановка реакций в пробирках')]");
        private ElementScope PositiveControls => Scope.FindXPath("//a[contains(text(),'Положительные контроли')]");
        private ElementScope AutoDripping => Scope.FindXPath("//a[@ui-sref ='autoDripping']");// раскапыватель
        private ElementScope BatchReverseTranscriptionFormingWorkplace => Scope.FindXPath("//a[@ui-sref='transcriptionBatchFactory']"); //РМ Формирование бэтчей с ОТ
        private ElementScope ReverseTranscriptionWorkplace => Scope.FindXPath("//a[@ui-sref='reverseTranscription']"); // РМ обратная транскрипция
        private ElementScope WorkplaceWithName(string _) => Scope.FindXPath($"//a[.='{_}']"); // РМ по названию
        //под-пункты Справочники:
        private ElementScope Dictionaries_СontrolMaterials => Scope.FindXPath("//a[@ui-sref ='controlMaterials']"); // контрольные материалы
        private ElementScope Dictionaries_IsolationGroups => Scope.FindXPath("//a[@ui-sref ='isolationGroups']"); // группы выделения
        private ElementScope Dictionaries_IsolationSubgroups => Scope.FindXPath("//a[@ui-sref ='isolationSubgroups']"); // подгруппы выделения
        private ElementScope Dictionaries_Multiplexes => Scope.FindXPath("//a[contains(text(),'Мультиплексы')]"); // мультиплексы
        private ElementScope Dictionaries_Tests => Scope.FindXPath("//a[contains(text(),'Тесты')]"); // тесты
        private ElementScope Dictionaries_Sched => Scope.FindXPath("//a[.='Расписания')]"); // безбожно устарело
        private ElementScope Dictionaries_Reagents => Scope.FindXPath("//a[@ui-sref ='reagents']"); // реагенты
        private ElementScope Dictionaries_ReasonCancellations => Scope.FindXPath("//a[@ui-sref ='reasonCancellations']"); // причины отмены

        // Заголовки для проверки и прочее:
        private ElementScope TitleDeviceSettings => Scope.FindXPath("//h3[.='Настройки оборудования']");
        private ElementScope LinkDefaultTerminalName => Scope.FindXPath("//b[.='не установлен ']");
        private ElementScope LinkTerminalName => Scope.FindXPath("//b[contains(text(),'Test')]");
        private ElementScope FieldToSetTermID => Scope.FindXPath("//input[@await-barcode='vm.awaitingTerminalCode']");
        private ElementScope TitleCheckTerm => Scope.FindCss("h2.ng-scope");
        private ElementScope TitleMainPage => Scope.FindXPath("//h1[.='Бэтч Дроппер']");
        private ElementScope BatchesTitle => Scope.FindXPath("//h3[.='Бэтчи']");
        private ElementScope TitleDictTest => Scope.FindXPath("//h3[.='Тесты']");
        private ElementScope TitleDictMultiplexes => Scope.FindXPath("//h3[.='Мультиплексы']");
        private ElementScope TitleDictSched => Scope.FindXPath("//h3[.='Расписания']");
        private ElementScope TitleSettings => Scope.FindXPath("//h3[.='Настройки']");
        private ElementScope TitleTerminals => Scope.FindXPath("//h3[.='Терминалы']"); // Заголовок Терминалы
        private ElementScope TitleReagents => Scope.FindXPath("//h3[.='Реагенты']"); // Заголовок справочника Реагенты
        private ElementScope SortingTitle => Scope.FindXPath("//h3[.='Сортировка']");
        private ElementScope DNKTitle => Scope.FindXPath("//h3[.='Выделение ДНК в пробирке']");
        private ElementScope ProductionAcceptanceTitle => Scope.FindXPath("//h3[.='Прием в постановке']");
        private ElementScope BatchFormingWorkplaceTitle => Scope.FindXPath("//h3[.='Формирование тестовых бэтчей']");
        private ElementScope BatchReverseTranscriptionFormingWorkplaceTitle => Scope.FindXPath("//h3[.='Формирование бэтчей с обратной транскрипцией']");
        private ElementScope ReverseTranscriptionWorkplaceTitle => Scope.FindXPath("//h3[.='Обратная транскрипция']");
        private ElementScope ReagentsWorkplaceTitle => Scope.FindXPath("//h3[.='Реагенты']");
        private ElementScope ManualDrippingTitle => Scope.FindXPath("//h3[contains(text(),'Ручное раскапывание')]");
        private ElementScope TubesManualDrippingTitle => Scope.FindXPath("//h3[contains(text(),'Постановка реакций в пробирках')]");
        private ElementScope PositiveControlsTitle => Scope.FindXPath("//h3[contains(text(),'Положительные контроли')]");
        private ElementScope TitleWorkplaceWithName(string _) => Scope.FindXPath($"//h3[.='{_}']"); // заголовок РМ по названию

        public bool AssertMainPageIsUnlocked()
        {
            if (Batches.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) })) return true;
            else Debug.WriteLine("Модуль не загрузился в течение 20 сек. "); return false;
        }

        // Метод для установки терминала
        public void SetTerminal(string termID)
        {
            LinkDefaultTerminalName.Click();
            FieldToSetTermID.SendKeys(termID);
            FieldToSetTermID.SendKeys(Keys.Enter);
        }

        // Метод для смены терминала
        public void SetAnotherTerminal(string termID)
        {
            ChangeTerminalButton.WaitForClickability();
            ChangeTerminalButton.Click();
            FieldToSetTermID.SendKeys(termID);
            FieldToSetTermID.SendKeys(Keys.Enter);
        }

        // Метод для проверки установленного имени терминала
        public bool AssertTermName()
        {
            if (LinkTerminalName.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для проверки заголовка главной страницы
        public bool AssertMainTitle()
        {
            if (TitleMainPage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
            //123
        }

        // Метод для перехода в меню Настройки - Настройки (+ проверка)
        public void OpenSettings()
        {
            Settings.Click();
            SettingsItem.Click();
        }
        public bool AssertOpenSettings()
        {
            if (TitleSettings.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }


        // Метод для перехода в меню Настройки - Терминалы (+ проверка)
        public void OpenTerminals()
        {
            Settings.Click();
            Settings_Terminals.Click();
        }
        public bool AssertOpenTerminals()
        {
            if (TitleTerminals.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }


        // Метод для перехода в меню Справочники - Тесты (+ проверка)
        public void OpenDictionaries()
        {
            Dictionaries.Click();
            Dictionaries_Tests.Click();
        }
        public bool AssertOpenDictTest()
        {
            if (TitleDictTest.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в меню Справочники - Реагенты (+ проверка)
        public void OpenDictionariesReagents()
        {
            Dictionaries.Click();
            Dictionaries_Reagents.Click();
        }
        public bool AssertOpenDictionaries_Reagents()
        {
            if (TitleReagents.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в справочники - Мультиплексы (+ проверка)
        public void OpenMultiplexes()
        {
            Dictionaries.Click();
            Dictionaries_Multiplexes.Click();
        }
        public bool AssertOpenMultiplexes()
        {
            if (TitleDictMultiplexes.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в меню Справочники - Расписания (+ проверка)

        public void OpenSchedules()
        {
            Dictionaries.Click();
            Dictionaries_Sched.Click();
            System.Threading.Thread.Sleep(1000);
        }
        public bool AssertOpenSchedules()
        {
            if (TitleDictSched.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Настройки - Оборудование (+ проверка)

        public void OpenDeviceSettings()
        {
            Settings.Click();
            DeviceSettings.Click();
        }
        public bool AssertOpenDeviceSettings()
        {
            if (TitleDeviceSettings.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Сортировка (+ проверка)
        public void OpenSorting()
        {
            Workflows.Click();
            Sorting.Click();
        }
        public bool AssertOpenSorting()
        {
            if (SortingTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Бэтчи (+ проверка)

        public void OpenBatches()
        {
            Batches.Click();
        }
        public bool AssertOpenBatches()
        {
            if (BatchesTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(10) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Выделение ДНК в пробирке + провкерка

        public void OpenDNKWorkplace()
        {
            Workflows.Click();
            DNK.Click();
        }
        public bool AssertOpenDNKWorkplace()
        {
            if (DNKTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Прием в постановке (+ проверка)

        public void OpenProductionAcceptance()
        {
            Workflows.Click();
            ProductionAcceptance.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(900) });
        }
        public bool AssertOpenProductionAcceptance()
        {
            if (ProductionAcceptanceTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Формирование бэтчей (+ проверка)

        public void OpenBatchFormingWorkplace()
        {
            Workflows.Click();
            BatchFormingWorkplace.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(900) });
        }
        public bool AssertOpenBatchFormingWorkplace()
        {
            if (BatchFormingWorkplaceTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Формирование бэтчей ОТ (+ проверка)

        public void OpenBatchReverseTranscriptionFormingWorkplace()
        {
            Workflows.Click();
            BatchReverseTranscriptionFormingWorkplace.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(900) });
        }
        public bool AssertBatchReverseTranscriptionFormingWorkplace()
        {
            if (BatchReverseTranscriptionFormingWorkplaceTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Обратная транскрипция (+ проверка)

        public void OpenReverseTranscriptionWorkplace()
        {
            Workflows.Click();
            ReverseTranscriptionWorkplace.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(900) });
        }
        public bool AssertReverseTranscriptionWorkplace()
        {
            if (ReverseTranscriptionWorkplaceTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Реагенты (+ проверка)

        public void OpenReagentsWorkplace()
        {
            Workflows.Click();
            ReagentsWorkplace.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(2050) });
        }
        public bool AssertOpenReagentsWorkplace()
        {
            if (ReagentsWorkplaceTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Ручное раскапывание (+ проверка)

        public void OpenManualDripping()
        {
            Workflows.Click();
            ManualDripping.Click();
        }
        public bool AssertOpenManualDripping()
        {
            if (ManualDrippingTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Постановка реакций в пробирках (+ проверка)

        public void OpenTubesManualDripping()
        {
            Workflows.Click();
            TubesManualDripping.Click();
        }
        public bool AssertOpenTubesManualDripping()
        {
            if (TubesManualDrippingTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место - Положительные контроли (+ проверка)

        public void OpenPositiveControls()
        {
            Workflows.Click();
            PositiveControls.Click();
        }
        public bool AssertOpenPositiveControls()
        {
            if (PositiveControlsTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Метод для перехода в Рабочее место с заданным именем (+ проверка)
        public void OpenWorkplaceWithName(string _)
        {
            Workflows.Click();
            WorkplaceWithName(_).Click();
        }
        public bool AssertWorkplaceWithName(string _)
        {
            if (TitleWorkplaceWithName(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }


    }
}

