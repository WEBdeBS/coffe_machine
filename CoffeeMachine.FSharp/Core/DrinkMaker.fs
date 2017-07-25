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


let mutable private isEmpty = true

let public setEmptyConfiguration empty = isEmpty <- empty

// let isEmpty = Convert.ToBoolean(ConfigurationManager.AppSettings.Item("Empty"))


let makeBeverage =
  let railway =
 
    parseOrder
    >> bind ``Check that drink exists``
    //>>= (dummy1 >> dummy2)
    >> lift putStick
    >> bind (checkMoneyAndSetListPrice priceOf)
    >> bind ``check that beverage makes sense``
    >> bind  (checkQuantity (fun b -> isEmpty) (ignore))
    >> lift saveIntoDb
  
  makeBeverage' railway
