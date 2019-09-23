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
    class BDDictTestPage : Page
    {
       public BDDictTestPage () : base()
        {
        }
        private ElementScope AddTestBtn => Scope.FindXPath("//a[.='Добавить']");
        private ElementScope RefreshTableIcon => Scope.FindCss("span.k-icon.k-i-refresh");
        private ElementScope CountOfTest => Scope.FindXPath("//span[.='Отображены записи 1 - 1 из 1']");
        private ElementScope CountOfFewTest(int _) => Scope.FindXPath($"//span[.='Отображены записи 1 - {_} из {_}']");


        // Кликнуть на кнопку Добавить
        public void ClickAddTestBtn()
        {
            AddTestBtn.Click();
        }
        // Кликнуть на значок обновления страницы
        public void CheckCountOfTests()
        {
            RefreshTableIcon.Click(new Options() { WaitBeforeClick = TimeSpan.FromSeconds(2) });
            RefreshTableIcon.Click(new Options() { WaitBeforeClick = TimeSpan.FromSeconds(2) });

        }
        // Проверка, что количество записей в таблице стало = 1
        public bool AssertCountOfTestInfo()
        {
            if (CountOfTest.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(6) })) return true;
            else return false;
        }

        // Проверка, что количество записей в таблице стало = n
        public bool AssertCountOfFewTests(int _)
        {
            if (CountOfFewTest(_).Exists()) return true;
            else return false;
        }

    }
}
