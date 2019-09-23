Feature: BDProhibitionOfDrippingTheCanceledBatch
This autotest is written by Dyomin S. based on BATCH-779 test-case
If some batch was canceled on-line during the sorting, sorting will not be continue

Background: I login
	Given  I login as admin "BatchDropper", "1"

Scenario: Batch_779-1-BatchCanceledInSorting
	Given I clear the data from BatchDropper database
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting"
	Then I fill-out the Sorting-planchet by 6 samples of test "CYTOMEGALOVIRUS_КРОВЬ_ДНК"
	When I canceled "parent" batch by database
	When I try to add some valid sample to sorting batch
	Then I check title for canceled batch

Scenario: Batch-779_2_CreateTest
	Given I clear the data from BatchDropper database
	When I create test named "CYTOMEGALOVIRUS_КРОВЬ_ДНК" with 1 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 1 will be created
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	Then I fill-out the Sorting-planchet by 18 samples of test "CYTOMEGALOVIRUS_КРОВЬ_ДНК"
	Then I click end sorting button
	When I confirm parent-batch id for start ProductionAcceptance
	Then The child-batch created

Scenario: Batch-779_3_ReagentsWithCanceledBatch
	When I canceled "parent" batch by database
	When I get task for dripping
	And I confirm test-batch id for start ReagentsDripping
	Then I try to fill-out Reagents-planchet for 1 times
	When I canceled "test" batch by database
	Then I try to fill-out Reagents-planchet for 1 times
	Then I check title for canceled batch

