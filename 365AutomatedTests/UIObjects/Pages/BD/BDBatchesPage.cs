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
    class BDBatchesPage : Page
    {
        public BDBatchesPage() : base()
        {
        }
        public BDBatchesPage(ElementScope scope) : base(scope)
        {
        }
        //Пустая таблица:
        private ElementScope EmptyTableTitle => Scope.FindXPath("//span[.='Нет записей для отображения']");
        // Записать в переменную самое верхнее значение id из таблицы
        private ElementScope TopIdFromTable => Scope.FindXPath("//*[@id='page-content']/section/div/div[2]/div[2]/table/tbody/tr[1]/td[1]/span");
        public string TextTopIdFromTable => TopIdFromTable.Text;
        // Записать в переменную значение дочернего бэтча в постановке(два раза клинкуть по фильтру, найти верхний элемент и записать его текст)
        private ElementScope ParentItemFilter => Scope.FindXPath("//a[contains(text(),'Родитель')]");

        private ElementScope ShowContentBatchWithIdButton(string _) => Scope.FindId($"{_}_showContentButton"); //нажать на кнопку Содержимое для бэтча с заданным ID 
        private ElementScope BatchWithId(string id) => Scope.FindXPath($"//td[.='{id}']"); // найти бэтч с заданными ID
        public ElementScope BatchWithIdAndStatus(string id, string status) => Scope.FindXPath($"//td[.='{id}']//..//td[.='{status}']"); // найти бэтч с заданными ID и статусом

        //статус "Отправлен на прибор"
        private ElementScope StatusSentToDevice (string _) => Scope.FindXPath($"//*[.='{_}']");
        private ElementScope ResultsWasCheckedIcon => Scope.FindXPath("//a[@title='Результаты проверены']");
        private ElementScope ResultsWasCheckedSpan => Scope.FindXPath("//span[contains(text(),'Результаты проверены')]");
        private ElementScope ResultsWasCheckedConfirm => Scope.FindXPath("//button[.='ОК']");
        //найти кнопку по заданному статусу и поредковому номеру (если таких несколько)
        private ElementScope ContentOfBatchForAnyStatusButton (string a,int b) => Scope.FindXPath($"(//td[.='{a}']//..//a[@title='Содержимое'])[{b}]");
        private ElementScope ButtonContentOfBatch => Scope.FindXPath("(//td[.='Требует валидации']//..//a[@title='Содержимое'])[1]");
        private ElementScope ButtonResultsReadyToSend => Scope.FindXPath("(//td[.='Результаты готовы к отправке']//..//a[@title='Отправить результаты'])[1]");
        private ElementScope ResultsReadyToSendSpan => Scope.FindXPath("//span[contains(text(),'Экспорт в LW')]");
        private ElementScope ResultsReadyToSendConfirm => Scope.FindXPath("//button[.='Экспортировать']");
        private ElementScope SucceedMessage => Scope.FindXPath("//div[.='Успешно']");
        private ElementScope ChildBatch(string _) => Scope.FindXPath($"//td[.='{_}']//.. //span[contains(text(),'C-')]"); // чайлд бэтч с указанным ID
        private ElementScope SearchButton => Scope.FindXPath("//button[.='Поиск']");

        public ElementScope IdBatchNeedsValidation(string _) => Scope.FindXPath($"//td[.='Требует валидации']//.. //span[.='{_}']"); // бэтч со статусом "Требует валидации"

        // взять список бэтчей с заданным статусом
        public List<ElementScope> ListOfBatchesWithThisStatus(string _) => Scope.FindAllXPath($"//td[.='{_}']").Select(i => (ElementScope)i).ToList();



        public bool AssertContentOfBatchButton() 
        {
            if (ButtonContentOfBatch.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(10) })) return true;
            else return false;
        }

        public void OpenContentOfFirstNeedValidateTest()
        {
            ButtonContentOfBatch.Click();
        }

        public void OpenContentOfBatchForAnyStatus(string a, int b)
        {
            ContentOfBatchForAnyStatusButton(a, b).Click();
        }

        public void GetTopChildBatchId()
        {
            ParentItemFilter.Click();
            System.Threading.Thread.Sleep(1000);
            ParentItemFilter.Click();
            System.Threading.Thread.Sleep(1000);

        }

        public bool AssertStatusSentToDevice(string status)
        {
            if (StatusSentToDevice(status).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(10) })) return true;
            else return false;
        }

        public bool TheTableIsEmpty()
        {
            if (EmptyTableTitle.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }


        public void ClickIconResultsWasChecked()
        {
            ResultsWasCheckedIcon.Click();
        }
        public bool AssertDialogWindowConfirmIsOpen()
        {
            if (ResultsWasCheckedSpan.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }
        public void ClickConfirmButtonResultsWasChecked()
        {
            ResultsWasCheckedConfirm.Click();
        }



        public void ClickIconResultsReadyToSend()
        {
            ButtonResultsReadyToSend.WaitForAvailability();
            ButtonResultsReadyToSend.Click();
        }

        public bool AssertDialogWindowReadyToSendIsOpen()
        {
            if (ResultsReadyToSendSpan.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }
        public void ClickConfirmReadyToSendButton()
        {
            ResultsReadyToSendConfirm.Click();
        }
        public bool AssertSucceedMessage()
        {
            if (SucceedMessage.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(3) })) return true;
            else return false;
        }

        public void ClickToShowContentBatchWithIdButton(string _)
        {
            ShowContentBatchWithIdButton(_).Click();
        }

        public bool AssertChildBatch(string _) // чайлд бэтч с таким ID существует
        {
            if (ChildBatch(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(10) })) return true;
            else return false;
        }

        public void WaitSearchButtonClickability()
        {
            SearchButton.WaitForClickability();
        }

        public bool AssertBatchWithId(string id)
        {
            if (BatchWithId(id).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(10) })) return true;
            else return false;
        }

    }

}
