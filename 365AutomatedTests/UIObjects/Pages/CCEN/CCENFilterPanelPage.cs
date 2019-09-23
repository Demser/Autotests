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
using OpenQA.Selenium.Support.UI;
using static NUnit.Framework.Assert;

namespace _365AutomatedTests.UIObjects.Pages
{
    class CCENFilterPanelPage : Page
    {
        public ElementScope CityFilter => Scope.FindXPath("//input[@placeholder='Город']");
        public ElementScope CityFilterInputField => Scope.FindXPath("//*[@id='ccen-filter-panel-city-wrapper']/span/span/input");
        public ElementScope InsuranceFilter => Scope.FindXPath("//input[@placeholder='Страховые компании/КК']");
        public ElementScope InsuranceFilterClearBtn => Scope.FindCss("#ccen-filter-panel-insurance-wrapper > span > span > span.k-icon.k-clear-value.k-i-close");
        public ElementScope PanelMobileCheckbox => Scope.FindId("ccen-filter-panel-mobile-checkbox");
        public ElementScope PanelCITOCheckbox => Scope.FindId("ccen-filter-panel-cito-checkbox");
        public ElementScope FlagWithCard => Scope.FindXPath("//label[.='С картой']");
        public ElementScope FlagWithoutCard => Scope.FindXPath("//label[.='Без карты']");
        public ElementScope PanelNumberOfCardInput => Scope.FindId("ccen-filter-loyality-cart-number-input");
        public ElementScope PanelUploadCardBtn => Scope.FindId("ccen-filter-loyality-cart-apply-btn");
        public ElementScope PanelNumberOfFlaerInput => Scope.FindId("ccen-filter-loyality-flyer-input");
        public ElementScope PanelEmployeeCheckbox => Scope.FindId("ccen-filter-panel-employee-checkbox");
        public ElementScope IListOfCities => Scope.FindId("ccen-filter-panel-city-select_listbox"); // UL
        public ElementScope OpenDropDownListIcon => Scope.FindXPath("//*[@aria-controls='ccen-filter-panel-city-select_listbox']"); // открыть список городов
        public ElementScope OpenDropDownListIconforInsurance => Scope.FindXPath("//*[@aria-controls='ccen-filter-panel-insurance-select_listbox']"); // открыть список КК
        public ElementScope IListOfInsurance => Scope.FindId("ccen-filter-panel-insurance-select_listbox");
        public ElementScope NoCitiesData => Scope.FindXPath("//div[@class='k-nodata ng-scope']/div[.='Нет данных']");
        public ElementScope FirstElementOfCitiesLi => Scope.FindXPath("//*[@id='ccen-filter-panel-city-select_listbox']/li[@data-offset-index='0']");

        //public ElementScope CityInListElement => Scope.FindXPath("//li[@data-offset-index='9']");
        int index;
        public ElementScope CityInListElement => Scope.FindXPath("//li[@data-offset-index='" + index + "']");
        
        ElementScope NomenclatureTest => Scope.FindId("ccen-hxidlist-nomenclatures-grid");//грид с номенклатурами появляется только после выбора города
        ElementScope LoaderIsActive => Scope.FindXPath("//*[@id='ccen-main-loader'and @class='page-content__loader-cover']");
        ElementScope LoaderIsInactive => Scope.FindXPath("//*[@id='ccen-main-loader'and @class='page-content__loader-cover ng-hide']");

        public void BeforeSetCity()
        {
        OpenDropDownListIcon.Click();

        if (NoCitiesData.Exists())
                {
                FirstElementOfCitiesLi.WaitForVisibility();
                OpenDropDownListIcon.Click();
                CityFilter.SendKeys(Keys.Tab);
                //CommonSteps common = new CommonSteps();
                //common.RefreshPage();
            }
                else
                OpenDropDownListIcon.Click();
                CityFilter.SendKeys(Keys.Tab);
        }

        private Dictionary<string, ElementScope> _elementsWithCheckBox;

        public CCENFilterPanelPage() : base()
        {
            _elementsWithCheckBox = new Dictionary<string, ElementScope>
            {
                { "InsuranceFilter", InsuranceFilter },
                { "PanelMobileCheckbox", PanelMobileCheckbox },
                { "PanelCITOCheckbox", PanelCITOCheckbox }  ,
                { "PanelEmployeeCheckbox", PanelEmployeeCheckbox }               
            };
        }


        public void SetCity(string city)
        {
            OpenDropDownListIcon.Click();
            System.Threading.Thread.Sleep(3000);
            IListOfCities.FillDropDownWith(city); // /Найти, навести и кликнуть указанное значение из выпадающего списка
            NomenclatureTest.WaitForVisibility(); // Приминение универсального экстеншнс для проверки появления элемента
         
        }

        public void ChooseCity(string city)
        {
            CityFilterClear();
            CityFilterInputField.SendKeys(city);
            Debug.WriteLine("Вводим город " + city);          
            System.Threading.Thread.Sleep(3000);

            for (index = 0; index < 100; index++)
            {

                if (CityInListElement.Text == city)
                {
                    String cityName = CityInListElement.Text;
                    Debug.WriteLine("Вводим город " + city);
                    Debug.WriteLine("Берем название города из элемента = " + CityInListElement.Text);
                    CityInListElement.Hover().Click();
                    Debug.WriteLine("В списке найден город " + cityName + "c индексом index = " + index);
                    break;
                }
                else
                {
                    Debug.WriteLine("Не найден город = " + city + "c индексом index = " + index);

                }
              //  NomenclatureTest.WaitForVisibility();
            }
        }


        public void SetSPB()
        {
            CityFilter.Click();
            CityFilter.SendKeys("Санкт-Петербург г");
            System.Threading.Thread.Sleep(1000);
            CityFilter.SendKeys(Keys.Enter);
        }

        public void SetInsurance(string insurance)
        {
            OpenDropDownListIconforInsurance.Click();
            System.Threading.Thread.Sleep(300);
            IListOfInsurance.FillDropDownWith(insurance);
            //NomenclatureTest.WaitForVisibility();
            NomenclatureTest.WaitForVisibility();
        }

        public void CityFilterClear()
        {
           // CityFilterInputField.Click();
            CityFilterInputField.SendKeys(Keys.Control + "a");
            CityFilterInputField.SendKeys(Keys.Delete);

        }


        public void SetClientCard(int card)
        {   
            PanelNumberOfCardInput.SendKeys(card.ToString());
            //PanelNumberOfCardInput.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(200);
            PanelUploadCardBtn.Hover();
            PanelUploadCardBtn.Click();
            System.Threading.Thread.Sleep(200);
        }


        public void InsuranceFilterClear()
        {
            InsuranceFilterClearBtn.Click();
            System.Threading.Thread.Sleep(3000);
        }

        public void Mobile(string _)
        {
            PanelMobileCheckbox.Check();
            NomenclatureTest.WaitForVisibility();
        }
        public void Cito()
        {
            PanelCITOCheckbox.Check();
            NomenclatureTest.WaitForVisibility();
        }

        public void SetFlagWithCard() // Установка флага "С КАРТОЙ"
        {
            FlagWithoutCard.Click();
            NomenclatureTest.WaitForVisibility();
        }

        public void SetFlagWithoutCard() // Установка флага "БЕЗ КАРТЫ"
        {
            FlagWithCard.Click();
            NomenclatureTest.WaitForVisibility();
        }

        public bool CheckboxStatusCheck(string checkbox) 
        {
       
            var element = _elementsWithCheckBox[checkbox];

             return element.Disabled;
            


        }

    }
}
