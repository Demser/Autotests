Feature: CCENCreatePreOrderWithMobile
	Created by Anna Shvetc
	Test-case CCEN-266


Background: I login
	Given I login as CallCenter user "CallCenter", "123456"

@mytag
Scenario Outline: CCEN-266_1_SelectCityAndNimenclatureItems
	Given I'm on the CallCenter Page
	When I select city <city> in the city filter
	Then the items list is displayed
	When I filter the item number <hxid>
	And I click the Add button
	Then the item number <hxid> is added to the basket

Scenario Outline: CCEN-266_2_SetMobileCheckboxOnAndCheckDefaultData
	Given I'm on the PersonalData tab
	When I set the Mobile checkbox on
	Then the New Mobile call-out section is displayed
#DateTime.Now class returns current date, AddDays() method can be used for adding/removing days
	And the default date is tomorrow
	And the default time interval is from <timefrom> till <timetill>
	And the default city is <city>

Scenario Outline: CCEN-266_3_FillingPersonalData
	Given I'm on the PersonalData tab
	When I enter <surname>
	And I enter <name>
	And I enter <parentalname>
	And I enter <age>
	And I enter <call-out date>
	And I enter <district>
	And I enter <street>
	Then I go to the Basket tab

Scenario Outline: CCEN-266_4_CheckBasket
	Given I'm on the Basket tab
	Then the nomenclature item <hxid> presents on the page
	And the Sum field is empty
	And the Discount field is empty
	And the In total field is empty
	And the field "Мобильный выезд" presents on the page
