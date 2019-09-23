Feature: BDSortingWithDNKIsolatedSample
This autotest is written by Dyomin S. based on BATCH-319 test-case
A sample from the test marked by checkbox named TubeIsolationForDNK should not be sorted. 
This sample must pass through the workplace named Isolation in DNK.
In this workplace sample, isolation control and control material should be added. 
All these elements must also pass through the workplace named ProductionAcceptance

Background: I login
	Given  I login as admin "BatchDropper", "1"

Scenario: Batch-319_1_CreateTest
	Given I clear the data from BatchDropper database
	When I create test named "HBV_ДНК" with 1 cells with "autotest1" group with "present" isolation control with wax "false" with DNA "true" with Active "true"
	Then Tests counts 1 will be created

Scenario: Batch-319_2_Sorting
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	And I try to set 1 DNK-Sample of test "HBV_ДНК" to sorting
	Then The sample dont add to sorting batch

Scenario: Batch-319_3_IsolationDNK
	When I add 1 DNK-Sample of test "HBV_ДНК" to "IsolationDNK" field
	And I click button for finish samples addition
	And I have to confirm control material "autotest" and isolation control in "IsolationDNK" field
	Then I check that all materials added to "IsolationDNK" workplace

Scenario: Batch-319_4_ProductionAcceptance
	When I add 1 DNK-Sample of test "HBV_ДНК" to "ProductionAcceptance" field
	And I have to confirm control material "autotest" and isolation control in "ProductionAcceptance" field
	Then I check that all materials added to "ProductionAcceptance" workplace