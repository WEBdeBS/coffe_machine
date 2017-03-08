module DrinkMaker.Data

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
}

type Drink =
  | Message of string
  | Drink of Beverage option
