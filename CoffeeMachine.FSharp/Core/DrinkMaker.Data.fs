module DrinkMaker.Data

let orderPattern = "^(\w{1})(h?)\:(\d*)\:(\d+\.\d+)$"
let messagePattern = "^M:(.*)$"
let reportPattern = "^report$"

type BeverageType =
  | InvalidOrder
  | Tea
  | Coffee
  | Chocolate
  | Orange

type OrderMoney =
  | MoneyInserted of float
  | MoneyLeft of float
  | Price of float

type Beverage = {
    Beverage: BeverageType
    ExtraHot: bool
    Sugar: int
    Stick: bool
    MoneyInserted: float
    ListPrice: float
}

type Drink =
  | Message of string
  | Drink of Beverage
