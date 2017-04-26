module internal CoffeeMachine.Core

open System
open System.Text.RegularExpressions
open CoffeeMachine.Maker
open DrinkMaker.Data
open CoffeeMachine.DrinkRepository.Data
open System.Linq
open Chessie.ErrorHandling

let display message =
  printfn "%s" message

let printReport' display reportLine =
  let d, c, t = reportLine
  sprintf "%A: %i; Total: %.2f" d c t
  |> display
  true

let printReport aTuple =
    printReport' display aTuple

let printTotal display reportLines =
  reportLines
  |> Seq.map (fun (d,c,t) -> t)
  |> Seq.reduce (+)
  |> sprintf "\nGrand total: %.2f\n"
  |> display

let printReceipt'' repository display =
  let data = Queryable.AsQueryable<BeverageReport>(snd repository)
  query {
    for line in data do
    groupBy line.Beverage into g
    let lineTotal =
      query {
        for beverage in g do
        sumBy beverage.Price
      }
    select (g.Key, g.Count(), lineTotal)
  }
  |> Seq.filter (fun r -> printReport' display r)
  |> printTotal display


let private (|OrderStr|MessageStr|ReportStr|OtherStr|) input =
    if Regex.IsMatch(input, orderPattern) then OrderStr (input)
    elif Regex.IsMatch(input, messagePattern)
      then MessageStr (Regex.Match(input, messagePattern).Groups.[1].Value)
    elif Regex.IsMatch(input, reportPattern) then ReportStr(input)
    else OtherStr(input)

let make''' drinkRepository display beverageMaker orderStr =

  orderStr
  |> function
  | MessageStr m -> display m
                    None
  | OtherStr o-> printf "\nInvalid Order: %s\n\n" o
                 None
  | ReportStr r -> printReceipt'' drinkRepository display
                   None
  | OrderStr o-> o
                |> beverageMaker
                |> function
                | Bad message -> display message.[0]
                                 None
                | Ok (beverage, _) -> beverage                                    
                                      |> Some
