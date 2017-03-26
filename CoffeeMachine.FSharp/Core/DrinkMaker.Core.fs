module internal DrinkMaker.Core

open DrinkMaker.Data
open DrinkMaker.OrderParser
open QuantityChecker
open Chessie.ErrorHandling
open System

let notEnoughMoney priceList order =  
   priceList order.Beverage - order.MoneyInserted > 0.0

let missingMoney priceList order =
  priceList order.Beverage - order.MoneyInserted

let checkMoney priceList beverage =
  let delta = priceList beverage.Beverage - beverage.MoneyInserted
  if delta > 0.0
  then fail (sprintf "%.1f Euros missing" delta)
  else ok beverage

let ``check that beverage makes sense`` beverage = 
  if beverage.Beverage = Orange && beverage.ExtraHot
  then fail "Cannot make an hot Orange Juice"
  else ok beverage


let makeBeverage''' parseOrder priceList beverageQuantityChecker orderStr =

  let order = parseOrder orderStr

  match order.Beverage, order.MoneyInserted, order.ExtraHot, beverageQuantityChecker order.Beverage with
  | InvalidOrder, _, _, _ -> orderStr |>  sprintf "%s is not a valid order " |> Message
  | _, _, _, Some m -> m |> Message
  | _, m, _, _ when notEnoughMoney priceList order->
      missingMoney priceList order
      |> sprintf "%.1f Euros missing"
      |> Message
  | b, _, h, _ when b = Orange && h -> "Cannot make an hot Orange Juice" |> Message
  |_, _, _, _ -> Some { order with MoneyInserted = order.Beverage |> priceList } |> Drink



let makeBeverage'''' railway orderStr =
  orderStr
  |> railway
  |> function
  | Ok (b,_) -> b |> Drink
  | Bad (s) -> s.[0] |> Message
