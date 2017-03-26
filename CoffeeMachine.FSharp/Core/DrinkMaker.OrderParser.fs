module DrinkMaker.OrderParser.Main

open DrinkMaker.OrderParser.Core
open DrinkMaker.Data
open Chessie.ErrorHandling

let parseOrder orderStr  =
    let beverageType, extraHot, sugar, moneyInserted = parseOrderString orderStr
    {
      Beverage = parseBeverage beverageType
      ExtraHot = parseExtraHot extraHot
      Sugar = parseSugar sugar
      Stick = sugar |> parseSugar |> parseSpoons
      MoneyInserted = parseMoney moneyInserted
    }    

let parseOrderFunctor orderStr  =
  try
    let beverageType, extraHot, sugar, moneyInserted = parseOrderString orderStr
    ok {
      Beverage = parseBeverage beverageType
      ExtraHot = parseExtraHot extraHot
      Sugar = parseSugar sugar
      Stick = sugar |> parseSugar |> parseSpoons
      MoneyInserted = parseMoney moneyInserted
    }
    with 
      | _ -> fail ("Cannot understand order")
