Feature: BD695ExperimentsStatusModeTesting
Автотест проверяет логику РМ Сортировка и Выделение ДНК в зависимости от настройки Режим испытаний (по умолчанию включён или true)
Если указана опция true, то в испытания можно пустить любые образцы, не зависимо от наличия резульатов. 
Если указана опция false, то в испытания можно пустить только те образцы, в которых нет результатов, что моделирует работу на проде.
Written by Dyomin S. 30/08/2019 ver 2.8.1.83

@mytag
Scenario: BATCH-695_ExperimentsStatusModeTesting
	Given  I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name              | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex   | rox   |
	| HCV_РНК_ГЕНОТИП_3 | abs        | Подгруппа №2 | 99     | false | false | true     | 1       |            | 1          | Положительный контроль для лунки |               | autoreagent  | true | false | false |
	Then Tests counts 1 will be created

	When I go to Settings page
	When I set Experiment Mode Status "false"
	Then I try to add a sample of test "HCV_РНК_ГЕНОТИП_3" and "HELIX-SPB" location and "ВЕНКРОВЬ" biomaterial "with" results in "sorting" workplace
	Then Message "нет тестов для выполнения" will be "shown"
	When I canceled "parent" batch by database
	Then I try to add a sample of test "HCV_РНК_ГЕНОТИП_3" and "HELIX-SPB" location and "ВЕНКРОВЬ" biomaterial "with" results in "isolation" workplace
	Then Message "нет тестов для выполнения" will be "shown"
	When I canceled "PT" batch by database

	When I go to Settings page
	When I set Experiment Mode Status "true"
	Then I try to add a sample of test "HCV_РНК_ГЕНОТИП_3" and "HELIX-SPB" location and "ВЕНКРОВЬ" biomaterial "without" results in "sorting" workplace
	Then Message "нет тестов для выполнения" will be "hidden"
	When I canceled "parent" batch by database
	Then I try to add a sample of test "HCV_РНК_ГЕНОТИП_3" and "HELIX-SPB" location and "ВЕНКРОВЬ" biomaterial "without" results in "isolation" workplace
	Then Message "нет тестов для выполнения" will be "hidden"
	When I canceled "PT" batch by database
