module DrinkMaker.Tests

open Xunit
open FsUnit.Xunit
open CoffeeMachine.Maker
open CoffeeMachine.PriceList
open DrinkMaker.Data
open DrinkMaker.Core


let extract =
  function
  | Drink d -> match d with
               | Some b -> b
               | None -> failwith "Error!"
  |Message m -> failwithf "I wanted a drink, got a message: %s" m


//[<Theory>]
//[<InlineData("C:1:0.9", Chocolate, 0.6)>]
//let ``I should make a drink if I have enough money`` (order: string) (bType: BeverageType) (sugar: int) (stick: bool) (price: float) =
//  let getPrice beverageType =
//    beverageType |> should equal bType
//    price
//
//  let drink = makeBeverage' getPrice order |> extract
//
//  drink.Beverage |> should equal bType
//  drink.Sugar |> should equal sugar
//  drink.Stick |> should equal stick


[<Fact>]
let ``It can make coffee with enough money`` () =
  let getPrice beverageType =
    beverageType |> should equal Coffee
    0.7
  let order = "C:1:0.9"
  let drink = makeBeverage' getPrice order |> extract

  drink.Beverage |> should equal Coffee
  drink.Sugar |> should equal 1
  drink.Stick |> should be True

[<Fact>]
let ``Cannot make coffee if I don't have enough money`` () =
  let getPrice beverageType =
    beverageType |> should equal Coffee
    0.7
  let order = "C:1:0.4"
  let drink = makeBeverage' getPrice order

  let message =
    match drink with
    | Drink d -> failwith "Error"
    | Message m -> m

  message |> should equal "0.3 Euros missing"

[<Fact>]
let ``Can Make an Orange juice for .6 euros`` () =
  let drink = "O:1:0.6" |> makeBeverage |> extract

  drink.Stick |> should be True
  drink.Beverage |> should equal Orange
  drink.Sugar |> should equal 1
