module CoffeeMachine.Maker

open DrinkMaker.Data
open DrinkMaker.Core
open DrinkMaker.OrderParser.Main
open QuantityChecker
open DrinkMaker.QuantityChecker.Core
open CoffeeMachine.PriceList
open System
open Chessie.ErrorHandling
open DrinkRepository.Main

open System.Configuration


let isEmpty = Convert.ToBoolean(ConfigurationManager.AppSettings.Item("Empty"))


//Not in chessie?? why?
let switch f x =
    f x |> ok

let makeBeverage =
  let railway order =
    order
    |> parseOrder
    >>= ``Check that drink exists``
    //>>= (dummy1 >> dummy2)
    >>= switch putStick
    >>= checkMoneyAndSetListPrice priceOf
    >>= ``check that beverage makes sense``
    >>=  checkQuantity (fun b -> isEmpty) (ignore)
    >>= switch saveIntoDb

  makeBeverage' railway
