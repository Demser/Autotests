using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _365AutomatedTests.UIObjects.Pages.Results
{
    public class OrderMainPage:Page
    {
        public OrderMainPage(ElementScope scope) : base(scope)
        {
        }

        public String MessageClassName = "notify-container";
        private string ConclusionDeleteButtonXPath = "//button[.='Удалить']";
        private string ConclusionRemoveConfirmDialogTitleXPath = "//span[.='Удаление заключения врача']";

        private ElementScope EditButton => Scope.FindXPath("//button[.='Редактировать']");
        private ElementScope AgeField => Scope.FindXPath("//div[.='Возраст']/../div[2]");
        private ElementScope BirthdateField => Scope.FindXPath("//div[.='Дата рождения']/../div[2]");
        private ElementScope HeightField => Scope.FindXPath("//div[.='Рост']/../div[2]");
        private ElementScope WeigthField => Scope.FindXPath("//div[.='Вес']/../div[2]");
        private ElementScope OrderNumberField => Scope.FindXPath("//div[@id='order-code']");
        private ElementScope ConclusionBlock => Scope.FindXPath("//div[.='Информация о комплексах исследований']");
        private ElementScope Conclusion_02_029_UploadButton => Scope.FindXPath("//td[.='02-029']/../td[4]/label");
        private ElementScope Conclusion_40_039_UploadButton => Scope.FindXPath("//td[.='40-039']/../td[4]/label");
        private ElementScope Conclusion_40_120_UploadButton => Scope.FindXPath("//td[.='40-120']/../td[4]/label");
        private ElementScope Conclusion_02_029 => Scope.FindXPath("//td[.='02-029']/../td[3]/span[@class='title']");
        private ElementScope Conclusion_40_039 => Scope.FindXPath("//td[.='40-039']/../td[3]/span[@class='title']");
        private ElementScope Conclusion_40_120 => Scope.FindXPath("//td[.='40-120']/../td[3]/span[@class='title']");
        private ElementScope Conclusion_02_029_DeleteButton => Scope.FindXPath("//td[.='02-029']/../td[4]/button");
        private ElementScope Conclusion_40_039_DeleteButton => Scope.FindXPath("//td[.='40-039']/../td[4]/button");
        private ElementScope Conclusion_40_120_DeleteButton => Scope.FindXPath("//td[.='40-120']/../td[4]/button");
        private ElementScope ConclusionRemoveConfirmDialogTitle => Scope.FindXPath(ConclusionRemoveConfirmDialogTitleXPath);
        private ElementScope ConfirmOkButton => Scope.FindXPath("//button[@id='confirm-dlg-ok']");
        private ElementScope ConfirmCancelButton => Scope.FindXPath("//button[@id='confirm-dlg-cancel']");
        private ElementScope UploadConclusionMessage => Scope.FindXPath("//div[@class='notify-container']/span[@data-notify='message']");

        public void EditButtonClick()
        {
            EditButton.Click(new Options() { Timeout = TimeSpan.FromSeconds(10) });
        }

        public string getFactAge()
        {
            return AgeField.Text;
        }

        public string getBirthdate()
        {
            return BirthdateField.Text;
        }

        public string getHeight()
        {
            return HeightField.Text;
        }

        public string getWeigth()
        {
            return WeigthField.Text;
        }

        public string getOrderNumber()
        {
            return OrderNumberField.Text;
        }

        public bool IsVisibleBirthdate()
        {
            return BirthdateField.Exists();
        }

        public bool IsVisibleHeight()
        {
            return HeightField.Exists();
        }

        public bool IsVisibleWeigth()
        {
            return WeigthField.Exists();
        }

        public bool ConclusionBlockExist()
        {
            return ConclusionBlock.Exists();
        }

        public void Conclusion_Upload(string hxid, string conclusionFileName)
        {
            switch (hxid)
            {
                case "02-029":
                    Conclusion_02_029_UploadButton.Click();
                    break;
                case "40-039":
                    Conclusion_40_039_UploadButton.Click();
                    break;
                case "40-120":
                    Conclusion_40_120_UploadButton.Click();
                    break;
            }
            Thread.Sleep(1000);
            SendKeys.SendWait(@Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "UploadFiles\\" + conclusionFileName));
            SendKeys.SendWait(@"{Enter}");
            if (!UploadConclusionMessage.Exists())
            {
                Tools.WaitClickableElementByXPath(ConclusionDeleteButtonXPath);
            }
        }

        public bool ConclusionExist(string hxid)
        {
            bool res = false;
            switch (hxid)
            {
                case "02-029":
                    res = Conclusion_02_029.Exists();
                    break;
                case "40-039":
                    res = Conclusion_40_039.Exists();
                    break;
                case "40-120":
                    res = Conclusion_40_120.Exists();
                    break;
                default:
                    break;
            }
            return res;
        }

        public void Conclusion_Download(string hxid)
        {
            switch (hxid)
            {
                case "02-029":
                    Conclusion_02_029.Click();
                    break;
                case "40-039":
                    Conclusion_40_039.Click();
                    break;
                case "40-120":
                    Conclusion_40_120.Click();
                    break;
            }
            Thread.Sleep(3000);
        }

        public int getCounclusionCount()
        {
            return Tools.getCountElementsByXPath(ConclusionDeleteButtonXPath);
        }

        public void Conclusion_Delete(string hxid, string conclusionFileName)
        {
            switch (hxid)
            {
                case "02-029":
                    Conclusion_02_029_DeleteButton.Click();
                    break;
                case "40-039":
                    Conclusion_40_039_DeleteButton.Click();
                    break;
                case "40-120":
                    Conclusion_40_120_DeleteButton.Click();
                    break;
            }
            Tools.WaitVisibleElementByXPath(ConclusionRemoveConfirmDialogTitleXPath);
        }

        public bool ConclusionRemoveConfirmDialogTitleExist()
        {
            return ConclusionRemoveConfirmDialogTitle.Exists();
        }

        public void Confirm()
        {
            ConfirmOkButton.Click();
        }

        public void NoConfirm()
        {
            ConfirmCancelButton.Click();
        }

        public string getUploadConclusionMessage()
        {
            return UploadConclusionMessage.Text;
        }
    }
}
