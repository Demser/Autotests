using _365AutomatedTests.Framework.Generic;
using Coypu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.UIObjects.Pages
{
    class TerminalSettingPage : Page
    {
        public TerminalSettingPage() : base()
        {
        }
        private ElementScope FieldToSetTermID => Scope.FindXPath("//input[@await-barcode='vm.awaitingTerminalCode']");
        private ElementScope TitleForChooseTerminalName => Scope.FindXPath("//h2[.='Установка терминала']"); // заголовок выбора терминала
        private ElementScope WrongName(string termID) => Scope.FindXPath($"//p[contains(text(),'{termID} не найден')]"); //ошибка с некорректным терминалом
        private ElementScope LinkWitnThisTerminalName(string _) => Scope.FindXPath($"//b[contains(text(),{_})]");
        private ElementScope LinkDefaultTerminalName => Scope.FindXPath("//b[.='не установлен ']");
        private ElementScope TerminalChangeLink => Scope.FindXPath("//span[@ng-click='vm.needSetTerminal()']"); //ссылка Терминал в верхнем праавом углу

        //проверить, что терминал не выбран
        public bool AssertLinkDefaultTerminalName()
        {
            if (LinkDefaultTerminalName.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        // проверить наличие поля ввода терминала и заголовка
        public bool AssertForChooseTerminalName()
        {
            if (FieldToSetTermID.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) }) || TitleForChooseTerminalName.Exists(new Options { Timeout = System.TimeSpan.FromSeconds(20) })) return true;
            else return false;
        }

        // Метод для установки терминала без подтверждения
        public void SetTerminalWithoutSave(string termID)
        {
            LinkDefaultTerminalName.Click();
            FieldToSetTermID.SendKeys(termID);
        }

        //проверка наличия сообщения о вводе несуществующего терминала
        public bool AssertWrongNameOfTerminal(string _)
        {
            if (WrongName(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        //проверка на отображение имении терминало под заданым ID
        public bool AssertLinkWitnThisTerminalName(string _)
        {
            if (LinkWitnThisTerminalName(_).Exists(new Options { Timeout = System.TimeSpan.FromSeconds(2) })) return true;
            else return false;
        }

        public void WaitAfterRefresh()
        {
            TerminalChangeLink.WaitForClickability();
        }
    }
}
