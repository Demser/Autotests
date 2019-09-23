Feature: BDCreateTestinDictionaries
	Фича для создания теста с возможностью указать каждый из параметров, включая множественный выбор.
	Отдельно фича не запускается. Использовалась для отладки универсального шага.

Scenario: CreateTestWithParameters
	Given I login as admin "BatchDropper", "1"
	When I create test with parameters from the table
	| name                      | ampProgram | subgroups    | volume | isDNA | isWAX | isActive | doubles | reagentsOT          | cellsCount | cellControl                      | cellStandarts         | cellReagents | fam  | hex  | rox  |
	| NEISSERIA_GONORRHOEAE_ДНК | abs        | Подгруппа №1 | 99     | false | false | true     | 5       | Реагент 1;Реагент 2 | 3          | Положительный контроль для лунки | Стандарт 1;Стандарт 1 | autoreagent  | true | true | true |
	Then Tests counts 1 will be created