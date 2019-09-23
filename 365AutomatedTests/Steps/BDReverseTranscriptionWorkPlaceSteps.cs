// Шаги для прохождения РМ Обратная транскрипция

using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;
using Coypu;
using OpenQA.Selenium;
using System.Linq;
using System.Diagnostics;

namespace _365AutomatedTests.Steps
{
    using Coypu;
    using OpenQA.Selenium;
    using System.Threading;
    using static NUnit.Framework.Assert;
    [Binding]
    public class BDReverseTranscriptionWorkPlaceSteps
    {
        [Then(@"I open reverse transcription workplace and start new batch")]
        public void ThenIOpenReverseTranscriptionWorkplaceAndStartNewBatch()
        {
            BDMainPage bDMainPage = new BDMainPage();
            bDMainPage.OpenReverseTranscriptionWorkplace();
            IsTrue(bDMainPage.AssertReverseTranscriptionWorkplace(), "Заголовок рабочего места не найден");

            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 9 and name like 'RT-%'";
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command); // Сколько бэтчей со статусом "Сформирован"
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches); // перевод количества в int

            if (intCount != 0)
            {
                for (int i = 1; i <= intCount; i++)
                {
                    Debug.WriteLine(i);
                    // иконка Взять в работу - подтвердить взять в работу
                    BDReagentsWorkplacePage bDReagentsWorkplacePage = new BDReagentsWorkplacePage();
                    bDReagentsWorkplacePage.ClickTakeToWorkButton();
                    bDReagentsWorkplacePage.ClickConfirmTakeToWorkButton();
                    CommonSteps commonSteps = new CommonSteps();
                    commonSteps.ThenThePopupWithTitleShouldBeClosed(); // закрыть окно с распечаткой
                    // скопипастить идентификатор и вставить его в поле ввода
                    bDReagentsWorkplacePage.SetTestBatchID();
                    // подтвердить ШК пользователя
                    bDReagentsWorkplacePage.SetUsercode();
                    // в поле "Личный штрих-код пользователя для следующего теста" вводить шк пользователя пока поле не станет
                    //с названием "Личный штрих-код пользователя для завершения"
                    if (!bDReagentsWorkplacePage.ConfirmUserCodeForEnding.Exists())
                    {
                        IsTrue(bDReagentsWorkplacePage.AssertThefieldIsActive(), "Поле для ввода не найдено или заблокировано");
                        bDReagentsWorkplacePage.FillOutTheReagents();
                    }
                    // ввести шк пользователя в поле "Личный штрих-код пользователя для завершения"
                    IsTrue(bDReagentsWorkplacePage.AssertConfirmUserCodeForEndingField(), "Поле для окончания ввода не найдено или заблокировано");
                    bDReagentsWorkplacePage.ConfirmUserCodeForEndingReagents();
                    // получить всплывающее сообщение. Автопереход к раскапыванию контролей
                    IsTrue(bDReagentsWorkplacePage.AssertReadyMessage(), "Сообщение о раскапанных реагентах не пришло");

                    BDTubesManualDrippingPage bDTubesManualDrippingPage = new BDTubesManualDrippingPage(); // переход от реагентов к ручному раскапыванию
                    System.Threading.Thread.Sleep(500); // костыль, но без него все сыпется
                    bDTubesManualDrippingPage.AddClipboardCopyOfBatches(); //раскапать бэтчи
                    bDTubesManualDrippingPage.AddClipboardCopyOfTubes(); // раскапать пробы
                    bDTubesManualDrippingPage.ConfurmEndOfWorkRT(); // подтвердить завершение раскапывания личным ШК пока есть поле для ввода

                    IsTrue(bDTubesManualDrippingPage.AssertBatchWithRTCompliteMessage(), "Сообщение о собранном бэтче не пришло");
                    commonSteps.ThenThePopupWithTitleShouldBeClosed(); // закрыть окно с распечаткой
                    
                    IsTrue(bDReagentsWorkplacePage.AssertTableOfBatches(), "Автоматический возврат на таблицу с бэтчами не сработал");
                }
            }
        }
    }
}



