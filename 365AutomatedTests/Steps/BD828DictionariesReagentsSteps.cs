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
    public class BD828DictionariesReagentsSteps
    {
        [Then(@"I have opened Settings Reagents")]
        public void ThenIHaveOpenedSettingsReagents()
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenDictionariesReagents();
            IsTrue(BDMain.AssertOpenDictionaries_Reagents(), "Не найден заголовок на странице Реагенты");
        }

        [When(@"I create new reagent, I check that it exist and check it in database")]
        public void WhenICreateNewReagentICheckThatItExistAndCheckItInDatabase()
        {
            BDDictionariesReagentsPage bDDictionariesReagentsPage = new BDDictionariesReagentsPage();
            bDDictionariesReagentsPage.ClickToAddReagent();
            IsTrue(bDDictionariesReagentsPage.AssertOpenAddingNewReagent(), "Что-то пошло не так, нет пустых полей для добавления нового реагента в справочник");
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("999");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.CheckNewCreatedReagent("Test1").Exists(), "Что-то пошло не так, новый реагент не добавлен");
            CommonSteps commons = new CommonSteps();
            commons.RefreshPage(); // обновили страницу
            bDDictionariesReagentsPage.WaitAfterRefresh();
            IsTrue(bDDictionariesReagentsPage.CheckNewCreatedReagent("Test1").Exists(), "Что-то пошло не так, новый реагент пропал после обновления");
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            // проверяем, что появилась запись в бд
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Reagents] Where name = 'Test1' and TubeVolume = 999 and SampleVolume=99"; //
            var countOfBatches = _msBDConnector.QueryExecutorScalar(command);
            int intCount;
            intCount = System.Convert.ToInt32(countOfBatches);
            if (intCount == 1)
            {
                Debug.WriteLine("ОК. Реагент успешно добавлен в базу данных");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, реагент не отображается в базе");
            }
            Debug.WriteLine("ОК. Проверка добавления реагента с валидными значениями успешно пройдена");
        }

        [Then(@"I check input fields on the reagents dictionary page")]
        public void ThenICheckInputFieldsOnTheReagentsDictionaryPage()
        {
            BDDictionariesReagentsPage bDDictionariesReagentsPage = new BDDictionariesReagentsPage();
            // пытаемся добавить реагент с незаполненными полями
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectValueMessage(), "Что-то пошло не так, нет сообщения о незаполненных обязательных полях");
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            Debug.WriteLine("ОК. Не сохраняется реагент с пустыми полями");
            //валидания поля Название
            bDDictionariesReagentsPage.ChangeReagentWithThisNameButton("Test1").Click();
            bDDictionariesReagentsPage.CleanReagentsNameField();
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectNameMessage(), "Что-то пошло не так, нет сообщения о незаполненом поле Название");
            bDDictionariesReagentsPage.AddInvalidName();
            IsTrue(bDDictionariesReagentsPage.ValidationNameFieldVolume(), "Что-то пошло не так, поле Название не ограничено в 255 символов");
            bDDictionariesReagentsPage.ChangeReagentWithThisNameButton("Цель феноменологической редукции — освободить сознание от «естественной " +
                "установки», цель экзистенциальной диалектики — преодолеть объективацию свободы (или, если угодно," +
                " трансценденции). Сходство целей сомнений не вызывает — и в том и в другом случае " +
                "это ").Click();
            bDDictionariesReagentsPage.CleanReagentsNameField();
            bDDictionariesReagentsPage.AddNewReagentName("!№;%:?*()_");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.CheckNewCreatedReagent("!№;%:?*()_").Exists(), "Что-то пошло не так, новый реагент с названием !№;%:?*()_не добавлен");
            bDDictionariesReagentsPage.ChangeReagentWithThisNameButton("!№;%:?*()_").Click();
            bDDictionariesReagentsPage.CleanReagentsNameField();
            bDDictionariesReagentsPage.AddNewReagentName("1");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.CheckNewCreatedReagent("1").Exists(), "Что-то пошло не так, новый реагент с названием 1 не добавлен");
            bDDictionariesReagentsPage.DeleteReagentWithThisNameButton("1").Click();
            bDDictionariesReagentsPage.ClickToConfurmDeleteReagentButton();
            Debug.WriteLine("ОК. Поле Название проверено");
            //валидация поля Объем пробирки
            bDDictionariesReagentsPage.ClickToAddReagent();
            IsTrue(bDDictionariesReagentsPage.AssertOpenAddingNewReagent(), "Что-то пошло не так, нет пустых полей для добавления нового реагента в справочник");
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectTubeVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при сохранении пустого поля");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("^*&^$%%$#");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectTubeVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе невалидных значений");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("-1");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectTubeVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе отрицательных значений");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("0");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectTubeVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе нулевых значений");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("1");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectTubeVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе значения ниже допустимого");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("10000");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectTubeVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе значения выше допустимого");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("9999");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("99");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsFalse(bDDictionariesReagentsPage.AccertErrorNotCorrectTubeVolumeMessage(), "Что то пошло не так, невалидное сообщение об ошибке при вводе валидных значений");
            if (bDDictionariesReagentsPage.CheckNewCreatedReagentWithVolume("Test1", "9999").Exists())
            {
                Debug.WriteLine("ОК. Реагент успешно сохранен. Поле Объем пробирки проверено");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, не сохранен реагент при вводе валидного значенияв поле Объем пробирки");
            }
            bDDictionariesReagentsPage.DeleteReagentWithThisNameButton("Test1").Click();
            bDDictionariesReagentsPage.ClickToConfurmDeleteReagentButton();
            //валидация поля Объем пробы
            bDDictionariesReagentsPage.ClickToAddReagent();
            IsTrue(bDDictionariesReagentsPage.AssertOpenAddingNewReagent(), "Что-то пошло не так, нет пустых полей для добавления нового реагента в справочник");
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("999");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectSampleVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при сохранении пустого поля");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("999");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("^*&^$%%$#");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectSampleVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе невалидных значений");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("999");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("-1");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectSampleVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе отрицательных значений");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("999");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("0");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectSampleVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе нулевых значений");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("999");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("100");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsTrue(bDDictionariesReagentsPage.AccertErrorNotCorrectSampleVolumeMessage(), "Что то пошло не так, нет сообщения об ошибке при вводе значения выше допустимого");
            bDDictionariesReagentsPage.CloseErrorMessage();
            bDDictionariesReagentsPage.ClickToCancelSaveReagentButton();
            bDDictionariesReagentsPage.ClickToAddReagent();
            bDDictionariesReagentsPage.AddNewReagentName("Test1");
            bDDictionariesReagentsPage.AddNewReagentTubeVolume("999");
            bDDictionariesReagentsPage.AddNewReagentsSampleVolume("1");
            bDDictionariesReagentsPage.ClickToSaveReagentButton();
            IsFalse(bDDictionariesReagentsPage.AccertErrorNotCorrectSampleVolumeMessage(), "Что то пошло не так, невалидное сообщение об ошибке при вводе валидных значений");
            if (bDDictionariesReagentsPage.CheckNewCreatedReagentWithVolume("Test1", "1").Exists())
            {
                Debug.WriteLine("ОК. Реагент успешно сохранен. Поле Объем пробы проверено");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, не сохранен реагент при вводе валидного значенияв поле Объем пробирки");
            }
            // проверка удаления записи
            bDDictionariesReagentsPage.DeleteReagentWithThisNameButton("Test1").Click();
            bDDictionariesReagentsPage.ClickToCloseComfurmDeleteWindowButton();
            if (bDDictionariesReagentsPage.CheckNewCreatedReagentWithVolume("Test1", "1").Exists())
            {
                Debug.WriteLine("ОК. Реагент не удалился при закрытии окна подтверждения удаления");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, при закрытии окна подтверждения удаления удаляется реагент");
            }
            bDDictionariesReagentsPage.DeleteReagentWithThisNameButton("Test1").Click();
            bDDictionariesReagentsPage.ClickToCancelDeleteReagentButton();
            if (bDDictionariesReagentsPage.CheckNewCreatedReagentWithVolume("Test1", "1").Exists())
            {
                Debug.WriteLine("ОК. Отмена удаления работает корректно");
            }
            else
            {
                Debug.WriteLine("Что-то пошло не так, при отмене удаления удаляется реагент");
            }
            bDDictionariesReagentsPage.DeleteReagentWithThisNameButton("Test1").Click();
            bDDictionariesReagentsPage.ClickToConfurmDeleteReagentButton();
            if (bDDictionariesReagentsPage.CheckNewCreatedReagentWithVolume("Test1", "1").Exists())
            {
                Debug.WriteLine("Что-то пошло не так, при удалении реагента он продолжает отображаться на странице");
            }
            else
            {
                Debug.WriteLine("ОК. Удаление реагента успешно");
            }
        }



    }

}

