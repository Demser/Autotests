using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using _365AutomatedTests.UIObjects.Pages.Inner365.PreOrdersJournal;


namespace _365AutomatedTests.Steps.PreOrdersJournal
{
    [Binding]
    public class SearchPreOrderByNumberSteps
    {
        [Given(@"I login as CallCenter user ""(.*)"", ""(.*)""")] //логин юзером CallCenter
        public void GivenILoginAsCallCenterUser(string login, string password)
        {
            LoginPage Login = new LoginPage();
            Login.LoginAsUser(login, password);
        }
        
        [Given(@"I open the PreOrdersJournal page")]
        public void GivenIOpenThePreOrdersJournalPage()
        {
            MainPage mainPage = new MainPage();
            mainPage.GoToPreOrdersJournal();
        }
        
        [When(@"I click the search-by-number button")]
        public void WhenIClickTheSearch_By_NumberButton() //переход на таб Поиск по номеру
        {
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            preOrderJournalPage.ClickSearchByNumberButton();
        }
/*
        [Then(@"the search-by-number screen opens")]
        public void ThenTheSearch_By_NumberScreenOpens()
        {
            ScenarioContext.Current.Pending();
        }
*/

        [When(@"I enter the preorder number ""(.*)"" to the number field")] //заполнение поля Номер предзаказа
        public void WhenIEnterThePreorderNumberToTheNumberField(string preOrderNumber)
        {
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            preOrderJournalPage.FillPreOrderNumberForm(preOrderNumber);
        }

        [When(@"I click the search button")]
        public void WhenIClickTheSearchButton() //нажать кнопку Поиск на табе "По номеру"
        {
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            preOrderJournalPage.StartSearchByPreOrderNumber();
        }
        

        
        [Then(@"The preorder with the correct number is displayed in the results grid")]
        public void ThenThePreorderWithTheCorrectNumberIsDisplayedInTheResultsGrid()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The results grid consists of only one order")]
        public void ThenTheResultsGridConsistsOfOnlyOneOrder()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
