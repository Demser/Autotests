Feature: Search_order_by_OrderNumber

@SearchOrderByOrderNumber
Scenario Outline: Search_order_by_OrderNumber
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by order number
	And I enter order number "<orderNumber>"
	And I click Search button
	Then I see order by order number "<orderNumber>"

	When I click OrderDetailsIconShow
	Then order details show

	When I click OrderDetailsIconHide
	Then order details hide

	When I click order and go to OrderMainPage
	Then I see new page order "<orderNumber>"
Examples: 
| orderNumber          |
| 30310-D4483-00507869 |
| 1037-4BF2-422701224  |

@SearchOrderByPartOrderNumber
Scenario Outline: Search_order_by_OrderNumberPart
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by order number
	And I enter order number "<orderNumberPart>"
	And I click Search button
	Then I see orders by orderNumberPart "<orderNumberPart>"
Examples: 
| orderNumberPart |
| 0-4BEDF-0		  |

@SearchOrderByOrderNumberNegative
Scenario: Search_order_by_OrderNumber_negative
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by order number
	And I enter order number ""
	And I click Search button
	Then I see message "Пустой номера заказа."
