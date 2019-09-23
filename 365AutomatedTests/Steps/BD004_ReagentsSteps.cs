using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using static NUnit.Framework.Assert;
using System.Diagnostics;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class BD004_ReagentsSteps
    {

        [When(@"I go to Reagents page")]
        public void WhenIGoToReagentsPage()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenReagentsWorkplace();
            IsTrue(mainPage.AssertOpenReagentsWorkplace(), "Не туда попали. Переход на страницу Реагенты не осуществлен!");
        }

        [Then(@"I fill-out the reagents planchetes")]
        public void ThenIFill_OutTheReagentsPlanchetes()
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSDbBatchDropperStab);
            string command = $@"Select count ([Id]) FROM [BatchDropperStab].[dbo].[Batches] Where Status = 9 and name like 'T-%'";
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
                    // скопипастить идентификатор и вставить его в поле ввода
                    bDReagentsWorkplacePage.SetTestBatchID();
                    // подтвердить ШК пользователя
                    bDReagentsWorkplacePage.SetUsercode();
                    // в поле "Личный штрих-код пользователя для следующего теста" вводить шк пользователя пока поле не станет
                    //с названием "Личный штрих-код пользователя для завершения"
                    if (!bDReagentsWorkplacePage.ConfirmUserCodeForEnding.Exists())
                    { 
                    IsTrue(bDReagentsWorkplacePage.AssertThefieldIsActive(),"Поле для ввода не найдено или заблокировано");
                    bDReagentsWorkplacePage.FillOutTheReagents();
                    }
                // ввести шк пользователя в поле "Личный штрих-код пользователя для завершения"
                    IsTrue(bDReagentsWorkplacePage.AssertConfirmUserCodeForEndingField(), "Поле для окончания ввода не найдено или заблокировано");
                    bDReagentsWorkplacePage.ConfirmUserCodeForEndingReagents();
                    // получить всплывающее сообщение. Происходит автопереход к списку оставшихся бэтчей в РМ Реагенты
                    IsTrue(bDReagentsWorkplacePage.AssertReadyMessage(), "Сообщение о раскапанных реагентах не пришло");
                    IsTrue(bDReagentsWorkplacePage.AssertTableOfBatches(), "Автоматический возврат на таблицу с бэтчами не сработал");
                }

            }
        }

    }
}
