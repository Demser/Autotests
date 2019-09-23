using _365AutomatedTests.Framework.Generic;
using Coypu;

namespace _365AutomatedTests.UIObjects.Pages
{
    class TestLoginWrongPasswordPage : Page
    {
        public TestLoginWrongPasswordPage() : base() { }

        private ElementScope WrongPasswordTitle => Scope.FindXPath("//li[.='Неправильные имя пользователя или пароль']");


        public bool AssertWrongPassword()
        {
            if (WrongPasswordTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(1) }))
            {
                return true;
            }
            else return false;
        }


    }
}
