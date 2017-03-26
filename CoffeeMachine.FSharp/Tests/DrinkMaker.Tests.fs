module DrinkMaker.Tests

open Xunit
open FsUnit.Xunit
open CoffeeMachine.Maker
open CoffeeMachine.PriceList
open DrinkMaker.Data
open DrinkMaker.Core
open DrinkMaker.OrderParser.Main
open Chessie.ErrorHandling

let extract =
  function
  | Ok(d,_) -> d
  | Bad(s) -> failwithf "I wanted a drink, got a message: %A" s

let mutable quantityChecked = false
let quantityChecker (drink: BeverageType) : string option=
  quantityChecked <- true
  None


let reset () =
  quantityChecked <- false

let happyPath order =
  ok {Beverage = Orange; ExtraHot = true; Sugar = 2; Stick = true; MoneyInserted = 0.9; ListPrice = 0.5}


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

  reset ()
  let getPrice beverageType =
    beverageType |> should equal Coffee
    0.7
  let order = "C:1:0.9"
  let drink = makeBeverage order |> extract

  drink.Beverage |> should equal Coffee
  drink.Sugar |> should equal 1
  drink.Stick |> should be True
  drink.ExtraHot |> should be False
  drink.MoneyInserted |> should equal 0.9
  drink.ListPrice |> should equal 0.6  

[<Fact>]
let ``Cannot make coffee if I don't have enough money`` () =
  reset()

  let order = "pippo"

  let imp order =
    order |> should equal "pippo"
    fail "0.3 Euros missing"

  let drink = makeBeverage' imp order

  let message =
    match drink with
    | Bad(m) -> m.[0]
    | _ -> failwith "Error"

  message |> should equal "0.3 Euros missing"  

[<Fact>]
let ``Can Make an Orange juice for .6 euros`` () =
  reset()

  let drink = "O:1:0.6" |> makeBeverage |> extract

  drink.Stick |> should be True
  drink.Beverage |> should equal Orange
  drink.Sugar |> should equal 1
  drink.ExtraHot |> should be False
  drink.MoneyInserted |> should equal 0.6

[<Fact>]
let ``It can make extra hot coffee``() =
  reset()
  let drink = "Ch:1:0.6"
              |> makeBeverage
              |> extract

  drink.Stick |> should be True
  drink.Sugar |> should equal 1
  drink.Beverage |> should equal Coffee
  drink.ExtraHot |> should be True
  drink.MoneyInserted |> should equal 0.6


