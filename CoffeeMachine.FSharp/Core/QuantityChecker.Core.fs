module DrinkMaker.QuantityChecker.Core

open DrinkMaker.Data



let beverageQuantityChecker'' isEmpty notifyMissingDrink beverage =
  match isEmpty beverage with
  | true -> notifyMissingDrink beverage
            sprintf "%A is empty. Vendor has been notified" beverage |> Some
  | _ -> None
