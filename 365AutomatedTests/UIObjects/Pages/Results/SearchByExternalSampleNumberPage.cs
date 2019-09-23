using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.UIObjects.Pages.Results
{
    public class SearchByExternalSampleNumberPage:Page
    {
        private ElementScope ByExternalSampleNumberTab => Scope.FindXPath("//a[.='По стороннему номеру образца']");
        private ElementScope ExternalSampleNumberField => Scope.FindId("foreign_label_id");
        private ElementScope DateFromField => Scope.FindId("for_sample_begin");
        private ElementScope DateToField => Scope.FindId("for_sample_end");
        private ElementScope EmptyExternalSampleNumberMessage => Scope.FindXPath("//div[@class='notify-container']/span[@data-notify='message']");

        public void ByExternalSampleNumberTabClick()
        {
            ByExternalSampleNumberTab.Click();
        }

        public void FillExternalSampleNumberField(string externalSampleNumber)
        {
            ExternalSampleNumberField.SendKeys(externalSampleNumber);
        }

        public void FillDateFromField(string dateFrom)
        {
            DateFromField.SendKeys(dateFrom);
        }

        public void FillDateToField(string dateTo)
        {
            DateToField.SendKeys(dateTo);
        }
        public string getEmptyExternalSampleNumberMessage()
        {
            return EmptyExternalSampleNumberMessage.Text;
        }
    }
}
