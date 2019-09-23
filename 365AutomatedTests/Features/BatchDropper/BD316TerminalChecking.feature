Feature: BD316TerminalChecking
Создан на основе BATCH-316. Проверяет корректность отображения и выбора терминала.
created by Evgrafova L.O. 13/09/19

@mytag
Scenario: BATCH-316_TerminalChecking
	Given I login as admin "BatchDropper" and don't select the terminal
	Then I go to workplace "Сортировка" and check that field for choosing terminal is exists
	When I introduce the terminal, I check terminal name field
	Then I set Terminal number "1"
	When I go to workplace "Выделение ДНК в пробирке", I check that Terminal number "1" exists  
	When I go to workplace "Прием в постановке", I check that Terminal number "1" exists 
	Then I delete all cookies and refresh page

Scenario: TerminalChecking2
	Given I login as admin "BatchDropper" and don't select the terminal
	Then I go to workplace "Выделение ДНК в пробирке" and check that field for choosing terminal is exists
	Then I set Terminal number "2"
	Then I delete all cookies and refresh page

Scenario: TerminalChecking3
	Given I login as admin "BatchDropper" and don't select the terminal
	Then I go to workplace "Прием в постановке" and check that field for choosing terminal is exists
	Then I set Terminal number "6"
	
