module CoffeeMachine.Core
open System
open System.Text.RegularExpressions
open CoffeeMachine.Maker
open DrinkMaker.Data

let someThing = "Pippo"


let showMessage display message =
  let pattern  = "^M:(.*)$"
  let matches = Regex.Match(message, pattern)
  display matches.Groups.[1].Value

let display message =
  printfn "%s" message

let make'' maker display (orderStr: string) =
  if orderStr.StartsWith("M")
  then showMessage display orderStr
       None
  else match makeBeverage orderStr with
       | Message m -> display m
                      None
       | Drink d -> d

let make' display orderStr =
  make'' makeBeverage display orderStr
