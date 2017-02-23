module QuantityChecker
open DrinkMaker.QuantityChecker.Core


let beverageQuantityChecker beverage =
  beverageQuantityChecker'' (fun b -> false) (fun b -> ())
