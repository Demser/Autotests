Feature: Display_birthdate_height_weight

@DisplayBirthdateAgeHeightWeight
Scenario Outline: Display_birthdate_height_weight
	Given I login as "SKP":"123456" if not logged yet
	When I go to Results page
	And I go to tab by order number
	And I enter order number "30310-1E274-00508123"
	And I click Search button
	And I click order and go to OrderMainPage
	And I change birthdate as "<birthdate>"
	And I change height as "<height>"
	And I change weight as "<weight>"
	Given I login as "EditComplexDocs":"123456" if not logged yet
	When I go to Results page
	And I go to tab by order number
	And I enter order number "30310-1E274-00508123"
	And I click Search button
	And I click order and go to OrderMainPage
	Then I see birthdate is "<birthdate>"
	And I see height is "<height>"
	And I see weight is "<weight>"

	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by order number
	And I enter order number "30310-1E274-00508123"
	And I click Search button
	And I click order and go to OrderMainPage
	Then I not see birthdate
	And I not see height
	And I not see weight
Examples:
| birthdate  | height | weight |
| 11.02.1985 | 0      | 0      |
| 20.08.2008 | 0      | 59     |
| 20.11.2014 | 165    | 59     |