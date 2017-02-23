
module CoffeeMachine.Tests

open Xunit
open FsUnit.Xunit
open System.Linq
open DrinkMaker.Data
open CoffeeMachine.PriceList
open CoffeeMachine.Main
open DrinkMaker.Data
open CoffeeMachine.Core
open CoffeeMachine.DrinkRepository

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
  {Beverage = Coffee; ExtraHot = true; Price = 0.2; Stick = true; Sugar = 1}
  |> Some |> Drink

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
  drink.Price |> should equal 0.2

[<Fact>]
let ``It should not make an unknown drink``() =
  reset()
  let noDrinkMaker order =
    made <- true
    None |> Drink

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
    "paperino" |> Message

  let maker = make''' fakeRepository fakeDisplay fakeMaker
  "pluto"
  |> maker
  |> function
  | None -> ()
  | _ -> failwith "This is not goot"

  displayed |> should be True
  messageDisplayed |> should equal "paperino"
  saved |> should be False

[<Fact>]
let ``I should be able to print a receipt``() =
  reset()
  printReceipt'' fakeRepository fakeDisplay

  displayed |> should be True
