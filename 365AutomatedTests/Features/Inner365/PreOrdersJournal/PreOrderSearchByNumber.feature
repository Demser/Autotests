Feature: SearchPreOrderByNumber
	Created by Anna Shvetc

Background: I login
	Given I login as CallCenter user "CallCenter", "123456"
	And I open the PreOrdersJournal page


@mytag
Scenario: Search Preorder by Number
	When I click the search-by-number button
#	Then the search-by-number screen opens
	When I enter the preorder number "58184-100001308868" to the number field
	And I click the search button
	Then The preorder with the correct number is displayed in the results grid
	And The results grid consists of only one order
