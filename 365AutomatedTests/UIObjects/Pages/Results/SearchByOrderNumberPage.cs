using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _365AutomatedTests.UIObjects.Pages.Results
{
    public class SearchByOrderNumberPage:Page
    {
        public String OrderItemClassName = "k-master-row";
        public String MessageClassName = "notify-container";

        private ElementScope ByOrderNumberTab => Scope.FindXPath("//a[.='По номеру заказа']");
        private ElementScope OrderNumberField => Scope.FindId("order_code");
        private ElementScope SearchButton => Scope.FindXPath("//button[.='Найти']");
        private ElementScope OrderItem => Scope.FindXPath("//tbody[@role='rowgroup']");
        private ElementScope CountOfOrders => Scope.FindXPath("//div[@id='order_grid']/div[@data-role='pager']/span");
        private ElementScope LogoutName => Scope.FindXPath("//div[@class='btn-group']");
        private ElementScope Logout => Scope.FindXPath("//a[.='Выйти']");
        private ElementScope CreatedOnDate => Scope.FindXPath("//tbody[@role='rowgroup']/tr[@class='k-master-row']/td[3]");
        private ElementScope OrderNumber => Scope.FindXPath("//tbody[@role='rowgroup']/tr[@class='k-master-row']/td[4]");
        private ElementScope FIO => Scope.FindXPath("//tbody[@role='rowgroup']/tr[@class='k-master-row']/td[5]");
        private ElementScope EmailAddress => Scope.FindXPath("//tbody[@role='rowgroup']/tr[@class='k-master-row']/td[6]");
        private ElementScope CellPhoneNumber => Scope.FindXPath("//tbody[@role='rowgroup']/tr[@class='k-master-row']/td[7]");
        private ElementScope EmptyOrderNumberMessage => Scope.FindXPath("//div[@class='notify-container']/span[@data-notify='message']");
        private ElementScope OrderDetailsIconShow => Scope.FindXPath("//tbody[@role='rowgroup']/tr[@class='k-master-row']/td[1]/a[@class='k-icon k-plus']");
        private ElementScope OrderDetailsIconHide => Scope.FindXPath("//tbody[@role='rowgroup']/tr[@class='k-master-row']/td[1]/a[@class='k-icon k-minus']");
        private ElementScope OrderDetails => Scope.FindXPath("//tr[@class='k-detail-row']");

        public void ByOrderNumberTabClick()
        {
            ByOrderNumberTab.Click();
        }

        public void FillOrderNumberField(string orderNumber)
        {
            OrderNumberField.SendKeys(orderNumber);
        }

        public void SearchButtonClick()
        {
            SearchButton.Click(new Options { Timeout = TimeSpan.FromSeconds(20) });
        }

        public void OrderItemClick()
        {
            OrderItem.Click(new Options { WaitBeforeClick = TimeSpan.FromSeconds(1) });
        }

        public string getCountOfOrders()
        {
            return CountOfOrders.Text.Split(' ').Last();
        }

        public bool IsLogged()
        {
            return LogoutName.Exists();
        }

        public void LogoutClick()
        {
            Tools.WaitInvisibilityOfElementByClassName(MessageClassName);
            LogoutName.Click();
            Logout.Click(new Options { WaitBeforeClick = TimeSpan.FromSeconds(1) });
        }

        public string getCreatedOnDate()
        {
            return CreatedOnDate.Text;
        }

        public string getOrderNumber()
        {
            return OrderNumber.Text;
        }

        public string getFIO()
        {
            return FIO.Text;
        }

        public string getEmailAddress()
        {
            return EmailAddress.Text;
        }

        public string getCellPhoneNumber()
        {
            return CellPhoneNumber.Text;
        }

        public string getEmptyOrderNumberMessage()
        {
            return EmptyOrderNumberMessage.Text;
        }

        public void OrderDetailsIconShowClick()
        {
            OrderDetailsIconShow.Click();
        }

        public void OrderDetailsIconHideClick()
        {
            OrderDetailsIconHide.Click();
        }

        public bool OrderDetailsExist()
        {
            return OrderDetails.Exists();
        }
    }
}
