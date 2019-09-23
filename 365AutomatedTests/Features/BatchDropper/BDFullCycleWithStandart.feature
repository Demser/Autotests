Feature: BDFullCycleWithStandart
	This test is based on BATCH-1162
	Describes creating a test with a standart and passing a full cycle from Sorting to Positive Controls.


Scenario: BATCH-1162_1_CreateTest
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name                      | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex  | rox  |
	| MYCOPLASMA_GENITALIUM_ДНК | abs        | Подгруппа №1 | 99     | false | true  | true     | 1       |            | 1          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | true | true |
	Then Tests counts 1 will be created

Scenario: BATCH-1162_2_Sorting
	Given I login as admin "BatchDropper", "1"
#	When I confirm parent-batch id for start Sorting 
#	And I enter usercode in the field on the planchet-position form for Sorting
	Then I fill-out the Sorting-planchet by "5" samples ""-own of "on"-union test "MYCOPLASMA_GENITALIUM_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO

Scenario: BATCH-1162_3_ProductionAcceptance
	Given I login as admin "BatchDropper", "1"
	When I confirm all parent-batches id for start ProductionAcceptance

Scenario: BATCH-1162_4_Forming
	Given I login as admin "BatchDropper", "1"
	When I go to Forming Page
	Then I forming the test "tripod" contains all samples 

Scenario: BATCH-1162_5_ReactionInTubes
	Given I login as admin "BatchDropper", "1"
	Then I open staging reactions in tubes workplace and start new batch

Scenario: BATCH-1162_6_PositiveControls
	Given I login as admin "BatchDropper", "1"
	When I go to Positive Controls Page
	Then I fill-out the test planchetes by positive controls