module CoffeeMachine.PriceList
open System.Collections.Generic

type BeverageType = Tea | Coffee | Chocolate | InvalidOrder

let prices = dict[Tea, 0.4; Coffee, 0.6; Chocolate, 0.5]

let priceList beverageTYpe =
  prices.Item beverageTYpe
