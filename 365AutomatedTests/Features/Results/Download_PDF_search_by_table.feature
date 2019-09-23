Feature: Download_PDF_search_by_table

@DownloadPDFSearchByTable
Scenario Outline: Download_PDF_search_by_table
	Given I login as "ResultsProf":"123456" if not logged yet
	When I go to Results page
	And I go to tab by table
	Then button save PDF unactive

	When I enter dateFrom "<dateFrom>"
	And I enter dateTo "<dateTo>"
	And I enter account "<account>"
	And I enter hxid "<hxid>"
	And I click Search button
	Then button save PDF active
	
	Given I remove PDF file if exists from DownloadedFiles
	When I click button save PDF
	Then download PDF file
Examples: 
| dateFrom   | dateTo     | account                | hxid                          |
| 01/03/2018 | 30/03/2018 | ДЦ на Карповке (10963) | (70-077) - anti-Brucella spp. |