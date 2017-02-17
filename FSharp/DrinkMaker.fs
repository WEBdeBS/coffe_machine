module CoffeeMachine.Maker
open CoffeeMachine.PriceList
open System.Text.RegularExpressions
open System



type Beverage = {
    Beverage: BeverageType
    Sugar: int
    Stick: bool
}

type Drink =
  | Message of string
  | Drink of Beverage option


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

let makeBeverageWPriceList priceList orderStr =
  let beverageType, sugar, moneyInserted =
    parseOrderString orderStr
  match parseBeverage beverageType, parseMoney moneyInserted with
  | InvalidOrder,_ -> None |> Drink
  | (_, m) when  ((beverageType |> parseBeverage |> priceList) - m) >  0.0 -> sprintf "%.1f Euros missing" ((beverageType |> parseBeverage |> priceList) - m) |> Message
  | (_ , _) ->  Some { Beverage= parseBeverage beverageType; Sugar = parseSugar sugar;
                  Stick =  parseSugar >> parseSpoons <| sugar } |> Drink

let makeBeverage orderStr =
  makeBeverageWPriceList priceList orderStr
