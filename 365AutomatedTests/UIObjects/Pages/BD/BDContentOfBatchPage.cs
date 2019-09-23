using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;

namespace _365AutomatedTests.UIObjects.Pages
{
    class BDContentOfBatchPage : Page
    {
        public BDContentOfBatchPage() : base()
        {
        }
        public BDContentOfBatchPage(ElementScope scope) : base(scope)
        {
        }
        private ElementScope CancelBatchButton => Scope.FindXPath("//a[@ng-show='vm.batch.status != vm.statuses.canceled']"); // найти кнопку Отменить бэтч
        private ElementScope FindSampleWithThisName (string _)=> Scope.FindXPath($"(//td[.='{_}'])[1]"); // найти пробу с таким именем

        public List<ElementScope> ListOfDrippingSamples => Scope.FindAllXPath("//td[contains(text(),'01-')]").Select(i => (ElementScope)i).ToList(); // взять список всех ячеек с образцами в содержимом бэтча

     


        public void ClickCancelBatchButton() // кликнуть на кнопку Отменить бэтч
        {
            CancelBatchButton.Click();
        }

        public bool AssertFindSampleWithThisName(string _)
        {
            if (FindSampleWithThisName(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(10) })) return true;
            else return false;
        }

    }
}
