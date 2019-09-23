using _365AutomatedTests.Framework.Generic;
using Coypu;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;


namespace _365AutomatedTests.UIObjects.Pages
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [Binding]
    class BDProductionAcceptancePage:Page
    {
        public BDProductionAcceptancePage(string css = ".container") : base(css)
        {
        }
        private ElementScope ProductionAcceptanceField => Scope.FindXPath("//*[@id='page-content']/section/div[1]/form/input");
        private ElementScope CountOfChildBatches => Scope.FindXPath("//span[.='Отображены записи 1 - 1 из 1']", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        private ElementScope SetBatchOrSampleField => Scope.FindXPath("//input[@placeholder='Штрих-код бэтча или образца']", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        private ElementScope SetControlmaterial =>  Scope.FindXPath("//input[@placeholder='Штрих-код бэтча или образца']", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        private ElementScope CountOfFewElements (string _) => Scope.FindXPath($"//span[.='Отображены записи 1 - {_} из {_}']", new Options() { Timeout = TimeSpan.FromSeconds(10) });



        public void SetParentBatch(string e)
        {
            ProductionAcceptanceField.Click();
            ProductionAcceptanceField.SendKeys(e);
            ProductionAcceptanceField.SendKeys(Keys.Enter);
           
            
        }

        public bool AssertCountOfChildBatches()
        {
            if (CountOfChildBatches.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public void AddSample(List <string> _)
        {
            foreach (string i in _)
            {
                SetBatchOrSampleField.SendKeys(i);
                SetBatchOrSampleField.SendKeys(Keys.Enter);
            }
        }

        public void AddControlMaterial(string _)
        {          
            SetControlmaterial.SendKeys(_);
            SetControlmaterial.SendKeys(Keys.Enter);
        }

        public bool AssertCountOfElementsInfo(string _)
        {
            if (CountOfFewElements(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(5) })) return true;
            else return false;
        }

    }
}


