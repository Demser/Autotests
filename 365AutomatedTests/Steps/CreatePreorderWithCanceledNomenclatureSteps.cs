using System;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using Coypu;
using _365AutomatedTests.Framework.Generic;


namespace _365AutomatedTests.Steps
{
    using static NUnit.Framework.Assert;
    [Binding]
    public class CreatePreorderWithCanceledNomenclatureSteps
    {
        ElementScope SecondTab;   
        
        [When(@"I cancel nomenclature ""(.*)"" by script")]
        public void WhenICancelNomenclatureByScript(string p0)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"UPDATE [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] SET Canceled = 1,Delay = 0, EndTime = '2020-12-31T00:00:00.000' where HXID= '21-019' and Id = (SELECT TOP (1) [Id] FROM [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] where HXID= '21-019' Order by EndTime desc)";
            var result = _msBDConnector.NonQueryExecutor(command);
        }
        
        [Then(@"I open preorder for edit")]
        public void ThenIOpenPreorderForEdit()
        {
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            preOrderJournalPage.OpenForEdit();
            System.Threading.Thread.Sleep(2000);
            CommonSteps common = new CommonSteps();
            SecondTab = common.ThenTheTabWithTitleShouldBeOpened("Edit");
            System.Threading.Thread.Sleep(2000);
        }
        
        [Then(@"The indicate of canceled nomenclature will be shown")]
        public void ThenTheIndicateOfCanceledNomenclatureWillBeShown()
        {
            CCENNomenclaturesPage nomenclaturesPage = new CCENNomenclaturesPage(SecondTab);
            nomenclaturesPage.AddSomeNomenctatureToCart("02-002"); // заменить на отмененный hxid после исправления CCEN-1039
            System.Threading.Thread.Sleep(2000);
        }

    }
}
