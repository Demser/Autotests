Feature: CCENPersonalDataChecklist
	This autotest written by Dyomin S. based on CCEN-945 test-case (checklist). Сhecks the validation of fields
	in the personal data tab.

@mytag
Scenario: CCEN-945_CCENPersonalDataChecklist
	Given I login as "CcenAuto" and password
	And I go to Calalog new module
	When I go to the personal-data tab
	Then I check that save button is inactive
	Then I check "age" field
	Then I check "birthday" field	
	Then I check "email" field
	Then I check "phone" field
