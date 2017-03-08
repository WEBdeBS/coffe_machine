module internal DrinkMaker.Core

open DrinkMaker.Data
open DrinkMaker.OrderParser
open QuantityChecker
open System

let private subtractFromSecond x y =
  y - x


let makeBeverage''' parseOrder priceList beverageQuantityChecker orderStr =

  let order = parseOrder orderStr

  match order.Beverage, order.MoneyInserted, order.ExtraHot, beverageQuantityChecker order.Beverage with
  | InvalidOrder, _, _, _ -> orderStr |>  sprintf "%s is not a valid order " |> Message
  | _, _, _, Some m -> m |> Message
  | _, m, _, _ when ( order.Beverage |> priceList |> subtractFromSecond order.MoneyInserted) > 0.0 ->
      order.Beverage |> priceList |> subtractFromSecond order.MoneyInserted
      |> sprintf "%.1f Euros missing"
      |> Message
  | b, _, h, _ when b = Orange && h -> "Cannot make an hot Orange Juice" |> Message
  |_, _, _, _ -> Some { order with MoneyInserted = order.Beverage |> priceList } |> Drink
