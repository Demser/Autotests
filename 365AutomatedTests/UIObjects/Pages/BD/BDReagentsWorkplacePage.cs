using _365AutomatedTests.Framework.Generic;
using Coypu;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.UIObjects.Pages
{
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    [Binding]
    class BDReagentsWorkplacePage : Page
    {
        public BDReagentsWorkplacePage() : base()
        {
        }
        private ElementScope GetTaskButton => Scope.FindXPath("//button[.='Получить задание']");
        private ElementScope BusyIndicatorIsDisabled => Scope.FindXPath("//div[@class='cg-busy cg-busy-animation ng-scope ng-hide']");
        private ElementScope WorkBatchTitle => Scope.FindXPath("//h4[.='Рабочий бэтч:']");
        public ElementScope TestBatchID => Scope.FindId("batchId");
        public string TestBatchIDValue => TestBatchID.Text;
        private ElementScope ConfirmBatchIDField => Scope.FindId("confirmBatchId");
        private ElementScope ConfirmUsercodeField => Scope.FindId("authenticateBatchId");
        private ElementScope ConfirmUserCodeForReagentsField => Scope.FindXPath("//input[@placeholder = 'Личный штрих-код пользователя для следующего теста']"); //FindId("reagentInputId");
        public ElementScope ConfirmUserCodeForEnding => Scope.FindXPath("//input[@placeholder='Личный штрих-код пользователя для завершения']"); //FindId("reagentInputId");
        private ElementScope ThereAreNotSamplesMessage => Scope.FindXPath("//div[.='Нет образцов для ручного раскапывания!']");
        public ElementScope TakeToWorkButton => Scope.FindXPath("(//a[@class='btn' and @title='Взять в работу'])[1]");
        public ElementScope ConfirmTakeToWorkButton => Scope.FindCss("button.k-button.ng-binding");
        public ElementScope ReadyMessage => Scope.FindXPath("//div[@class='toast-message' and contains (text(),'Реагенты раскапаны')]");
        private ElementScope TableOfBatches => Scope.FindCss("div.k-grid.k-widget");

        public void ClickTakeToWorkButton() { TakeToWorkButton.Click(); }
        public void ClickConfirmTakeToWorkButton() { ConfirmTakeToWorkButton.Click(); }

        // Проверка, что кнопка есть на странице
        public bool AssertButtonIsVisible()
        {
            //  System.Threading.Thread.Sleep(5000); // КОСТЫЛЬ 
            if (GetTaskButton.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }
        // проверка, что отобразилось сообщение
        public bool AssertThereAreNotSamplesMessage()
        {
            if (ThereAreNotSamplesMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

        // Проверка, что кнопка активна и на неё можно кликнуть
        public bool AssertTheBusyIndicatorIsDisabled()
        {
            System.Threading.Thread.Sleep(500); //КОСТЫЛЬ 
            if (BusyIndicatorIsDisabled.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

        // Нажать на кнопку Получить задание
        public void GetTaskForReagents()
        {

            Scope.ClickButton("Получить задание", (new Options { Timeout = System.TimeSpan.FromSeconds(25) }));
            System.Threading.Thread.Sleep(2000);
        }

        // Проверка, что отобразился заголовок рабочий бэтч
        public bool AssertWorkBatchTitleActive() {
            if (WorkBatchTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(80) })) // Плохое место. Задача разработчикам
            {
                return true;
            }
            else return false;

        }

        // Подтвердить бэтч
        public void SetTestBatchID()
        {
            ConfirmBatchIDField.Click();
            ConfirmBatchIDField.SendKeys(TestBatchIDValue);
            ConfirmBatchIDField.SendKeys(Keys.Enter);

        }

        // Подтвердить шк пользователя в окне установки планшета
        public void SetUsercode()
        {
            ConfirmUsercodeField.WaitForClickability();
            ConfirmUsercodeField.SendKeys(Config.UserBarCode);
            ConfirmUsercodeField.SendKeys(Keys.Enter);
        }

        public bool AssertConfirmUserCodeForEndingField()
        {
            if (ConfirmUserCodeForEnding.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) { return true; }
            else return false;
        }
        public void ConfirmUserCodeForEndingReagents()
        {
            ConfirmUserCodeForEnding.SendKeys(Config.UserBarCode);
            ConfirmUserCodeForEnding.SendKeys(Keys.Enter);
        }
        

        //Нажимать на кнопку  до тех пор, пока она не станет активной
        public bool AssertThefieldIsActive()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    ConfirmUserCodeForReagentsField.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(250) });
                    return true;
                }
                catch (System.Exception e)
                {
                    count++;
                    if (count > 2) break;
                }
            }
            return false;
        }
            

        public void FillOutTheReagents()
        {

            while (AssertThefieldIsActive())
            {
                try { ConfirmUserCodeForReagentsField.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter); }
                catch (System.Exception e) { break; }
            }      
        }

        public void FillOutTheReagents(int count)
        {
            for(int i=0; i<count;i++)
            {
                if(AssertThefieldIsActive()) ConfirmUserCodeForReagentsField.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);
            }
        }

        public bool AssertReadyMessage()
        {
            if (ReadyMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) { return true; }
            else return false;
        }

        public bool AssertTableOfBatches()
        {
            if (TableOfBatches.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) { return true; }
            else return false;
        }

    }
}
