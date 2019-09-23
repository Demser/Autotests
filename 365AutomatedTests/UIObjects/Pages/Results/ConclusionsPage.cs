using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.UIObjects.Pages.Results
{
    class ConclusionsPage:Page
    {
        private string ConclusionItemsXpath = "//tbody[@role='rowgroup']";

        private ElementScope OrderNumberField => Scope.FindId("OrderCode");
        private ElementScope DateFromField => Scope.FindId("From");
        private ElementScope DateToField => Scope.FindId("To");
        private ElementScope SearchButton => Scope.FindXPath("//button[.='Найти']");
        private ElementScope ConclusionItems => Scope.FindXPath(ConclusionItemsXpath);

        public void FillOrderNumberField(string orderNumber)
        {
            OrderNumberField.SendKeys(orderNumber);
        }

        public void FillDateFromField(string dateFrom)
        {
            Tools.ClearTextFromField(DateFromField);
            DateFromField.SendKeys(dateFrom);
        }

        public void FillDateToField(string dateTo)
        {
            Tools.ClearTextFromField(DateToField);
            DateToField.SendKeys(dateTo);
        }

        public void ConclusionItemsClick()
        {
            Tools.WaitClickableElementByXPath(ConclusionItemsXpath);
            ConclusionItems.Click();
        }
    }
}
