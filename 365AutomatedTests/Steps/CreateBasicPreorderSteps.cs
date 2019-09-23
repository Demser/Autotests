using _365AutomatedTests.UIObjects.Pages;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static NUnit.Framework.Assert;
using Coypu;


namespace _365AutomatedTests.Steps
{
    [Binding]
    public class CreateBasicPreorderSteps
    {

        bool isInsurenced = false;
        bool isMobile = false;
        bool isCito = false;
        bool isEmployee = false;
        Models.Patient createdPatient;
        Models.InsurancedPatient createdInsurancedPatient;
        [Given(@"I go to Calalog new module")]
        [When(@"I go to Calalog new module")]
        public void GivenIGoToCalalogNewModule()
        {
            MainPage mainPage = new MainPage();
            mainPage.GoToCCEN();
        }


        [When(@"I set parameters by filter-panel")]
        public void WhenISetParametersByFilter_Panel(Table table)
        {
            var account = table.CreateInstance<Models.PreorderData>();
            isMobile = account.TakingType == Models.TakingType.Mobile; // будет ли ПЗ с мобильным выездом
            isCito = account.TakingType == Models.TakingType.CITO; // будет ли ПЗ с конкрактом ЦИТО
            isInsurenced = account.Insurance != ""; // будет ли ПЗ со страховым контрактом
            bool isCardNumber = account.CartNumber != 0; // будет ли учитываться карта клиента в ПЗ
            isEmployee = account.IsEmployee == true;
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            cCENFilterPanelPage.BeforeSetCity();
            cCENFilterPanelPage.CityFilterClear();
            cCENFilterPanelPage.SetCity(account.City); // в любом случае выбираем Город



            if (isCardNumber)
            {
                cCENFilterPanelPage.SetClientCard(account.CartNumber);
            }
            if (isInsurenced) // если ПЗ со страховым контрактом
            {
                //createdPatient = table.CreateInstance<Models.InsurancedPatient>();
                cCENFilterPanelPage.SetInsurance(account.Insurance); // выбираем из выпадающего списка СК значение, которое прописано в фиче
            }

            if (isMobile) // если ПЗ с мобильным выездом
            {
                //createdPatient = table.CreateInstance<Models.Patient>();
                cCENFilterPanelPage.PanelMobileCheckbox.Check(); // устанавливаем чекбокс мобильного выезда
                System.Threading.Thread.Sleep(3000); // Костыль, т.к. лоадер появляется дважды (баг).
            }
            if (isCito) // если ПЗ с контрактом ЦИТО
            {                
                cCENFilterPanelPage.PanelCITOCheckbox.Check(); // устанавливаем чекбокс ЦИТО
                System.Threading.Thread.Sleep(3000); // Костыль, т.к. лоадер появляется дважды (баг).
            }
            if (isEmployee) // если ПЗ с контрактом ЦИТО
            {
                cCENFilterPanelPage.PanelEmployeeCheckbox.Check(); // устанавливаем чекбокс ЦИТО
                System.Threading.Thread.Sleep(3000); // Костыль, т.к. лоадер появляется дважды (баг).
            }

        }



        [When(@"I go to the personal-data tab")]
        public void WhenIGoToThePersonal_DataTab()
        {
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage();
            cCENPersonalDataPage.GoToPersonalDataTab();
        }



        //I set additional parameters by filter-panel
        [When(@"I set additional parameters by filterpanel")]
        public void WhenISetAdditionalParametersByFilterpanel(Table table)
        {
            CCENPersonalDataPage cCENPersonalDataPage = new CCENPersonalDataPage(); // определяем страницу персональных данных
            if (isInsurenced) // если ПЗ страховой
            {
                createdInsurancedPatient = table.CreateInstance<Models.InsurancedPatient>(); // берем коллекцию СтраховойПациент и заполняем страховые поля
                cCENPersonalDataPage.SetInsurancePatientData(createdInsurancedPatient.PolicyNumber, createdInsurancedPatient.Dispetcher, createdInsurancedPatient.Validity);
            }

            // далее в любом случае заполняем основные данные пациента
             createdPatient = table.CreateInstance<Models.Patient>();                                  
            cCENPersonalDataPage.SetFIO(createdPatient.Firstname, createdPatient.Middlename, createdPatient.Lastname, createdPatient.Dateofbirth);
            cCENPersonalDataPage.SetGender(createdPatient.Gender);
            System.Threading.Thread.Sleep(900); // ожидание, поскольку есть небольшое зависание после выбора пола
        }

        [When(@"I add nomenclature ""(.*)"" to the cart")]
        public void WhenIAddNomenclatureToTheCart(string hxid)
        {
            CCENNomenclaturesPage cCENNomenclaturesPage = new CCENNomenclaturesPage();
            cCENNomenclaturesPage.SearchSomeHXID(hxid);
            cCENNomenclaturesPage.AddSomeNomenctatureToCart(hxid); // добавление исследования в корзину и ожидание загрузки
        }
   
        [When(@"I go to the cart tab")]
        public void WhenIGoToTheCartTab()
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.GoToCartTab();
        }

        [When(@"I go to the HXIDS tab")]
        public void WhenIGoToTheHXIDSTab()
        {
            CCENNomenclaturesPage cCENNomenclaturesPage = new CCENNomenclaturesPage();
            cCENNomenclaturesPage.GoToHXIDSTab();
        }


        [When(@"I choose logistic option like ""(.*)""")]
        public void WhenIChooseLogisticOptionLike(string name)
        {
            if (isMobile)
            {
                
                CCENCartPage cCENCartPage = new CCENCartPage();
                if (cCENCartPage.LogisticOptionIcon.Exists())//если блок с выбором опции мобильного выезда засерен, нужно выбрать мобильную опцию
                {
                    IsTrue(cCENCartPage.AssertLogisticBlock(), "Блок логистической номенклатуры не подгружен!");
                    cCENCartPage.ChooseLogisticOption(name);
                }
            }
        }

        [When(@"I choose ""(.*)"" diagnostic center")]
        public void WhenIChooseDiagnosticCenter(string id)
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            var rb = cCENCartPage.DiagnosticCenterRadioButton(id);
            rb.WaitForVisibility();
            cCENCartPage.ChooseDiagnosticalCenter(id);           
        }

        [When(@"I choose ""(.*)"" diagnostic center by contract")]
        public void WhenIChooseDiagnosticCenterByContract(string value) //выбрать ДЦ по номеру контракта
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            var rb = cCENCartPage.DiagnosticCenterRadioButtonByValue(value);
            cCENCartPage.ChooseDiagnosticalCenterByValue(value);
        }


        [When(@"I click save button")]
        public void WhenIClickSaveButton()
        {
            System.Threading.Thread.Sleep(4000);
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.SavePreorderButton.WaitForAvailability();
            IsTrue(cCENCartPage.AssertSaveButtonIsUnlock(),"метод не успел отработать");
        }

        [When(@"I set card option ""(.*)""")]
        public void WhenISetCardOption(string optionOfFlag)
        {
            CCENFilterPanelPage cCENFilterPanelPage = new CCENFilterPanelPage();
            switch (optionOfFlag)
            {
                case "С картой":
                    cCENFilterPanelPage.SetFlagWithCard();
                    break;

                case "Без карты":
                    cCENFilterPanelPage.SetFlagWithoutCard();
                    break;
                default:
                    throw new Exception("Ни одно из значений не подходит! Возможно, опечатка в теле сценария. Введеное значение: "+ optionOfFlag);                   
            }
        }



        [Then(@"The pop-up window for a successful save will be shown")]
        public void ThenThePop_UpWindowForASuccessfulSaveWillBeShown()
        {
            CCENMainPage cCENMainPage = new CCENMainPage();
            cCENMainPage.PreorderNumberSpan.WaitForVisibility();
            IsTrue(cCENMainPage.AssertGetPreorderNumber(),"Не удалось сохранить предзаказ, либо окно с номером еще не подгрузилось");
            cCENMainPage.ClosedPreorderNumberWindow();
        }

        [Then(@"I delete all hxids from cart")]
        public void ThenIDeleteAllHxidsFromCart()
        {
            CCENCartPage cCENCartPage = new CCENCartPage();
            cCENCartPage.DeleteAllHXIDsFromCart();
        }

    }
}
