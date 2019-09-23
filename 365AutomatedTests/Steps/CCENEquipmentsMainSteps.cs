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

namespace _365AutomatedTests
{
    [Binding]
    public class CCENEquipmentsMainSteps
    {
        [When(@"I cancel equipment ""(.*)"" by script")]
        public void WhenICancelEquipmentByScript(string equipment)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"Update [AllCenterGlobalDictionariesWork].[dbo].[AllEquipments] SET IsRemoved = 1 Where Code = {equipment}";
            var result = _msBDConnector.NonQueryExecutor(command);
        }
        
        [Then(@"I check diagnostical center ""(.*)"" equipment is ""(.*)""")]
        public void ThenICheckDiagnosticalCenterEquipmentIs(int dc, string status)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            switch (status)
            {
                case "absent":
                    IsFalse(cCENCartPage.AssertDCHasEquipment(dc), "Ошибка. В выбранном ДЦ не отображается флаг оборудования");
                    break;
                case "present":
                    IsTrue(cCENCartPage.AssertDCHasEquipment(dc), "Ошибка. Оборудование отсутствует, но флаг отображается.");
                    break;
            }
        }
        
        [Then(@"I get message ""(.*)""")]
        public void ThenIGetMessage(string messText)
        {
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage();
            cCENPersonalDataPage.MessаgePopup(messText).WaitForVisibility();
            IsTrue(cCENPersonalDataPage.MessageIsShown(messText), "Сообщение об ошибке отсутствует");
        }

        [Then(@"I check that diagnostical center block is empty")]
        public void ThenICheckDiagnosticalCenterIsEmpty()
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            IsTrue(cCENCartPage.DiagnosticalCenterIsEmpty(), "не совпадает с ожидаемым результатом, ДЦ должны отсутствовать");
        }

        [Then(@"I make active equipment ""(.*)"" by script")]
        public void ThenIMakeActiveEquipmentByScript(string eqId)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"Update [AllCenterGlobalDictionariesWork].[dbo].[AllEquipments] SET IsRemoved = 0 Where Code = {eqId}";
            var result = _msBDConnector.NonQueryExecutor(command);
        }
    }
}
