
module CoffeeMachine.Tests

open Xunit
open FsUnit.Xunit
open DrinkMaker.Data
open CoffeeMachine.PriceList
open CoffeeMachine.Main
open DrinkMaker.Data
open CoffeeMachine.Core

let extract  =
  function
  | Some b -> b
  | None -> failwith "No cup!"

[<Fact>]
let ``It should make tea`` () =

  let order = "T:1:0.6"
  let beverage = order |> make |> extract

  beverage.Beverage
  |> should equal Tea
  beverage.Sugar
  |> should equal 1
  beverage.Stick
  |> should be True
  beverage.Price |> should equal 0.4

[<Fact>]
let ``It should make Chocolate with no sugar`` () =
    let order = "H::0.6"
    let beverage = order |> make |> extract

    beverage.Beverage |> should equal Chocolate
    beverage.Sugar |> should equal 0
    beverage.Stick |> should be False
    beverage.Price |> should equal 0.5

[<Fact>]
let ``It should make Coffee with two sugar and a stick``() =
  let order = "C:2:0.6"
  let drink = make order |> extract
  drink.Beverage |> should equal Coffee
  drink.Sugar |> should equal 2
  drink.Stick |> should be True
  drink.Price |> should equal 0.6

[<Fact>]
let ``It should not make an unknown drink``() =
    let order = "A:0:0.6"

    match make order with
    | None -> ()
    | _ -> failwith "fail!!!"

[<Fact>]
let ``It should display messages on the interface`` () =
  //Mocking function??
  let mutable testMessage = "Pippo"
  let display message =
    testMessage <- message
  let order = "M:message-content"
  let beverage = make' display order
  testMessage |> should equal "message-content"

[<Fact>]
let ``It should Not make coffee if not enough money`` () =
  let order = "C:0:0.2"
  let mutable testMessage = "Pippo"
  let display message =
    testMessage <- message
  let beverage = make' display order
  testMessage |> should equal "0.4 Euros missing"
