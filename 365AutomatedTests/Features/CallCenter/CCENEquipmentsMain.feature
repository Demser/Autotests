Feature: CCENEquipmentsMain
	Checking for equipment in diagnostic centers. If one HXID has several types of equipment for one gender - there must be ALL the necessary equipment at the Pickup Location (for this gender).
	Written by Dyomin.S based on CCEN-983 test-case.

@mytag
Scenario: CCEN-983_CreatePreorderAndCheckEquipmentDependings
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I add nomenclature "90-714" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "11864" equipment is "present"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "11864" equipment is "absent"
	
	When I go to the personal-data tab	
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | M      |            |              |          |
	When I go to the cart tab
	Then I get message "не соответствуют указанному полу клиента"

	When I cancel equipment "17" by script
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	Then I get message "требуют наличия специального оборудования"
	Then I make active equipment "17" by script