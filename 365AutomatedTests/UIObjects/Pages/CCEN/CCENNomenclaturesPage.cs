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
    class CCENNomenclaturesPage : Page
    {
        public CCENNomenclaturesPage() : base()
        {
        }
        public CCENNomenclaturesPage(ElementScope scope) : base(scope)
        {
        }
        public ElementScope NomenclaturesTab => Scope.FindId("ccen-main-tabs-hxidlist");
        public ElementScope TextWhenCityIsNotChoose => Scope.FindXPath("//p[contains(text(),'Необходимо указать город для получения списка исследований')]");
        public ElementScope SearchInput => Scope.FindXPath("//*[@id='kToolbar']/div/input");
        public ElementScope AddSomeNomenctatureToCartField(string _) => Scope.FindId($"ccen-hxidlist-hxid-{_}-add-btn");
        public ElementScope InfoOfSomeNomenclature(string _) => Scope.FindId($"ccen-hxidlist-hxid-{_}-info-btn");
        public ElementScope NameOfSomeNomenclature(string _) => Scope.FindId($"ccen-hxidlist-hxid-{_}-caption-value");
        ElementScope NomenclatureTest => Scope.FindId("ccen-hxidlist-nomenclatures-grid");//грид с номенклатурами
        ElementScope LoaderIsActive => Scope.FindXPath("//*[@id='ccen-main-loader'and @class='page-content__loader-cover']");
        ElementScope LoaderIsInactive => Scope.FindXPath("//*[@id='ccen-main-loader'and @class='page-content__loader-cover ng-hide']");
        public ElementScope PersonalDataTab => Scope.FindId("ccen-main-tabs-personal");
        public ElementScope CanceledHXIDMessage => Scope.FindXPath("//span[.='Это исследование временно отменено']");

        public void GoToHXIDSTab()
        {
            System.Threading.Thread.Sleep(100);
            NomenclaturesTab.Click();
        }

        public void SearchSomeHXID(string _)
        {
            SearchInput.WaitForClickability();
            SearchInput.Click();
            SearchInput.SendKeys(_);
            SearchInput.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(2000);
        }
        public void AddSomeNomenctatureToCart(string _)
        {
            AddSomeNomenctatureToCartField(_).WaitForVisibility();
            AddSomeNomenctatureToCartField(_).Click(); // нажать на значок добавления определенной номенклатуры в корзину
            AddSomeNomenctatureToCartField(_).WaitForVisibility(); // подождать, пока исчезнет лоадер(разблокируется значок добавления)
            //PersonalDataTab.WaitForClickability(); // подождать, пока исчезнет лоадер(разблокируется вкладка персональных данных)
            SearchInput.SendKeys(Keys.Control + "a");
            SearchInput.SendKeys(Keys.Delete);
        }

        //public bool AssertMessageNomenclatureWas

        public bool AssertNomenclatureWasCanceled()
        {
            if (CanceledHXIDMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(1) }))
                return true;
            else return false;
        }



    }
}
