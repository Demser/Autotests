Feature: Access_modules_by_role_EditComplexDocs

@AccessModulesByRoleEditComplexDocs
Scenario Outline: Access_modules_by_role_EditComplexDocs
	Given I login as "<role>":"123456" if not logged yet
	And I remove all conclusions in order "<orderNumber>"
	When I click modules and results
	Then I see modules by role "<role>"

	When I go to Conclusions page
	And I enter order number "<orderNumber>" on ConclusionsPage
	And I enter order dateFrom "<dateFrom>"
	And I enter order dateTo "<dateTo>"
	And I click Search button
	And I click conclusion items and go to OrderMainPage
	Then I see conclusion block

	When I upload conclusions by "<hxid>" and "<conclusionFileName>"
	Then I see conclusion by "<hxid>" in conclusion block

	When I donwload conclusions by "<hxid>"
	Then I see conclusions in DownloadedFiles

	When I remove conclusions by "<hxid>" and "<conclusionFileName>"
	Then I see remove confirm dialog
	When I confirm remove
	Then I not see conclusion by "<hxid>" in conclusion block
Examples: 
| role			   | orderNumber          | dateFrom   | dateTo     | hxid   | conclusionFileName          |
| EditComplexDocs  | 30310-2C11C-00508180 | 18-06-2018 | 18-07-2018 | 02-029 | Заключение врача 02-029.pdf |
| EditComplexDocs1 | 30310-2C11C-00508180 | 18-06-2018 | 18-07-2018 | 02-029 | Заключение врача 02-029.pdf |

@AccessModulesByRoleResults
Scenario Outline: Access_modules_by_role_Results
	Given I login as "<role>":"123456" if not logged yet
	And I remove all conclusions in order "<orderNumber>"
	When I click modules and results
	Then I see modules by role "<role>"

	When I go to Conclusions page
	And I enter order number "<orderNumber>" on ConclusionsPage
	And I enter order dateFrom "<dateFrom>"
	And I enter order dateTo "<dateTo>"
	And I click Search button
	And I click conclusion items and go to OrderMainPage
	Then I see conclusion block
	And I not see upload conclusion button
	And I not see delete conclusion button

	When I donwload conclusions by "<hxid>"
	Then I see conclusions in DownloadedFiles
Examples: 
| role	   | orderNumber          | dateFrom   | dateTo     |
| Results  | 30310-2C11C-00508180 | 18-06-2018 | 18-07-2018 |