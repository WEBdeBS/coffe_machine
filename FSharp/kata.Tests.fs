
module CoffeMachine.Tests

//open CoffeMachine
open Xunit
open FsUnit.Xunit
open CoffeeMachine.Core

    

[<Fact>]
let ``Green Test``() =
    someThing
    |> should equal "Pippo"

[<Fact>]
let ``It should make tea`` () =

  let order = "T:1:0"
  let beverage = make order

  beverage.Beverage 
  |> should equal Tea
  beverage.Sugar 
  |> should equal 1
  beverage.Stick
  |> should be True

[<Fact>]
  let ``It should make Chocolate with no sugar`` () =
    let order = "H::"
    let beverage = make order

    beverage.Beverage |> should equal Chocolate
    beverage.Sugar |> should equal 0
    beverage.Stick |> should be False
