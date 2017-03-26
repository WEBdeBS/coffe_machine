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

let makeBeverage orderStr =
  makeBeverage''' parseOrder priceList beverageQuantityChecker orderStr


let railWay =
  parseOrderFunctor
  >> bind (checkMoney priceList)
  >> bind (checkQuantity (fun b -> false) (ignore))
  >> bind ``check that beverage makes sense``


let railWayWithInfixOperator order =
  order
  |> parseOrderFunctor
  >>= (checkMoney priceList)
  >>=  (checkQuantity (fun b -> false) (ignore))
  >>= ``check that beverage makes sense``


let makeBeverageWithFunctors orderStr =
  orderStr
  |> railWayWithInfixOperator