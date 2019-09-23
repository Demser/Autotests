Feature: CCENDelayedNomenclatureAssert


@mytag
Scenario: CCEN-957_DelayedNomenclatureAssert
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I create simple preorder with "21-071" nomenclature and get preorders nubmer
	When I delay nomenclature "21-071" on "24" hours by script
	When I find preorder by number in journal
	Then I open preorder for edit
	Then The indicate of delayed nomenclature will be shown
	Given I go to Calalog new module
	When I try to create preorder with "delayed" nomenclature "21-071" and check message
	Then I make active nomenclature "21-071"
