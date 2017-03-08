module CoffeeMachine.Maker
open DrinkMaker.Data
open CoffeeMachine.PriceList
open System.Text.RegularExpressions
open System

let makeBeverage orderStr =
  DrinkMaker.Core.makeBeverage' priceList orderStr
