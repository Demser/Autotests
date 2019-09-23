using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System;
using System.Diagnostics;
using System.IO;
using static NUnit.Framework.Assert;
using System.Windows.Forms;
using TechTalk.SpecFlow;
using Coypu;

using System.Windows.Automation;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White;

namespace _365AutomatedTests.Steps
{

    [Binding]
    public class BDFullCycleOneScenarioWithStandartAndWaxSteps
    {
        ElementScope SecondTab;
        private TestStack.White.Application application;

        [Then(@"I get result file by Amplificator Utilite")]
        public void ThenIGetResultFileByAmplificatorUtilite()
        {
            BDMainPage bDMainPage = new BDMainPage();
            bDMainPage.OpenBatches(); // переходим на страницу Бэтчи
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 40"; // считаем количество бэтчей в статусе "Отправлен на прибор"
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command);
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches);


            if (intCount != 0)
            {
                string command1 = $@"SELECT [Id] FROM[BatchDropperStab].[dbo].[Batches] Where Status = 40 Order by ChangedOn desc";
                var listOfID = _msBDConnector.QueryExecutor(command1);

                foreach (string id in listOfID) // Если подходящих бэтчей несколько, берём каждый
                {
                    int intId = System.Convert.ToInt32(id); //Переводим id бэтча в числовое значение:
                    string fullId = ""; // Определяем строку полного номера бэтча:

                    if (intId >= 1 && intId <= 999999)  // Присваиваем полный номер бэтча, в зависимости от его id:
                    {
                        fullId = intId.ToString().PadLeft(6, '0'); // переводим номер бэтча в строку и добавляем 0 впереди пока длина строки не станет равна 6
                    }
                    else { throw new NullReferenceException("Ошибочка вышла! Значение id бэтча не может быть более 999999 или менее 1. Ваше значение: " + intId); }

                    System.Diagnostics.Debug.WriteLine("Окончательный номер бэтча: " + fullId);
                    // Копируем из папки заданий бэтчдроппера в утилитную папку заданий:
                    File.Copy($"\\\\tst10-web.medlinx.corp\\AmplificatorStab\\Task\\{fullId}.xml", $"D:\\UtilityAmplificator\\tasks\\{fullId}.xml");

                    // Запускаем утилиту у себя на компе:
                    Process emulationProc = new Process();
                    emulationProc = Process.Start("D:\\UtilityAmplificator\\Console.lnk");
                    System.Threading.Thread.Sleep(1000);
                    SendKeys.SendWait("{Enter}");
                    System.Threading.Thread.Sleep(1000);

                    // Копируем файл с результатами из утилитной папки в папку результатов бэтчдроппера:
                    File.Copy($"D:\\UtilityAmplificator\\results\\{fullId}_result.xml", $"\\\\tst10-web.medlinx.corp\\AmplificatorStab\\Result\\{fullId}_result.xml");
                    // выносим номер бетча строкой без нулей 
                    var actualBatchId = intId.ToString();
                    // проверяем, что актуальный бэтч перешел статус "Требует валидации" за 10 секунд (?)
                    BDBatchesPage bDBatchesPage = new BDBatchesPage();
                    IsFalse(bDBatchesPage.IdBatchNeedsValidation(actualBatchId).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(10) }), "Внимание! Бэтч не сменил статус!");
                    // берем бэтч
                    string command2 = $@"SELECT top 1 [Id] FROM [BatchDropperStab].[dbo].[Batches] Where Status = 50 order by ChangedOn desc";
                    var batchNeedValidation = _msBDConnector.QueryExecutorScalar(command2);
                    if (batchNeedValidation == actualBatchId)
                    {
                        System.Diagnostics.Debug.WriteLine("Результаты для бэтча " + id + " выгружены в бэтчдроппер");
                    }
                    else
                    {
                        throw new NullReferenceException("Ошибочка вышла! Бэтч не перешел в статус Требует валидации. Id бэтча: " + intId);
                    }
                }
            }
        }

        [Then(@"I check results, send them and copy file csv to LabWare folder")]
        public void ThenICheckSendCopyResultsInBatchPage()
        {
            BDMainPage bDMainPage = new BDMainPage();
            bDMainPage.OpenBatches(); // переходим на страницу Бэтчи
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 50"; // считаем количество бэтчей в статусе "Требует валидации"
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command);
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches);

            if (intCount != 0)
            {
                string command1 = $@"SELECT [Id] FROM[BatchDropperStab].[dbo].[Batches] Where Status = 50 Order by ChangedOn desc";
                var listOfID = _msBDConnector.QueryExecutor(command1);

                foreach (string id in listOfID) // Если подходящих бэтчей несколько, берём каждый
                {
                    // Кликаем на галку проверки на странице бэтчей
                    BDBatchesPage bDBatchesPage = new BDBatchesPage();
                    CommonSteps commons = new CommonSteps();
                    commons.RefreshPage();
                    IsTrue(bDBatchesPage.AssertContentOfBatchButton(), "Не найдена кнопка для открытия содержимого первого бэтча со статусом Требует валидации");
                    bDBatchesPage.OpenContentOfFirstNeedValidateTest();

                    SecondTab = commons.ThenTheTabWithTitleShouldBeOpened("BatchDropper");

                    BDBatchesPage bDBatchesPageNew = new BDBatchesPage(SecondTab);
                    bDBatchesPageNew.ClickIconResultsWasChecked();
                    IsTrue(bDBatchesPageNew.AssertDialogWindowConfirmIsOpen(), "Не открылось диалоговое окно подтверждения действия Результаты проверены");
                    bDBatchesPageNew.ClickConfirmButtonResultsWasChecked();

                    // отпраавляем результат
                    bDMainPage.OpenBatches();
                    commons.RefreshPage();

                    bDBatchesPage.ClickIconResultsReadyToSend();
                    IsTrue(bDBatchesPage.AssertDialogWindowReadyToSendIsOpen(), "Не открылось диалоговое окно подтверждения действия Экспорт в LW");
                    bDBatchesPage.ClickConfirmReadyToSendButton();
                    IsTrue(bDBatchesPage.AssertSucceedMessage(), "Сообщение об успехе не появилось");

                    // копируем файл с подтвержденным результатом в LabWare
                    var batchNeedsCopy = System.Convert.ToInt32(id);
                    string[] getFile = Directory.GetFiles(@"\\\\tst10-web.medlinx.corp\\BatchDropper\\LW_Stab\\", $"*{batchNeedsCopy}.csv");
                    string fileName = Path.GetFileName(getFile[0]);
                    System.Diagnostics.Debug.WriteLine(fileName);

                    File.Copy(getFile[0], $"\\\\tst10-lwapp.medlinx.corp\\LW-LIMS-V6\\LW-LIMS_KULIKOV-A\\{fileName}");

                    // проверяем, что файл скопировался
                    string[] checkFile = Directory.GetFiles(@"\\\\tst10-lwapp.medlinx.corp\\LW-LIMS-V6\\LW-LIMS_KULIKOV-A\\", $"*{batchNeedsCopy}.csv");
                    string checkName = Path.GetFileName(checkFile[0]);
                    if (fileName == checkName)
                    {
                        System.Diagnostics.Debug.WriteLine("Результаты для бэтча " + batchNeedsCopy + " выгружены в LabWare");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Что то пошло не так. Результаты бэтча " + batchNeedsCopy + " не удалось выгрузить в LabWare");
                    }
                    System.Threading.Thread.Sleep(4000);
                }// Этот блок будет воспроизведен для всех подходящих по статусу бэтчей
            }
            else
            {
                throw new NullReferenceException("Ошибочка вышла! В базе не найдено бэтчей, требующих проверки результатов");
            }
        }


        [Then(@"I start LabWare and processed results")]
        public void ThenIStartLabWareAndProcessedResults()
        {
            System.Media.SystemSounds.Beep.Play();
           
            //System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"D:\STalkToME.wav");
            //sp.Play();

            application = TestStack.White.Application.Launch("\\\\tst10-lwapp.medlinx.corp\\LW-LIMS-V6\\lw-lims.lnk");
            Window mainWindow = application.GetWindow("LabWare LIMS");
            TestStack.White.UIItems.Button btnbuttonintoolbar = mainWindow.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByText("Log into LIMS"));
            btnbuttonintoolbar.Click();
            Window logInWindow = application.GetWindow("Please Log In");
            TestStack.White.UIItems.TextBox loginField = logInWindow.Get<TestStack.White.UIItems.TextBox>(SearchCriteria.ByAutomationId("101"));
            loginField.BulkText="testopr";
            TestStack.White.UIItems.TextBox passField = logInWindow.Get<TestStack.White.UIItems.TextBox>(SearchCriteria.ByAutomationId("102"));
            passField.BulkText = "123456";
            TestStack.White.UIItems.Button okButton = logInWindow.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("104"));
            okButton.Click();

            Window locationWin = application.GetWindow("Выберите расположение");
            TestStack.White.UIItems.ListViewRow chooseSPB = locationWin.Get<TestStack.White.UIItems.ListViewRow>(SearchCriteria.ByText("HELIX-SPB")); //= locationWin.Get<TestStack.White.UIItems.TextBox>(SearchCriteria.ByClassName("HELIX-SPB"));
            chooseSPB.Click();
            TestStack.White.UIItems.Button okLocationButton = locationWin.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("104"));
            okLocationButton.Click();
           

            Window windowAfterLogin = Desktop.Instance.Windows().Find(obj => obj.Title.Contains("User TESTOPR logged"));
            var menuBar = windowAfterLogin.Get<MenuBar>(SearchCriteria.ByText("Приложение"));
            var menu = menuBar.MenuItem("Configure","System", "Client...");
            
            menu.Click();

            Window passworDialogWindow = application.GetWindow("Password Dialog");
            TestStack.White.UIItems.Button okButtonInPasswordDialog = passworDialogWindow.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("102"));
            okButtonInPasswordDialog.Click();

            Window configurationManagerWindow = application.GetWindow("Configuration Manager");
            TestStack.White.UIItems.Button okButtonInConfigurationManager = configurationManagerWindow.Get<TestStack.White.UIItems.Button>(SearchCriteria.ByAutomationId("102"));
            okButtonInConfigurationManager.Click();
            System.Threading.Thread.Sleep(3000);
            application.Close();
        }
    }
}




