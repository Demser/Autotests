Feature: BDTwoTestsInOneGroupFullReagentPlanchet
	This autotest cheked optimal configuration of test-batch dripping in spite of diffrent groups and count of samples
	This autotest is written by Dyomin S. based on BATCH-489 test-case

Background: I login
	Given  I login as admin "BatchDropper", "1"

Scenario: Batch-489_1_CreateTests
	Given I clear the data from BatchDropper database
	When I create test named "CYTOMEGALOVIRUS_КРОВЬ_ДНК" with 4 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	And I create test named "HBV_ДНК" with 8 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 2 will be created

Scenario: Batch-489_2_SortingAndReagents
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting"
	Then I fill-out the Sorting-planchet by 22 samples of test "CYTOMEGALOVIRUS_КРОВЬ_ДНК"
	Then I fill-out the Sorting-planchet by 10 samples of test "HBV_ДНК"
	Then I click end sorting button

Scenario: Batch-489_3_ReagentsAndManualDripping
	When I confirm parent-batch id for start ProductionAcceptance
	When I get task for dripping
    And I confirm test-batch id for start ReagentsDripping
    Then I fill-out the Reagents-planchet for 11 times
    And I go to Batches tab and check status "Реагенты собраны"
	When I confirm test-batch id for start ManualDripping
	Then the popup with ""Бэтчи и пробы, которые необходимы"" title should be closed
	And I enter usercode in the field on the planchet-position form for Manual
	And I confirm child-batch for start dripping
	And I enter usercode in the field on the planchet-position form for Manual
	Then The test with max cells quantity "CYTOMEGALOVIRUS_КРОВЬ_ДНК" will be dropped first