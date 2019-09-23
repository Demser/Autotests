Feature: BD433SimpleFormingWithSamplesForOneTest
	Формирование алгоритма раскапывания теперь происходит в рабочем месте Формирование тестовых бэтчей.
	Автотест воспроизводит стандартный цикл от создания теста до раскапывания реагентов, включая проверку формирования.
	writting by Dyomin S. based on BATCH-433
	01.08.2019 ver 2.7.1


Scenario: BATCH-433_SimpleFormingWithSamplesForOneTest
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name         | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex   | rox   |
	| HPV_6-82_ДНК | abs        | Подгруппа №1 | 99     | false | false | true     | 1       |            | 1          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | false | false |
	Then Tests counts 1 will be created
	Then I fill-out the Sorting-planchet by "7" samples ""-own of "on"-union test "HPV_6-82_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I confirm all parent-batches id for start ProductionAcceptance
	When I go to Forming Page
	Then I forming the test "tablet" contains all samples 
	When I go to Reagents page
	Then I fill-out the reagents planchetes