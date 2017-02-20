module CoffeeMachine.Maker
open DrinkMaker.Data
open DrinkMaker.Core
open CoffeeMachine.PriceList
open System.Text.RegularExpressions
open System

let makeBeverage orderStr =
  makeBeverage' priceList orderStr
