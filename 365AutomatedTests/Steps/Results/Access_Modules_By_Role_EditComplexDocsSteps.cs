using _365AutomatedTests.UIObjects.Pages;
using Coypu;
using System;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.Steps.Results
{
    [Binding]
    public class Access_Modules_By_Role_EditComplexDocsSteps
    {
        ElementScope pageInAnotherTab;

        [When(@"I click modules and results")]
        public void WhenIClickModulesAndResults()
        {
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"I see modules by role ""(.*)""")]
        public void ThenISeeModulesByRole(string p0)
        {
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I not see upload conclusion button")]
        public void ThenINotSeeUploadConclusionButton()
        {
            //ScenarioContext.Current.Pending();
        }

        [Then(@"I not see delete conclusion button")]
        public void ThenINotSeeDeleteConclusionButton()
        {
            //ScenarioContext.Current.Pending();
        }

    }
}
