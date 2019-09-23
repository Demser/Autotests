Feature: CCENCreatePreorderAndDeleteAllNomenlatures
	test

@mytag
Scenario: CCEN-950_CreatePreorderAndDeleteNomenclaturesByEdit
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I create simple preorder with "02-025" , "03-010" , "06-047" nomenclatures and get preorders nubmer
	When I find preorder by number in journal
	Then I open preorder for edit and delete all nomenclatures from cart
	Then I see empty cart
	
