using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coypu
{
    public static class ElementScopeExtension
    {
        public static void FillDropDownWith(this ElementScope dropdown, string option, Options coypuOptions=null) //  метод ищет в выпадающем списке все значения, содержащие город и берет из них первый
        {
            if (coypuOptions == null) coypuOptions = new Options() {TextPrecision=TextPrecision.Exact,ConsiderInvisibleElements=false };
            //var optionToSelect = dropdown.FindXPath($".//li[.='{option}']");
            Thread.Sleep(900);
            var optionsToSelect = dropdown.FindAllXPath($".//li[contains(.,'{option}')]");
            var optionToSelect = optionsToSelect.First(f=>f.Text == option);
            optionToSelect.Hover();
            Thread.Sleep(200);
            optionToSelect.Click(coypuOptions);
        }


        
            public static void CheckListElementExist(this ElementScope dropdown, string option, Options coypuOptions = null) // метод проверяет существует ли в списке город
            {
            if (coypuOptions == null) coypuOptions = new Options() { TextPrecision = TextPrecision.Exact, ConsiderInvisibleElements = false };
            //var optionToSelect = dropdown.FindXPath($".//li[.='{option}']");
            Thread.Sleep(900);

            //var optionsToSelect = dropdown.FindAllXPath($".//li[contains(.,'{option}')]");
            var optionsToSelect = dropdown.FindXPath($"//li[.='{option}']");

            optionsToSelect.Hover();
            Thread.Sleep(200);
            optionsToSelect.Click(coypuOptions);
        }





            public static void WaitForVisibility(this ElementScope element)
        {
            while (true)
            {
                try
                {                    
                    element.Hover(new Options { WaitBeforeClick = System.TimeSpan.FromMilliseconds(350) });
                    return;
                }
                catch (Exception)
                {
                }
            }
        }
        public static void WaitForClickability(this ElementScope element)
        {
            while (true)
            {
                try
                {
                    element.Click();
                    return;
                }
                catch (Exception)
                {
                    Debug.WriteLine("Элемент заблокирован. Пытаемся кликнуть...");
                }
            }
        }

        public static void WaitForAvailability(this ElementScope element)
        {
            while (element.Disabled)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
