using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _365AutomatedTests.Framework.Generic;
using _365AutomatedTests.Models;
using _365AutomatedTests.Steps;
using Coypu;
using OpenQA.Selenium;
using static NUnit.Framework.Assert;

namespace _365AutomatedTests.UIObjects.Pages
{
    class CCENCartPage : Page
    {
        public CCENCartPage() : base()
        {
        }
        public CCENCartPage(ElementScope scope) : base(scope)
        {
        }

        public ElementScope CartTab => Scope.FindId("ccen-main-tabs-cart");
        public ElementScope DiagnosticsCentersNotAvailble => Scope.FindId("ccen-cart-accounts-not-available-msg");
        public ElementScope DiagnosticCenterRadioButton(string _) => Scope.FindId($"ccen-cart-accounts-item-{_}-radio-btn");
        // public ElementScope DiagnosticCenterFromXPathButton(string _) => Scope.FindXPath($"//input[@id='ccen-cart-accounts-item-{_}-radio-btn'][0]");
        public ElementScope DiagnosticCenterRadioButtonByValue(string _) => Scope.FindXPath($"//input[@name='RealContractCode'and @value = '{_}']");
        public ElementScope DiagnosticCenterKarpovkaHardcode => Scope.FindId("ccen-cart-accounts-item-10963-radio-btn");
        public ElementScope SavePreorderButton => Scope.FindId("ccen-save-preorder-btn");
        public ElementScope SavePreorderButtonIsInactive => Scope.FindXPath("//button[@id='ccen-save-preorder-btn' and @disabled = 'disabled']");
        public ElementScope LogisticBlock => Scope.FindId("ccen-cart-items-content-item-logistic");
        public ElementScope LogisticInput => Scope.FindXPath("//input[@placeholder='Выберите услугу мобильного выезда']");
        public ElementScope LogisticOptionIcon => Scope.FindId("ccen-cart-items-content-item-logistic-is-valid");
        public ElementScope DelayedStrongText => Scope.FindXPath("//strong[contains(text(),'Задержка')]");
        public ElementScope DeleteAllHXIDsFromCartIcon => Scope.FindId("ccen-cart-items-footer-remove-all-btn");
        public ElementScope HxidPrice (string _) => Scope.FindId($"ccen - cart - items - content - item - {_} - price - content");
        public ElementScope DeleteHxidByNumberBtn(string HxidNumber) => Scope.FindId($"ccen-cart-items-content-item-{HxidNumber}-remove-btn");
        public ElementScope EmptyCartArea => Scope.FindId("ccen-cart-empty-msg-wrapper");
        public ElementScope ExecLongTitle (string _) => Scope.FindXPath($"//p[@id = 'ccen-cart-items-footer-days-content' and contains(.,'{_}')]");
        public ElementScope FullPriceTitle => Scope.FindId("ccen-cart-items-footer-price");
        string FullPriceTitleValue => FullPriceTitle.Value;
        public ElementScope DiscountTitle => Scope.FindId("ccen-cart-items-footer-discount");
        public ElementScope CartCounter => Scope.FindCss("#ccen-main-tabs-cart > a");
        public ElementScope HxidByNumberLoc (string _) => Scope.FindId($"ccen-cart-items-content-item-{_}-hxid");

        string DiscountTitleValue => DiscountTitle.Value;
        public ElementScope PriceWithDiscountTitle => Scope.FindId("ccen-cart-items-footer-price-with-discount");
        string PriceWithDiscountTitleValue => PriceWithDiscountTitle.Value;
        public ElementScope DiagnosicCenterItemsCollection => Scope.FindXPath("//div[@id='ccen-cart-accounts-items-collection'][1]");// первая строка из коллекций ДЦ(будет найдена, если будет отображаться хотя бы один пункт)
        public ElementScope TextOfRulePrepare(string _) => Scope.FindXPath($"//li[contains(text(),'{_}')]");
        public ElementScope LabelDiagnosticalCenterHasEquipment(int _) => Scope.FindXPath($"//div[@id='ccen-cart-accounts-item-{_}-has-equip']");
       
        public void GoToCartTab()
        {
            System.Threading.Thread.Sleep(1000);
            CartTab.Click();
        }

        public bool AssertExecLongValue(string text)
        {
            if (ExecLongTitle(text).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(5) })) return true;
            else return false;
        }

        public void CheckHxidByNumberExistsInCart(string HXID)
        {
            string hxidNmb = HxidByNumberLoc(HXID).Text;
            
            IsTrue(hxidNmb == $"[{HXID}]", $"Ошибка! В корзине нет добавленного исследования {HXID}");
         
        }

        public void CheckHxidByNumberMissingInCart(string HXID)
        {
            string hxidNmb = HxidByNumberLoc(HXID).Text;

            IsFalse(hxidNmb == $"[{HXID}]", $"Ошибка! В корзине есть удаленное исследование {HXID}"); 

        }

        public void DeleteHxidByNumber(string HxidNumber)
        {
            DeleteHxidByNumberBtn(HxidNumber).Click();
        }

        public void DeleteAllHXIDsFromCart()
        {
            DeleteAllHXIDsFromCartIcon.Click();
        }

        public bool AssertEmptyCart()
        {           
            if (EmptyCartArea.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool SaveButtonIsInactive() // используется в чеклисте персональных данных
        {
            if (SavePreorderButtonIsInactive.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }


        public void ChooseDiagnosticalCenter(string _)
        {
            //System.Threading.Thread.Sleep(1000);
            DiagnosticCenterRadioButton(_).Click();            
        }

        public void ChooseDiagnosticalCenterByValue(string _)
        {
            //System.Threading.Thread.Sleep(1000);
            DiagnosticCenterRadioButtonByValue(_).Click();
        }

        public void ChooseKarpovkaDC()
        {
            DiagnosticCenterKarpovkaHardcode.Click();
        }

        public bool AssertSaveButtonIsUnlock()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    SavePreorderButton.Click();
                    return true;
                }
                catch (System.Exception e)
                {
                    count++;
                    System.Threading.Thread.Sleep(100);
                    if (count > 200)  break;
                }
            }
            return false;
        }

        public void PressSaveButton()
        {
            SavePreorderButton.Click();
        }

        public bool AssertLogisticBlock()
        {
            if (LogisticBlock.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public bool AssertRuleIsPresent(string _)
        {
            if (TextOfRulePrepare(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

         public bool AssertDelayedText() // проверка что отображается подпись о задержке
         {
             if (DelayedStrongText.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
             else return false;
         }

        public bool AssertDCHasEquipment(int _) // проверка что ДЦ имеет флаг, обозначающий что в нем есть нужное оборудование
        {
            if (LabelDiagnosticalCenterHasEquipment(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public void ChooseLogisticOption(string _)
        {
            LogisticInput.Click();
            LogisticInput.SendKeys(_);
            System.Threading.Thread.Sleep(1000);
            LogisticInput.SendKeys(Keys.Enter);
            LogisticInput.SendKeys(Keys.Enter);
        }

        public bool DiagnosticalCenterIsEmpty() // проверить отсутсвие ДЦ в корзине 
        {
            if (DiagnosticsCentersNotAvailble.Exists()) return true;
            else return false;
        }

            public void CheckCartCounter(string counter)
        {

            if (CartCounter.Text == counter)
            {
                Console.WriteLine("Счетчик в карзине совпадает с указанным значением " + CartCounter.Text);
            }

            else
            {
                Console.WriteLine("Счетчик в карзине " + CartCounter.Text + " не совпадает с ожидаемым результатом" + counter);
                throw new System.ArgumentException("Счетчик в карзине " + CartCounter.Text + "не совпадает с ожидаемым результатом " + counter);
            }
        }
       
        
    }
}
