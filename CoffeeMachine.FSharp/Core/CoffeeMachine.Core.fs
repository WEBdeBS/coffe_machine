module internal CoffeeMachine.Core
open System
open System.Text.RegularExpressions
open CoffeeMachine.Maker
open DrinkMaker.Data
open CoffeeMachine.DrinkRepository.Data
open System.Linq
open Chessie.ErrorHandling

let (|OrderStr|MessageStr|OtherStr|) (input:string) =
  if Regex.IsMatch(input, orderPattern) then OrderStr
  elif Regex.IsMatch(input, messagePattern) then MessageStr
  else OtherStr

let showMessage display message =
  let pattern  = messagePattern
  let matches = Regex.Match(message, pattern)
  display matches.Groups.[1].Value

let display message =
  printfn "%s" message

let printReport' display reportLine =
  let d, c, t = reportLine
  sprintf "%A: %i; Total: %.2f" d c t
  |> display
  true

let printReport aTuple =
    printReport' display aTuple

let make''' drinkRepository display beverageMaker orderStr =
  orderStr
  |> function
  | MessageStr -> showMessage display orderStr
                  None
  | OtherStr -> orderStr
                |> sprintf "Invalid Order: %s"
                |> display
                None
  | OrderStr -> orderStr
                |> beverageMaker
                |> function
                | Bad message -> display message.[0]
                                 None
                | Ok (beverage, _) -> beverage
                                      |> fst drinkRepository
                                      |> Some

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
