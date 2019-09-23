Feature: BDOptimalConfigurationOfManualDripping
This autotest is written by Dyomin S. based on BATCH-488 test-case

Background: I login
	Given  I login as admin "BatchDropper", "1"

Scenario: Batch-488_1_CreateTest
	Given I clear the data from BatchDropper database
    When I create test named "HELICOBACTER_PYLORI_ДНК" with 10 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	And I create test named "MYC_TUBERC_ДНК" with 2 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	And I create test named "EPSTEIN-BARR_VIRUS_ДНК" with 1 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 3 will be created

Scenario: Batch-488_2_Sorting	
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	Then I fill-out the Sorting-planchet by samples of tests "HELICOBACTER_PYLORI_ДНК", "MYC_TUBERC_ДНК","EPSTEIN-BARR_VIRUS_ДНК"
	Then I remove all schedules from dictionaries

Scenario: Batch-488_3_ManualDripping
	When I confirm parent-batch id for start ProductionAcceptance
	When I create new sched by database for "SPB" hub
	When I get task for dripping
	And I confirm test-batch id for start ReagentsDripping
	Then I fill-out the Reagents-planchet for 11 times
	When I confirm test-batch id for start ManualDripping
	Then the popup with "Бэтчи и пробы, которые необходимы" title should be closed
	And I enter usercode in the field on the planchet-position form for Manual
	And I confirm child-batch for start dripping
	And I enter usercode in the field on the planchet-position form for Manual
	Then The test with max cells quantity "HELICOBACTER_PYLORI_ДНК" will be dropped first