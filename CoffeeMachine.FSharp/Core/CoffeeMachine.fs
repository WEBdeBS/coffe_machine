module CoffeeMachine.Main

open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.Maker
open CoffeeMachine.DrinkRepository.Main
open System

let make =
  make''' drinkRepository display makeBeverage 

let usage args =
  if Array.length args <> 1
    then printfn "Usage is CoffeeMachine report|<order>"
         false
    else true
