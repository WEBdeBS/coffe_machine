
module CoffeeMachine.Tests

open Xunit
open FsUnit.Xunit
open System.Linq
open DrinkMaker.Data
open CoffeeMachine.PriceList
open CoffeeMachine.Main
open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.DrinkRepository.Data
open Chessie.ErrorHandling


[<Fact>]
let ``The coffee machine can make a drink``() =
  let beverage = {
    Beverage = Tea
    ExtraHot = true
    Sugar = 1
    Stick = true |> Some
    MoneyInserted = 0.9
    ListPrice = 0.6 |> Some
  }

  let mutable called = false
  let order = "pluto"

  let railway order =
    order |> should equal "pluto"
    called <- true
    beverage |> ok

  make' railway order
  |> function
  | Beverage b -> b |> should equal beverage
  |_ -> failwith "What's happening?"

  called |> should be True

[<Fact>]
let ``The coffee machine can also not make a drink``() =
  let mutable called = false
  let res = fail "Pippo"
  let order = "pluto"

  let railway order =
     order |> should equal "pluto"
     called <- true
     res

  make' railway order
  |> function
  | Message m -> ()
  |_ -> failwith "What's happening?"

  called |> should be True
