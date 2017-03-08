module QuantitiChecker.Tests

open Xunit
open FsUnit.Xunit
open DrinkMaker.Data
open DrinkMaker.QuantityChecker.Core

let mutable quantityChecked = false
let isEmpty beverage =
  quantityChecked <- true
  true

let mutable notified = false
let notifyMissingDrink beverage =
  notified <- true
  ()

let reset ()=
  notified <- false
  quantityChecked <- false

[<Fact>]
let ``Can check for missing beverage`` () =
  reset ()

  let beverageQuantityChecker = beverageQuantityChecker'' isEmpty notifyMissingDrink

  let res =
    match beverageQuantityChecker Coffee with
    | Some m -> m
    | None -> failwith "Error"

  res |> should equal "Coffee is empty. Vendor has been notified"

  notified |> should be True
  quantityChecked |> should be True

[<Fact>]
let ``Can check for existing Beverage`` () =
  reset ()
  let isEmpty beverage =
    quantityChecked <- true
    false

  let beverageQuantityChecker = beverageQuantityChecker'' isEmpty notifyMissingDrink

  let res =
    match beverageQuantityChecker Coffee with
    | Some m -> failwith "Error"
    | None -> ()


  notified |> should be False
  quantityChecked |> should be True
