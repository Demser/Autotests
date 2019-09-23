using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;

namespace _365AutomatedTests.Steps
{
    using System;
    using static NUnit.Framework.Assert;
    [Binding]
    public class BDFullCycleWithStandartSteps
    {
        [When(@"I go to Forming Page")]
        public void WhenIGoToFormingPage()
        {
            BDMainPage mainPage = new BDMainPage();
            mainPage.OpenBatchFormingWorkplace();
            IsTrue(mainPage.AssertOpenBatchFormingWorkplace(), "Отсутствует заголовок Формирование тестовых бэтчей");

        }

        [Then(@"I forming the test ""(.*)"" contains all samples")]
        public void ThenIFormingTheTestContainsAllSamples(string type)
        {
            BDFormingPage bDFormingPage = new BDFormingPage();
            if (type == "tripod") // если штатив
            {
                bDFormingPage.ClickCreateTripod(); // показавать возможный штатив
                IsTrue(bDFormingPage.AssertPreviewBatchMessage(), "Отсутствует сообщение о получении превью бэтча");
                IsTrue(bDFormingPage.AssertWindowTitle(), "Не успело подгрузиться окно предпросмотра бэтча");
                bDFormingPage.ClickToFormBatchButton();
            }
            else if (type == "tablet") // если планшет
            {
                bDFormingPage.ClickCreateTablet(); // показавать возможный планшет
                IsTrue(bDFormingPage.AssertPreviewBatchMessage(), "Отсутствует сообщение о получении превью бэтча");
                IsTrue(bDFormingPage.AssertWindowTitle(), "Не успело подгрузиться окно предпросмотра бэтча");
                bDFormingPage.ClickToFormBatchButton();
            }
            else { throw new NullReferenceException("Неправильно задан параметр type. Допустимые варианты: tripod или tablet."); }
            IsTrue(bDFormingPage.AssertBatchHaveFormedMessage(), "Отсутствует заключительное сообщение о том, что бэтч сформирован");
        }

        
    }
}
