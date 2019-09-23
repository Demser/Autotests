Feature: ManualDrippingFullfillingAlgorythm1
created by Anna Shvetc
	Test scenario in JIRA - BATCH-440
	Algorythm 1 (without schedule).
	Stage - Manual Dropping
	The tablet is fullfilled

Background: I login
	Given  I login as admin "BatchDropper", "1"

Scenario: Batch-440_1_CreateTest
	Given I clear the data from BatchDropper database
	When I create test named "HELICOBACTER_PYLORI_ДНК" with 12 cells with "autotest1" group with "absent" isolation control with wax "false" with DNA "false" with Active "true"
	Then Tests counts 1 will be created
	Then I remove all schedules from dictionaries

Scenario: Batch-440_2_Sorting
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	Then I fill-out the Sorting-planchet by 6 samples of test "HELICOBACTER_PYLORI_ДНК"
	Then I click end sorting button

Scenario: Batch-440_3_Reagents
	When I confirm parent-batch id for start ProductionAcceptance
	When I get task for dripping
	And I confirm test-batch id for start ReagentsDripping
	Then I fill-out the Reagents-planchet for 11 times
	And I go to Batches tab and check status "Реагенты собраны"

Scenario: Batch-440_4_ManualDripping
	When I confirm test-batch id for start ManualDripping
	Then the popup with ""Бэтчи и пробы, которые необходимы"" title should be closed
	And I enter usercode in the field on the planchet-position form for Manual
	And I confirm child-batch for start dripping
	And I enter usercode in the field on the planchet-position form for Manual
	Then I start Manual Dripping by confirm userbarcode "5" items
	And I start dripping Negative Controls "12" items
	Then the popup with "Содержимое бэтча" title should be closed
	And I go to Batches tab and check status "Отрицательные контроли собраны"

Scenario: Batch-440_5_PositiveControls
	When I confirm test-batch id for start PositiveControlsDripping
	And I enter usercode in the field on the planchet-position form for PositiveControls
	Then I start PositiveControlsDripping by confirm "111" control "24" item(s)
	And I confirm userbarcode for end dripping
	And I go to Batches tab and check status "Отправлен на прибор"