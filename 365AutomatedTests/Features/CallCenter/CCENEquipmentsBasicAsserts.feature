Feature: CCENEquipmentsBasicAsserts
	Автотест запускает базовые проверки оборудования.
	Для входных данных, в AllEquipmentsInNomenclatures требуется оборудование, не находящееся в ДЦ СПб и относящееся в мужскому полу.


Scenario: CCEN_984_1_EquipmentIsNotInCityAndGender
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I add nomenclature "90-1653" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | M      |            |              |          |
	Then I get message "требуют наличия специального оборудования"
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	Then I get message "не соответствуют указанному полу клиента"
@ignore
Scenario: CCEN_984_2_EquipmentIsInCityAndDifferentGender
	#Given I login as "CcenAuto" and password
	#And I go to Calalog new module
	When I go to the HXIDS tab
	When I add nomenclature "90-014" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | M      |            |              |          |
	Then I get message "требуют наличия специального оборудования"
	Then I get message "не соответствуют указанному полу клиента"
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	Then I get message "не соответствуют указанному полу клиента"
@ignore
Scenario:  CCEN_984_3_ChooseNomenclaturesWithUnknownGender
	#Given I login as "CcenAuto" and password
	#And I go to Calalog new module
	#When I set parameters by filter-panel
	#| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	#| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I go to the HXIDS tab
	When I add nomenclature "90-1653" to the cart
	When I add nomenclature "90-002" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	Then I get message "не соответствуют указанному полу клиента"
	When I go to the cart tab
	Then I delete all hxids from cart
@ignore
Scenario: CCEN_984_4_EquipmentIsInDifferentCitiesAndOneGender
	#Given I login as "CcenAuto" and password
	#And I go to Calalog new module
	When I go to the HXIDS tab
	#When I set parameters by filter-panel
	#| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	#| Санкт-Петербург г |           | Nothing    |             |            | false      |	
	When I add nomenclature "90-040" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "11864" equipment is "absent"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "11864" equipment is "present"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "11864" equipment is "absent"