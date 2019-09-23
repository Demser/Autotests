Feature: Calculating_age_in_order

@CalculatingAgeInOrder
Scenario Outline: Calculating_age_in_order
	Given I login as "SKP":"123456" if not logged yet
	When I go to Results page
	And I go to tab by order number
	And I enter order number "30310-E520B-00507938"
	And I click Search button
	And I click order and go to OrderMainPage
	And I change birthdate as "<birthdate>"
	Then age is "<age>"
Examples: 
| birthdate		| age        |
| 24.06.2018	| 8 дней     |
| 01.02.2018	| 5 месяцев  |
| 01.01.2018	| 6 месяцев  |
| 01.08.2017	| 11 месяцев |
| 02.07.2017	| 1 год      |
| 02.07.2016	| 2 года     |
| 02.07.2013	| 5 лет      |
| 02.07.1910	| 108 лет    |
| 02.07.1908	| 110 лет    |
| 02.07.1906	| Не указано |
| 02.07.1907	| 111 лет    |