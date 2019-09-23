using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using static NUnit.Framework.Assert;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class BDSortingWithExperimentModeStatusSettingsSteps
    {
        [When(@"I go to Settings page")]
        public void WhenIGoToSettingsPage()
        {
            BDMainPage bDMainPage = new BDMainPage();
            bDMainPage.OpenSettings();
            IsTrue(bDMainPage.AssertOpenSettings(),"Не найден заголовок пункта");
        }
        
        [When(@"I set Experiment Mode Status ""(.*)""")]
        public void WhenISetExperimentModeStatus(string mode)
        {
            BDSettingsPage bDSettingsPage = new BDSettingsPage();
            bDSettingsPage.OpenNameFilter();
            bDSettingsPage.SetOptionOfName();
            System.Threading.Thread.Sleep(1000);
            bDSettingsPage.ClickSettingEditBtn();
            if (mode.Contains("true"))
            {
                bDSettingsPage.ClickSettingOff();
                System.Threading.Thread.Sleep(2000);
                bDSettingsPage.ClickModeSettingIsOn();
            }
            else if (mode.Contains("false"))
            {
                bDSettingsPage.ClickSettingOn();
                System.Threading.Thread.Sleep(2000);
                bDSettingsPage.ClickModeSettingIsOff();
            }

            bDSettingsPage.ClickSettingSaveBtn();
        }
        
        [Then(@"I check valid notification about Experiment Mode")]
        public void ThenICheckValidNotificationAboutExperimentMode()
        {
            BDSortingPage bDSortingPage = new BDSortingPage();
            IsTrue(bDSortingPage.AssertExpModeMessage(),"Предупредительное сообщение отсутствует");
        }
    }
}
