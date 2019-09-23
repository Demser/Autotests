Feature: BD301DictionariesTerminal
Автотест проверяет валидацию справочника Терминалы. 16/08/2019 ver 2.7.2.80
@mytag
Scenario: BD301DictionariesTerminal
	Given I login as admin "BatchDropper", "1"
	And I have opened Settings Terminals 
	Then I check input fields on the terminal page
	And create new terminal, refresh page and check that it exist
	Then I try to create terminal with not unique filds
	Then I try to change input fields in new terminal
	Then I try to delete new terminal
	
