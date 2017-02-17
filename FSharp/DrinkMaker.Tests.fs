module DrinkMaker.Tests

open Xunit
open FsUnit.Xunit
open CoffeeMachine.Maker
open CoffeeMachine.PriceList


let extract =
  function
  | Drink d -> match d with
               | Some b -> b
               | None -> failwith "Error!"
  |Message m -> failwithf "I wanted a drink, got a message: %s" m


[<Fact>]
let ``It can make coffee with enough money`` () =
  let getPrice beverageType =
    beverageType |> should equal Coffee
    0.7
  let order = "C:1:0.9"
  let drink = makeBeverageWPriceList getPrice order |> extract

  drink.Beverage |> should equal Coffee
  drink.Sugar |> should equal 1
  drink.Stick |> should be True

[<Fact>]
let ``Cannot make coffee if I don't have enough money`` () =
  let getPrice beverageType =
    beverageType |> should equal Coffee
    0.7
  let order = "C:1:0.4"
  let drink = makeBeverageWPriceList getPrice order

  let message =
    match drink with
    | Drink d -> failwith "Error"
    | Message m -> m

  message |> should equal "0.3 Euros missing"
