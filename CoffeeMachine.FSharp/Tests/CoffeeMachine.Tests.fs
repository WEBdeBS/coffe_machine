
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
  ok {Beverage = Coffee; ExtraHot = true; MoneyInserted = 0.2; Stick = true; Sugar = 1; ListPrice = 0.7}
  

let reset () =
  made <- false
  displayed <- false
  saved <- false

[<Fact>]
let ``It should be able to make any Beverage`` () =
  reset()
  let fakeMake = make''' fakeRepository fakeDisplay fakeBeverageMaker
  let drink = "pippo" |> fakeMake |> extract

  made |> should be True
  saved |> should be True
  drink.Beverage |> should equal Coffee
  drink.Sugar |> should equal 1
  drink.ExtraHot |> should be True
  drink.MoneyInserted |> should equal 0.2

[<Fact>]
let ``It should not make an unknown drink``() =
  reset()
  let noDrinkMaker order =
    made <- true
    fail ""

  let make = make''' fakeRepository fakeDisplay noDrinkMaker

  "pippo"
  |> make
  |> function
  | None -> ()
  | _ -> failwith "I have a drink!!"

  made |> should be True
  saved |> should be False

[<Fact>]
let ``It should display messages on the interface`` () =
  reset ()
  let make = make''' fakeRepository fakeDisplay fakeBeverageMaker

  "M:message-content"
  |> make
  |> function
  | None -> ()
  | _ -> failwith "I have a drink!!"


  messageDisplayed |> should equal "message-content"
  displayed |> should be True
  saved |> should be False
  made |> should be False

[<Fact>]
let ``It should Not make coffee if not enough money`` () =
  reset ()

  let fakeMaker order =
    order |> should equal "pluto"
    fail "Not enough money"

  let drink = 
    make''' fakeRepository fakeDisplay fakeMaker "pluto"  
    |> function
    | None -> ()
    | _ -> failwith "This is not good"

  displayed |> should be True
  messageDisplayed |> should equal "Not enough money"
  saved |> should be False

[<Fact>]
let ``I should be able to print a receipt``() =
  reset()
  printReceipt'' fakeRepository fakeDisplay
  loaded |> should be True
  displayed |> should be True
