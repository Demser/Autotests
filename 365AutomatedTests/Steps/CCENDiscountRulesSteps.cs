using System;
using TechTalk.SpecFlow;
using _365AutomatedTests.Framework;
using _365AutomatedTests.UIObjects.Pages;
using Coypu;
using System;
using TechTalk.SpecFlow;
using static NUnit.Framework.Assert;
using _365AutomatedTests.Framework.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CCENDiscountRulesSteps
    {
        [Then(@"I check that discount is ""(.*)"" percent")]
        public void ThenICheckThatDiscountIsPercent(int per)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            var fullPriceText = cCENCartPage.FullPriceTitle.Text;
            var discountText = cCENCartPage.DiscountTitle.Text;
            var priceWithDiscountText = cCENCartPage.PriceWithDiscountTitle.Text;
            Debug.WriteLine("Общая сумма: "+fullPriceText+"  Скидка: " +discountText+"  Итого к оплате: "+priceWithDiscountText);
            double fullPriceDouble = Convert.ToDouble(fullPriceText);
            double discountDouble = Convert.ToDouble(discountText);
            double priceWithDiscountDouble = Convert.ToDouble(priceWithDiscountText);
            if (discountDouble == 0)
            {
                throw new Exception("Скидка равна нулю. Для этого ДЦ нет дисконтных правил. Выберите другой ДЦ");       
            }
            double persent = 100- (100 * priceWithDiscountDouble / fullPriceDouble); // расчет скидки с плавающей точкой
            int persentInt = Convert.ToInt32(persent); // округление скидки до формата int
            IsTrue((per==persentInt),"Скидка не совпадает"); // сравниваем полученное значение с параметром из сценария
        }
    }
}
