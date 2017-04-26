
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

let extract  =
  function
  | Some b -> b
  | None -> failwith "No cup!"

let mutable saved = false
let save (beverage: Beverage) =
  saved <- true
  beverage

let mutable loaded = false
let loadAll=
  loaded <- true
  [{Beverage = Coffee; Price = 0.6};
  {Beverage = Chocolate; Price = 0.5};
  {Beverage = Coffee; Price = 0.6}]

let fakeRepository = save, loadAll

let mutable displayed = false
let mutable messageDisplayed = ""
let fakeDisplay message =
  displayed <- true
  messageDisplayed <- message

let mutable made = false
let fakeBeverageMaker order =
  made <- true
  ok {Beverage = Coffee; ExtraHot = true; MoneyInserted = 0.2; Stick = true |> Some; Sugar = 1; ListPrice = Some(0.7)}


let reset () =
  made <- false
  displayed <- false
  saved <- false


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


