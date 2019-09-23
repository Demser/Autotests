using _365AutomatedTests.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;


namespace _365AutomatedTests.UIObjects.Pages
{
    class BDTubesManualDrippingPage : Page
    {
        public BDTubesManualDrippingPage() : base()
        {
        }
        private ElementScope GetTaskButtonIsVisible => Scope.FindXPath("(//i[@class='fa fa-play fa-2x'])[2]", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        //private ElementScope TaskButton => Scope.FindButton("Взять в работу"); // найти кнопку взять в работу
        private ElementScope ConfurmTaskButton => Scope.FindCss("button.k-button.ng-binding"); // кнопка подтверждения "Взять в работу" в всплывающем окне
        private ElementScope ConfurmTestBatchField => Scope.FindId("confirmBatchId"); // поле подтверждения ID бэтча
        private ElementScope ConfurmTestReactionsField => Scope.FindXPath("//input[@placeholder='Штрих-код исходного бэтча или пробирки']"); // поле "Штрих-код исходного бэтча или пробирки"
        private ElementScope ConfurmTestUsercodeField => Scope.FindXPath("//input[@placeholder='Личный штрих-код пользователя']"); // поле "Личный штрих-код пользователя"
        private ElementScope TestBatchID => Scope.FindId("batchId");
        private string TestBatchIDValue => TestBatchID.Text; // для взятия ID бэтча, взятого в работу
        private ElementScope ConfirmUsercodeField => Scope.FindId("authenticateBatchId"); // поле ШК пользователя в всплывающем окне

        public List<ElementScope> ListOfTubes => Scope.FindAllXPath("//tr[@class='ng-scope' and contains(@ng-repeat,'tube in')]").Select(i => (ElementScope)i).ToList(); // взять список всех проб
        public List<ElementScope> ListOfBatches => Scope.FindAllXPath("//tr[@class='ng-scope' and contains(@ng-repeat,'batch in')]").Select(j => (ElementScope)j).ToList(); // взять список всех бэтчей
        public ElementScope FirstBatch => Scope.FindXPath("(//tr[@class='ng-scope' and contains(@ng-repeat,'batch in')])[1]"); // первый бэтч в списке
        public ElementScope FirstTube => Scope.FindXPath("(//tr[@class='ng-scope' and contains(@ng-repeat,'tube in')])[1]"); // первая проба в списке

        private ElementScope SetChildBatchField => Scope.FindXPath("//input[@placeholder='Штрих-код бэтча или образца']");
        private ElementScope UserBarcodeForTubesManualDripping => Scope.FindXPath("//input[@placeholder='Личный штрих-код пользователя']");
        //private ElementScope AssertTitleTestName => Scope.FindXPath("//h3[.='CYTOMEGALOVIRUS_ДНК:']");

        private ElementScope BatchWithRTCompliteMessage => Scope.FindXPath("//div[@class='toast-message' and contains(text(),'Успешно создан бэтч с ОТ')]"); // сообщение для ручного раскапывания бэтча с ОТ

        public void ClickTakeToWorkButton() { GetTaskButtonIsVisible.Click(); }  // Нажать на кнопку Получить задание
        public void ClickConfirmTakeToWorkButton() { ConfurmTaskButton.Click(); }

        // Проверка, что кнопка есть на странице
        public bool AssertButtonIsVisible()
        {
            if (GetTaskButtonIsVisible.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

       
        public void GetTaskForStartWorking()
        {
            GetTaskButtonIsVisible.Click();
        }

        // Подтвердить бэтч
        public void SetTestBatchID()
        {
            ConfurmTestBatchField.Click();
            ConfurmTestBatchField.SendKeys(TestBatchIDValue);
            ConfurmTestBatchField.SendKeys(Keys.Enter);

        }

        // Подтвердить шк пользователя в окне установки планшета
        public void SetUsercode()
        {
            ConfirmUsercodeField.SendKeys(Config.UserBarCode);
            ConfirmUsercodeField.SendKeys(Keys.Enter);
        }

        // Подтвердить шк по завершению раскапывания образцов пока есть поле ввода личного ШК
        public void ConfurmEndOfWork()
        {
            while (ConfurmTestUsercodeField.Exists())
            {
                ConfurmTestUsercodeField.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);
            }
        }

        // Подтвердить шк по завершению раскапывания образцов пока есть поле ввода личного ШК для ОТ
        public void ConfurmEndOfWorkRT()
        {
            if (ConfurmTestUsercodeField.Exists())
            {
                ConfurmTestUsercodeField.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);
            }
        }


        public void AddClipboardCopyOfTubes()
        {
            if (ListOfTubes != null)
            {
                foreach (ElementScope i in ListOfTubes)
                {
                    ConfurmTestReactionsField.WaitForClickability();
                    var s = FirstTube["clipboard-copy"];
                    System.Diagnostics.Debug.WriteLine(s);
                    ConfurmTestReactionsField.SendKeys(s).SendKeys(Keys.Enter);
                }
            }
        }
        public void AddClipboardCopyOfBatches()
        {
            if (ListOfBatches != null)
            {
                foreach (ElementScope i in ListOfBatches)
                {
                    ConfurmTestReactionsField.WaitForClickability();
                    var s = FirstBatch["clipboard-copy"];
                    System.Diagnostics.Debug.WriteLine(s);
                    ConfurmTestReactionsField.SendKeys(s).SendKeys(Keys.Enter);
                    BDReagentsWorkplacePage bDReagentsWorkplacePage = new BDReagentsWorkplacePage();
                    bDReagentsWorkplacePage.SetUsercode();
                    while (UserBarcodeForTubesManualDripping.Exists())
                    {
                        try { UserBarcodeForTubesManualDripping.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter); }
                        catch (System.Exception e) { break; }
                    }
                }
            }
        }

        public bool AssertBatchWithRTCompliteMessage() // проверка на появление сообщения "Успешно создан бэтч с ОТ"
        {
            if (BatchWithRTCompliteMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) { return true; }
            else return false;
        }

    }
}



       

