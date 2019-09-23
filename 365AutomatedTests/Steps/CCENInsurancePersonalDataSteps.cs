using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Coypu;
using System;


namespace _365AutomatedTests.Steps
{
    using static Crawler;
    using static Assert;
    [Binding]
    public class CCENInsurancePersonalDataSteps
    {
        ElementScope SecondTab;
        ElementScope SecondTab1;
        [Then(@"I see that insurance fields block is ""(.*)""")]
        public void ThenISeeThatInsuranceFieldsBlockIs(string status)
        {
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage();
            switch (status)
            {
                case "absent":
                    IsFalse(cCENPersonalDataPage.AssertInsuranceFieldsBlock(), "Ошибка. Блок с полями страховой отображается при выборе КК.");
                    break;
                case "present":
                    IsTrue(cCENPersonalDataPage.AssertInsuranceFieldsBlock(), "Ошибка. Блок с полями страховой отсутствует при выборе СК.");
                    break;

            }
        }

        [Then(@"I open preorder for edit and see values in the insurance fields")]
        public void ThenIOpenPreorderForEditAndSeeValuesInTheInsuranceFields()
        {
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            preOrderJournalPage.OpenForEdit();
            System.Threading.Thread.Sleep(2000);
            CommonSteps common = new CommonSteps();
            SecondTab = common.ThenTheTabWithTitleShouldBeOpened("Edit");
            System.Threading.Thread.Sleep(2000);
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage(SecondTab);
            cCENPersonalDataPage.GoToPersonalDataTab();
            System.Threading.Thread.Sleep(1000);
            IsTrue(cCENPersonalDataPage.AssertNumberOfPolicyFieldValueIsNotEmpty(),"Ошибка. Это поле пустое.");
            IsTrue(cCENPersonalDataPage.AssertDispatcherFieldValueIsNotEmpty(), "Ошибка. Это поле пустое.");
            IsTrue(cCENPersonalDataPage.AssertValidityFieldValueIsNotEmpty(), "Ошибка. Это поле пустое.");
        }

        [Then(@"I open preorder for reply and see values in the insurance fields")]
        public void ThenIOpenPreorderForReplyAndSeeValuesInTheInsuranceFields()
        {
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            preOrderJournalPage.OpenForReply();
            System.Threading.Thread.Sleep(2000);
            CommonSteps common = new CommonSteps();
            SecondTab1 = common.ThenTheTabWithTitleShouldBeOpened("Edit");
            System.Threading.Thread.Sleep(2000);
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage(SecondTab1);
            cCENPersonalDataPage.GoToPersonalDataTab();
            System.Threading.Thread.Sleep(1000);
            IsTrue(cCENPersonalDataPage.AssertNumberOfPolicyFieldValueIsNotEmpty(), "Ошибка. Это поле пустое.");
            IsTrue(cCENPersonalDataPage.AssertDispatcherFieldValueIsNotEmpty(), "Ошибка. Это поле пустое.");
            IsTrue(cCENPersonalDataPage.AssertValidityFieldValueIsNotEmpty(), "Ошибка. Это поле пустое.");
            // объединить два шага в один с прим.параметра edit/reply
        }



    }
}
