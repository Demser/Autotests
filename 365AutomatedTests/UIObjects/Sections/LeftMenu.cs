using _365AutomatedTests.Framework.Generic;
using Coypu;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.UIObjects.Sections
{
    public class LeftMenu : Section

    {
        public LeftMenu(string css = ".container") : base (css)
        {
        }

        private ElementScope MenuItemLearning => Scope.FindXPath("//p[.='Обучение']");

        private ElementScope SubMenuItemSendLearningRequest => Scope.FindXPath("//p[.='Подать заявку на обучение']");

        private ElementScope SubMenuItemHelixConsumables => Scope.FindXPath("//p[.='Расходные материалы Хеликс']");

        private ElementScope SubMenuItemContainerMarkingRules => Scope.FindXPath("//p[.='Правила маркировки контейнеров с биологическим материалом']");

        private ElementScope SubMenuItemLearningRequestsLog => Scope.FindXPath("//p[.='Журнал заявок']");

        private ElementScope MenuItemFeedback => Scope.FindXPath("//p[.='Обратная связь']");

        private ElementScope SubMenuItemOrderExpressDelivery => Scope.FindXPath("//p[.='Заказать доставку курьером']");

        private ElementScope SubMenuItemOrderConsumables => Scope.FindXPath("//p[.='Заказать расходные материалы']");

        private ElementScope SubMenuItemAskViaEmail => Scope.FindXPath("//p[.='Задать вопрос по e-mail']");

        private ElementScope MenuItemCatalogue => Scope.FindXPath("//p[.='Каталог']");

        private ElementScope SubMenuItemAnalysisList => Scope.FindXPath("//p[.='Список исследований']");

        private ElementScope SubMenuItemModuleCallCentreCatalogue => Scope.FindXPath("//p[.='Каталог для КЦ (модуль)']");

        private ElementScope SubMenuItemPreordersLog => Scope.FindXPath("//p[.='Журнал предварительных заказов']");

        private ElementScope SubMenuItemTemplatesRegister => Scope.FindXPath("//p[.='Журнал шаблонов']");

        private ElementScope SubMenuItemExceptionsList => Scope.FindXPath("//p[.='Список исключений']");

        private ElementScope SubMenuItemMobileGroupsRoutesLog => Scope.FindXPath("//p[.='Журнал маршрутов моб. выездов']");

        private ElementScope MenuItemMISIntegration => Scope.FindXPath("//p[.='Интеграция МИС']");

        private ElementScope SubMenuItemMISConformityEditor => Scope.FindXPath("//p[.='Редактор соответствий МИС']");

        private ElementScope SubMenuItemMISSettings => Scope.FindXPath("//p[.='Настройки МИС']");

        private ElementScope SubMenuItemMISOrdersRegister => Scope.FindXPath("//p[.='Журнал заказов']");

        private ElementScope MenuItemMedicalrepresentatives => Scope.FindXPath("//p[.='Медицинские представители']");

        private ElementScope MenuItemNewAnalysis => Scope.FindXPath("//p[.='Новые исследования']");

        private ElementScope MenuItemLoyaltyManagement => Scope.FindXPath("//p[.='Система лояльности']");

        private ElementScope SubMenuItemDiscountCardsGenerator => Scope.FindXPath("//p[.='Генератор карт скидок']");

        private ElementScope SubMenuItemModuleDiscountCardsGenerator => Scope.FindXPath("//p[.='Генератор карт скидок (модуль)']");

        private ElementScope SubMenuItemCustomerCardsRegister => Scope.FindXPath("//p[.='Реестр карт клиентов']");

        private ElementScope SubMenuItemModuleCustomerCardsRegister => Scope.FindXPath("//p[.='Реестр карт клиентов (модуль)']");

        private ElementScope SubMenuItemAccountsTransactions => Scope.FindXPath("//p[.='Операции по счетам']");

        private ElementScope SubMenuItemModuleAccountsTransactions => Scope.FindXPath("//p[.='Операции по счетам (модуль)']");

        private ElementScope SubMenuItemMasterAccountsFlowOfFundsPerPeriod => Scope.FindXPath("//p[.='Движение средств по основным счетам за период']");

        private ElementScope SubMenuItemModuleMasterAccountsFlowOfFundsPerPeriod => Scope.FindXPath("//p[.='Движение средств по основным счетам за период (модуль)']");

        private ElementScope SubMenuItemLoyaltyManager => Scope.FindXPath("//p[.='Менеджер лояльности']");

        private ElementScope SubMenuItemModuleLoyaltyManager => Scope.FindXPath("//p[.='Менеджер лояльности (модуль)']");

        private ElementScope SubMenuItemLoyaltyPartners => Scope.FindXPath("//p[.='Партнеры лояльности']");

        private ElementScope SubMenuItemModuleLoyaltyPartners => Scope.FindXPath("//p[.='Партнеры лояльности (модуль)']");

        private ElementScope MenuItemRegistration => Scope.FindXPath("//p[.='Регистрация']");

        private ElementScope MenuItemCustomerReviews => Scope.FindXPath("//p[.='Отзывы клиентов']");

        private ElementScope SubMenuItemDCServiceQualitySurvey => Scope.FindXPath("//p[.='Опрос по качеству обслуживания в ДЦ']");

        private ElementScope SubMenuItemMSServiceQualitySurvey => Scope.FindXPath("//p[.='Опрос по качеству обслуживания в МС']");

        private ElementScope SubMenuItemDCExtendedReport => Scope.FindXPath("//p[.='Расширенный отчет по ДЦ']");

        private ElementScope SubMenuItemMCExtendedReport => Scope.FindXPath("//p[.='Расширенный отчет по МС']");

        private ElementScope MenuItemResults => Scope.FindXPath("//p[.='Результаты']");

        private ElementScope SubMenuItemViewResults => Scope.FindXPath("//p[.='Просмотр результатов']");

        private ElementScope SubMenuItemResultsReprint => Scope.FindXPath("//p[.='Повторная печать']");

        private ElementScope SubMenuItemConclusions => Scope.FindXPath("//p[.='Заключения']");

        //here is the place for Diagnostics description


        private ElementScope SubMenuItemEditPackages => Scope.FindXPath("//p[.='Редактирование пакетов']");

    }
}
