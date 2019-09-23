Feature: Search_order_by_ExternalSampleNumber

@SearchOrderByExternalSampleNumber
Scenario Outline: Search_order_by_ExternalSampleNumber
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by external sample number
	And I enter external sample number "<externalSampleNumber>"
	And I enter external sample dateFrom "<dateFrom>"
	And I enter external sample dateTo "<dateTo>"
	And I click Search button
	Then I see orders by externalSampleNumber "<externalSampleNumber>":"<dateFrom>":"<dateTo>"

	When I click OrderDetailsIconShow
	Then order details show

	When I click OrderDetailsIconHide
	Then order details hide

	When I click order and go to OrderMainPage
	Then I see new page order by externalSampleNumber "<externalSampleNumber>":"<dateFrom>":"<dateTo>"
Examples: 
| externalSampleNumber | dateFrom   | dateTo     |
| 6746                 | 11/07/2018 | 13/07/2018 |

@SearchOrderByExternalSampleNumberPart
Scenario Outline: Search_order_by_ExternalSampleNumberPart
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by external sample number
	And I enter external sample number "<externalSampleNumberPart>"
	And I enter external sample dateFrom "<dateFrom>"
	And I enter external sample dateTo "<dateTo>"
	And I click Search button
	Then I see orders by externalSampleNumberPart "<externalSampleNumberPart>":"<dateFrom>":"<dateTo>"
Examples: 
| externalSampleNumberPart | dateFrom   | dateTo     |
| 67                       | 11/07/2018 | 13/07/2018 |

@SearchOrderByExternalSampleNumberNegative
Scenario Outline: Search_order_by_ExternalSampleNumber_negative
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by external sample number
	And I enter external sample number ""
	And I click Search button
	Then I see message "Не указан сторонний номер образца."

	When I enter external sample number "<externalSampleNumber>"
	And I enter external sample dateFrom "<dateFrom>"
	And I enter external sample dateTo "<dateTo>"
	And I click Search button
	Then I see no orders
Examples: 
| externalSampleNumber     | dateFrom   | dateTo     |
| 6746                     | 01/12/2017 | 11/12/2017 |