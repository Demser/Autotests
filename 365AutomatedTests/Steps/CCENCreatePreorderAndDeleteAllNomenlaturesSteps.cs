using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.UIObjects.Pages;
using System;
using TechTalk.SpecFlow;
using System.Linq;
using TechTalk.SpecFlow.Assist;
using _365AutomatedTests.Framework;
using static NUnit.Framework.Assert;
using Coypu;
using NUnit.Framework;
using System.Diagnostics;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CCENCreatePreorderAndDeleteAllNomenlaturesSteps
    {
        ElementScope SecondTab;
        ElementScope SecondTab1;
        [When(@"I create simple preorder with ""(.*)"" , ""(.*)"" , ""(.*)"" nomenclatures and get preorders nubmer")]
        public void WhenICreateSimplePreorderWithNomenclaturesAndGetPreordersNubmer(string hxid1, string hxid2, string hxid3)
        {
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            cCENFilterPanelPage.BeforeSetCity();
            cCENFilterPanelPage.SetSPB();
            System.Threading.Thread.Sleep(3000);
            // принудительное снятие чекбокса мобильный выезд, по причине бага 1076.
            cCENFilterPanelPage.PanelMobileCheckbox.Uncheck(); // "УБРАТЬ UNCHECK ПОСЛЕ ФИКСА БАГА CCEN-1076"
            Debug.WriteLine("УБРАТЬ UNCHECK ПОСЛЕ ФИКСА БАГА CCEN-1076");
            // Debug.WriteLine(" Принудительный UNCHECK Мобильного выезда отключен.");

            CCENNomenclaturesPage cCENNomenclaturesPage = new CCENNomenclaturesPage();
            cCENNomenclaturesPage.SearchSomeHXID(hxid1);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid1);
            cCENNomenclaturesPage.SearchSomeHXID(hxid2);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid2);
            cCENNomenclaturesPage.SearchSomeHXID(hxid3);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid3);

            CCENDelayConfirmWindow cCENDelay = new CCENDelayConfirmWindow();
            var a = cCENDelay.AssertConfirmDialogWindow();
            if (a)
            {
                cCENDelay.AddDelayedNomenclature();
            }
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.GoToCartTab();
            cCENCartPage.ChooseKarpovkaDC();
            IsTrue(cCENCartPage.AssertSaveButtonIsUnlock(), "Кнопка Сохранить не активировалась за долгое время. Проблемы с сайтом");
            CCENMainPage cCENMainPage = new CCENMainPage();
            System.Threading.Thread.Sleep(5500);
            IsTrue(cCENMainPage.AssertGetPreorderNumber(), "Не удалось сохранить предзаказ, либо окно с номером еще не подгрузилось");
            //PreorderId = cCENMainPage.NumberOfPreorder;
            cCENMainPage.ClosedPreorderNumberWindow();
            System.Threading.Thread.Sleep(1500);
        }

        [Then(@"I open preorder for edit and delete all nomenclatures from cart")]
        public void ThenIOpenPreorderForEditAndDeleteAllNomenclaturesFromCart()
        {
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            preOrderJournalPage.OpenForEdit();
            System.Threading.Thread.Sleep(2000);
            CommonSteps common = new CommonSteps();
            SecondTab = common.ThenTheTabWithTitleShouldBeOpened("Edit");
            System.Threading.Thread.Sleep(2000);
            CCENCartPage cCENCartPage = new CCENCartPage(SecondTab);
            cCENCartPage.GoToCartTab();
            cCENCartPage.DeleteAllHXIDsFromCart();
            //IsTrue(cCENCartPage.AssertEmptyCart(), "Корзина не пустая");
        }


        [Then(@"I see empty cart")]
        public void ThenISeeEmptyCart()
        {
            CommonSteps common = new CommonSteps();
            SecondTab1 = common.ThenTheTabWithTitleShouldBeOpened("Edit");
            CCENCartPage cCENCartPage = new CCENCartPage(SecondTab1);
            IsTrue(cCENCartPage.AssertEmptyCart(), "Корзина не пустая");
        }
    }
}
