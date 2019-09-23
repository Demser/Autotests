using _365AutomatedTests.Framework.Generic;
using Coypu;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.UIObjects.Pages
{
    using System;
    using OpenQA.Selenium;
    [Binding]
    class BDSortingPage : Page
   {

        public BDSortingPage() : base()
        {
        }
        public ElementScope ParentID => Scope.FindXPath("//*[@id='confirmBatchId']/../../batch-info/table/tbody/tr[1]/td[2]");
        public string ParentBatchID => ParentID.Text;
        private ElementScope ConfirmBatchIDField => Scope.FindXPath("//input[@id='confirmBatchId']");
        public ElementScope CodeInputField => Scope.FindXPath("//input[@id='codeInput']"); // поле для считывания        
        private ElementScope SortingTitle => Scope.FindXPath("//h3[.='Сортировка']");
        private ElementScope SampleCodeFieldIsActive => Scope.FindCss(".cg-busy.cg-busy-backdrop.cg-busy-backdrop-animation.ng-scope.ng-hide",new Options {ConsiderInvisibleElements=false });
        public ElementScope StartSortingButton => Scope.FindId("createBatchButton");//XPath("//button[contains(text(),'Создать')]"); // кнопка создать бэтч
        private ElementScope BatchIsAlreadyCreated => Scope.FindXPath("//h4[.='Рабочий бэтч:']");       
        private ElementScope VisibleButtonCreateBatch => Scope.FindXPath("//div[@ng-show='vm.mode === vm.modes.createBatch'and @class='']");
        public ElementScope CompleteBatchButton => Scope.FindId("completeButton");
        public ElementScope CompleteAddSamplesButton => Scope.FindButton("Взять в работу");
        public ElementScope CompleteAddSamplesButtonDisable => Scope.FindXPath("//button[@id='completeButton']");
        public ElementScope ConfirmEndOfSamplesButton => Scope.FindButton("Завершить");
        private ElementScope ApplyCompleteAddSamplesButton => Scope.FindXPath("//button[@class='k-button my-focused-button ng-binding' and .='Завершить']");
        private ElementScope IsolationControlField => Scope.FindXPath("//input[@id='codeInput' and @placeholder='Штрих-код контроля выделения']");
        private ElementScope FirstCellIsGrey => Scope.FindXPath("//div[@id='1H' and contains(@style,'lightgray')]");//первая лунка виртуального планшета серая
        private ElementScope CanseledBatchTitle => Scope.FindXPath("//h3[.='Статус бэтча не соответствует ожидаемому. Возможно, бэтч был отменен или изменен кем-то другим. Для продолжения работы с другим бэтчем обновите страницу']");
        private ElementScope ExpModeMessage => Scope.FindXPath("//div[contains(text(),'нет тестов для выполнения')]");
        public ElementScope BusyIndicatorAddingSamples => Scope.FindCss("div.qa-item-sample-adding-loader.ng-scope.ng-hide");
        public ElementScope DNASampleMessage => Scope.FindXPath("//div[contains(text(),'Образец должен выделяться в пробирке')]");
        public ElementScope FieldWithUsercodePlaceholder => Scope.FindXPath("//input[@placeholder='Штрих-код пользователя']");
        private ElementScope SampleFromAnotherLocationMessage(string _) => Scope.FindXPath($"//div[contains(text(),'Проба принадлежит лаборатории {_}')]");

        //private ElementScope KMName(string num,string name) => Scope.FindXPath($"//span[@clipboard-copy='{num}'and contains(text(),'{name}')]");
        public ElementScope KmName(string name) => Scope.FindXPath($"//span[contains(text(),'{name}')]");
        public string greenColor = "background-color: rgb(96, 213, 172);"; // Значение атрибута style когда зелёный фон
        public void SetValueOfKM(string name)
        {
            var numberOfKM = KmName(name)["clipboard-copy"];// Получаем значения атрибута clipboard-copy элемента KmName с заданным параметром (name)
           
            CodeInputField.WaitForAvailability();
            CodeInputField.SendKeys(numberOfKM);
            CodeInputField.SendKeys(Keys.Enter);
            CodeInputField.WaitForAvailability();
            CodeInputField.WaitForClickability();
            var colorOfKM = KmName(name)["style"];
            while (colorOfKM == greenColor && KmName(name).Exists()) // Пока КМ есть в информационном блоке и подсвечен зелёным фоном
            {
                if (CodeInputField.Exists())
                {
                    CodeInputField.SendKeys(Config.UserBarCode);  // подтверждаем раскапывание КМ в каждую лунку
                    CodeInputField.SendKeys(Keys.Enter);
                    //var a = CodeInputField.Exists();

                    var a = StartSortingButton.Exists();
                    if (a==false) // если кнопки "Создать Бэтч" нет
                    {
                        CodeInputField.WaitForAvailability();
                        CodeInputField.WaitForClickability();
                    }

                    //System.Threading.Thread.Sleep(200);
                }
                else
                {
                    break;
                }

            }

        }


      


        // Проверка что отобразилось сообщение после попытки добавления образца в отключенном режиме испытаний
        public bool AssertExpModeMessage()
        {
            if (ExpModeMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(3) }))
            {
                return true;
            }
            else return false;
        }
        
        // Проверка, что кнопка создания бэтча подгрузилась и на неё можно кликнуть.
        public bool AssertTheButtonIsVisible()
        {
            if (VisibleButtonCreateBatch.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

        public bool AssertTheFirstCellIsGrey()
        {
            if (FirstCellIsGrey.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

        // Нажать на кнопку создать Бэтч
        public void ClickCreateBatch()
        {
            StartSortingButton.Click();
        }

        // Проверка, что кнопка нажата и отображается заголовок рабочего бэтча
        public bool AssertBatchIsCreated()
        {
            if (BatchIsAlreadyCreated.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Проверка, что подгрузилось поле для подтверждения бэтча
        public bool AssertTheConfirmBatchFieldIsVisible()
        {
            if (ConfirmBatchIDField.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

        // Проверка на сообщение при отмененном бэтче
        public bool AssertCanseledBatchTitle()
        {
            if (CanseledBatchTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // Подтвердить бэтч
        public void SetParentBatchID()
        {
            ConfirmBatchIDField.Click();
            ConfirmBatchIDField.SendKeys(ParentBatchID);
            ConfirmBatchIDField.SendKeys(Keys.Enter);         

        }

        public void EndBatch()
        {
            CompleteBatchButton.Click();
            ApplyCompleteAddSamplesButton.Click(new Options { Timeout = System.TimeSpan.FromSeconds(2) });

        }
        
        // Проверка, что поле для ввода образцов кликабельно и в него можно добавлять последующий образец.
        public bool IsSampleCodeFieldActive()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    CodeInputField.Click(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(250) });                                     
                    return true;
                }
                catch (System.InvalidOperationException e)
                {
                    count++;
                    if (count > 100) break;
                }
            }
            return false;

        }
        

        public void SetUsercodeInSortingField()
        {
            CodeInputField.SendKeys("7777");
            CodeInputField.SendKeys(Keys.Enter);
        }

        public bool AssertSampleFromAnotherLocationMessage(string _)
        {
            if (SampleFromAnotherLocationMessage(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(3) }))
            {
                return true;
            }
            else return false;
        }



    }

}