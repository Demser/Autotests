using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.UIObjects.Pages;
using System.Linq;
using TechTalk.SpecFlow.Assist;
using _365AutomatedTests.Framework;
using static NUnit.Framework.Assert;
using Coypu;
using NUnit.Framework;
using System.Diagnostics;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CCENConsiderGenderAndAgeOfPatientsSteps
    {
        [Then(@"I check that ""(.*)"" rule is ""(.*)""")]
        public void ThenICheckThatRuleIs(string rule, string status)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            switch (status)
            {
                case "absent":
                    IsFalse(cCENCartPage.AssertRuleIsPresent(rule), "Ошибка. Правило отображается, однако не должно");
                    break;
                case "present":
                    IsTrue(cCENCartPage.AssertRuleIsPresent(rule), "Ошибка. Правило отсутствует в блоке подготовки, либо находится не на первом месте.");
                    break;
            }
        }
    }
}
