using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;

namespace _365AutomatedTests.UIObjects.Pages
{
    class NewLogin365Page : Page
    {
        public NewLogin365Page() : base() { }

        private ElementScope LoginField => Scope.FindId("UserName");
        private ElementScope PasswordField => Scope.FindId("Password");
        private ElementScope ButtonLogin => Scope.FindButton("Вход");

        private ElementScope NewPasswordField => Scope.FindXPath("//input[@name='ChangePasswordModel.ConfirmPassword']");


        public void LoginAsUser(string login)
        {
            LoginField.SendKeys(login, new Options() { Timeout = TimeSpan.FromSeconds(10) });
            PasswordField.SendKeys(Config.UserPassword);
            ButtonLogin.Click();
            //System.Threading.Thread.Sleep(1000); // будем менять на ожидания id menu
        }

        public bool AssertNewPasswordField()
        {
            if (NewPasswordField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(80) }))
            {
                return true;
            }
            else return false;
        }



        
    }



}
