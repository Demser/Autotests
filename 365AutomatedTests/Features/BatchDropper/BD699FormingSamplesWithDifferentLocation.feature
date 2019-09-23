Feature: BATCH699FormingSamplesWithDifferentLocation
Автотест написан по тест-кейсу BATCH-699. Проверяет отображение проб и бэтчей в зависимости от локации.
30.08.2019 Evgrafova L.

@mytag
Scenario: BATCH-699_FormingSamplesWithDifferentLocation
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name          | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex   | rox   |
	| HPV_16-70_ДНК | abs        | Подгруппа №1 | 99     | false | false | true     | 1       |            | 1          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | false | false |
	Then Tests counts 1 will be created
	Then I check fill-out the isolation-planchet by "3" samples of test "HPV_16-70_ДНК" with biomaterial "СОСКОБУГ" from first location "HELIX-SPB" and second location "HELIX-MSK" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I confirm all parent-batches id for start ProductionAcceptance
	Then I change Terminal to "2"
	When I create test with parameters from the table
	| name          | ampProgram | subgroups   | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex   | rox   |
	| HPV_16-70_ДНК | abs        | Подгруппа 1 | 99     | false | false | true     | 1       |            | 1          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | false | false |
	Then Tests counts 1 will be created
	Then I check fill-out the isolation-planchet by "3" samples of test "HPV_16-70_ДНК" with biomaterial "СОСКОБУГ" from first location "HELIX-MSK" and second location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	Then I go to the Batches page to check that from location "HELIX-MSK" is "exists"
	Then I go to the Batches page to check that from location "HELIX-SPB" is "not exists"
	Then I change Terminal to "1"
	Then I go to the Batches page to check that from location "HELIX-SPB" is "exists"
	Then I go to the Batches page to check that from location "HELIX-MSK" is "not exists"