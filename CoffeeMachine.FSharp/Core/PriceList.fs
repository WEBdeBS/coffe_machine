module CoffeeMachine.PriceList

open System.Collections.Generic

open DrinkMaker.Data
open Chessie.ErrorHandling

let private prices = [Tea, 0.4; Coffee, 0.6; Chocolate, 0.5; Orange, 0.6] |> dict

let priceOf beverageType =
  if prices.ContainsKey beverageType
    then prices.Item beverageType
  else failwith "Invalid Beverage!"

let ``Check that drink exists`` beverage =
  if prices.ContainsKey beverage.Beverage
    then ok beverage
  else fail "Got an invalid order"
