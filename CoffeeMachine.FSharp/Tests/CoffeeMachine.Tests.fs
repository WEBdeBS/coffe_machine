
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
  let mutable called = false
  let res = ok "Pippo"
  let order = "pluto"

  let railway order =
    order |> should equal "pluto"
    called <- true
    res

  make' railway order
  |> function
  | Some m -> m |> should equal "Pippo"
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
  | None -> ()
  |_ -> failwith "What's happening?"

  called |> should be True
