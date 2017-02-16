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


let makeDisp (display: string -> unit) (orderStr: String) =
  if orderStr.StartsWith("M")
  then showMessage display orderStr
       None |> Drink
  else match makeBeverage orderStr with
       | Message m -> display m
                      None |> Drink
       | Drink d -> d |> Drink

let make orderStr =
  makeDisp display orderStr
