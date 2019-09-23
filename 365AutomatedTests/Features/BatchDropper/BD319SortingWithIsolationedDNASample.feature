Feature: BD319SortingWithIsolationedDNASample
Создано по тест-кейсу BATCH-319. Описывает поведение РМ Сортировка, при добавлении пробы, назначенной на тест, который выделяется в пробирке.
Также описывает добавление этой же пробы в РМ Выделение в ДНК и дальнейшая работа с ней до Формирования тестового бэтча.
Written by Dyomin S. 31/07/2019 ver 2.7.2.80

Scenario: BATCH-319_SortingWithIsolationedDNASample
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name                      | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex  | rox   |
	| MYCOPLASMA_GENITALIUM_ДНК | abs        | Подгруппа №1 | 99     | true  | false | true     | 1       |            | 1          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | true | false |
	Then Tests counts 1 will be created
	Then I try to fill-out the Sorting-planchet by "1" samples "" of test "MYCOPLASMA_GENITALIUM_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I open isolation DNA workplace and start new batch 
	Then I fill-out the isolation-planchet by "2" samples "" of test "MYCOPLASMA_GENITALIUM_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I confirm all parent-batches id for start ProductionAcceptance
	When I go to Forming Page
	Then I forming the test "tablet" contains all samples