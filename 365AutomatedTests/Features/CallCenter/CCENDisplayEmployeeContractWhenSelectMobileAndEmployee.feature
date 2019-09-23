Feature: CCENDisplayEmployeeContractWhenSelectMobileAndEmployee
	Employee checkbox and checkbox selection at the same time. Written by Dyomin.S based on CCEN-998 tast-case.

@mytag
Scenario: CCEN_998_CreateMobileEmployeePreorderandCheckDiagnosticalCenters
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	#Внимание! Перед выполнением этого сценария необходимо убрать принудительное снятие чекбокса мобильного выезда в следующем шаге
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Mobile     |             |            | true       |
	When I add nomenclature "02-001" to the cart
	When I go to the personal-data tab
	Then I see that mobile block is "present"
	When I go to the cart tab
	When I choose "12067" diagnostic center
	When I go to the personal-data tab
	Then I set account status "6" for diagnostic center "12067"
	When I go to the cart tab
	When I choose "12067" diagnostic center
	Then I set account status "1" for diagnostic center "12067"
