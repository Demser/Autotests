using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365AutomatedTests.Framework.Generic;
using _365AutomatedTests.Models;
using _365AutomatedTests.Steps;
using Coypu;
using OpenQA.Selenium;

namespace _365AutomatedTests.UIObjects.Pages
{
    class CCENDelayConfirmWindow : Page
    {
        public CCENDelayConfirmWindow() : base()
        {
        }
        public ElementScope ConfirmDialogWindow => Scope.FindId("confirm-dialog_wnd_title");
        public ElementScope OkButton => Scope.FindButton("Добавить");


        public bool AssertConfirmDialogWindow()
        {
            if (ConfirmDialogWindow.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(1) }))
                return true;
            else return false;
        }

        public void AddDelayedNomenclature()
        {
            OkButton.Click();
        }



    }
}
