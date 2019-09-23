Feature: CreatePreorderWithCanceledNomenclature
	This autotest abt create preorder choosing some hxid witch will be canceled and assert this


Scenario: CCEN-959_1_CreatePreorderWithCanceledNomenclature
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I add nomenclature "02-002" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname   | dateofbirth | gender | dispetcher | policynumber | validity |
	| Бот       | Порталович | Автотестов | 15101993    | M      | Путишенко  | 88005553535  | 31122020 |
	When I go to the cart tab
	When I choose "10963" diagnostic center
	When I click save button
	Then The pop-up window for a successful save will be shown

Scenario: CCEN-959_2_RunCancellingScriptAndTryToCreatePreorder
	#Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	And I cancel nomenclature "21-019" by script
	When I add nomenclature "21-019" to the cart
	#Then The message about cancel nomenclature will be shown
	
