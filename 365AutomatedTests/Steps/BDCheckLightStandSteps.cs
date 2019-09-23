using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps
{
    using static NUnit.Framework.Assert;
    [Binding]
    public class BDCheckLightStandSteps
    {
        [When(@"I go to Devises page")]
        public void WhenIGoToDevisesPage()
        {
            BDMainPage bDMainPage = new BDMainPage();
            bDMainPage.OpenDeviceSettings();
            IsTrue(bDMainPage.AssertOpenDeviceSettings(),"Заголовок Настройки оборудования отсутствует, либо страница не открылась");
        }
        

        [Then(@"I start check for lightstand")]
        public void ThenIStartCheckForLightstand()
        {
            BDSettingsDevicePage bDSettingsDevicePage = new BDSettingsDevicePage();
            bDSettingsDevicePage.ClickLightStand();
        }
    }
}
