module DrinkMaker.QuantityChecker.Core

open DrinkMaker.Data
open Chessie.ErrorHandling


let beverageQuantityChecker'' isEmpty notifyMissingDrink beverage =
  match isEmpty beverage with
  | true -> notifyMissingDrink beverage
            sprintf "%A is empty. Vendor has been notified" beverage |> Some
  | _ -> None


let checkQuantity isEmpty notifyMissingDrink beverage =
  match isEmpty beverage with
  | true -> notifyMissingDrink beverage
            sprintf "%A is empty. Vendor has been notified" beverage.Beverage |> fail
  | _    -> ok beverage

let putStick beverage =  
  ok {beverage with Stick = beverage.Sugar > 0}