Feature: BD828DictionariesReagents
	Валидация полей и проверка функциональных кнопок в Справочнике Реагенты.
	writting by Evgrafova L. based on BATCH-828
	07.08.2019 ver 2.7.1

@mytag
Scenario: BD-828_DictionariesReagents
	Given I login as admin "BatchDropper", "1"
	Then I have opened Settings Reagents
	When I create new reagent, I check that it exist and check it in database
	Then I check input fields on the reagents dictionary page
	
