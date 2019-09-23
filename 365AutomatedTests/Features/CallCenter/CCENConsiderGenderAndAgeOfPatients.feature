Feature: CCENConsiderGenderAndAgeOfPatients
	The test verifies that when making a pre-order in the cart, the gender and age of the patient are taken into account when deriving the preparation rules. Written by Dyomin S. based on CCEN-912



Scenario: CCEN-912_CreatePreorderAndCheckRulesDepending
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I add nomenclature "90-532" to the cart
	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname   | dateofbirth | gender | dispetcher | policynumber | validity |
	| Бот       | Порталович | Автотестов | 15101993    | M      |            |              |          |	
	When I go to the cart tab
	When I choose "24833" diagnostic center
	Then I check that "Мужчинам рекомендуется" rule is "present"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15102015    | M      |            |              |          |
	When I go to the cart tab
	Then I check that "Мужчинам рекомендуется" rule is "absent"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | U      |            |              |          |
	When I go to the cart tab
	Then I check that "Мужчинам рекомендуется" rule is "present"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15102015    | U      |            |              |          |
	When I go to the cart tab
	Then I check that "Мужчинам рекомендуется" rule is "absent"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15101993    | F      |            |              |          |
	When I go to the cart tab
	Then I check that "Мужчинам рекомендуется" rule is "absent"

	When I go to the personal-data tab
	When I set additional parameters by filterpanel
	| firstname | middlename | lastname | dateofbirth | gender | dispetcher | policynumber | validity |
	|           |            |          | 15102015    | F      |            |              |          |
	When I go to the cart tab
	Then I check that "Мужчинам рекомендуется" rule is "absent"