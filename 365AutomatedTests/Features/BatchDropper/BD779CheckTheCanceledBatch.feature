Feature: BD779CheckTheCanceledBatch
Проверка на запрет на работу с отмененным бэтчем. Для РМ Сортировка и Выделение ДНК.
Written by Dyomin S. 09/09/2019 ver 2.8.1.83

Background: Clear the database and login on 365
	Given I clear all data except multiplexes from BatchDropper database
	Given I login as admin "BatchDropper", "1"

Scenario: BATCH-779_1_Sorting
	Given I clear the data from BatchDropper database
	When I create test with parameters from the table
	| name                     | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex   | rox   |
	| BORDETELLA_PERTUSSIS_ДНК | abs        | Подгруппа №3 | 99     | false | false | true     | 1       |            | 1          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | false | false |
	Then Tests counts 1 will be created
	Then I try to add a sample of test "BORDETELLA_PERTUSSIS_ДНК" and "HELIX-SPB" location and "МАЗЗЕВ" biomaterial "with" results in "sorting" workplace
	When I canceled "parent" batch by database
	When Being in "sorting" workplace I add "1" sample of test "BORDETELLA_PERTUSSIS_ДНК" and "HELIX-SPB" location and "МАЗЗЕВ" biomaterial "with" results
	Then I check title for canceled batch

Scenario:  BATCH-779_2_IsolationDNA
	Then I try to add a sample of test "BORDETELLA_PERTUSSIS_ДНК" and "HELIX-SPB" location and "МАЗЗЕВ" biomaterial "with" results in "isolation" workplace
	When I canceled "PT" batch by database
	When Being in "isolation" workplace I add "1" sample of test "BORDETELLA_PERTUSSIS_ДНК" and "HELIX-SPB" location and "МАЗЗЕВ" biomaterial "with" results
	#Then I check title for canceled batch
	# шаг закомментирован до исправления баги BATCH-2143