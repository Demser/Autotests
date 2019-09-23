Feature: BD_Isolation_DNA_WorkPlace
Was updated by Evgrafova L.O.

@mytag
Scenario: BD_Isolation_DNA_WorkPlace
	Given I login as admin "BatchDropper", "1"
	When I open isolation DNA workplace and start new batch 
	Then I fill-out the isolation-planchet by "10" samples "" of test "MYCOPLASMA_GENITALIUM_ДНК" with biomaterial "СОСКОБУГ" and location "HELIX-SPB" and 'Внутренний контроль 1' VK and 'Отрицательный контроль 1' OKO and 'Положительный контроль 1' PKO