using TechTalk.SpecFlow;
using _365AutomatedTests.UIObjects.Pages;
using static NUnit.Framework.Assert;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CCENPersonalDataChecklistSteps
    {
        [Then(@"I check that save button is inactive")]
        public void ThenICheckThatSaveButtonIsInactive()
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            IsTrue(cCENCartPage.SaveButtonIsInactive(),"Кнопка сохранения предзаказа активна!");
        }
        
        [Then(@"I check ""(.*)"" field")]
        public void ThenICheckField(string fieldname)
        {
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage();
            switch (fieldname)
            {
                case "birthday":
                    cCENPersonalDataPage.ClearAndSetValueAge("");
                    cCENPersonalDataPage.ClearAndSetValueDate("77777777");
                    IsTrue(cCENPersonalDataPage.AssertInvalidTooltip(),"Сообщение о невалидности введеных данных не отобразилось");
                    cCENPersonalDataPage.ClearAndSetValueDate("12122080");
                    IsTrue(cCENPersonalDataPage.AssertFutureTooltip(),"Сообщение об указанной будущей дате не отобразилось");
                    break;

                case "age":
                    cCENPersonalDataPage.ClearAndSetValueAge("0");
                    IsTrue(cCENPersonalDataPage.AssertTodayIsBirthday(),"не удалось");
                    IsTrue(cCENPersonalDataPage.AssertSomeYearsAge(10), "не удалось");
                    IsTrue(cCENPersonalDataPage.AssertSoOldAgeTooltip("222"),"Не удалось");
                    IsTrue(cCENPersonalDataPage.AssertFullBirthday(120), "Не удалось");
                    break;

                case "email":
                    IsTrue(cCENPersonalDataPage.AssertInvalidEmailTooltip("abc"),"Сообщение не отобразилось");//проверка сообщения 
                    IsTrue(cCENPersonalDataPage.AssertEmailCheckboxIsUnchecked(), "Проверка на неустановленный чекбокс не пройдена");// проверка что чекбокс не установлен
                    IsTrue(cCENPersonalDataPage.AssertValidEmailAndSetCheckbox("a@bk.ru"), "Чекбокс при правильном имейле не установился");// ввод правильного имейла и проверка что чекбокс установился

                    break;
                case "phone":
                    IsTrue(cCENPersonalDataPage.AssertInvalidMobileTooltip("1234"), "Сообщение не отобразилось");//проверка сообщения 
                    IsTrue(cCENPersonalDataPage.AssertMobileCheckboxIsUnchecked(), "Проверка на неустановленный чекбокс не пройдена");// проверка что чекбокс не установлен
                    IsTrue(cCENPersonalDataPage.AssertValidMobileAndSetCheckbox("9819819811"), "Чекбокс при правильном имейле не установился");// ввод правильного номера и проверка что чекбокс установился
                    break;

                case "policy-number":
                    IsTrue(cCENPersonalDataPage.AssertPolicyNumberFieldIsRequired(), "Проверка на обязательность поля Номер Полиса не пройдена");
                    break;

                case "dispatcher":
                    IsTrue(cCENPersonalDataPage.AssertDispatcherFieldIsRequired(), "Проверка на обязательность поля Диспетчер не пройдена");
                    break;

                case "validity":
                    cCENPersonalDataPage.ClearAndSetPolicyDate("12121912");
                    IsTrue(cCENPersonalDataPage.AssertExpiredValidity(), "Сообщение о истекшем полисе не отобразилось");
                    cCENPersonalDataPage.ClearAndSetPolicyDate("08012500");
                    IsTrue(cCENPersonalDataPage.AssertValidityFieldIsRequired(), "Сообщение об обязательности заполнения поля срок полиса не отобразилось");
                    break;
            }

        }
    }
}
