using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace _365AutomatedTests.UIObjects.Pages
{
    [Binding]

    class BDSettingsDevicePage : Page
    {
        public BDSettingsDevicePage() : base()
        {
        }
        private ElementScope CheckLightStandBtn => Scope.FindXPath("//td[.='Световая подставка']/..//a[contains(@class,'Проверить')]");//Кнопка Проверить
        public void ClickLightStand()
        {
            int tryCount = 0;
            for (int i = 0; i < 400; i++)
            {
                if (CheckBtnIsPresent())
                {
                    while (true)
                    {
                        if (CheckLightStandBtn.Text == "Проверить") break;
                        Thread.Sleep(100);                        
                        tryCount++;
                        //if (tryCount > 600000) throw new Exception("After 600 seconds button is still 'Cancel' instead of 'Check'");
                    }                   
                    CheckLightStandBtn.Click();
                }
                string date = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss");
                Debug.WriteLine("Прошла проверка: " + i + " в "+ date);
            }
                                
        }



        private bool CheckBtnIsPresent()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    return CheckLightStandBtn.Exists();
                }
                catch (System.Exception e)
                {
                    count++;
                    if (count > 100) break;
                }
            }
            return false;
        }
    }

}

