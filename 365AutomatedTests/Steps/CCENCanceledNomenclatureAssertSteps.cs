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

namespace _365AutomatedTests
{
    [Binding]
    public class CCENCanceledNomenclatureAssertSteps
    {
        public static string PreorderId = "";

        [When(@"I create simple preorder with ""(.*)"" nomenclature and get preorders nubmer")]
        public void WhenICreateSimplePreorderWithNomenclatureAndGetPreordersNubmer(string hxid)
        {
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            cCENFilterPanelPage.BeforeSetCity();
            cCENFilterPanelPage.SetSPB();
            CCENNomenclaturesPage cCENNomenclaturesPage = new CCENNomenclaturesPage();
            cCENNomenclaturesPage.AddSomeNomenctatureToCartField("02-001").WaitForVisibility();
            cCENNomenclaturesPage.SearchSomeHXID(hxid);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid);

            // принудительное снятие чекбокса мобильный выезд, по причине бага 1076.
            cCENFilterPanelPage.PanelMobileCheckbox.Uncheck(); // "УБРАТЬ UNCHECK ПОСЛЕ ФИКСА БАГА CCEN-1076"
            Debug.WriteLine("УБРАТЬ UNCHECK ПОСЛЕ ФИКСА БАГА CCEN-1076");
            // Debug.WriteLine(" Принудительный UNCHECK Мобильного выезда отключен.");

            CCENDelayConfirmWindow cCENDelay = new CCENDelayConfirmWindow();
            var a = cCENDelay.AssertConfirmDialogWindow();
            if (a)
            {
                cCENDelay.AddDelayedNomenclature();
            }

            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.GoToCartTab();
            cCENCartPage.DiagnosicCenterItemsCollection.WaitForVisibility();
            cCENCartPage.ChooseKarpovkaDC();
            //IsTrue(cCENCartPage.AssertSaveButtonIsUnlock(),"Кнопка Сохранить не активировалась за долгое время. Проблемы с сайтом");

            System.Threading.Thread.Sleep(4000);            
            cCENCartPage.SavePreorderButton.WaitForAvailability();
            IsTrue(cCENCartPage.AssertSaveButtonIsUnlock(), "метод не успел отработать");

            CCENMainPage cCENMainPage = new CCENMainPage();
            cCENMainPage.PreorderNumberSpan.WaitForVisibility();
            IsTrue(cCENMainPage.AssertGetPreorderNumber(), "Не удалось сохранить предзаказ, либо окно с номером еще не подгрузилось");
            PreorderId = cCENMainPage.NumberOfPreorder;
            cCENMainPage.ClosedPreorderNumberWindow();
            System.Threading.Thread.Sleep(1500);
        }

        [When(@"I try to create preorder with ""(.*)"" nomenclature ""(.*)"" and check message")]
        public void WhenITryToCreatePreorderWithNomenclatureAndCheckMessage(string status, string hxid)
        {
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            cCENFilterPanelPage.BeforeSetCity();
            cCENFilterPanelPage.SetSPB();
            //System.Threading.Thread.Sleep(3000);
            CCENNomenclaturesPage cCENNomenclaturesPage = new CCENNomenclaturesPage();
            cCENNomenclaturesPage.SearchSomeHXID(hxid);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid);

            switch (status)
            {
                case "delayed":
                    CCENDelayConfirmWindow cCENDelay = new CCENDelayConfirmWindow();
                    var a = cCENDelay.AssertConfirmDialogWindow();
                    if (a)
                    {
                        cCENDelay.AddDelayedNomenclature();
                    }
                    CCENCartPage cCENCartPage = new CCENCartPage();
                    cCENCartPage.GoToCartTab();
                    IsTrue(cCENCartPage.AssertDelayedText(), "Предупреждение о задержанной номенклатуре отсутствует");
                    break;

                case "canceled":
                    IsTrue(cCENNomenclaturesPage.AssertNomenclatureWasCanceled(), "Предупреждение об отмененной номенклатуре отсутствует");
                    break;
            }
        }


        [When(@"I find preorder by number in journal")]
        public void WhenIFindPreorderByNumberInJournal()
        {
            //var a = PreorderId;
            PreOrderJournalPage preOrderJournalPage = new PreOrderJournalPage();
            // preOrderJournalPage.ClickSearchByNumberButton();
            // preOrderJournalPage.FillPreOrderNumberForm(a);
            // preOrderJournalPage.StartSearchByPreOrderNumber();
            preOrderJournalPage.OpenFirstRowInTable();
            System.Threading.Thread.Sleep(3500);
            //preOrderJournalPage.

        }




    }
}

