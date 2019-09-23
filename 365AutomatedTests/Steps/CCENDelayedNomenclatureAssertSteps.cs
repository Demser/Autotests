using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using Coypu;
using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework.Generic;


namespace _365AutomatedTests.Steps
{
    using static NUnit.Framework.Assert;
    [Binding]
    public class CCENDelayedNomenclatureAssertSteps
    {
        ElementScope SecondTabs;
        [When(@"I delay nomenclature ""(.*)"" by script")]
        public void WhenIDelayNomenclatureByScript(string p0)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"UPDATE [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] SET Canceled = 0,Delay = 24, EndTime = '2020-12-31T00:00:00.000' where HXID= '21-071' and Id = (SELECT TOP (1) [Id] FROM [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] where HXID= '21-071' Order by EndTime desc)";
            var result = _msBDConnector.NonQueryExecutor(command);

        }

        [When(@"I delay nomenclature ""(.*)"" on ""(.*)"" hours by script")]
        public void WhenIDelayNomenclatureOnHoursByScript(string hxid, int hours)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"UPDATE [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] SET Canceled = 0,Delay = {hours}, EndTime = '2020-12-31T00:00:00.000' where HXID= '{hxid}' and Id = (SELECT TOP (1) [Id] FROM [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] where HXID= '{hxid}' Order by EndTime desc)";
            var result = _msBDConnector.NonQueryExecutor(command);
        }

        [Then(@"I make active nomenclature ""(.*)""")]
        public void ThenIMakeActiveNomenclature(string hxid)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"UPDATE [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] SET Canceled = 0,Delay = 0, EndTime = '2020-12-31T00:00:00.000' where HXID= '{hxid}' and Id = (SELECT TOP (1) [Id] FROM [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureExceptions] where HXID= '{hxid}' Order by EndTime desc)";
            var result = _msBDConnector.NonQueryExecutor(command);
        }




        [Then(@"The indicate of delayed nomenclature will be shown")]
        public void ThenTheIndicateOfDelayedNomenclatureWillBeShown()
        {
            CommonSteps common = new CommonSteps();
            SecondTabs = common.ThenTheTabWithTitleShouldBeOpened("Edit");
            CCENNomenclaturesPage nomenclaturesPage = new CCENNomenclaturesPage(SecondTabs);
            nomenclaturesPage.AddSomeNomenctatureToCart("02-002");
            //CCENCartPage cCENCartPage = new CCENCartPage(SecondTab);
            //cCENCartPage.GoToCartTab();
           // IsTrue(cCENCartPage.AssertDelayedText(),"Нет подписи о задержке выполнения номенклатуры");
        }
    }
}
