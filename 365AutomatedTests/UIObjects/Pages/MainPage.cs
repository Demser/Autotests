using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Remote;
using System.Threading;

namespace _365AutomatedTests.UIObjects.Pages
{
    public class MainPage : Page
    {
        /*public MainPage(string css = ".container") : base(css)
       {
       }*/
       
        #region   // Хэдер
        private ElementScope HelixLogo => Scope.FindXPath("//[@class='navbar-brand']");
        private ElementScope ModulesLink => Scope.FindXPath("//a[.='Модули']");
        private ElementScope HelpLink => Scope.FindXPath("//a[.='Помощь']");
        private ElementScope UserMenuLink => Scope.FindCss("a.btn.btn-lg.dropdown-toggle");
        private ElementScope LogoutLink => Scope.FindXPath("//a[.='Выйти']");
        private ElementScope UserSettingsLink => Scope.FindXPath("//a[.='Настройки пользователя']");       
        private ElementScope ChangePasswordLink => Scope.FindXPath("//a[.='Сменить пароль']");


        #endregion

        #region   // Меню слева
        private ElementScope LeftMenu => Scope.FindId("menu");
        private ElementScope BDIconMenu => Scope.FindXPath("//p[.='BatchDropper']");
        private ElementScope ResultsIconMenu => Scope.FindXPath("//p[.='Результаты']");
        private ElementScope SubMenuItemPreordersJournal => Scope.FindXPath("//p[.='Журнал предварительных заказов']");
        private ElementScope CatalogMenu => Scope.FindXPath("//p[.='Каталог']");
        private ElementScope CatalogCCENItem => Scope.FindXPath("//p[.='Каталог для КЦ (модуль)']");
        #endregion

        #region   // Работа с элементами Хэдера

        public void RefreshOnLogoIcon()
        {       
           HelixLogo.Click();
        }

        public void HelpMenuOpen()
        {
            HelpLink.Click();
        }

        public void UserMenuOpen()
        {
            UserMenuLink.Hover();
        }

        public void Logout()
        {
            UserMenuOpen();
            LogoutLink.Click();
        }

        public void ChangePasswordWindowOpen()
        {
            UserMenuOpen();
            ChangePasswordLink.Click();
        }

        public void OpenUserSettings()
        {
            UserMenuOpen();
            UserSettingsLink.Click();
        }

        #endregion

        public bool AssertLeftMenu()
        {
            if (LeftMenu.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) })) return true;
            else return false;
        }


        public void GoToBatchDropper() {
            BDIconMenu.Click();
            }


        public void GoToPreOrdersJournal()
        {
            CatalogMenu.Click();
            SubMenuItemPreordersJournal.Click();
        }

        public void GoToCCEN()
        {
            CatalogMenu.Click();
            CatalogCCENItem.Click();
        }

    }
}
 