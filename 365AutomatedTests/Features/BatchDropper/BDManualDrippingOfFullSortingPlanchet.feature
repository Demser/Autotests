Feature: BDManualDrippingOfFullSortingPlanchet
Creating test with control and reagent. Fill 96 samples on Sorting Batch and Dripping this batch. 
This autotest is written by Dyomin S. based on BATCH-631 test-case

@mytag
Scenario Outline: BD001_Create test
	Given I clear the data from BatchDropper database
	And I login as admin "BatchDropper", "1"
	When I create Test with <count> cell(s) and select <i> set of params from the table
	| Fam | Hex | Rox | Cy5 | Cy5.5 |
	| Cp  | BK  |     |     |       |
	Then test is created with <count> cell(s)
Examples: 
| count | i |
| 2     | 2 |


@mytag
Scenario: BD002_Sorting
	Given I login as admin "BatchDropper", "1"
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	Then I fill-out the Sorting-planchet 

@mytag
Scenario: BD003_ProductionAcceptance
	Given I login as admin "BatchDropper", "1"
	When I confirm all parent-batches id for start ProductionAcceptance
	#When I confirm parent-batch id for start ProductionAcceptance
	Then The child-batch created


@mytag
Scenario: BD004_ReagentsWorkplace
	Given I login as admin "BatchDropper", "1"
	When I go to Reagents page
	Then I fill-out the reagents planchetes
	#When I get task for dripping
	#And I confirm test-batch id for start ReagentsDripping
	#Then I fill-out the Reagents-planchet for 11 times
	
@mytag
Scenario: BD005_ManualDripping
	Given I login as admin "BatchDropper", "1"
	When I go to Manual Dripping Page
	Then I fill-out the test planchetes by manual dripping
	#When I confirm test-batch id for start ManualDripping
	#Then the popup with "Бэтчи и пробы, которые необходимы" title should be closed
	#And I enter usercode in the field on the planchet-position form for Manual
	#And I confirm child-batch for start dripping
	#And I enter usercode in the field on the planchet-position form for Manual
	#Then I start Manual Dripping by confirm userbarcode "94" items
	#And I start dripping Negative Controls "1" items
	#Then the popup with "Содержимое бэтча" title should be closed
	#And I go to Batches tab and check status "Отрицательные контроли собраны"

@mytag
Scenario: BD006_PositiveControls
	Given I login as admin "BatchDropper", "1"
	When I go to Positive Controls Page
	Then I fill-out the test planchetes by positive controls
	#When I confirm test-batch id for start PositiveControlsDripping
	#And I enter usercode in the field on the planchet-position form for PositiveControls
	#Then I start PositiveControlsDripping by confirm "111" control "1" item(s)
	#And I confirm userbarcode for end dripping
	#And I go to Batches tab and check status "Отправлен на прибор" 

