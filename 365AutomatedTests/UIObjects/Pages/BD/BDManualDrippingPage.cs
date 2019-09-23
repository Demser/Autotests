using System;
using System.Collections.Generic;
using System.Linq;
using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;


namespace _365AutomatedTests.UIObjects.Pages
{
    class BDManualDrippingPage : Page
    {
        public BDManualDrippingPage() : base()
        {
        }

        public ElementScope ConfirmTestBatchField => Scope.FindXPath("//input[contains(@placeholder,'бэтча')]", new Options() {Timeout=TimeSpan.FromSeconds(10) });
        private ElementScope ConfirmBatchOrSample => Scope.FindXPath("//input[@placeholder='Штрих-код исходного бэтча или пробирки']", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        private ElementScope ConfirmManualDrippingField => Scope.FindXPath("//input[@placeholder='Личный штрих-код пользователя']");
        private ElementScope ConfirmTestBatchFieldForStart => Scope.FindXPath("//input[@placeholder='Введите штрихкод бэтча ']", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        private ElementScope DrippingBatchTitle(string _) => Scope.FindXPath($"//td[.='{_}']");

        public List<ElementScope> ListOfTubes => Scope.FindAllXPath("//tr[@class='ng-scope' and contains(@ng-repeat,'tube in')]").Select(i => (ElementScope)i).ToList(); // взять список всех проб
        public List<ElementScope> ListOfBatches => Scope.FindAllXPath("//tr[@class='ng-scope' and contains(@ng-repeat,'batch in')]").Select(j => (ElementScope)j).ToList(); // взять список всех бэтчей
        public ElementScope FirstBatch => Scope.FindXPath("(//tr[@class='ng-scope' and contains(@ng-repeat,'batch in')])[1]"); // первый бэтч в списке
        public ElementScope FirstTube => Scope.FindXPath("(//tr[@class='ng-scope' and contains(@ng-repeat,'tube in')])[1]"); // первая проба в списке
        
        public void AddClipboardCopyOfTubes()
        {
            if (ListOfTubes != null)
            {
                foreach (ElementScope i in ListOfTubes)
                {
                    var s = FirstTube["clipboard-copy"];
                    System.Diagnostics.Debug.WriteLine(s);
                    ConfirmBatchOrSample.SendKeys(s).SendKeys(Keys.Enter);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
        public void AddClipboardCopyOfBatches()
        {
            if (ListOfBatches != null)
            {
                foreach (ElementScope i in ListOfBatches)
                {
                    var s = FirstBatch["clipboard-copy"];
                    System.Diagnostics.Debug.WriteLine(s);
                    ConfirmBatchOrSample.SendKeys(s).SendKeys(Keys.Enter);
                    System.Threading.Thread.Sleep(500);
                    BDReagentsWorkplacePage bDReagentsWorkplacePage = new BDReagentsWorkplacePage();
                    bDReagentsWorkplacePage.SetUsercode();
                    while (ConfirmManualDrippingField.Exists())
                    {
                        try { ConfirmManualDrippingField.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter); }
                        catch (System.Exception e) { break; }
                    }
                }
            }
        }

        public void ConfimEndOfDripping() // добавлять ШК пользователя пока есть поле "Личный ШК пользователя"
        {
            while (ConfirmManualDrippingField.Exists())
            {
                try { ConfirmManualDrippingField.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter); }
                catch (System.Exception e) { break; }
            }
        }

        public bool AssertNameOfDrippingTest(string _)
        {
            if (DrippingBatchTitle(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }) )
            return true;
            else return false;
        }

        public void ConfirmTestBatchId(string X)
        {
            //System.Threading.Thread.Sleep(500);
            ConfirmTestBatchField.SendKeys(X);
            ConfirmTestBatchField.SendKeys(Keys.Enter);

        }


        public void ConfirmChildBatchId(string Y)
        {
            ConfirmBatchOrSample.SendKeys(Y);
            ConfirmBatchOrSample.SendKeys(Keys.Enter);

        }

        //нажимать на поле до того момента пока не станет активным
        public bool AssertThefieldIsActive()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    ConfirmManualDrippingField.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(250) });
                    return true;
                }
                catch (System.Exception e)
                {
                    count++;
                    if (count > 100) break;
                }
            }
            return false;
        }

        // вводить в поле штрихкод пользователя н-ное количаство раз
        public void ManualDripping(int count)
        {

            for (int i = 0; i < count; i++)
            {
                if (AssertThefieldIsActive()) ConfirmManualDrippingField.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter); 
            }

        }

        // проверить, какой бэтч сейчас раскапывается
        public bool BatchIsDripping(string _)
        {
            if (DrippingBatchTitle(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;

        }



    }
}

