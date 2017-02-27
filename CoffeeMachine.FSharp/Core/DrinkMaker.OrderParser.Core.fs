module internal DrinkMaker.OrderParser.Core

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

let parseExtraHot h : bool =
  if String.IsNullOrEmpty h
  then false
  else h.EndsWith ("h")

let parseOrderString order =
  let pattern = "^(\w{1})(h?)\:(\d*)\:(\d+\.\d+)$"
  if Regex.IsMatch(order, pattern) then
    let matches = Regex.Match(order, pattern)
    let beverageType = matches.Groups.[1].Value
    let extraHot = matches.Groups.[2].Value
    let sugar = matches.Groups.[3].Value
    let money = matches.Groups.[4].Value
    beverageType, extraHot, sugar, money
  else failwith "Invalid order format"
