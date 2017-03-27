module OrderParser.Tests

open FsUnit.Xunit
open Xunit
open DrinkMaker.Core
open CoffeeMachine.PriceList
open DrinkMaker.Data



// [<Theory>]
// [<InlineData("C:1:0.9", Chocolate, 0.6)>]
// let ``I should make a drink if I have enough money`` (order: string) (bType: BeverageType) (sugar: int) (stick: bool) (price: float) =
//  let getPrice beverageType =
//    beverageType |> should equal bType
//    price

//  let drink = makeBeverage' getPrice order |> extract

//  drink.Beverage |> should equal bType
//  drink.Sugar |> should equal sugar
//  drink.Stick |> should equal stick


