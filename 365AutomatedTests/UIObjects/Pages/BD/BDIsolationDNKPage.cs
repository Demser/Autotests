using System;
using _365AutomatedTests.Framework;
using _365AutomatedTests.Framework.Generic;
using TechTalk.SpecFlow;
using Coypu;

namespace _365AutomatedTests.UIObjects.Pages
{
    using OpenQA.Selenium;
    using System.Collections.Generic;

    [Binding]
    class BDIsolationDNKPage:Page
    {
        public BDIsolationDNKPage() : base()
        {
        }
        private ElementScope NoMoreSamplesYetTitle => Scope.FindXPath("//p[.='Еще нет обработанных образцов!']");
        public ElementScope ConfirmSampleField => Scope.FindXPath("//input[@placeholder='Штрих-код образца']", new Options() { Timeout = TimeSpan.FromSeconds(10) });
        private ElementScope CountOfAddedSamples (int _) => Scope.FindXPath($"//td[@class='col-md-1 ng-binding' and .='{_}']"); // для проверки, что добавилось столько-то записей.
        private ElementScope IsolationControlsItemsTitle => Scope.FindXPath("//h4[.='Контроли выделения:']");
        private ElementScope TakeThisPartButton => Scope.FindButton("Взять в работу партию");
        private ElementScope TakeThisSamplesButton => Scope.FindXPath("(//button[contains(text(),'Взять в работу')])[1]");
        private ElementScope ConfirmTakeThisPartButton => Scope.FindXPath("//button[@class='k-button my-focused-button ng-binding']");
        private ElementScope AssertIsolationControlExist => Scope.FindXPath("//p[@ng-repeat='ic in vm.isolationControls']"); // наличие контроля выделения
        private ElementScope ConfirmControlMaterialInput => Scope.FindXPath("//input[@placeholder='Штрих-код контрольного материала']");
        private ElementScope ConfirmIsolationControlInput => Scope.FindXPath("//input[@placeholder='Штрих-код пробирки']");//119,111

        private ElementScope ControlsOnThePage => Scope.FindXPath("//p[@ng-repeat='cm in vm.controlMaterials'][1]");
        private ElementScope FindActiveTestTubes => Scope.FindXPath("(//div[contains (@class,'col-md-4') and contains(@style,'color: rgb(153, 153, 0);')])[1]"); // поиск пробирки с зеленой подцветкой

        private ElementScope VisibleButtonCreateBatch => Scope.FindXPath("//div[@ng-show='vm.mode === vm.modes.createBatch'and @class='']");
        private ElementScope StartIsolationButton => Scope.FindXPath("//button[.='Создать бэтч']");
        private ElementScope BatchIsAlreadyCreated => Scope.FindXPath("//h4[.='Рабочий бэтч:']");

        public ElementScope ParentID => Scope.FindXPath("//*[@id='confirmBatchId']/../../batch-info/table/tbody/tr[1]/td[2]");
        public string ParentBatchID => ParentID.Text;
        private ElementScope ConfirmBatchIDField => Scope.FindXPath("//input[@id='confirmBatchId']");
        public ElementScope NoTestsForWorkMessage(string _) => Scope.FindXPath($"//div[contains(text(),'{_}')]", new Options() { Timeout = TimeSpan.FromSeconds(3) });
        // Проверка, что кнопка создания бэтча подгрузилась и на неё можно кликнуть.
        public bool AssertTheButtonIsVisible()
        {
            if (VisibleButtonCreateBatch.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

        // Нажать на кнопку создать Бэтч
        public void ClickCreateBatch()
        {
            StartIsolationButton.Click();
        }

        // Проверка, что кнопка нажата и отображается заголовок рабочего бэтча
        public bool AssertBatchIsCreated()
        {
            if (BatchIsAlreadyCreated.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public void SetParentBatchID()
        {
            ConfirmBatchIDField.Click();
            ConfirmBatchIDField.SendKeys(ParentBatchID);
            ConfirmBatchIDField.SendKeys(Keys.Enter);
        }
        
        public void AddSample(List <string> _)
        {
            foreach (string i in _)
            {
                System.Threading.Thread.Sleep(1000);
                ConfirmSampleField.SendKeys(i);
                ConfirmSampleField.SendKeys(Keys.Enter);
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void TakeThisSamples() // Нажать на кнопку Взять в работу образцы и подтвердить в диал окне
        {
            System.Threading.Thread.Sleep(2000);
            TakeThisSamplesButton.Click();
            System.Threading.Thread.Sleep(1500);
            ConfirmTakeThisPartButton.Click();
        }

        public void AddControlMaterial(string _) // Считать штрих-код контрольного материала (для ВК)
        {
            if (ConfirmControlMaterialInput.Exists())
            {
                var a = ControlsOnThePage["clipboard-copy"];
                ConfirmControlMaterialInput.SendKeys(a);
                ConfirmControlMaterialInput.SendKeys(Keys.Enter);              
            }
        }

        public void AddIsolationControl(string _) // Считать штрих-код пробирки
        {           
            ConfirmIsolationControlInput.SendKeys(_).SendKeys(Keys.Enter);
            //System.Threading.Thread.Sleep(500);
            ConfirmIsolationControlInput.WaitForClickability();
        }

        public bool NoMoreSamplesYet() // Проверка подписи что нет больше образцов
        {
            if (NoMoreSamplesYetTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public void ControlsBoardCodeEnter(string _) // считать ШК для КМ типа ПКО и ОКО, если КМ считан - считать пробирку
        {
            if (ConfirmControlMaterialInput.Exists())
            {
                var a = ControlsOnThePage["clipboard-copy"];
                ConfirmControlMaterialInput.SendKeys(a);
                ConfirmControlMaterialInput.SendKeys(Keys.Enter);

                if (FindActiveTestTubes.Exists())
                {
                    var b = FindActiveTestTubes["clipboard-copy"];
                    ConfirmIsolationControlInput.SendKeys(b);
                    ConfirmIsolationControlInput.SendKeys(Keys.Enter);
                }
            }
        }

        public void AddActiveIsolationControl(string _) // Считать штрих-код пробирки с зеленой подцветкой
        {
            if (ConfirmIsolationControlInput.Exists())
            {
                ConfirmIsolationControlInput.WaitForClickability();
                var a = FindActiveTestTubes["clipboard-copy"];
                ConfirmIsolationControlInput.SendKeys(a);
                ConfirmIsolationControlInput.SendKeys(Keys.Enter);
                //System.Threading.Thread.Sleep(500);           
            }
        }

        public bool NeedActiveTestTubes() // Проверка подписи Ввести ШК пробирки
        {
            if (FindActiveTestTubes.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

    }

}
