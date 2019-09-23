using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;

namespace _365AutomatedTests.UIObjects.Pages
{
    class BDPlanshetPositionPage : Page
    {
        public BDPlanshetPositionPage() : base()
        {
        }
        public ElementScope ConfirmBatchWindowTitle => Scope.FindId("authenticationWindow_wnd_title");
        private ElementScope PrintBarcodeIcon => Scope.FindXPath("//span[.='PrintBarcode']");
        private ElementScope RightPositionTitle => Scope.FindXPath("//h3[.='Установите планшет справа']");
        private ElementScope LeftPositionTitle => Scope.FindXPath("//h3[.='Установите планшет слева']");
        private ElementScope SetUsercodeField => Scope.FindId("authenticateBatchId");

        // Проверка что окно отобразилось
        public bool AssertConfirmBatchWindow()
        {
            if (ConfirmBatchWindowTitle.Exists())
            {
                return true;
            }
            else return false;
        }

        //Подтверждение штрихкода пользователя
        public void ConfirmUsercodeInPlanshetPositionPage()
        {
            SetUsercodeField.WaitForClickability();
            SetUsercodeField.SendKeys(Config.UserBarCode);
            SetUsercodeField.SendKeys(Keys.Enter);
        }
       
    }
    
}
