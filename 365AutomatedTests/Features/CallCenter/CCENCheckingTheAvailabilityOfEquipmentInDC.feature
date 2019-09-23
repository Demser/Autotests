Feature: CCENCheckingTheAvailabilityOfEquipmentInDC
	 For one HXID has several types of equipment for one gender - check one of this equipment is Removed or Inactive.
	 Written by Dyomin.S based on CCEN-989 and CCEN-990 tast-cases.	 

Scenario: CCEN_990_CheckForInactiveStatusOfOneTypeOfEquipment
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I add nomenclature "90-029" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname   | dateofbirth | gender | dispetcher | policynumber | validity |
	| Бот       | Порталович | Автотестов | 15101993    | F      |            |              |          |	
	When I go to the cart tab
	When I choose "10968" diagnostic center

	When I go to the personal-data tab
	When I set status "0" in diagnostical center "10968" for equipment "23" by script
	
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	Then I get message "требуют наличия специального оборудования"
	When I set status "1" in diagnostical center "10968" for equipment "23" by script

Scenario: CCEN_989_CheckForRemovedStatusOfOneTypeOfEquipment
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I add nomenclature "90-029" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname   | dateofbirth | gender | dispetcher | policynumber | validity |
	| Бот       | Порталович | Автотестов | 15101993    | F      |            |              |          |	
	When I go to the cart tab
	When I choose "10968" diagnostic center

	When I go to the personal-data tab
	When I cancel equipment "23" by script
	
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	Then I get message "требуют наличия специального оборудования"
	Then I make active equipment "23" by script