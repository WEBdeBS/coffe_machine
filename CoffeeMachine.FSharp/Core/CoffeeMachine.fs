module CoffeeMachine.Main

open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.Maker
open CoffeeMachine.DrinkRepository

let make orderStr =
  make''' drinkRepository display makeBeverage orderStr

let printReceipt =
  printReceipt'' drinkRepository display
