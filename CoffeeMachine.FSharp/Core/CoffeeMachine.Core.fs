module internal CoffeeMachine.Core
open System
open System.Text.RegularExpressions
open CoffeeMachine.Maker
open DrinkMaker.Data
open CoffeeMachine.DrinkRepository
open System.Linq

let showMessage display message =
  let pattern  = "^M:(.*)$"
  let matches = Regex.Match(message, pattern)
  display matches.Groups.[1].Value

let display message =
  printfn "%s" message

let printReport' display reportLine =
  let d, c, t = reportLine
  sprintf "%A: %i; Total: %.2f" d  c t
  |> display

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
let printTotal display reportLines =
  reportLines
  |> Seq.map (fun (d,c,t) -> t)
  |> Seq.reduce (+)
  |> sprintf "\nGrand total: %.2f\n"
  |> display

let printReceipt'' repository display =
  let data = Queryable.AsQueryable<BeverageReport>(snd repository)
  let report = query {
    for line in data do
    groupBy line.Beverage into g
    let lineTotal =
      query {
        for beverage in g do
        sumBy beverage.Price
      }
    select (g.Key, g.Count(), lineTotal)
  }
  report
  |> Seq.iter (fun r -> printReport' display r)

  printTotal display report
