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


//Not in chessie?? why?
let switch f x =
    f x |> ok

let makeBeverage orderStr =
  let railway order =
    order
    |> parseOrder
    >>= switch putStick
    >>= checkMoney priceOf
    >>=  checkQuantity (fun b -> false) (ignore)
    >>= ``check that beverage makes sense``

  orderStr
  |> makeBeverage' railway
