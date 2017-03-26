module DrinkMaker.OrderParser.Main

open DrinkMaker.OrderParser.Core
open DrinkMaker.Data
open Chessie.ErrorHandling

let parseOrder orderStr  =
  try
    let beverageType, extraHot, sugar, moneyInserted = parseOrderString orderStr
    ok {
      Beverage = parseBeverage beverageType
      ExtraHot = parseExtraHot extraHot
      Sugar = parseSugar sugar
      Stick = false
      MoneyInserted = parseMoney moneyInserted
      ListPrice = 0.0
    }
    with 
      | _ -> fail ("Cannot understand order")
