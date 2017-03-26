module internal CoffeeMachine.Core
open System
open System.Text.RegularExpressions
open CoffeeMachine.Maker
open DrinkMaker.Data
open CoffeeMachine.DrinkRepository.Data
open System.Linq
open Chessie.ErrorHandling

let showMessage display message =
  let pattern  = "^M:(.*)$"
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


let make''' drinkRepository display beverageMaker (orderStr: string)  =
  if orderStr.StartsWith("M")
  then showMessage display orderStr
       None
  else try
         match beverageMaker orderStr with
         | Message m -> display m
                        None
         | Drink drink -> match drink with
                          | Some beverage -> beverage
                                             |> fst drinkRepository
                                             |> Some

                          | None -> None
       with
         | _ -> display "Cannot understand order"
                None

let make'''' drinkRepository display beverageMaker (orderStr: string) =
  if orderStr.StartsWith("M")
    then showMessage display orderStr
         None
    else orderStr
         |> beverageMaker
         |> function
         | Bad message -> display message.[0]
                          None
         | Ok (beverage,_) -> beverage
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
