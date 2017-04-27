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

let parallelRailway order =
  trial{
    let displayMessage = displayMessage' display order
    let checkValid = invalidOrder order
    let takeOrder = takeOrder'' display makeBeverage order
    let print = print'' drinkRepository display order

    let! result::_ = [displayMessage; checkValid; print] |> collect

    return
      takeOrder
      |> function
      | Ok (beverage, _) -> beverage
      | Bad message -> failwith result
  }


let make =
  make' railway
  //make' parallelRailway

let usage args =
  if Array.length args <> 1
    then printfn "Usage is CoffeeMachine report|<order>"
         false
    else true

let printReport aTuple =
  printReport' display aTuple
