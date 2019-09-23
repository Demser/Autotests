Feature: DTSJobsRun
Пробный запуск ДТС пакетов для прогрузки акций через автотесты. Имеем существующую новую акцию. Необходимо запустить два ДТС-пакета
и убедиться что акция попала в таблицу из ACGD

@mytag
Scenario: RunDTSforPromoToACGD
	Given I login as "CcenAuto" and password
	When I start job for tables with letter "C"
	And I start job for tables with letter "NP"
	Then I wait for loading to ACGD and check it
