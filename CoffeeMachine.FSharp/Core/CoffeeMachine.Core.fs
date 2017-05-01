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


let printTotal display reportLines =
  reportLines
  |> Seq.map (fun (d,c,t) -> t)
  |> Seq.reduce (+)
  |> sprintf "\nGrand total: %.2f\n"
  |> display

let printReceipt'' repository display =
  let loadAll = snd repository
  let data = Queryable.AsQueryable<BeverageReport>(loadAll())
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

let displayMessage order =
  order
  |> function
  | MessageStr m -> m
                    |> sprintf "I have a message for you: %s"
                    |> fail
  | _ -> ok order

let invalidOrder order =
  order
  |> function
  | OtherStr o -> o
                  |> sprintf "Invalid Order: %s"
                  |> fail
  | _ -> ok order

let print'' repository display order =
  order
  |> function
  | ReportStr r -> printReceipt'' repository display
                   fail order
  | _ -> ok order

let takeOrder'' beverageMaker order =
  match order with
  | OrderStr o -> o
                  |> beverageMaker
                  |> function
                  | Bad message ->  fail message.[0]
                  | Ok (beverage, _) -> ok beverage
  | _ -> fail order


let make' railway order =
  order
  |> railway
  |> function
  | Ok (beverage, _) -> beverage |> Beverage
  | Bad messages -> messages.[0] |> Message
