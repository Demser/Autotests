Feature: BDTwoTestsInDifferentGroups
Creating two tests in different groups with control and reagent. Fill some samples on Sorting Batch and dont use schedule. Dripping this batch. Add Schedule. Task for dripping create for one of tests.
This autotest is written by Dyomin S. based on BATCH-444 test-case

Background: I login
	Given I login as admin "BatchDropper", "1"

Scenario: Batch-444_1_CreateAndSorting
	Given I clear the data from BatchDropper database
	When I create test named "CYTOMEGALOVIRUS_КРОВЬ_ДНК" with 8 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	And I create test named "UREAPLASMA_SPECIES_ДНК" with 4 cells with "autotest2" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 2 will be created
	Then I remove all schedules from dictionaries
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting"
	Then I fill-out the Sorting-planchet by 6 samples of test "CYTOMEGALOVIRUS_КРОВЬ_ДНК"
	Then I fill-out the Sorting-planchet by 6 samples of test "UREAPLASMA_SPECIES_ДНК"
	Then I click end sorting button

Scenario: Batch_444_2_Dripping
	When I confirm parent-batch id for start ProductionAcceptance
	When I create new sched by database for "SPB" hub
	When I get task for dripping
	And I confirm test-batch id for start ReagentsDripping
	Then I fill-out the Reagents-planchet for 8 times
	And I go to Batches tab and check status "Реагенты собраны"