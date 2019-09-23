Feature: BDTwoTestsInOneGroupWithoutSched
Creating two tests in one group with control and reagent. Fill some samples on Sorting Batch and dont use schedule. Dripping this batch. Task for dripping must create without schedule cause we have full test-batch.
This autotest is written by Dyomin S. based on BATCH-435 test-case

Background: I login
Given  I login as admin "BatchDropper", "1"

Scenario: Batch-435_1_CreateTwoTests
	Given I clear the data from BatchDropper database
	When I create test named "CYTOMEGALOVIRUS_КРОВЬ_ДНК" with 8 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	And I create test named "UREAPLASMA_SPECIES_ДНК" with 4 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 2 will be created
	Then I remove all schedules from dictionaries

Scenario: Batch-435_2_Sorting
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting"
	Then I fill-out the Sorting-planchet by 6 samples of test "CYTOMEGALOVIRUS_КРОВЬ_ДНК"
	Then I fill-out the Sorting-planchet by 6 samples of test "UREAPLASMA_SPECIES_ДНК"
	Then I click end sorting button

Scenario: Batch-435_3_ReagentsAndFullPlanchetDripping
	When I confirm parent-batch id for start ProductionAcceptance
	When I get task for dripping
	And I confirm test-batch id for start ReagentsDripping
	Then I fill-out the Reagents-planchet for 11 times
	And I go to Batches tab and check status "Реагенты собраны"

Scenario: Batch-435_4_ManualDrippingSecondBatch
	When I confirm test-batch id for start ManualDripping
	Then the popup with ""Бэтчи и пробы, которые необходимы"" title should be closed
	And I enter usercode in the field on the planchet-position form for Manual
	And I confirm child-batch for start dripping
	And I enter usercode in the field on the planchet-position form for Manual
	Then The test with max cells quantity "CYTOMEGALOVIRUS_КРОВЬ_ДНК" will be dropped first
	Then I start Manual Dripping by confirm userbarcode "5" items
	And I confirm child-batch for start dripping
	And I enter usercode in the field on the planchet-position form for Manual
	Then The test "UREAPLASMA_SPECIES_ДНК" will be dropped in same planshet
	Then I start Manual Dripping by confirm userbarcode "5" items
	And I go to Batches tab and check status "Собран" 	
