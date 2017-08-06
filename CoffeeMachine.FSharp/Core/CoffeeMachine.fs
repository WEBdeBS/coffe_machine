module CoffeeMachine.Main

open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.Maker
open CoffeeMachine.DrinkRepository.Main
open System
open Chessie.ErrorHandling
open System.Collections.Generic

let railway =  
  displayMessage
  >> bind invalidOrder
  >> bind (print'' drinkRepository display)
  >> bind (takeOrder'' makeBeverage)

//let parallelRailway order =
//  trial{
//    let displayMessage = displayMessage' display order
//    let checkValid = invalidOrder order
//    let takeOrder = takeOrder'' display makeBeverage order
//    let print = print'' drinkRepository display order
//
//    let! result::_ = [displayMessage; checkValid; print] |> collect
//
//    return
//      takeOrder
//      |> function
//      | Ok (beverage, _) -> beverage
//      | Bad message -> failwith result
//  }


let make =
  make' railway
  //make' parallelRailway

let usage args =
  if Array.length args <> 1
    then printfn "Usage is CoffeeMachine report|<order>"
         false
    else true

let printReceipt () =
  let receipt = new List<string>();
  let display = fun line -> receipt.Add(line)
  printReceipt'' drinkRepository display
  receipt
