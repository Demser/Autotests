Feature: BD631SortingWithFullTablet
	Автотест написан по тест-кейсу BATCH-631. Покрывает кейс от создания теста до выгрузки файла задания на амплификатор (все РМ)
	Особенность: на этапе Сортировки планшет заполнен полностью. Тест без ОКО и ПКО.
	Written by Dyomin S. 16/08/2019 ver 2.7.2.80

Scenario: BATCH-631_SortingWithFullTablet	
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name                      | ampProgram | subgroups | volume | isDNA | isWAX | isActive | doubles | reagentsOT | cellsCount | cellControl                      | cellStandarts | cellReagents | fam  | hex  | rox   |
	| CHLAMYDIA_TRACHOMATIS_ДНК | abs        | Пустая    | 99     | false | false | true     | 1       |            | 1          | Положительный контроль для лунки |               | autoreagent  | true | true | false |
	Then Tests counts 1 will be created
	Then I fill-out the Sorting-planchet by "96" samples ""-own of ""-union test "CHLAMYDIA_TRACHOMATIS_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and '' VK and '' OKO and '' PKO
	When I confirm all parent-batches id for start ProductionAcceptance
	When I go to Forming Page
	Then I forming the test "tablet" contains all samples 
	When I go to Reagents page
	Then I fill-out the reagents planchetes
	When I go to Manual Dripping Page
	Then I fill-out the test planchetes by manual dripping