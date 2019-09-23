Feature: Add_remove_conclusion

@CheckConclusionBlock
Scenario Outline: 1_Check_conclusion_block
	Given I login as "EditComplexDocs":"123456" if not logged yet
	And I remove all conclusions in order "<orderNumber>"
	When I go to Conclusions page
	And I enter order number "<orderNumber>" on ConclusionsPage
	And I enter order dateFrom "<dateFrom>"
	And I enter order dateTo "<dateTo>"
	And I click Search button
	And I click conclusion items and go to OrderMainPage
	Then I see conclusion block
Examples: 
| orderNumber          | dateFrom   | dateTo     |
| 30310-2C11C-00508180 | 18-06-2018 | 18-07-2018 |

@AddConclusionNegative
Scenario Outline: 2_Add_conclusion_negative
	When I upload conclusions by "<hxid>" and "<conclusionFileName>"
	Then I see message "Допустима загрузка только PDF-файлов." in OrderMainPage
Examples:
| hxid   | conclusionFileName           |
| 02-029 | Заключение врача 02-029.docx |
| 02-029 | Заключение врача 02-029.jpg  |
| 02-029 | Заключение врача 02-029.png  |
| 02-029 | Заключение врача 02-029.xlsx |

@AddConclusion
Scenario Outline: 3_Add_conclusion
	When I upload conclusions by "<hxid>" and "<conclusionFileName>"
	Then I see conclusion by "<hxid>" in conclusion block

	When I donwload conclusions by "<hxid>"
	Then I see conclusions in DownloadedFiles
Examples:
| hxid   | conclusionFileName          |
| 02-029 | Заключение врача 02-029.pdf |
| 40-039 | Заключение врача 40-039.pdf |
| 40-120 | Заключение врача 40-120.pdf |

Scenario Outline: 4_Check_conclusions_in_database
	Given I see conclusions in database by "<orderNumber>"
Examples:
| orderNumber          |
| 30310-2C11C-00508180 |

Scenario Outline: 5_Remove_conclusion_negative
	When I remove conclusions by "<hxid>" and "<conclusionFileName>"
	Then I see remove confirm dialog
	When I not confirm remove
	Then I see conclusion by "<hxid>" in conclusion block
Examples:
| hxid   | conclusionFileName          |
| 02-029 | Заключение врача 02-029.pdf |
| 40-039 | Заключение врача 40-039.pdf |
| 40-120 | Заключение врача 40-120.pdf |

@RemoveConclusion
Scenario Outline: 6_Remove_conclusion
	When I remove conclusions by "<hxid>" and "<conclusionFileName>"
	Then I see remove confirm dialog
	When I confirm remove
	Then I not see conclusion by "<hxid>" in conclusion block
Examples:
| hxid   | conclusionFileName          |
| 02-029 | Заключение врача 02-029.pdf |
| 40-039 | Заключение врача 40-039.pdf |
| 40-120 | Заключение врача 40-120.pdf |

Scenario Outline: 7_Check_conclusions_in_database_negative
	Given I not see conclusions in database by "<orderNumber>"
Examples:
| orderNumber          |
| 30310-2C11C-00508180 |