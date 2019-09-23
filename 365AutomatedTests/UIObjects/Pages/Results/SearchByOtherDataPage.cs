using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.UIObjects.Pages.Results
{
    public class SearchByOtherDataPage:Page
    {
        private ElementScope DateFromField => Scope.FindId("date_begin");
        private ElementScope DateToField => Scope.FindId("date_end");
        private ElementScope CityField => Scope.FindXPath("//input[@placeholder='Введите город']");
        private ElementScope AccountField => Scope.FindXPath("//input[@placeholder='Введите место забора']");
        private ElementScope DoctorField => Scope.FindXPath("//input[@placeholder='Введите фамилию врача']");
        private ElementScope SecondNameField => Scope.FindId("lastname");
        private ElementScope FirstNameField => Scope.FindId("firstname");
        private ElementScope PatronymicField => Scope.FindId("middlename");
        private ElementScope BirthDateField => Scope.FindId("birthDay");
        private ElementScope CustomerCardField => Scope.FindId("service_card_number");
        private ElementScope EmailField => Scope.FindId("Email");
        private ElementScope PhoneNumberField => Scope.FindId("Phone");
        private ElementScope OrderStatusField => Scope.FindXPath("//input[@aria-owns='resultStatuses_taglist resultStatuses_listbox']");
        private ElementScope HxidField => Scope.FindXPath("//input[@aria-owns='Hxides_taglist Hxides_listbox']");

        public void FillDateFromField(string dateFrom)
        {
            DateFromField.SendKeys(dateFrom);
        }

        public void FillDateToField(string dateTo)
        {
            DateToField.SendKeys(dateTo);
        }

        public void FillCityField(string city)
        {
            CityField.SendKeys(city);
        }

        public void FillAccountField(string account)
        {
            AccountField.SendKeys(account);
        }

        public void FillDoctorField(string doctor)
        {
            DoctorField.SendKeys(doctor);
        }

        public void FillSecondNameField(string secondName)
        {
            SecondNameField.SendKeys(secondName);
        }

        public void FillFirstNameField(string firstName)
        {
            FirstNameField.SendKeys(firstName);
        }

        public void FillPatronymicField(string patronymic)
        {
            PatronymicField.SendKeys(patronymic);
        }

        public void FillBirthDateField(string birthDate)
        {
            BirthDateField.SendKeys(birthDate);
        }

        public void FillCustomerCardField(string customerCard)
        {
            CustomerCardField.SendKeys(customerCard);
        }

        public void FillEmailField(string email)
        {
            EmailField.SendKeys(email);
        }

        public void FillPhoneNumberField(string phoneNumber)
        {
            PhoneNumberField.SendKeys(phoneNumber);
        }

        public void FillOrderStatusField(string orderStatus)
        {
            OrderStatusField.SendKeys(orderStatus);
        }

        public void FillHxidField(string hxid)
        {
            HxidField.SendKeys(hxid);
        }
    }
}
