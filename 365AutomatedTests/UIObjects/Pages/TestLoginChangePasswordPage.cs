using _365AutomatedTests.Framework.Generic;
using Coypu;

namespace _365AutomatedTests.UIObjects.Pages
{
    class TestLoginChangePasswordPage : Page
    {
        public TestLoginChangePasswordPage() : base() { }

        private ElementScope NewPasswordField => Scope.FindXPath("//input[@name='ChangePasswordModel.ConfirmPassword']");


        public bool AssertNewPasswordField()
        {
            if (NewPasswordField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(1) }))
            {
                return true;
            }
            else return false;
        }
    }
}
