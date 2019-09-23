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
    class BDSettingsLaboratoriesPage : Page
    {
        public BDSettingsLaboratoriesPage() : base()
        {
        }
        private ElementScope BDSettingsLaboratoriesPageTitle => Scope.FindXPath("//h3[.='Лаборатории']"); //заголовок
        private ElementScope BDAddLaboratoryBtn => Scope.FindXPath("//a[.='Добавить']");//кнопка Добавить

        // private ElementScope BDLaboratoriesListColumnName => Scope.FindXPath("//a[.='Название']");//колонка Название лаборатории
        // private ElementScope BDLaboratoriesListColumnDescription => Scope.FindXPath("//a[.='Описание']");//колонка Описание лаборатории

        private ElementScope BDEditLaboratoryBtn => Scope.FindXPath("//*[@id='page - content']/section/div/div[3]/table/tbody/tr[1]/td[3]/a[1]");//кнопка Изменить для первой лаборатории в списке
       // private ElementScope BDDeleteLaboratoryBtdn => Scope.FindButton("//a[.='Удалить']")[1];//кнопка Удалить. Надо будет отредактировать

        private ElementScope RefreshLaboratoriesTableIcon => Scope.FindXPath("//span[@class = 'k-icon k-i-refresh']");//значок Обновить (таблицу)



    }
}
