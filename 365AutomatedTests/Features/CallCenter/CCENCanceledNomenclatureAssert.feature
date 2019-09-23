Feature: CCENCanceledNomenclatureAssert
	Creating simple preorder with some nomenclature, then set for this nomenclature canceled status 
	and try to create new preorder or edit old preorder.


Scenario: CCEN_959_CanceledNomenclatureAssert
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	#Внимание! Перед выполнением этого сценария необходимо убрать принудительное снятие чекбокса мобильного выезда в следующем шаге
	When I create simple preorder with "21-019" nomenclature and get preorders nubmer
	And I cancel nomenclature "21-019" by script
	When I find preorder by number in journal
	Then I open preorder for edit
	Then The indicate of canceled nomenclature will be shown
	Given I go to Calalog new module
	When I try to create preorder with "canceled" nomenclature "21-019" and check message
	Then I make active nomenclature "21-019"
