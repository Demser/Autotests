Feature: CCENInsurancePersonalData
	The feature Сhecks the validation of three insurance fields in the personal data tab.
	Written by Dyomin S. based on CCEN-956 test-case. 08/02/2019 and 14/02/2019 ccen v 1.5.6.


Scenario: CCEN_956_1InsuranceFieldsAreAbsentWhenChoosingСС
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance  | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г | Гастроника | Nothing    |             |            | false      |
	When I go to the personal-data tab
	Then I see that insurance fields block is "absent"

Scenario: CCEN_956_2InsuranceFieldsArePresentWhenChoosingInsuranceCompany
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance           | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г | АЛЬФАСТРАХОВАНИЕ АО | Nothing    |             |            | false      |
	When I go to the personal-data tab
	Then I see that insurance fields block is "present"
	Then I check "policy-number" field	
	Then I check "dispatcher" field
	Then I check "validity" field

Scenario: CCEN_956_3InsurancePreorderEdit
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance     | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г | СК Ингосстрах | Nothing    |             |            | false      |
	When I add nomenclature "02-003" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname   | dateofbirth | gender | dispetcher | policynumber | validity |
	| Бот       | Порталович | Автотестов | 15101993    | M      | Путишенко  | 88005553535  | 31122020 |
	When I go to the cart tab
	When I choose "24833" diagnostic center
	When I click save button
	Then The pop-up window for a successful save will be shown
	When I find preorder by number in journal
	Then I open preorder for edit and see values in the insurance fields

Scenario: CCEN_956_4InsurancePreorderReply
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance     | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г | СК Ингосстрах | Nothing    |             |            | false      |
	When I add nomenclature "02-003" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname   | dateofbirth | gender | dispetcher | policynumber | validity |
	| Бот       | Порталович | Автотестов | 15101993    | M      | Путишенко  | 88005553535  | 31122020 |
	When I go to the cart tab
	When I choose "24833" diagnostic center
	When I click save button
	Then The pop-up window for a successful save will be shown
	When I find preorder by number in journal
	Then I open preorder for reply and see values in the insurance fields