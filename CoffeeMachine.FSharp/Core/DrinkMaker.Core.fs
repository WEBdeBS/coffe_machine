module DrinkMaker.Core

open DrinkMaker.Data
open System.Text.RegularExpressions
open System

let parseBeverage beverage =
  match beverage with
  |"T" -> Tea
  |"H" -> Chocolate
  |"C" -> Coffee
  |"O" -> Orange
  |_ -> InvalidOrder

let parseSugar sugar =
    if String.IsNullOrEmpty sugar
    then 0
    else System.Int32.Parse sugar

let parseMoney money =
  if String.IsNullOrEmpty money
  then 0.0
  else Double.Parse money

let parseSpoons sugar =
  sugar > 0

let parseExtraHot (h:String) : bool =
  if String.IsNullOrEmpty h
  then false
  else h.EndsWith ("h")

let parseOrderString order =
  let pattern = "^(\w{1})(h?)\:(\d*)\:(\d+\.\d+)$"
  let matches = Regex.Match(order, pattern)
  let beverageType = matches.Groups.[1].Value
  let extraHot = matches.Groups.[2].Value
  let sugar = matches.Groups.[3].Value
  let money = matches.Groups.[4].Value
  beverageType, extraHot, sugar, money

//I don't like parenthesis :O)
let invertedSubtract x y =
  y - x

let makeBeverage' priceList orderStr =
  let beverageType, extraHot, sugar, moneyInserted =
    parseOrderString orderStr
  match parseBeverage beverageType, parseMoney moneyInserted, parseExtraHot extraHot with
  | InvalidOrder,_,_ -> None |> Drink
  | _, m, _ when  beverageType |> parseBeverage |> priceList |> invertedSubtract  m >  0.0 ->
        sprintf "%.1f Euros missing" (beverageType |> parseBeverage |> priceList |> invertedSubtract  m) |> Message
  | b, _, h when b = Orange && h  -> "Cannot make an hot Orange Juice" |> Message
  | _ , _, h ->  Some { Beverage= parseBeverage beverageType; Sugar = parseSugar sugar;
                  Stick =  parseSugar >> parseSpoons <| sugar; ExtraHot = h }
                  |> Drink
