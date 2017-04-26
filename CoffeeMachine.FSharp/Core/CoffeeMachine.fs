module CoffeeMachine.Main

open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.Maker
open CoffeeMachine.DrinkRepository.Main
open System
open Chessie.ErrorHandling

let make =
  make''' drinkRepository display makeBeverage 

let makeComposed  order =  
  let displayMessage = displayMessage' display
  let print = print'  drinkRepository display
  let takeOrder = takeOrder'' display makeBeverage

  order
  |> displayMessage
  >>= invalidOrder  
  >>= print
  >>= takeOrder
   


let usage args =
  if Array.length args <> 1
    then printfn "Usage is CoffeeMachine report|<order>"
         false
    else true
