module CoffeeMachine.Core
open System
open System.Text.RegularExpressions

let someThing = "Pippo"

type BeverageType = Tea | Coffee | Chocolate


type Beverage = {
    Beverage: BeverageType
    Sugar: int
    Stick: bool
}

let parseOrderString order =
  let pattern = "^(\w*)\:(\d*)\:(\d*)$"
  let matches = Regex.Match(order, pattern)
  let beverageType = matches.Groups.[1].Value
  let sugar = matches.Groups.[2].Value
  let stick = matches.Groups.[3].Value
  beverageType, sugar, stick
let showMessage display message =
  let pattern  = "^M:(.*)$"
  let matches = Regex.Match(message, pattern)
  display matches.Groups.[1].Value


let parseBeverage beverage = 
  match beverage with
  |"T" -> Tea
  |"H" -> Chocolate
  |"C" -> Coffee
  |_ -> failwithf "Unknown drink %s" beverage

let parseSugar sugar =
    if String.IsNullOrEmpty sugar 
    then 0
    else System.Int32.Parse sugar
let parseSpoons sugar =
  sugar > 0

let display message =
  printfn "%s" message

let parseOrder orderStr =
  let beverageType, sugar, stick = 
    parseOrderString orderStr  
        
  { Beverage= parseBeverage beverageType; Sugar = parseSugar sugar; 
    Stick =  parseSugar >> parseSpoons <| sugar }    

let makeDisp (display: string -> unit) (orderStr: String) =  
  if orderStr.StartsWith("M")
  then showMessage display orderStr
       None
  else Some (parseOrder orderStr)

let make orderStr =
  makeDisp display orderStr