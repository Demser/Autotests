using _365AutomatedTests.Framework.Generic;
using Coypu;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;

namespace _365AutomatedTests.UIObjects.Pages
{
    using OpenQA.Selenium;
    using System.Diagnostics;

    [Binding]
    class BDPositiveControlsWorkplacePage : Page
    {
        public BDPositiveControlsWorkplacePage() : base()
        {
        }
        private ElementScope SetTestBatchField => Scope.FindXPath("//input[@placeholder='Штрих-код бэтча']");
        private ElementScope ConfirmUsercodeInLocation => Scope.FindXPath("//input[@id='confirmBatchLocationId']", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        //private ElementScope ConfirmUsercodeInLocation => Scope.FindId("confirmBatchLocationId", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        private ElementScope ConfirmControlMaterialBarcodeField => Scope.FindXPath("//input[@placeholder='Штрих-код контрольного материала']");

        private ElementScope ConfirmUserCodeForContinuingPositiveControlsDripping => Scope.FindXPath("//input[@placeholder='Личный штрих-код пользователя']", new Options() { Timeout = TimeSpan.FromSeconds(10) });

        private ElementScope ConfirmUsercodeForEndingPositiveControlDripping 
            => Scope.FindXPath("//input[@placeholder='Личный штрих-код пользователя для завершения раскапывания']", new Options() { Timeout = TimeSpan.FromSeconds(10) });

        private ElementScope PostitiveControlFirstItem => Scope.FindXPath("(//p[contains(@ng-style,'cell') and contains(@style,'black')])[1]");


        public void DrippingPositiveControls()
        {
            if (ConfirmControlMaterialBarcodeField.Exists())
            {
                do
                {
                    var codeOfPC = PostitiveControlFirstItem["clipboard-copy"];
                    Debug.WriteLine("Буду раскапывать: " + codeOfPC);
                    ConfirmControlMaterialBarcodeField.WaitForAvailability();
                    ConfirmControlMaterialBarcodeField.SendKeys(codeOfPC);
                    ConfirmControlMaterialBarcodeField.SendKeys(Keys.Enter);
                    System.Threading.Thread.Sleep(600);
                    Debug.WriteLine("Раскапал: " + codeOfPC);
                }
                while (PostitiveControlFirstItem.Exists());
            }
            ConfirmUsercodeForEndingPositiveControlDripping.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);







           // while (ConfirmControlMaterialBarcodeField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) // пока существует поле для ввода ПК
           // {
           //     Debug.WriteLine("Поле для ввода ПК обнаружено.");
            //    while (PostitiveControlFirstItem.Exists()) // пока существуют ПК
             //   {
              //      var codeOfPC = PostitiveControlFirstItem["clipboard-copy"];
               //     Debug.WriteLine("Буду раскапывать: "+ codeOfPC);
                //    ConfirmControlMaterialBarcodeField.WaitForAvailability();
                 //   ConfirmControlMaterialBarcodeField.SendKeys(codeOfPC);
                  //  ConfirmControlMaterialBarcodeField.SendKeys(Keys.Enter);
                   // Debug.WriteLine("Раскапал: " + codeOfPC);
               // }
           // }
           // Debug.WriteLine("Поле для ввода ПК исчезло. Теперь вводим ШКП");
           // ConfirmUsercodeForEndingPositiveControlDripping.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);
        }

        //---something old...
        public void SetTestBatch(string id)
        {
            SetTestBatchField.Click(new Options() { WaitBeforeClick = TimeSpan.FromSeconds(1) }).SendKeys(id);
            SetTestBatchField.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(1000);
        }

        public void ConfirmUsercode()
        {
            ConfirmUsercodeInLocation.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);
        }

        public void ConfirmControlMaterialBarcode(int count, string idCM)
        {
            for (int i = 1; i <= count; i++) {
                ConfirmControlMaterialBarcodeField.Click(new Options() { WaitBeforeClick = TimeSpan.FromSeconds(1) });
                ConfirmControlMaterialBarcodeField.SendKeys(idCM).SendKeys(Keys.Enter);
            }

        }

        public void ConfirmUserCodeForContinuingDripping()
        {
                ConfirmUserCodeForContinuingPositiveControlsDripping.Click(new Options() { WaitBeforeClick = TimeSpan.FromSeconds(2) });
                ConfirmUserCodeForContinuingPositiveControlsDripping.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);

        }
        public void ConfirmUsercodeForEndingDripping() {
            ConfirmUsercodeForEndingPositiveControlDripping.Click(new Options() { WaitBeforeClick = TimeSpan.FromSeconds(2) });
            ConfirmUsercodeForEndingPositiveControlDripping.SendKeys(Config.UserBarCode).SendKeys(Keys.Enter);

        }


    }
}
