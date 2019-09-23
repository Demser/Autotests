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
    public class SearchByTablePage:Page
    {
        public String OrderItemClassName = "k-master-row";

        private ElementScope ByTableTab => Scope.FindXPath("//div[@id='tabstrip']/ul/li[5]/a");
        private ElementScope DateFromField => Scope.FindId("tv_date_begin");
        private ElementScope DateToField => Scope.FindId("tv_date_end");
        private ElementScope AccountField => Scope.FindXPath("//input[@name='tv_accounts_input']");
        private ElementScope HxidField => Scope.FindXPath("//input[@name='tv_hxid_input']");
        private ElementScope SearchButton => Scope.FindXPath("//button[.='Найти']");
        private ElementScope PDFSaveButton => Scope.FindXPath("//button[.='Скачать PDF']");
        private ElementScope FailedDownloadPDFMessage => Scope.FindXPath("//div[@class='notify-container']");

        public void ByTableTabClick()
        {
            ByTableTab.Click();
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

        public void FillAccountField(string account)
        {
            AccountField.SendKeys(account);
        }

        public void FillHxidField(string hxid)
        {
            HxidField.SendKeys(hxid);
        }

        public void SearchButtonClick()
        {
            SearchButton.Click();
        }

        public void PDFSaveButtonClick()
        {
            PDFSaveButton.Click();
        }

        public bool PDFSaveButtonIsDisabled()
        {
            return PDFSaveButton.Disabled;
        }

        public bool FailedDownloadPDFMessageExists()
        {
            return FailedDownloadPDFMessage.Exists();
        }
    }
}
