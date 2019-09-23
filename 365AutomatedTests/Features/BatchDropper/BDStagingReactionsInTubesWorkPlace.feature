Feature: BDStagingReactionsInTubesWorkPlace
This feature is created as a baseline for passing Staging reactions in tubes Workplace. 
Created by Evgrafova L. 
Автотест отдельно не запускается. Фича была написана для отладки шага.

Scenario: StagingReactionsInTubesWorkPlace
	Given I login as admin "BatchDropper", "1"
	Then I open staging reactions in tubes workplace and start new batch
