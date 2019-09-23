using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using NUnit.Framework;
using Coypu;

namespace _365AutomatedTests.Steps
{
    using static Crawler;
    using static Assert;
    [Binding]
    public class CCENEquipmentsAssertsSteps
    {
        [When(@"I set gender '(.*)' for '(.*)' nomenclature item")]
        public void WhenISetGenderForNomenclatureItem(string gender, string hxid)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"Update [AllCenterGlobalDictionariesWork].[dbo].[AllNomenclatureItems] SET Gender = '{gender}' Where HXID = '{hxid}'";
            _msBDConnector.NonQueryExecutor(command);
        }
        
        [When(@"I delete all equipments for '(.*)' nomenclature")]
        public void WhenIDeleteAllEquipmentsForNomenclature(string hxid)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"DELETE FROM [AllCenterGlobalDictionariesWork].[dbo].[AllEquipmentsInNomenclatures] Where HXID = '{hxid}'";
            _msBDConnector.NonQueryExecutor(command);
        }
        
        [When(@"I add new equimpent type '(.*)' for '(.*)' nomenclature and set '(.*)' gender")]
        public void WhenIAddNewEquimpentTypeForNomenclatureAndSetGender(string eqCode, string hxid, string gender)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            string command = $@"INSERT INTO [AllCenterGlobalDictionariesWork].[dbo].[AllEquipmentsInNomenclatures] VALUES ({eqCode},'{hxid}','{gender}','2019-01-01T00:09:07.683')";
            _msBDConnector.NonQueryExecutor(command);
        }
        
        [When(@"I set '(.*)' status for all equipments and '(.*)' equipmentscode and '(.*)' accountcode")]
        public void WhenISetStatusForAllEquipmentsAndEquipmentscodeAndAccountcode(string status, string eqCode, string account)
        {
            MSDatabaseConnector _msBDConnector = new MSDatabaseConnector(Config.MSACGD);
            if (eqCode == "")
            {
                string command = $@"Update [AllCenterGlobalDictionariesWork].[dbo].[AllEquipmentsInAccounts] SET Status = {status} Where AccountCode = '{account}'";
                _msBDConnector.NonQueryExecutor(command);
            }
            else if (eqCode != "")
            {
                string command = $@"Update [AllCenterGlobalDictionariesWork].[dbo].[AllEquipmentsInAccounts] SET Status = {status} Where AccountCode = '{account}' and Code = {eqCode}";
                _msBDConnector.NonQueryExecutor(command);
            }

        }       
    }
}
