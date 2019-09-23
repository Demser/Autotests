Feature: BDReagentsSmokeWithSched
This autotest is written by Dyomin S. based on BATCH-433 test-case

Background: I login
	Given I login as admin "BatchDropper", "1"

Scenario: Batch-433_1_CreateTest
	Given I clear the data from BatchDropper database
	When I create test named "HELICOBACTER_PYLORI_ДНК" with 1 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 1 will be created

Scenario: Batch-433_2_Sorting
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	Then I fill-out the Sorting-planchet by 7 samples of test "HELICOBACTER_PYLORI_ДНК"
	Then I click end sorting button

Scenario: Batch-433_3_ProductionAcceptance
	When I confirm parent-batch id for start ProductionAcceptance
	Then The child-batch created
	Then I remove all schedules from dictionaries

Scenario: Batch-433_4_Reagents
	When I create new sched by database for "SPB" hub
	When I get task for dripping
	Then Task for reagents will be create