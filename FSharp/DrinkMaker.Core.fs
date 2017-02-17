module DrinkMaker.Core
open DrinkMaker.Data
open System.Text.RegularExpressions
open System

let parseBeverage beverage =
  match beverage with
  |"T" -> Tea
  |"H" -> Chocolate
  |"C" -> Coffee
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

let parseOrderString order =
  let pattern = "^(\w*)\:(\d*)\:(\d+\.\d+)$"
  let matches = Regex.Match(order, pattern)
  let beverageType = matches.Groups.[1].Value
  let sugar = matches.Groups.[2].Value
  let money = matches.Groups.[3].Value
  beverageType, sugar, money

//I don't like parenthesis :O)
let invertedSubtract x y =
  y - x

let makeBeverage' priceList orderStr =
  let beverageType, sugar, moneyInserted =
    parseOrderString orderStr
  match parseBeverage beverageType, parseMoney moneyInserted with
  | InvalidOrder,_ -> None |> Drink
  | (_, m) when  beverageType |> parseBeverage |> priceList |> invertedSubtract  m >  0.0 -> sprintf "%.1f Euros missing" (beverageType |> parseBeverage |> priceList |> invertedSubtract  m) |> Message
  | (_ , _) ->  Some { Beverage= parseBeverage beverageType; Sugar = parseSugar sugar;
                  Stick =  parseSugar >> parseSpoons <| sugar } |> Drink
