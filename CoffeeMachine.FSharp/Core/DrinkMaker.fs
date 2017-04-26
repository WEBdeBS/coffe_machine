module CoffeeMachine.Maker

open DrinkMaker.Data
open DrinkMaker.Core
open DrinkMaker.OrderParser.Main
open QuantityChecker
open DrinkMaker.QuantityChecker.Core
open CoffeeMachine.PriceList
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
    >>= checkMoneyAndSetListPrice priceOf
    >>=  checkQuantity (fun b -> false) (ignore)
    >>= ``check that beverage makes sense``

  orderStr
  |> makeBeverage' railway
