Feature: BD435ManualDrippingTwoTestsFullTablet
	Автотест написан по  критичному тест-кейсу BATCH-435 и воспроизводит полный цикл от создания двух многолуночных тестов 
	с одной программой амплификации и одной группой, сортировку образцов, назначенных на оба теста и прохождениме всех РМ
	до РМ Ручное раскапывание включительно
	Written by Dyomin S. 05/08/2019 ver 2.7.2.80


Scenario: BATCH-435_ManualDrippingTwoTestsFullTablet
Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name                      | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex  | rox   |
	| GARDNERELLA_VAGINALIS_ДНК | abs        | Подгруппа №1 | 99     | false | false | true     | 1       |            | 8          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | true | false |
	When I create test with parameters from the table
	| name                   | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex  | rox   |
	| UREAPLASMA_SPECIES_ДНК | abs        | Подгруппа №1 | 99     | false | false | true     | 1       |            | 4          | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | true | false |	
	Then Tests counts 2 will be created
	Then I fill-out the Sorting-planchet by "6" samples ""-own of "on"-union test "GARDNERELLA_VAGINALIS_ДНК,UREAPLASMA_SPECIES_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I confirm all parent-batches id for start ProductionAcceptance
	When I go to Forming Page
	Then I forming the test "tablet" contains all samples 
	When I go to Reagents page
	Then I fill-out the reagents planchetes
	When I go to Manual Dripping Page
	Then I fill-out the test planchetes by manual dripping