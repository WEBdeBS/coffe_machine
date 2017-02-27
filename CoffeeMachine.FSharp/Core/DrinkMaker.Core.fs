module internal DrinkMaker.Core

open DrinkMaker.Data
open DrinkMaker.OrderParser
open QuantityChecker
open System


let makeBeverage'' parseOrder priceList orderStr =
  let order = parseOrder orderStr
  match order.Beverage, order.MoneyInserted, order.ExtraHot with
  | InvalidOrder, _, _ -> None |> Drink
  | _, m, _ when ( (order.Beverage |> priceList) - order.MoneyInserted) > 0.0 ->
      sprintf "%.1f Euros missing" ((order.Beverage |> priceList) - order.MoneyInserted)
      |> Message
  | b, _, h when b = Orange && h -> "Cannot make an hot Orange Juice" |> Message
  |_, _, _ -> Some { order with MoneyInserted = (order.Beverage |> priceList) } |> Drink
