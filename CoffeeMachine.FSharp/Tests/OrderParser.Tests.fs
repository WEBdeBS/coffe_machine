module OrderParser.Tests

open FsUnit.Xunit
open Xunit
open DrinkMaker.Core
open CoffeeMachine.PriceList
open DrinkMaker.Data
open Microsoft.FSharp.Reflection
open DrinkMaker.OrderParser.Main
open Chessie.ErrorHandling
open System.Text.RegularExpressions

let fromString<'a> (s:string) =
    match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
    |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
    |_ -> None

let extract =
    function
    | Some i ->i
    | None -> failwith "Error!!"

let extractOk =
    function
    | Ok(value, _) -> value
    | Bad(_) -> failwith "Error!"


[<Theory>]
[<InlineData("C:2:0.9", "Coffee", 2, 0.9,false)>]
[<InlineData("Ch:2:0.9", "Coffee", 2,0.9,true)>]
[<InlineData("T:2:0.9", "Tea", 2, 0.9,false)>]
[<InlineData("Th:2:0.9", "Tea", 2,0.9,true)>]
[<InlineData("H:2:0.9", "Chocolate", 2, 0.9,false)>]
[<InlineData("Hh:2:0.9", "Chocolate", 2,0.9,true)>]
[<InlineData("O:2:0.9", "Orange", 2, 0.9,false)>]
let ``I should be able to parse an order`` (order: string) (bType: string) (sugar: int) (moneyInserted: float) (extraHot: bool) =


    let bev =
        {
        Beverage = fromString<BeverageType> bType |> extract
        ExtraHot = extraHot
        MoneyInserted = moneyInserted
        ListPrice = 0.0
        Sugar = sugar
        Stick = false
    }

    order
    |> parseOrder
    |> extractOk
    |> should equal bev
[<Theory>]
[<InlineData("M:La figa!!")>]
[<InlineData("Th:2:0.9")>]
[<InlineData("Pippo")>]
let ``Can use active patterns`` (input: string) =
      let orderPattern = "^(\w{1})(h?)\:(\d*)\:(\d+\.\d+)$"
      let messagePattern = "^M:(.*)$"

      let (|OrderStr|MessageStr|OtherStr|) (input:string) =
        if Regex.IsMatch(input, orderPattern) then OrderStr
        elif Regex.IsMatch(input, messagePattern) then MessageStr
        else OtherStr


      match input with
      | MessageStr m -> printfn "%s è un messaggio!" input
      | OrderStr o -> printfn "%s è un ordine!" input
      | _ -> printfn "Cannot understand order %s" input
