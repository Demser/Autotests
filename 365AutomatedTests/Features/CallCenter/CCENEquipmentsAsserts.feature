Feature: CCENEquipmentsAsserts
	Оборудование. Отображение ДЦ в зависимости от доступности оборудования, ограниченного по полу клиента (номенклатура для U).
	This autotest is written by Dyomin S. based on CCEN-1128 test-case

Scenario: CCEN_1128_CheckAllAssertWhenGenderIsUnknown
	Given I login as "CcenAuto" and password
	Given I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I add nomenclature "90-585" to the cart

	When I set gender 'U' for '90-585' nomenclature item
	When I delete all equipments for '90-585' nomenclature
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	#конец проверки
	When I add new equimpent type '19' for '90-585' nomenclature and set 'M' gender
	When I set '0' status for all equipments and '' equipmentscode and '12142' accountcode
	When I set '1' status for all equipments and '19' equipmentscode and '12142' accountcode
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "present"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	#конец проверки
	When I add new equimpent type '21' for '90-585' nomenclature and set 'F' gender
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I get message "требуют наличия специального оборудования"
	#конец проверки
	When I set '1' status for all equipments and '21' equipmentscode and '12142' accountcode
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "present"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	#конец проверки
	When I set '0' status for all equipments and '' equipmentscode and '12142' accountcode
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I get message "требуют наличия специального оборудования"
	Then I check that diagnostical center block is empty
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I get message "требуют наличия специального оборудования"
	Then I check that diagnostical center block is empty
	#конец проверки

	
Scenario:CCEN_1129_CheckAllAssertWhenGenderIsMF
	Given I login as "CcenAuto" and password
	Given I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I set gender 'M' for '90-585' nomenclature item
	When I delete all equipments for '90-585' nomenclature
	When I add nomenclature "90-585" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	#проверка
	Then I check diagnostical center "12142" equipment is "absent"
	#конец проверки
	When I add new equimpent type '19' for '90-585' nomenclature and set 'M' gender
	When I set '1' status for all equipments and '19' equipmentscode and '12142' accountcode
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "present"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I get message "не соответствуют указанному полу"
	Then I check diagnostical center "12142" equipment is "absent" 
	#конец проверки
	When I add new equimpent type '21' for '90-585' nomenclature and set 'F' gender
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "present"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I get message "не соответствуют указанному полу"
	Then I check diagnostical center "12142" equipment is "absent" 
	#конец проверки
	When I set '1' status for all equipments and '21' equipmentscode and '12142' accountcode
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "present"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I get message "не соответствуют указанному полу"
	Then I check diagnostical center "12142" equipment is "absent"
	#конец проверки
	When I set gender 'F' for '90-585' nomenclature item
	#проверка
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | U      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "present"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | M      |            |              |          |
	When I go to the cart tab
	Then I get message "не соответствуют указанному полу"
	Then I check diagnostical center "12142" equipment is "absent"
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          |             | F      |            |              |          |
	When I go to the cart tab
	Then I check diagnostical center "12142" equipment is "absent"
	#конец проверки
