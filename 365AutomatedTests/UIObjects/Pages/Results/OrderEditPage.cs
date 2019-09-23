using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _365AutomatedTests.UIObjects.Pages.Results
{
    public class OrderEditPage:Page
    {
        public OrderEditPage(ElementScope scope) : base(scope)
        {
        }

        private String SaveMessageClassName = "notify-container";

        private ElementScope BirthdateField => Scope.FindXPath("//input[@name='BirthDate']");
        private ElementScope FormattedHeightField => Scope.FindXPath("//*[@id='editOrderForm']/div[2]/div[8]/div[2]/span/span/input[1]");
        private ElementScope HeightField => Scope.FindXPath("//input[@name='Height']");
        private ElementScope FormattedWeigthField => Scope.FindXPath("//*[@id='editOrderForm']/div[2]/div[9]/div[2]/span/span/input[1]");
        private ElementScope WeigthField => Scope.FindXPath("//input[@name='Weigth']");
        private ElementScope SaveButton => Scope.FindXPath("//button[@id='save_btn']");

        public void FillBirthdateField(string birthDate)
        {
            Thread.Sleep(1000);
            Tools.ClearTextFromField(BirthdateField);
            BirthdateField.SendKeys(birthDate);
        }

        public void FillHeightField(string height)
        {
            Thread.Sleep(5000);
            FormattedHeightField.Click();
            Tools.ClearTextFromField(HeightField);
            HeightField.SendKeys(height);
        }

        public void FillWeigthField(string weigth)
        {
            Thread.Sleep(5000);
            FormattedWeigthField.Click();
            Tools.ClearTextFromField(WeigthField);
            WeigthField.SendKeys(weigth);
        }

        public void SaveButtonClick()
        {
            SaveButton.Click();
            Tools.WaitElementByClassName(SaveMessageClassName);
        }
    }
}
