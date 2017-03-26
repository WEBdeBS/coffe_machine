module CoffeeMachine.Main

open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.Maker
open CoffeeMachine.DrinkRepository.Main

let make orderStr =
  make''' drinkRepository display makeBeverage orderStr

let makeWithFunctors orderStr=
  make'''' drinkRepository display makeBeverageWithFunctors orderStr

let printReceipt =
  printReceipt'' drinkRepository display
