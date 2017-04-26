module CoffeeMachine.Main

open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.Maker
open CoffeeMachine.DrinkRepository.Main
open System
open Chessie.ErrorHandling

let railway order =
  order
  |> displayMessage' display
  >>= invalidOrder
  >>= print'' drinkRepository display
  >>= takeOrder'' display makeBeverage

let make =
  make' railway

let usage args =
  if Array.length args <> 1
    then printfn "Usage is CoffeeMachine report|<order>"
         false
    else true

let printReport aTuple =
  printReport' display aTuple
