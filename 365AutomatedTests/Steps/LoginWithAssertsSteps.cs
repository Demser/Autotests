using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using System.Collections.Generic;
using static NUnit.Framework.Assert;
using NUnit.Framework;
using Coypu;
using _365AutomatedTests.Steps;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class LoginWithAssertsSteps
    {
        [Given(@"I login as ""(.*)"" and password")]
        public void GivenILoginAsAndPassword(string login)
        {
            NewLogin365Page testLogin365Page = new NewLogin365Page(); // стандартная форма авторизации
            testLogin365Page.LoginAsUser(login);
            TestLoginChangePasswordPage testLoginChangePasswordPage = new TestLoginChangePasswordPage(); // Форма смены пароля 
            IsFalse(testLoginChangePasswordPage.AssertNewPasswordField(), "Пароль устарел! Смените пароль для этого пользователя в администрировании 365, а затем пропишите его в файле App.config в разделе UserSettings. Рекомендуемый пароль: Autotests+инкремент, например Autotests5");
            TestLoginWrongPasswordPage testWrongPasswordPage = new TestLoginWrongPasswordPage(); // Форма неправильного пароля
            IsFalse(testWrongPasswordPage.AssertWrongPassword(),"Вы ввели неправильный пароль. Проверьте правильность пароля");
            Console.WriteLine("Вход на сайт осуществлен");
        }
        
        [Then(@"I see main page")]
        public void ThenISeeMainPage()
        {
            ScenarioContext.Current.Pending();
        }
    }
}