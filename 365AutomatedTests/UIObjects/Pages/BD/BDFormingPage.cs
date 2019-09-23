// РМ Формирование тестовых бэтчей и Формирование тестовых бэтчей с OT

using _365AutomatedTests.Framework.Generic;
using Coypu;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using TechTalk.SpecFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace _365AutomatedTests.UIObjects.Pages
{
    using System;
    using OpenQA.Selenium;
    [Binding]
    class BDFormingPage : Page
    {

        public BDFormingPage() : base()
        {
        }
        private ElementScope PreviewTabletButton => Scope.FindId("preview_tablet_button"); // кнопка "Показать возможный планшет"
        private ElementScope PreviewTripodButton => Scope.FindId("preview_tubes_button"); // кнопка "Показать возможный штатив"
        private ElementScope PreviewWindowTitle => Scope.FindCss("span.k-window-title");
        private ElementScope ToFormBatchButton => Scope.FindId("to_form_batch_button"); // кнопка "Сформировать бэтч"
        private ElementScope CancelTOFormBatch => Scope.FindId("hide_batch_preview_button"); // кнопка "Отмена" в окне предварительного просмотра бэтча
        private ElementScope PreviewBatchMessage => Scope.FindXPath("//div[@class='toast-message' and .='Превью бэтча получено']"); // сообщение "Превью Бэтча получено"
        private ElementScope WarningCanNotForming => Scope.FindXPath("//div[@class='toast-title' and contains (text(),'Предупреждение')]"); // предупреждение: нет образцов для формирования бэтча
        private ElementScope BatchHaveFormedMessage => Scope.FindXPath("//div[@class='toast-message' and .='Бэтч сформирован']"); //сообщение с текстом Бэтч сформирован
        private ElementScope FirstSampleInListForForming => Scope.FindXPath("(//span[contains(text(),'01-')])[1]"); // нарвая проба в списке для формирования бэтча

        public List<ElementScope> ListOfAmplificationProgramms => Scope.FindAllXPath("//p[contains(text(),'Программа амплификации')]").Select(i => (ElementScope)i).ToList(); // получить список программ аплификации
        


        // actions:
        public void ClickCreateTablet() // Нажать на кнопку "Показать возможный планшет"
        {
            PreviewTabletButton.Click();
        }

        public void ClickCreateTripod() // Нажать на кнопку "Показать возможный штатив"
        {
            PreviewTripodButton.Click();
        }

        public bool AssertWindowTitle() // Проверка, что диалоговое окно предпросмотра и формирования отобразилось
        {
            if (PreviewWindowTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(4) }))
            {
                return true;
            }
            else return false;
        }


        public void ClickToFormBatchButton() // Нажать на кнопку "Сформировать бэтч"
        {
            ToFormBatchButton.Click();
        }
        
        public void ClickCancelTOFormBatch() // Нажать на кнопку "Отмена" в окне предварительного просмотра бэтча
        {
            CancelTOFormBatch.Click();
        }


        //cheking:
        public bool AssertPreviewBatchMessage() // проверка отображения сообщения "Превью Бэтча получено"
        {
            if (PreviewBatchMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(3) }))
            {
                return true;
            }
            else return false;
        }

        public bool AssertWarningCanNotForming() //проверка отображения предупреждения: нет образцов для формирования бэтча
        {
            if (WarningCanNotForming.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(3) }))
            {
                return false;
            }
            else return true;
        }

        public bool AssertBatchHaveFormedMessage() //проверка отображения предупреждения: бэтч сформирован
        {
            if (BatchHaveFormedMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(7) }))
            {
                return true;
            }
            else return false;
        }

        public bool AssertFirstSampleInListForForming() //проверка, есть ли на странице пробя для формирования
        {
            if (FirstSampleInListForForming.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) }))
            {
                return true;
            }
            else return false;
        }

        public void WaitForVisibility() // Нажать на кнопку "Показать возможный планшет"
        {
            PreviewTabletButton.WaitForVisibility();
        }
}
}






