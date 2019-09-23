Feature: CCENFilterRulesInteraction
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: CCEN_267_FilterRules_Interaction
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	Then I check checkbox "InsuranceFilter" is "disabled" status
	Then I check checkbox "PanelMobileCheckbox" is "disabled" status
	Then I check checkbox "PanelCITOCheckbox" is "disabled" status
	Then I check checkbox "PanelEmployeeCheckbox" is "disabled" status
    When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            |            |
	Then I check checkbox "InsuranceFilter" is "enabled" status
	Then I check checkbox "PanelMobileCheckbox" is "enabled" status
	Then I check checkbox "PanelCITOCheckbox" is "enabled" status
	Then I check checkbox "PanelEmployeeCheckbox" is "enabled" status	
	When I set parameters by filter-panel
	| city              | insurance           | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г | АЛЬФАСТРАХОВАНИЕ АО | Nothing    |             |            |            |
	Then I check checkbox "PanelCITOCheckbox" is "disabled" status
	Then I check checkbox "PanelEmployeeCheckbox" is "disabled" status
	When I clear insuranceFilter
	Then I check checkbox "PanelCITOCheckbox" is "enabled" status
	Then I check checkbox "PanelEmployeeCheckbox" is "enabled" status
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Mobile     |             |            |            |
	Then I check checkbox "PanelCITOCheckbox" is "disabled" status
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            |            |
	Then I check checkbox "PanelCITOCheckbox" is "enabled" status
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | CITO       |             |            |            |
	Then I check checkbox "PanelMobileCheckbox" is "disabled" status
	Then I check checkbox "InsuranceFilter" is "disabled" status
	When I set parameters by filter-panel
	| city     | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Москва г |           | Nothing    |             |            |            |
	Then I check checkbox "InsuranceFilter" is "enabled" status
	Then I check checkbox "PanelMobileCheckbox" is "enabled" status
	Then I check checkbox "PanelCITOCheckbox" is "enabled" status
	Then I check checkbox "PanelEmployeeCheckbox" is "enabled" status
