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
    class CCENPersonalDataPage : Page
    {
        public CCENPersonalDataPage() : base()
        {
        }
        public CCENPersonalDataPage(ElementScope scope) : base(scope)
        {
        }
        public ElementScope PersonalDataTab => Scope.FindId("ccen-main-tabs-personal");
        public ElementScope PersonalDataBlock => Scope.FindId("ccen-personal-data");
        public ElementScope SecondNameField => Scope.FindId("SecondName");
        public ElementScope FirstNameField => Scope.FindId("FirstName");
        public ElementScope MiddleNameField => Scope.FindId("MiddleName");
        public ElementScope GenderMenLabel => Scope.FindId("ccen-personal-data-gender-input-M");
        public ElementScope GenderWomenLabel => Scope.FindId("ccen-personal-data-gender-input-W");
        public ElementScope GenderUnknownLabel => Scope.FindId("ccen-personal-data-gender-input-U");
        public ElementScope BirthDayInput => Scope.FindId("BirthDay");
        public string BirthDayInputValue => BirthDayInput.Value;

        //public string Time = "15/01/2018";
        public ElementScope AgeInput => Scope.FindId("ccen-personal-data-age-in-month-input");
        public ElementScope GenderButton(char G) => Scope.FindId($"ccen-personal-data-gender-input-{G}");
        public ElementScope EmailField => Scope.FindId("Mail");
        public ElementScope EmailCheckbox => Scope.FindId("SendMail");
        public ElementScope EmailCheckboxIsChecked => Scope.FindXPath("//input[@id='SendMail' and @class='ng-pristine ng-valid ng-touched ng-not-empty' ]");
        public ElementScope EmailCheckboxIsCheckedAndHover => Scope.FindXPath("//input[@id='SendMail' and @class='ng-valid ng-touched ng-dirty ng-valid-parse ng-not-empty']");
        public ElementScope MobilePhoneField => Scope.FindId("CellPhone");
        public ElementScope MobileCheckbox => Scope.FindId("SendSMS");
        public ElementScope MobileCheckboxIsChecked => Scope.FindXPath("//input[@id='SendSMS' and @class = 'ng-pristine ng-valid ng-touched ng-not-empty' ]");
        public ElementScope CommentsField => Scope.FindId("Comments");
        public ElementScope MobileBlock => Scope.FindId("ccen-personal-mobile-header");

        public ElementScope InsuranceFieldsBlock => Scope.FindId("ccen-personal-insurance");//Блок с полями для страхововй
        public ElementScope NumberOfPolicyField => Scope.FindId("ccen-personal-insurance-number-input"); // Номер полиса
        public ElementScope DispatcherField => Scope.FindId("ccen-personal-insurance-operator-input"); // Диспетчер СК
        public ElementScope ValidityField => Scope.FindId("ccen-personal-insurance-end-data-input"); // Срок действия полиса

        public string NumberOfPolicyFieldValue => NumberOfPolicyField.Value;
        public string DispatcherFieldValue => DispatcherField.Value;
        public string ValidityFieldValue => ValidityField.Value;

        public ElementScope TooltipPolicyNumberField => Scope.FindXPath("//span[contains(text(),'Укажите номер полиса')]");
        public ElementScope TooltipDispatcherField => Scope.FindXPath("//span[contains(text(),'Укажите диспетчера СК')]");
        public ElementScope TooltipValidityExpired => Scope.FindXPath("//span[contains(text(),'Срок действия полиса истек более года назад')]");
        public ElementScope TooltipValidityField => Scope.FindXPath("//span[contains(text(),'Укажите срок действия')]");
        public ElementScope TooltipInvalidDate => Scope.FindXPath("//span[contains(text(),'Укажите дату в формате DD')]");
        public ElementScope TooltipFutureDate => Scope.FindXPath("//span[contains(text(),'Пациент еще не родился')]");
        public ElementScope TooltipOldAge => Scope.FindXPath("//span[contains(text(),'Пациенты старше 150 лет не обслуживаются')]");
        public ElementScope TooltipInvalidEmail => Scope.FindXPath("//span[contains(text(),'Укажите корректный e-mail')]");
        public ElementScope TooltipInvalidPhone => Scope.FindXPath("//span[contains(text(),'Укажите корректный номер моб. телефона')]");

        public ElementScope MessаgePopup(string _) => Scope.FindXPath($"//span[contains(text(),'{_}')]");
        public void GoToPersonalDataTab()
        {
            PersonalDataTab.Click();

        }

        public bool MessageIsShown(string _)
        {
            if (MessаgePopup(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(3) })) return true;
            else return false;
        }

        public void SetFIO(string firstname,string middlename,string secondname,string NoB)
        {
            FirstNameField.Click().SendKeys(firstname);
            MiddleNameField.Click().SendKeys(middlename);
            SecondNameField.Click().SendKeys(secondname);
            if (BirthDayInputValue != "")
            {
                BirthDayInput.Click().SendKeys(Keys.Control + "a").SendKeys(Keys.Delete).SendKeys(Keys.Tab);
            }
                BirthDayInput.Click().SendKeys(NoB);
            System.Threading.Thread.Sleep(100);
            BirthDayInput.Click().SendKeys(Keys.Tab);
        }

        public void SetGender(char genderm)
        {
            GenderButton(genderm).Click();
            //System.Threading.Thread.Sleep(2000); // добавить если нужно
        }

        public void SetInsurancePatientData(string numberOfPolicy, string dispatcher, string validity)
        {
            NumberOfPolicyField.SendKeys(numberOfPolicy);
            DispatcherField.SendKeys(dispatcher);
            ValidityField.Click();
            System.Threading.Thread.Sleep(100);
            ValidityField.SendKeys(validity);
            System.Threading.Thread.Sleep(100);
        }

        //для чеклиста:
        public void ClearAndSetValueDate(string value)
        {
            BirthDayInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            BirthDayInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            BirthDayInput.SendKeys(value).SendKeys(Keys.Tab);
        }

        public void ClearAndSetPolicyDate(string value)
        {
            ValidityField.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            ValidityField.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            ValidityField.SendKeys(value).SendKeys(Keys.Tab);
        }

        public bool AssertExpiredValidity()
        {
            if (TooltipValidityExpired.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertInvalidTooltip()
        {
            if (TooltipInvalidDate.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertFutureTooltip()
        {
            if (TooltipFutureDate.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertSoOldAgeTooltip(string value)
        {
            AgeInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            AgeInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            AgeInput.SendKeys(value).SendKeys(Keys.Tab);
            if (TooltipOldAge.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }


        public void ClearAndSetValueAge(string value)
        {
            AgeInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            AgeInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            AgeInput.SendKeys(value).SendKeys(Keys.Tab);
        }

        public bool AssertTodayIsBirthday()
        {
            string Now = DateTime.Now.ToUniversalTime().ToString(@"dd/MM/yyyy");

            if (BirthDayInput.Value == Now) return true;
            else return false;
        }

        public bool AssertPolicyNumberFieldIsRequired()
        {         
            if (TooltipPolicyNumberField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertDispatcherFieldIsRequired()
        {
            if (TooltipDispatcherField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertValidityFieldIsRequired()
        {
            if (TooltipValidityField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }



        public bool AssertSomeYearsAge(int value)
        {
            AgeInput.SendKeys(Keys.Control + "a");
            AgeInput.SendKeys(Keys.Delete);
            string AgeToString = value.ToString();
            AgeInput.SendKeys(AgeToString).SendKeys(Keys.Tab);
            string Now = DateTime.Now.ToUniversalTime().ToString(@"dd/MM/yyyy");
            string SomeYears = DateTime.Now.AddYears(-value).ToString(@"dd/MM/yyyy");
            if (BirthDayInput.Value == SomeYears) return true;
            else return false;
        }

        public bool AssertFullBirthday(int value)
        {
            AgeInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            AgeInput.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            string AgeToString = value.ToString();
            AgeInput.SendKeys(AgeToString).SendKeys(Keys.Tab);
            string SomeYears = "";
            if (BirthDayInput.Value == SomeYears) return true;
            else return false;
        }

        public bool AssertInvalidEmailTooltip(string value) 
        {
            EmailField.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            EmailField.SendKeys(value).SendKeys(Keys.Tab);
            if (TooltipInvalidEmail.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertEmailCheckboxIsUnchecked() // проверка что чкб не активен
        {
            if (EmailCheckbox.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertValidEmailAndSetCheckbox(string value)//ввод правильного имейла и проверка, что чкб установлен
        {
            EmailField.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            EmailField.SendKeys(value).SendKeys(Keys.Tab);
            if (EmailCheckboxIsChecked.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }



        public bool AssertInvalidMobileTooltip(string value)
        {
            MobilePhoneField.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            MobilePhoneField.SendKeys(value).SendKeys(Keys.Tab);
            if (TooltipInvalidPhone.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertMobileCheckboxIsUnchecked() // проверка что чкб не активен
        {
            if (MobileCheckbox.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertValidMobileAndSetCheckbox(string value)//ввод правильного имейла и проверка, что чкб установлен
        {
            MobilePhoneField.SendKeys(Keys.Control + "a").SendKeys(Keys.Delete);
            MobilePhoneField.SendKeys(value).SendKeys(Keys.Tab);
            if (MobileCheckboxIsChecked.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertInsuranceFieldsBlock()
        {
            if (InsuranceFieldsBlock.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertMobileBlock()
        {
            if (MobileBlock.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertNumberOfPolicyFieldValueIsNotEmpty() // Проверка, что поле номер полиса не пустое
        {
            Console.WriteLine(NumberOfPolicyFieldValue);
            if (NumberOfPolicyFieldValue != "") return true;
            else return false;
        }
        public bool AssertDispatcherFieldValueIsNotEmpty() // Проверка, что поле Диспетчер не пустое
        {
            Console.WriteLine(DispatcherFieldValue);
            if (DispatcherFieldValue != "") return true;
            else return false;
        }
        public bool AssertValidityFieldValueIsNotEmpty() // Проверка, что поле Срок действия полиса не пустое
        {
            Console.WriteLine(ValidityFieldValue);
            if (ValidityFieldValue != "") return true;
            else return false;
        }

    }






}
