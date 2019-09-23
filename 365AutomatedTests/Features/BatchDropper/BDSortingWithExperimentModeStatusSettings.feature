Feature: BDSortingWithExperimentModeStatusSettings
This autotest check the case when we try to add sample with results to sorting batch with experiment-mode on and experiment-mode off.
This autotest is written by Dyomin S. based on BATCH-695 test-case

@mytag
Scenario: Batch-695_1_SettingsAndSorting
	Given  I login as admin "BatchDropper", "1"
	Given I clear the data from BatchDropper database
	When I go to Settings page
	When I set Experiment Mode Status "false"
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for "Sorting"
	Then I fill-out the Sorting-planchet by 1 samples of test "HELICOBACTER_PYLORI_ДНК"
	Then I check valid notification about Experiment Mode
	When I canceled "parent" batch by database
	When I go to Settings page
	When I set Experiment Mode Status "true"
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for "Sorting"
	Then I fill-out the Sorting-planchet by 1 samples of test "HELICOBACTER_PYLORI_ДНК"
	Then I click end sorting button
	And I go to Batches tab and check status "Собран"
	