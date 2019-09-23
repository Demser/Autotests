Feature: Search_order_by_SampleNumber

@SearchOrderBySampleNumber
Scenario Outline: Search_order_by_SampleNumber
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by sample number
	And I enter sample number "<sampleNumber>"
	And I enter sample dateFrom "<dateFrom>"
	And I enter sample dateTo "<dateTo>"
	And I click Search button
	Then I see orders by sampleNumber "<sampleNumber>":"<dateFrom>":"<dateTo>"

	When I click OrderDetailsIconShow
	Then order details show

	When I click OrderDetailsIconHide
	Then order details hide

	When I click order and go to OrderMainPage
	Then I see new page order by sampleNumber "<sampleNumber>":"<dateFrom>":"<dateTo>"
Examples: 
| sampleNumber     | dateFrom   | dateTo     |
| 6765735673568563 | 01/07/2018 | 11/07/2018 |

@SearchOrderBySampleNumberPart
Scenario Outline: Search_order_by_SampleNumberPart
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by sample number
	And I enter sample number "<sampleNumberPart>"
	And I enter sample dateFrom "<dateFrom>"
	And I enter sample dateTo "<dateTo>"
	And I click Search button
	Then I see orders by sampleNumberPart "<sampleNumberPart>":"<dateFrom>":"<dateTo>"
Examples: 
| sampleNumberPart | dateFrom   | dateTo     |
| 4567             | 01/07/2018 | 11/07/2018 |

@SearchOrderBySampleNumberNegative
Scenario Outline: Search_order_by_SampleNumber_negative
	Given I login as "Results":"123456" if not logged yet
	When I go to Results page
	And I go to tab by sample number
	And I enter sample number ""
	And I click Search button
	Then I see message "Не указан номер образца."

	When I enter sample number "<sampleNumber>"
	And I enter sample dateFrom "<dateFrom>"
	And I enter sample dateTo "<dateTo>"
	And I click Search button
	Then I see no orders
Examples: 
| sampleNumber     | dateFrom   | dateTo     |
| 7683567356745634 | 07/05/2018 | 08/05/2018 |