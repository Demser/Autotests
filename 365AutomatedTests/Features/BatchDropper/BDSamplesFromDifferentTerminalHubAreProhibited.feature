Feature: BDSamplesFromDifferentTerminalHubAreProhibited
This autotest is written by Dyomin S. based on BATCH-699 test-case
Check the dependence of batches and samples from the location of the laboratory

Scenario: Batch-699_1_TerminalSPB
	Given I login as admin "BatchDropper", "1"
	Given I clear the data from BatchDropper database
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	Then The sample with wrong location dont add to sorting
	When I confirm parent-batch id for start ProductionAcceptance
	Then The child-batch created
	Then I remove all schedules from dictionaries
	Then I check task for reagents

Scenario: Batch-699_2_TerminalMSK
	Given I login as admin "BatchDropper","123456", "2"
	When I create test named "HELICOBACTER_PYLORI_ДНК" with 1 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 1 will be created
	When I create new sched by database
	Then I check task for reagents
	Then I check count of batches
