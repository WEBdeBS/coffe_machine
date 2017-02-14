
module CoffeMachine.Tests

open Xunit
open FsUnit.Xunit
open CoffeeMachine.Core

let extract beverage =
  match beverage with
  | Some b -> b
  | None -> failwithf "Invalid beverage"

[<Fact>]
let ``Green Test``() =
    someThing
    |> should equal "Pippo"

[<Fact>]
let ``It should make tea`` () =

  let order = "T:1:0"
  let beverage = order |> make |> extract  

  beverage.Beverage 
  |> should equal Tea
  beverage.Sugar 
  |> should equal 1
  beverage.Stick
  |> should be True
  


  
[<Fact>]
let ``It should make Chocolate with no sugar`` () =
    let order = "H::"
    let beverage = order |> make |> extract

    beverage.Beverage |> should equal Chocolate
    beverage.Sugar |> should equal 0
    beverage.Stick |> should be False

[<Fact>]
let ``It should make Coffee with two sugar and a stick``() =
  let order = "C:2:0"
  let drink = make order |> extract
  drink.Beverage |> should equal Coffee
  drink.Sugar |> should equal 2
  drink.Stick |> should be True

[<Fact>]
let ``It should display messages on the interface`` () =
  //Mocking function??
  let mutable testMessage = "Pippo"
  let display message =
    testMessage <- message
  let order = "M:message-content"
  let beverage = makeDisp display order
  testMessage |> should equal "message-content"
    

