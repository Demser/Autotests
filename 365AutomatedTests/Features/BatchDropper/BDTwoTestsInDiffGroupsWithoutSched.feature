Feature: BDTwoTestsInDiffGroupsWithoutSched
This autotest check the negative-case when we have full-dripping planchet but dont get task cause our tests has different groups
This autotest is written by Dyomin S. based on BATCH-487 test-case

Background: I login
	Given I login as admin "BatchDropper", "1"

Scenario: Batch-487_1_CreateTests
	Given I clear the data from BatchDropper database
	When I create test named "BORDETELLA_PERTUSSIS_ДНК" with 8 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	And I create test named "UREAPLASMA_SPECIES_ДНК" with 4 cells with "autotest2" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 2 will be created

Scenario: Batch-487_2_SortingAndReagents
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for "Sorting"
	Then I fill-out the Sorting-planchet by 6 samples of test "BORDETELLA_PERTUSSIS_ДНК"
	Then I fill-out the Sorting-planchet by 6 samples of test "UREAPLASMA_SPECIES_ДНК"
	Then I click end sorting button
	Then I remove all schedules from dictionaries
	When I confirm parent-batch id for start ProductionAcceptance
	Then I check task for reagents