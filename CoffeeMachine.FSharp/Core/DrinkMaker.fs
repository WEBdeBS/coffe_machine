module CoffeeMachine.Maker
open DrinkMaker.Data
open DrinkMaker.Core
open DrinkMaker.OrderParser.Main
open QuantityChecker
open CoffeeMachine.PriceList
open System.Text.RegularExpressions
open System

let makeBeverage orderStr =
  makeBeverage''' parseOrder priceList beverageQuantityChecker orderStr
