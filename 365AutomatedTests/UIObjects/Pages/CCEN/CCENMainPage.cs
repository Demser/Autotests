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
    class CCENMainPage : Page
    {
        public CCENMainPage() : base()
        {
        }

        public ElementScope BreadcrumbsOfCatalog => Scope.FindXPath("//li[.='Каталог']");
        public ElementScope PageOfCCENDiv => Scope.FindId("page-content"); //див с контентом страницы
        public ElementScope TitleOfCCENModule => Scope.FindId("ccen-header-title");
        public ElementScope DebugModeCheckbox => Scope.FindId("ccen-header-is-develop-checkbox");
        public ElementScope OneMoreMobilePreorderChekbox => Scope.FindId("ccen-header-is-same-mobile-checkbox");
        public ElementScope SaveButtonOfCCENModule => Scope.FindId("ccen-save-preorder-btn");
        public ElementScope CancelButtonOfCCENModule => Scope.FindId("ccen-cancel-preorder-btn");
        public ElementScope PreorderNumberSpan => Scope.FindId("ccen-result-modal-full-number");
        public ElementScope NumberOfPreorderSpan => Scope.FindId("ccen-result-modal-full-number");
        public ElementScope ClosePreorderNumberWindowsIcon => Scope.FindId("ccen-result-modal-close-btn");

        public string NumberOfPreorder => NumberOfPreorderSpan.Text;



        public bool AssertGetPreorderNumber()
        {
            if (PreorderNumberSpan.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) }))
            {
                return true;
            }
            else return false;
        }

        public void GetPreorderNumber()
        {
            
        }

        public void ClosedPreorderNumberWindow()
        {
            ClosePreorderNumberWindowsIcon.Click();
        }



    }



}

