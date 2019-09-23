Feature: CreateMobilePreorder
	This autotest created simple preorder with mobile option


Scenario: CreateMobilePreorderWithMobileOption
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Mobile     |             |            | false      |
	When I add nomenclature "02-002" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname   | dateofbirth | gender | dispetcher | policynumber | validity |
	| Бот       | Порталович | Автотестов | 15101993    | M      | Путишенко  | 88005553535  | 31122020 |
	When I go to the cart tab
	When I choose logistic option like "Выезд"
	When I choose "C000018795" diagnostic center by contract
	When I click save button
	Then The pop-up window for a successful save will be shown
