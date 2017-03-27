module CoffeeMachine.PriceList

open System.Collections.Generic
open DrinkMaker.Data

let private prices = [Tea, 0.4; Coffee, 0.6; Chocolate, 0.5; Orange, 0.6] |> dict

let priceOf beverageTYpe =
  prices.Item beverageTYpe
