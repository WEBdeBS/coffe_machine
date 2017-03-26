module CoffeeMachine.Maker
open DrinkMaker.Data
open DrinkMaker.Core
open DrinkMaker.OrderParser.Main
open QuantityChecker
open DrinkMaker.QuantityChecker.Core
open CoffeeMachine.PriceList
open System.Text.RegularExpressions
open System
open Chessie.ErrorHandling

let railway order =
  order
  |> parseOrder
  >>= putStick
  >>= checkMoney priceList
  >>=  checkQuantity (fun b -> false) (ignore)
  >>= ``check that beverage makes sense``


let makeBeverage orderStr =
  orderStr
  |> makeBeverage' railway