Feature: BDCheckLightStand
	Проверка световой подставки из интерфейса приложения. Циклическое нажатие кнопки проверить
	с выводом интервала времени проверки. Более расширенная проверка реализована в утилите
	LightStandConfigApp, которая хранится в директории билда.

Scenario: SimpleCheckLightStand
	Given I login as admin "BatchDropper", "1"
	When I go to Devises page
	Then I start check for lightstand
