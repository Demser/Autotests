Feature: BD440ManualDrippingOneTestFullTablet
	Аатотест написан на основании тест-кейса BATCH-440. Автоматизирует прохождение от создания 12-луночного теста до раскапывания положительных контролей.
	Описывает логику полного заполнения планшета типа тест. 
	Written by Dyomin S. 07/08/2019 ver 2.7.2.80

Scenario: BATCH-440_ManualDrippingOneTestFullTablet
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name                 | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex  | rox   |
	| CANDIDA_ALBICANS_ДНК | abs        | Подгруппа №1 | 99     | false | false | true     | 1       |            | 12         | Положительный контроль для лунки | Стандарт 1    | autoreagent  | true | true | false |	
	Then Tests counts 1 will be created
	Then I fill-out the Sorting-planchet by "3" samples ""-own of ""-union test "CANDIDA_ALBICANS_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO
	When I confirm all parent-batches id for start ProductionAcceptance
	When I go to Forming Page
	Then I forming the test "tablet" contains all samples 
	When I go to Reagents page
	Then I fill-out the reagents planchetes
	When I go to Manual Dripping Page
	Then I fill-out the test planchetes by manual dripping
	When I go to Positive Controls Page
	Then I fill-out the test planchetes by positive controls