Feature: PROMOCreatePromotion
	I go to promo-module in 365 portal. I create new promotion with some paremeters

@mytag
Scenario: CreatePromotion
	Given I login as "CcenAuto" and password
	Given I go to Promo module and click "Constructor" item
	When I set promo name, type of payment, period from "01012018" till "31122020"
	When I set mechanic option "Хотя бы одна единица номенклатуры из группы" and assert it
	When I set discount option "Скидка в процентах" with value "50"
	When I set checkboxes federal "true", ispublic "false" doesnotwork "false"
	When I set contract name as "ДЦ Автово (21111)"
	When I set nomenclature as "02-012"
	Then I click save button and automaticly go to promo catalog