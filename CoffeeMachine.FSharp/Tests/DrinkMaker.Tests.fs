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

let extractError =
  function
  | Ok(d,_) -> failwith "I wanted an error, got a drink"
  | Bad(s) -> s.[0]

let mutable quantityChecked = false
let quantityChecker (drink: BeverageType) : string option=
  quantityChecked <- true
  None


let reset () =
  quantityChecked <- false

let beverage = {Beverage = Orange;
                ExtraHot = true;
                Sugar = 2;
                Stick = true;
                MoneyInserted = 0.9;
                ListPrice = 0.5}

let happyPath order =
  ok beverage


[<Fact>]
let ``It can make beverage if I'm on the happy path`` () =

  reset ()

  let order = "pippo"
  let drink = makeBeverage' happyPath order |> extract

  drink.Beverage |> should equal Orange
  drink.Sugar |> should equal 2
  drink.Stick |> should be True
  drink.ExtraHot |> should be True
  drink.MoneyInserted |> should equal 0.9
  drink.ListPrice |> should equal 0.5

[<Fact>]
let ``Cannot make a beverage if something goes wrong`` () =
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
let ``Cannot make an hot Orange Juice`` () =
  reset ()

  beverage
  |> ``check that beverage makes sense``
  |> extractError
  |> should equal "Cannot make an hot Orange Juice"


[<Fact>]
let ``Will put stick if beverage has sugar`` =
  let beverageWithNoStick = {beverage with Stick=false}
  let beverageWithStick = {beverage with Stick = true}

  let res = putStick beverageWithNoStick

  res.Stick |> should be True

  res |> should equal beverageWithStick


[<Fact>]
let ``Can check enough money`` () =
  reset ()
  let priceOf beverage =
    beverage |> should equal Orange
    0.7

  let bev = {beverage with MoneyInserted = 0.8; ListPrice = 0.0}
  let res = checkMoney priceOf bev |> extract

  res.ListPrice |> should equal 0.7
  res.MoneyInserted |> should equal 0.8

[<Fact>]
let ``Can check not enough money`` () =
  reset ()
  let priceOf beverage =
    beverage |> should equal Orange
    0.7

  let bev = {beverage with MoneyInserted = 0.4; ListPrice = 0.0}
  checkMoney priceOf bev
  |> extractError
  |> should equal "0.3 Euros missing"
