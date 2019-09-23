using Coypu;
using _365AutomatedTests.Framework.Generic;



namespace _365AutomatedTests.UIObjects.Pages
{
    public class PreOrderJournalPage : Page

    {
        public PreOrderJournalPage() : base()
        {
        }
        private ElementScope SearchByNumberTabButton => Scope.FindXPath("//a[.='По номеру предварительного заказа']");//кнопка выбора поиска по номеру ПЗ
        private ElementScope SearchByOtherParamsTabButton => Scope.FindXPath("//a[.='По прочим данным']");//кнопка поиска по прочим параметрам

        private ElementScope PreOrderNumberForm => Scope.FindId("FullNumber");//поле ввода номера предзаказа для поиска

        private ElementScope SearchByPreorderNumberButton => Scope.FindXPath("//button[@class='btn btn-primary control']");//кнопка поиска по номеру предзаказа

        private ElementScope SearchByOtherParamsButton => Scope.FindXPath("//button[@class='btn btn-primary' and contains(text(),'Поиск')]");//кнопка поиска по прочим параметрам
        private ElementScope CountOfFoundPreOrders => Scope.FindXPath(".//tbody[@role='rowgroup']/tr[@role='row']");

        private ElementScope NumberOfPreorderInTheGrid(string _) => Scope.FindXPath($"//td[.='{_}']");

        private ElementScope OpenForEditButton => Scope.FindXPath("//button[.='Загрузить для редактирования']");
        private ElementScope OpenForReplaceButton => Scope.FindXPath("//button[.='Повторить предзаказ']");
        private ElementScope OpenForSettingsButton => Scope.FindXPath("//button[.='Подробности']");
        private ElementScope OpenForHistoryButton => Scope.FindXPath("//button[.='Открыть историю']");

        public ElementScope FirstRowInTable => Scope.FindXPath("//tr[@class='k-master-row']");

        public void OpenFirstRowInTable()
        {
            FirstRowInTable.Click();
        }

        public void ClickSearchByNumberButton() //переход на форму поиска по номеру
        {
            SearchByNumberTabButton.Click();
        }

        public void FillPreOrderNumberForm(string preOrderNumber) //заполнение поля "Номер предзаказа"
        {
            PreOrderNumberForm.SendKeys(preOrderNumber);
        }

        public void StartSearchByPreOrderNumber()
        {
            SearchByPreorderNumberButton.Click(); //старт поиска но номеру предзаказа
        }

        public void SelectedFirstPreorder(string _)
        {
            NumberOfPreorderInTheGrid(_).Click();
        }

        public void OpenForEdit()
        {
            OpenForEditButton.Click();
        }

        public void OpenForReply()
        {
            OpenForReplaceButton.Click();
        }

    }
}