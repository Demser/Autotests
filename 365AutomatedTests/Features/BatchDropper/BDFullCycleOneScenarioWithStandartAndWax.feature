Feature: BDFullCycleOneScenarioWithStandartAndWax
Еnd-2-End autotest for BatchDropper. Used full cycle from create a test to procces
results in LabWare app. Was writting by Dyomin S.

Scenario: BATCH-1162_FullCycleOneScenarioWithStandartAndWax
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name                      | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex  | rox  |
	| MYCOPLASMA_GENITALIUM_ДНК | abs        | Подгруппа №1 | 99     | false | true  | true     | 1       |            | 1          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | true | true |
	Then Tests counts 1 will be created
	Then I fill-out the Sorting-planchet by "2" samples ""-own of "on"-union test "MYCOPLASMA_GENITALIUM_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I confirm all parent-batches id for start ProductionAcceptance
	When I go to Forming Page
	Then I forming the test "tripod" contains all samples 
	Then I open staging reactions in tubes workplace and start new batch
	When I go to Positive Controls Page
	Then I fill-out the test planchetes by positive controls
	Then I get result file by Amplificator Utilite
	Then I check results, send them and copy file csv to LabWare folder
	Then I start LabWare and processed results