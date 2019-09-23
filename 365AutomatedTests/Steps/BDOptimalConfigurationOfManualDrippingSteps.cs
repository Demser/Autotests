using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using Coypu;

// Класс для проверки, что отображается заданное количество тестов
namespace _365AutomatedTests.Steps
{
    using static NUnit.Framework.Assert;
    [Binding]
    public class BDOptimalConfigurationOfManualDrippingSteps
    {
        ElementScope FirstTab;
        ElementScope SecondTab;
        [Then(@"Tests counts (.*) will be created")]
        public void ThenTestsCountsWillBeCreated(int countOfMultiplexes)
        {
            BDMainPage BDMain = new BDMainPage();
            BDMain.OpenDictionaries();
            CommonSteps common = new CommonSteps();
            common.RefreshPage();
            BDDictTestPage BDDictTest = new BDDictTestPage();
            BDDictTest.CheckCountOfTests();
            IsTrue(BDDictTest.AssertCountOfFewTests(countOfMultiplexes),"Количество созданных тестов не соответствует ожидаемому.");   
        }
    }
}
