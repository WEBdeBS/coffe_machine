module DrinkMaker.OrderParser.Main

open DrinkMaker.OrderParser.Core
open DrinkMaker.Data


let parseOrder orderStr  =
  let beverageType, extraHot, sugar, moneyInserted = parseOrderString orderStr
  {
    Beverage = parseBeverage beverageType
    ExtraHot = parseExtraHot extraHot
    Sugar = parseSugar sugar
    Stick = sugar |> parseSugar |> parseSpoons
    MoneyInserted = parseMoney moneyInserted
  }
