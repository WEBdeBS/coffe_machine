module internal DrinkMaker.Core

open DrinkMaker.Data
open DrinkMaker.OrderParser
open QuantityChecker
open Chessie.ErrorHandling
open System

let checkMoneyAndSetListPrice priceList beverage =
  let delta = priceList beverage.Beverage - beverage.MoneyInserted
  if delta > 0.0
  then fail (sprintf "%.1f Euros missing" delta)
  else ok {beverage with ListPrice = priceList beverage.Beverage}

let ``check that beverage makes sense`` beverage =
  if beverage.Beverage = Orange && beverage.ExtraHot
  then fail "Cannot make an hot Orange Juice"
  else ok beverage

let putStick beverage =
   {beverage with Stick = beverage.Sugar > 0}

let makeBeverage' railway orderStr =
  orderStr
  |> railway
  |> function
  | Bad(errors) -> fail errors.[0]
  | Ok(b,_) -> ok b
