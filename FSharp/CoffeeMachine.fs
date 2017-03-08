module CoffeeMachine.Main
open CoffeeMachine.Core
open CoffeeMachine.Maker

let make orderStr =
  make'' makeBeverage display orderStr
