Feature: LengthOfExecution



Scenario: CCEN_419_1_LengthOfExecutionIsMaxWeight
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I create simple preorder with "42-024" , "40-115" , "21-839" nomenclatures and choose "10963" diagnostical center
	Then I see length of execution "7 суток"

Scenario: CCEN_419_2_LengthOfExecutionIsMaxLength
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I create simple preorder with "02-036" , "02-037" , "02-027" nomenclatures and choose "10963" diagnostical center
	Then I see length of execution "До 12:00 следующего дня"

Scenario: CCEN_419_3_LengthOfExecutionPriorityThanEqualToDelay
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I delay nomenclature "40-007" on "24" hours by script
	When I create simple preorder with "02-009" , "02-010" , "40-007" nomenclatures and choose "10963" diagnostical center
	Then I see length of execution "2 суток"
	Then I make active nomenclature "40-007"

Scenario: CCEN_419_4_LengthOfExecutionIsMaxLengthWithDelay
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I delay nomenclature "40-007" on "48" hours by script
	When I create simple preorder with "02-009" , "02-010" , "40-007" nomenclatures and choose "10963" diagnostical center
	Then I see length of execution "2 суток"
	Then I make active nomenclature "40-007"