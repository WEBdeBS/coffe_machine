module CoffeeMachine.Core
open System
open System.Text.RegularExpressions
open CoffeeMachine.Maker

let someThing = "Pippo"


let showMessage display message =
  let pattern  = "^M:(.*)$"
  let matches = Regex.Match(message, pattern)
  display matches.Groups.[1].Value

let display message =
  printfn "%s" message

let makeDispMaker (maker: string -> Drink) (display: string -> unit) (orderStr: String) =
  if orderStr.StartsWith("M")
  then showMessage display orderStr
       None
  else match makeBeverage orderStr with
       | Message m -> display m
                      None
       | Drink d -> d

let makeDisp display orderStr =
  makeDispMaker makeBeverage display orderStr


let make orderStr =
  makeDispMaker makeBeverage display orderStr
