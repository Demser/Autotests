using _365AutomatedTests.Framework.Generic;
using Coypu;
using System.Collections.Generic;


namespace _365AutomatedTests.UIObjects.Pages
{
    public class UserSettingsPage : Page
    {

        #region   Пользовательские настройки

        private ElementScope AutoLoadServicesCheckbox => Scope.FindCss("input#AutoLoadServices");
        
        private ElementScope SaveBtn => Scope.FindCss("button.btn.btn-primary");



        private Dictionary<string, ElementScope> _elementsWithCheckBox;

        public UserSettingsPage() : base()
        {
            _elementsWithCheckBox = new Dictionary<string, ElementScope>
            {
                { "AutoLoadServicesCheckbox", AutoLoadServicesCheckbox },
               // { "PanelMobileCheckbox", PanelMobileCheckbox },
               // { "PanelCITOCheckbox", PanelCITOCheckbox }  ,
               // { "PanelEmployeeCheckbox", PanelEmployeeCheckbox }
            };
        }

        #endregion

        public void AutoLoadServicesCheckboxClick(string checkbox)
        {
            AutoLoadServicesCheckbox.Click();
        }

        public bool CheckboxStatusCheck(string checkbox)
        {
            var element = _elementsWithCheckBox[checkbox];
            return element.Selected;
        }

        public void SaveBtnClick()
        {
            SaveBtn.Click();
        }
    }
}
