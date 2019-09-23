using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using NUnit.Framework;
using Coypu;


namespace _365AutomatedTests.Steps
{
    using static Assert;
    [Binding]
    public class CCENDisplayEmployeeContractWhenSelectMobileAndEmployeeSteps
    {
        [Then(@"I see that mobile block is ""(.*)""")]
        public void ThenISeeThatMobileBlockIs(string status)
        {
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage();
            switch (status)
            {
                case "absent":
                    IsFalse(cCENPersonalDataPage.AssertMobileBlock(), "Ошибка. Блок с полями Мобильного выезда отображается при выборе КК.");
                    break;
                case "present":
                    IsTrue(cCENPersonalDataPage.AssertMobileBlock(), "Ошибка. Блок с полями Мобильного выезда отсутствует при выборе СК.");
                    break;
            }
        }
        
        [Then(@"I set account status ""(.*)"" for diagnostic center ""(.*)""")]
        public void ThenISetAccountStatusForDiagnosticCenter(int accStatus, int dc)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"UPDATE [AllCenterGlobalDictionariesWork].[dbo].[AllAccounts] SET AccStatusCode = {accStatus} where Code = {dc}";
            var result = _msBDConnector.NonQueryExecutor(command);
        }
    }
}
