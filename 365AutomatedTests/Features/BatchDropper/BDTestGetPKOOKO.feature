Feature: BDTestGetPKOOKO

Автотест отдельно не запускается. Фича была написана для отладки шага.

Scenario: BDTestSortingWithKM
	Given I login as admin "BatchDropper", "1"
	When I confirm parent-batch id for start Sorting 
	And I enter usercode in the field on the planchet-position form for Sorting
	Then I fill-out the Sorting-planchet by "10" samples ""-own of "on"-union test "NEISSERIA_GONORRHOEAE_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and '' OKO and 'Положительный контроль 1' PKO