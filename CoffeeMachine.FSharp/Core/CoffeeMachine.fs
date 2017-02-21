module CoffeeMachine.Main

open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.Maker
open CoffeeMachine.DrinkRepository

let make orderStr : Beverage option=
  make''' drinkRepo display makeBeverage orderStr
