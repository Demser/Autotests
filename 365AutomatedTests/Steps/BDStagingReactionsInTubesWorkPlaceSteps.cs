// прохождение РМ Постановка реакций в пробирках для всех бэтчей с подходящим статусом и типом бэтча

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
    public class BDStagingReactionsInTubesWorkPlaceSteps
    {
        [Then(@"I open staging reactions in tubes workplace and start new batch")]
        public void ThenIOpenStagingReactionsInTubesWorkplaceAndStartNewBatch()
        {
            BDMainPage bDMainPage = new BDMainPage();
            bDMainPage.OpenTubesManualDripping();
            IsTrue(bDMainPage.AssertOpenTubesManualDripping(), "Заголовок рабочего места не найден");
            
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 9 and name like 'TT-%'";
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command); // Сколько бэтчей со статусом "Сформирован"
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches); // перевод количества в int

            if (intCount != 0)
            {
                for (int i = 1; i <= intCount; i++)
                {
                    BDTubesManualDrippingPage bDTubesManualDrippingPage = new BDTubesManualDrippingPage();
                    bDTubesManualDrippingPage.AssertButtonIsVisible();  // подтверждение, что кнопка взять в работу есть
                    bDTubesManualDrippingPage.ClickTakeToWorkButton(); // нажать взять в работу
                    bDTubesManualDrippingPage.ClickConfirmTakeToWorkButton(); // подтвердить взятие в работу бэтча
                    CommonSteps commonSteps = new CommonSteps();
                    commonSteps.ThenThePopupWithTitleShouldBeClosed(); // закрыть окно с распечаткой
                    bDTubesManualDrippingPage.SetTestBatchID(); // подтвердить ID бэтча
                    commonSteps.ThenThePopupWithTitleShouldBeClosed(); // закрыть окно с распечаткой
                    bDTubesManualDrippingPage.SetUsercode(); // ввести ШК пользователя
                    bDTubesManualDrippingPage.AddClipboardCopyOfBatches(); //раскапать бэтчи
                    bDTubesManualDrippingPage.AddClipboardCopyOfTubes(); // раскапать пробы
                    bDTubesManualDrippingPage.ConfurmEndOfWork(); // подтвердить завершение раскапывания личным ШК пока есть поле для ввода
                    commonSteps.ThenThePopupWithTitleShouldBeClosed(); // закрыть окно с распечаткой
                }
            }
            else
            {
                throw new NullReferenceException("Ошибочка вышла! В базе не найдено бэтчей со статусом: Отрицательные контроли собраны");
            }

        }
    }
}
