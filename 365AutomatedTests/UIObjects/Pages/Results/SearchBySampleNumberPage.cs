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
    public class SearchBySampleNumberPage:Page
    {
        private ElementScope BySampleNumberTab => Scope.FindXPath("//a[.='По номеру образца']");
        private ElementScope SampleNumberField => Scope.FindId("label_id");
        private ElementScope DateFromField => Scope.FindId("sample_begin");
        private ElementScope DateToField => Scope.FindId("sample_end");
        private ElementScope SearchButton => Scope.FindXPath("//button[.='Найти']");
        private ElementScope NoOrdersMessage => Scope.FindXPath("//span[.='Нет записей для отображения']");

        public void BySampleNumberTabClick()
        {
            BySampleNumberTab.Click();
        }

        public void FillSampleNumberField(string sampleNumber)
        {
            SampleNumberField.SendKeys(sampleNumber);
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

        public void SearchButtonClick()
        {
            SearchButton.Click(new Options { Timeout = TimeSpan.FromSeconds(20) });
        }

        public bool NoOrdersMessageExist()
        {
            return NoOrdersMessage.Exists();
        }
    }
}
