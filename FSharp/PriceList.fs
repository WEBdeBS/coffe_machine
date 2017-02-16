module CoffeeMachine.PriceList

type BeverageType = Tea | Coffee | Chocolate | InvalidOrder

let priceList = dict["T", 0.4; "C", 0.6; "H", 0.5]