Feature: BDReverseTranscriptionWorkPlace
	This feature is created as a baseline for passing Reverse Transcription Workplace. 
	Created by Evgrafova L. 
	Автотест отдельно не запускается. Фича была написана для отладки шага.

@mytag
Scenario: ReverseTranscriptionWorkPlace
	Given I login as admin "BatchDropper", "1"
	Then I open reverse transcription workplace and start new batch
