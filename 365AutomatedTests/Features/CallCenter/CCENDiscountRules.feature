Feature: CCENDiscountRules
This autotest is written by Dyomin S. based on CCEN-283 test-case. Ccen version 1.6.5.493.
The calculation of the correct discount depending on the discount rules of 3 5 or 10 percent.

Scenario: CCEN_283_1_DiscountIs3Persent
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I set card option "С картой"
	When I add nomenclature "02-002" to the cart
	When I go to the cart tab
	When I choose "10963" diagnostic center
	Then I check that discount is "3" percent

Scenario: CCEN_283_2_DiscountIs5Percent
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I set card option "С картой"
	When I add nomenclature "02-008" to the cart
	When I go to the cart tab
	When I choose "10963" diagnostic center
	Then I check that discount is "5" percent

Scenario: CCEN_283_3_DiscounIs10Percent
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I set parameters by filter-panel
	| city              | insurance | takingType | flaerNumber | cartNumber | isEmployee |
	| Санкт-Петербург г |           | Nothing    |             |            | false      |
	When I set card option "С картой"
	When I add nomenclature "06-242" to the cart
	When I go to the cart tab
	When I choose "10963" diagnostic center
	Then I check that discount is "10" percent