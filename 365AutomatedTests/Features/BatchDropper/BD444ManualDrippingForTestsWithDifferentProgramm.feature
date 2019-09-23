Feature: BD444ManualDrippingForTestsWithDifferentProgramm
Тест-кейс создан ан основе BATCH-444. Проверяет вхождение тестов с разными программами амплификации в разные тестовые бэтчи.
21.08.2019 Евграфова Л.О.


@mytag
Scenario: BD-444_ManualDrippingForTestsWithDifferentProgramm
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name        | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex   | rox   |
	| ФЕМОФЛОР_16 | 1          | Подгруппа №1 | 99     | false | false | true     | 1       |            | 8          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | false | false |
	When I create test with parameters from the table
	| name          | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex   | rox   |
	| АНДРОФЛОР_ДНК | 2          | Подгруппа №1 | 99     | false | false | true     | 1       |            | 4          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | false | false |
	Then Tests counts 2 will be created
	Then I fill-out the Sorting-planchet by "6" samples ""-own of "on"-union test "ФЕМОФЛОР_16,АНДРОФЛОР_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I go to bathes page, I check that the tablet consist of "12" samples and batch has status "Собран"
	When I confirm all parent-batches id for start ProductionAcceptance
	Then I check that child-batch was created
	When I check tests with "2" amplification programms on the Formig page
	Then I create all available test batches on the Formig page  
	Then go to the bathes page to check for the presence of "3" bathes in the status "Сформирован"
	Then I check that batches with status "Сформирован" had samples only with test "ФЕМОФЛОР_16" or only with "АНДРОФЛОР_ДНК"
	When I go to Reagents page
	Then I fill-out the reagents planchetes
	When I go to Manual Dripping Page
	Then I fill-out the test planchetes by manual dripping
	Then go to the bathes page to check for the presence of "3" bathes in the status "Отрицательные контроли собраны"
	When I go to database, I check that "3" batches has status "20"
